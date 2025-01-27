using System;
using System.Collections;
using Core.Audio;
using Cysharp.Threading.Tasks;
using SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

namespace SceneController
{
	public class CutSceneController : BaseSceneController
	{
		[SerializeField] private GameObject skipText;
		[SerializeField] private SerializedSceneInfo nextSceneInfo;
		[SerializeField] private float waitSkipTimer = 3;
		[SerializeField] private UnityEvent onLeaveScene;
		[SerializeField] private PlayableDirector timelineDirector;
		[SerializeField] private FMODCutScenePlayer cutScenePlayer;
		
		
		private bool _skip;
		private bool _loadComplete;
		private Coroutine _cutSceneCor;

		private void Awake()
		{
			if (skipText != null)
			{
				skipText.gameObject.SetActive(false);
			}
		}

		private void Start()
		{
			if (VariableSystem.Instance != null)
			{
				Destroy(VariableSystem.Instance.gameObject);
			}
		}

		private void Update()
		{
			if (_skip)
				return;

			if (!_loadComplete)
				return;

			if (Input.GetKeyDown(KeyCode.Space))
			{
				_skip = true;
				StopCoroutine(_cutSceneCor);
				LoadNextScene();
			}
		}

		private IEnumerator ShowSkipCutSceneCor()
		{
			yield return new WaitForSecondsRealtime(waitSkipTimer);
			if (skipText != null)
			{
				skipText.gameObject.SetActive(true);
			}
		}

		public void LoadNextScene()
		{
			var context = new HubSceneContext();
			context.sceneInfo = nextSceneInfo;
			context.spawnPointName = "Default";
			SceneLoader.LoadScene(context);
		}

		public override async UniTask Load(SceneContext sceneContext, IProgress<LoadingProgress> progress)
		{
			await cutScenePlayer.LoadBanks();
			progress.Report(new LoadingProgress() { progress = 0.3f });
			await UniTask.Yield();
			progress.Report(new LoadingProgress() { progress = 0.4f });
			await UniTask.Delay(100);
			progress.Report(new LoadingProgress() { progress = 1f });
		}

		public override async UniTask Unload()
		{
			onLeaveScene.Invoke();
			await UniTask.Yield();
		}

		public override void OnLoadComplete()
		{
			_cutSceneCor = StartCoroutine(ShowSkipCutSceneCor());
			_loadComplete = true;

			if (Application.isFocused)
			{
				timelineDirector.Play();
				cutScenePlayer.Play();
			}
		}
		
		private void OnApplicationFocus(bool hasFocus)
		{
			Debug.Log($"OnApplicationFocus {hasFocus}");
			if (hasFocus)
			{
				if (cutScenePlayer.IsPaused())
				{
					cutScenePlayer.ResumePlaying();
					timelineDirector.Resume();
					return;
				}
				
				if (!cutScenePlayer.IsPlaying())
				{
					timelineDirector.Play();
					cutScenePlayer.Play();
				}
			}
			else
			{
				if (!cutScenePlayer.IsPaused())
				{
					cutScenePlayer.PausePlaying();
					timelineDirector.Pause();
				}
			}
		}
		
		private void OnApplicationPause(bool paused)
		{
			Debug.Log($"OnApplicationPause {paused}");
		}
	}
}