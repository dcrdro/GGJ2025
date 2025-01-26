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

		private void OnApplicationFocus(bool hasFocus)
		{
			if (!hasFocus) 
				return;

			Play();
		}

		public void Play()
		{
			if (!clipInstance.isValid())
			{
				clipInstance = RuntimeManager.CreateInstance(clip);
			}
			clipInstance.getPlaybackState(out var state);
			if (state == PLAYBACK_STATE.STOPPED)
			{
				clipInstance.start();
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

			clipInstance.release();
		}
		
		private void OnApplicationPause(bool hasFocus)
		{
			
		}

		private void OnDestroy()
		{
			StopPlaying();
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