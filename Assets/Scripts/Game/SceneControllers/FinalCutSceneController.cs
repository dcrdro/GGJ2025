using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using SceneManagement;
using UnityEngine;
using UnityEngine.Events;

namespace SceneController
{
	public class FinalCutSceneController : BaseSceneController
	{
		[SerializeField] private GameObject skipText;
		[SerializeField] private SerializedSceneInfo nextSceneInfo;
		[SerializeField] private float waitSkipTimer = 3;
		[SerializeField] private UnityEvent onLeaveScene;
		
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

		private IEnumerator showSkipCutSceneCor()
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
			await UniTask.Yield();
			progress.Report(new LoadingProgress() { progress = 1f });
		}

		public override async UniTask Unload()
		{
			onLeaveScene.Invoke();
			await UniTask.Yield();
		}

		public override void OnLoadComplete()
		{
			_cutSceneCor = StartCoroutine(showSkipCutSceneCor());
			_loadComplete = true;
		}
	}
}