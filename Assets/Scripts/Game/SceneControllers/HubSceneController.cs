using System;
using Cysharp.Threading.Tasks;
using Game.Player;
using SceneManagement;
using UnityEngine;

namespace SceneController
{
	public class HubSceneController : BaseSceneController
	{
		[SerializeField] private EntityStateSystem entityStateSystem;
		[SerializeField] private VariableSystem variableSystem;
		[SerializeField] private PlayerSpawner playerSpawner;
		[SerializeField] private Player playerController;

		private bool alreadyEntered;
		private bool doAppear;

#if UNITY_EDITOR
		private void OnValidate()
		{
			entityStateSystem = GetComponent<EntityStateSystem>();
		}
#endif

		private void Awake()
		{
			Application.targetFrameRate = 60;
		}

		private async void Start()
		{
			await UniTask.Delay(500);
			if (alreadyEntered)
				return;
		
			var progress = new StudProgress();
			await Load(null, progress);
		}

		//TODO save data for reload scene 
		// private HubSceneContext _savedSceneContext;
		// private VariableSystem.Data _savedVariableSystem;
		// private Inventory.Data _savedInventory;


		public override async UniTask Load(SceneContext sceneContext, IProgress<LoadingProgress> progress)
		{
         	if (sceneContext is RoomSceneContext roomSceneContext)
         	{
         		var location = playerSpawner.GetSpawnLocation(roomSceneContext.spawnPointName);
         		playerController.transform.position = location.position;
         		playerController.transform.forward = location.forward;
	            doAppear = true;
            }
            
            if (sceneContext is HubSceneContext hubSceneContext)
            {
	            //TODO first run
	            // var location = playerSpawner.GetSpawnLocation(hubSceneContext.spawnPointName);
	            // playerController.transform.position = location.position;
	            // playerController.transform.forward = location.forward;
	            // playerController.Appear();
            }
            progress.Report(new LoadingProgress() { progress = 0.5f });
            await UniTask.Yield();
            entityStateSystem.Load();
            progress.Report(new LoadingProgress() { progress = 1f });
		}

		public override void OnLoadComplete()
		{
			if (doAppear)
			{
				playerController.Appear();
			}
			base.OnLoadComplete();
		}
	}
}