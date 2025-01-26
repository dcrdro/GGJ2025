using System.Collections;
using Game.Player;
using Game.Core;
using UnityEngine;

public class TakeInteraction : Interaction
{
	public override Player.State State => Player.State.TakeItem;
	public override bool Interactable => true;

	public override void Interact(VariableSystem variableSystem)
	{
		StartCoroutine(InvisibleCor(variableSystem));
	}

	private IEnumerator InvisibleCor(VariableSystem variableSystem)
	{
		yield return new WaitForSeconds(0.5f);
		if (entity.info.name.ToLower().Contains("key"))
			AudioManager.PlayOneShot(FMODEvents.Instance.key);

        variableSystem.SetVariable(entity.info.variableName + Entity.VisibleSuffix, "false", true);
		variableSystem.Inventory.AddItem(entity.info);
		entity.gameObject.SetActive(false);
		onInteract.Invoke();
	}

	public override void LoadState(VariableSystem variableSystem)
	{
	}
}