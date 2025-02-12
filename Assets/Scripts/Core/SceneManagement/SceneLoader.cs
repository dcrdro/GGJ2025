using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace SceneManagement
{
	public class SceneLoader : MonoBehaviour, IProgress<LoadingProgress>
	{
		public enum EState
		{
			Idle,
			Loading
		}

		public static SceneLoader Instance
		{
			get
			{
				if (_instance != null) 
					return _instance;
				
				var prefab = Resources.Load("UI/SceneLoader", typeof(SceneLoader)) as SceneLoader;
				_instance = Instantiate(prefab);
				_instance.name = "SceneLoader";
				return _instance;
			}
		}

		public EState State { get; private set; }

		private static SceneLoader _instance;
		private BaseSceneController _loadingSceneController;
		private BaseSceneController _currentSceneController;

		[SerializeField] private TMPro.TextMeshProUGUI _progressText;
		[SerializeField] private CanvasGroup _canvasGroup;
		[SerializeField] private Image _fillBar;
		[SerializeField] private float fadeInTime = 0.3f;
		[SerializeField] private float fadeOutTime = 0.3f;
		[SerializeField] private GameObject _progressGroup;
		[SerializeField] private float loadSceneTimeOut = 10f;
		
		private ISceneInfo _currentScene;
		private readonly TimeoutController _timeoutController = new TimeoutController();

		private void Awake()
		{
			if (_instance != null && _instance != this)
			{
				Debug.LogError("[Scene Loader] Another Instance already created");
				Destroy(gameObject);
				return;
			}

			_instance = this;
			DontDestroyOnLoad(gameObject);
			SceneManager.sceneLoaded += OnSceneLoaded;
		}
		
		private void OnDestroy()
		{
			if (_instance == null || _instance != this) 
				return;
			
			SceneManager.sceneLoaded -= OnSceneLoaded;
			_instance = null;
		}

		public static void Cleanup()
		{
			_instance = null;
		}
		
		private void OnApplicationQuit()
		{
			//maybe not needed but scene can contain some static fields, witch should be cleared
			var task = UnloadCurrentScene();
		}
		
		private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			var rootObjects = scene.GetRootGameObjects();
			foreach (var rootObject in rootObjects)
			{
				var sceneController = rootObject.GetComponent<BaseSceneController>();
				if (sceneController == null)
					continue;
				_loadingSceneController = sceneController;
				_currentSceneController = _loadingSceneController;
			}
		}

		public static void LoadScene(SceneContext context)
		{
			if (Instance.State == EState.Loading)
			{
				Debug.LogWarning("[Scene loader] :: Another scene currently loading");
			}
			else
			{
				var task = Instance.ChangeScene(context);
			}
		}

		public static void ReloadScene()
		{
			_instance._currentSceneController.Reload();
			// var c = new SceneContext() { sceneInfo = new SerializedSceneInfo() { sceneName = SceneManager.GetActiveScene().name } };
			// LoadScene(c);
		}

		private async UniTask UnloadCurrentScene()
		{
			if (_currentSceneController != null)
			{
				await _currentSceneController.Unload();
				_currentSceneController.OnUnloadComplete();
			}

			_currentSceneController = null;
		}
		
		private async UniTask ChangeScene(SceneContext context)
		{
			State = EState.Loading;
			
			if (!context.sceneInfo.Instant)
			{
				_progressGroup.SetActive(true);
				var fadeIn = _canvasGroup.DOFade(1f, fadeInTime);
				await fadeIn.AsyncWaitForCompletion();
			}
			else
			{
				_progressGroup.SetActive(false);
				_canvasGroup.alpha = 0;
			}
			_progressGroup.SetActive(true);
			await UnloadCurrentScene();
			
			_loadingSceneController = null;
			await SceneManager.LoadSceneAsync(context.sceneInfo.SceneName);
			
			var token = _timeoutController.Timeout(TimeSpan.FromSeconds(loadSceneTimeOut));
			while (_loadingSceneController == null && !token.IsCancellationRequested)
			{
				await UniTask.Delay(100);
			}
			_timeoutController.Reset();
			await UniTask.DelayFrame(1);
			await Resources.UnloadUnusedAssets();
			await UniTask.DelayFrame(1);
			GC.Collect();
			if (_loadingSceneController != null)
			{
				await _loadingSceneController.Load(context, this);
				if (!context.sceneInfo.Instant)
				{
					_progressGroup.SetActive(false);
					var fadeOut = _canvasGroup.DOFade(0f, fadeOutTime);
					await fadeOut.AsyncWaitForCompletion();
				}
				else
				{
					_canvasGroup.alpha = 0;
					_progressGroup.SetActive(false);
				}
				State = EState.Idle;
				_loadingSceneController.OnLoadComplete();
				_loadingSceneController = null;
				_currentScene = context.sceneInfo;
			}
			else
			{
				Debug.LogError($"[Scene Loader] Can't fid scene controller in scene {context.sceneInfo.SceneName}");
			}
		}
		
		public void Report(LoadingProgress value)
		{
			_progressText.text = value.description;
			_fillBar.fillAmount = value.progress;
		}
	}
}