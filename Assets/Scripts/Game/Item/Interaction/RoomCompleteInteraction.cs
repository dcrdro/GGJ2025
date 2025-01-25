using Game.Player;
using UnityEngine;

public class RoomCompleteInteraction : Interaction
{
	public override Player.State State => Player.State.Idle;

	public override bool Interactable => true;
	
	[SerializeField] private BaseCondition condition;
	[SerializeField] private UIPanel completePanel;

	public override void Interact(VariableSystem variableSystem)
	{
		if (condition != null && !condition.Satisfied()) 
			return;
		
		GameUIManager.Instance.ShowUI(completePanel);
	}

	public override void LoadState(VariableSystem variableSystem)
	{
		
	}
}
