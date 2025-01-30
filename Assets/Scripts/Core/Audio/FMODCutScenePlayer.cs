using Cysharp.Threading.Tasks;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace Core.Audio
{
	public class FMODCutScenePlayer : MonoBehaviour
	{
		[SerializeField] private StudioBankLoader bankPrefab;
		[SerializeField] private EventReference clip;
		[SerializeField] private bool playOnStart;
		[SerializeField] private bool autoLoad = true;

		private EventInstance clipInstance;

		private StudioBankLoader bank;

		private void Awake()
		{
			if (autoLoad)
			{
				_ = LoadBanks();
			}
		}

		public async UniTask LoadBanks()
		{
			bank = Instantiate(bankPrefab);
			bank.Load();

			// Keep yielding the co-routine until all the bank loading is done
			// (for platforms with asynchronous bank loading)
			while (!RuntimeManager.HaveAllBanksLoaded)
			{
				await UniTask.Yield();
			}

			// Keep yielding the co-routine until all the sample data loading is done
			while (RuntimeManager.AnySampleDataLoading())
			{
				await UniTask.Yield();
			}

			Debug.Log("Banks Loaded");
		}

		private void Start()
		{
			if (playOnStart)
			{
				clipInstance = RuntimeManager.CreateInstance(clip);
				clipInstance.start();
			}
		}
		
		public bool IsPlaying()
		{
			if (!clipInstance.isValid())
				return false;
			
			clipInstance.getPlaybackState(out var state);
			return state == PLAYBACK_STATE.PLAYING;
		}

		public void Play()
		{
			if (!clipInstance.isValid())
			{
				clipInstance = RuntimeManager.CreateInstance(clip);
				clipInstance.start();
				return;
			}

			clipInstance.getPlaybackState(out var state);
			if (state == PLAYBACK_STATE.STOPPED)
			{
				clipInstance.start();
				return;
			}

			clipInstance.getPaused(out var isPaused);
			if (isPaused)
			{
				clipInstance.setPaused(false);
			}
		}

		public bool IsPaused()
		{
			if (!clipInstance.isValid())
				return false;
			
			clipInstance.getPaused(out var isPaused);
			return !isPaused;
		}
		

		public void PausePlaying()
		{
			if (!clipInstance.isValid())
				return;
			
			clipInstance.getPlaybackState(out var state);
			if (state == PLAYBACK_STATE.STOPPED)
				return;

			clipInstance.getPaused(out var isPaused);
			if (!isPaused)
			{
				clipInstance.setPaused(true);
			}
		}
		
		public void ResumePlaying()
		{
			if (!clipInstance.isValid())
				return;

			clipInstance.getPaused(out var isPaused);
			if (isPaused)
			{
				clipInstance.setPaused(false);
			}
		}

		public void StopPlaying()
		{
			if (!clipInstance.isValid())
				return;
			
			clipInstance.getPlaybackState(out var state);
			if (state == PLAYBACK_STATE.PLAYING)
			{
				clipInstance.stop(STOP_MODE.IMMEDIATE);
			}
		}
		
		

		private void OnDestroy()
		{
			StopPlaying();
			clipInstance.release();
			if (bank != null)
			{
				bank.Unload();
				bank = null;
			}
		}

		private void OnApplicationQuit()
		{
			StopPlaying();
			if (bank != null)
			{
				bank.Unload();
				bank = null;
			}
		}
	}
}