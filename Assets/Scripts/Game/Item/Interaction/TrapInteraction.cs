using System.Collections;
using Game.Player;
using UnityEngine;

public class TrapInteraction : Interaction
{
	[SerializeField] private Transform target;
	[SerializeField] private Player player;
	
	public override Player.State State => Player.State.TeleportIn;

	public override bool Interactable => true;
	
	public override void Interact(VariableSystem variableSystem)
	{
		StartCoroutine(TrapCor());
	}

	private IEnumerator TrapCor()
	{
		//yield return new WaitForSeconds(interactionTime);
		player.TakeDamage();
		yield return new WaitForSeconds(interactionTime / 2);
		player.Disappear();
		yield return new WaitForSeconds(interactionTime);
		player.transform.position = target.position;
		player.Appear();
	}

	public override void LoadState(VariableSystem variableSystem)
	{
		
	}
}
