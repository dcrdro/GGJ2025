using System.Collections;
using Game.Player;
using Game.Core;
using UnityEngine;
using UnityEngine.Events;

public class RoomCompleteInteraction : Interaction
{
	public override Player.InteractState InteractState => Player.InteractState.Idle;
	
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
