using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.UI;

namespace SceneController
{
	public class FinalCutSceneController : BaseSceneController
	{
		[SerializeField] private PlayableDirector timeline;
		
		[SerializeField] private Scrollbar scrollbar;
		[SerializeField] private GameObject creditsPanel;
		[SerializeField] private Button exit;
		[SerializeField] private float speedScrollSpeed;
		[SerializeField] private SerializedSceneInfo nextSceneInfo;


		private float _timer;
		private bool _cutSceneComplete;
		
		private void Awake()
		{
			exit.onClick.AddListener(OnExitButtonClick);
			creditsPanel.gameObject.SetActive(false);
		}
		
		private void Update()
		{
			if (!_cutSceneComplete)
				return;

			_timer += Mathf.Clamp01(Time.deltaTime * speedScrollSpeed);
			scrollbar.value = 1f-_timer;
		}
		
		public override async UniTask Load(SceneContext sceneContext, IProgress<LoadingProgress> progress)
		{
			await UniTask.Yield();
			progress.Report(new LoadingProgress() { progress = 1f });
		}
		
		public override void OnLoadComplete()
		{
			if (VariableSystem.Instance != null)
			{
				Destroy(VariableSystem.Instance.gameObject);
			}

			if (AudioManager.instance != null)
			{
				AudioManager.instance.StopMusic();
				AudioManager.instance.PlayFinalCutScene();
			}
			timeline.Play();
		}

		[ContextMenu("Complete Animation")]
		public void OnCompleteAnimation()
		{
			_cutSceneComplete = true;
			creditsPanel.gameObject.SetActive(true);	
		}

		private void OnExitButtonClick()
		{
			AudioManager.Shutdown();
			if (VariableSystem.Instance != null)
			{
				Destroy(VariableSystem.Instance.gameObject);
			}
			
			var context = new SceneContext();
			context.sceneInfo = nextSceneInfo;
			SceneLoader.LoadScene(context);
		}
	}
}