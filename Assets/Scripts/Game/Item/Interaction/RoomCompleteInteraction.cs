using Game.Player;
using UnityEngine;

public class RoomCompleteInteraction : Interaction
{
	public override Player.State State => Player.State.Idle;
	
	public override bool Interactable { get; protected set; }
	
	[SerializeField] private BaseCondition condition;
	[SerializeField] private UIPanel completePanel;

	private void Awake()
	{
		Interactable = true;
	}

	public override void Interact(VariableSystem variableSystem)
	{
		if (!Interactable)
			return;
		
		if (condition != null && !condition.Satisfied()) 
			return;
		
		GameUIManager.Instance.ShowUI(completePanel);
	}

	public override void LoadState(VariableSystem variableSystem)
	{
		
	}
}
