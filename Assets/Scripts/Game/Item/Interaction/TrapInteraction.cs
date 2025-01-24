using Game.Player;
using UnityEngine;

public class TrapInteraction : Interaction
{
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

		var variable = variableSystem.GetVariable("Trap", true);
	}

	public override void LoadState(VariableSystem variableSystem)
	{
		
	}
}
