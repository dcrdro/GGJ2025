using System.Collections;
using Game.Player;
using UnityEngine;

public class TrapInteraction : Interaction
{
	[SerializeField] private Transform target;
	[SerializeField] private Player player;
	
	public override Player.State State => Player.State.TeleportIn;
	
	public override bool Interactable { get; protected set; }
	
	private void Awake()
	{
		Interactable = true;
	}

	public override void Interact(VariableSystem variableSystem)
	{
		if (!Interactable)
			return;

		//var variable = variableSystem.GetVariable("Trap", true);
		StartCoroutine(TrapCor());
	}

	private IEnumerator TrapCor()
	{
		yield return new WaitForSeconds(interactionTime);
		player.Disappear();
		yield return new WaitForSeconds(1f);
		player.transform.position = target.position;
		player.Appear();
	}

	public override void LoadState(VariableSystem variableSystem)
	{
		
	}
}
