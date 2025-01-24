using System.Collections;
using Game.Player;
using UnityEngine;

public class TrapInteraction : Interaction
{
	[SerializeField] private Transform target;
	public override Player.State State => Player.State.Damage;
	
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
		yield return new WaitForSeconds(0.5f);
		
	}

	public override void LoadState(VariableSystem variableSystem)
	{
		
	}
}
