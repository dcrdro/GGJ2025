using System.Collections;
using Game.Player;
using Game.Core;
using UnityEngine;
using UnityEngine.Events;

public class UseItemInteraction : Interaction
{
	public override Player.State State => Player.State.UseItem;

	public override bool Interactable => condition == null || condition.Satisfied();
	[SerializeField] private BaseCondition condition;
	[SerializeField] private EntityInfo usedEntity;
	[SerializeField] private bool removeAfterUse;
	[SerializeField] private EntityInfo returnEntity;
	[SerializeField] private UnityEvent onUseItem;
	
	private bool alreadyUsed;

	private void Awake()
	{
		alreadyUsed = false;
	}

	public override void Interact(VariableSystem variableSystem)
	{
		if (alreadyUsed)
			return;
		
		if (!condition.Satisfied()) 
			return;
		
		variableSystem.SetVariable(usedEntity.variableName+"_ON_"+entity.info.variableName + Entity.UsedSuffix, "true", true);
		if (removeAfterUse)
		{
			variableSystem.Inventory.RemoveItem(usedEntity);
		}

		if (returnEntity != null)
		{
			variableSystem.Inventory.AddItem(returnEntity);
		}

		StartCoroutine(UseItemCor());
	}

	private IEnumerator UseItemCor()
	{
		alreadyUsed = true;
		yield return new WaitForSeconds(interactionTime);
		onInteract.Invoke();
		onUseItem.Invoke();
	}

	public override void LoadState(VariableSystem variableSystem)
	{
		var variable = variableSystem.GetVariable(usedEntity.variableName+"_ON_"+entity.info.variableName + Entity.UsedSuffix);
		if (variable != null && variable.Value == "true")
		{
			onUseItem.Invoke();
			alreadyUsed = true;
		}
	}
}
