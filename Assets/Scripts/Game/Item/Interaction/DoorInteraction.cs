using System.Collections;
using Game.Player;
using SceneManagement;
using UnityEngine;

public class DoorInteraction : Interaction
{
	public override Player.State State
	{
		get 
		{
			if (openDoorCondition != null && !openDoorCondition.Satisfied())
			{
				return Player.State.Idle;	
			}
			return provideState;
		}
	}
	
	public override bool Interactable { get; protected set; }
	
	[SerializeField] private BaseCondition openDoorCondition;
	[SerializeField] private Player.State provideState = Player.State.Teleport;
	//[SerializeField] private AutomaticDoor door;
	
	[SerializeField] private string exitPointName; 
	[SerializeField] private SerializedSceneInfo nextSceneInfo;

	private void Awake()
	{
		Interactable = true;
	}

	public override void Interact(VariableSystem variableSystem)
	{
		if (openDoorCondition == null)
		{
			StartCoroutine(LoadNextLevelCor());
		}
		else
		{
			if (openDoorCondition.Satisfied())
			{
				StartCoroutine(LoadNextLevelCor());
			}
		}
	}

	private IEnumerator LoadNextLevelCor()
	{
		onInteract.Invoke();
		yield return new WaitForSecondsRealtime(0.3f);
		//Teleport animation here
		Debug.Log("Teleport animation here");
		//door.Open();
		yield return new WaitForSecondsRealtime(0.5f);
		LoadNextScene();
	}

	public override void LoadState(VariableSystem variableSystem)
	{
		
	}

	private void LoadNextScene()
	{
		var nextSceneContext = new RoomSceneContext();
		nextSceneContext.sceneInfo = nextSceneInfo;
		nextSceneContext.spawnPointName = exitPointName;
		SceneLoader.LoadScene(nextSceneContext);
	}
}
