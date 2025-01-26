using System.Collections;
using Game.Player;
using Game.Core;
using Game.Sequence;
using UnityEngine;

public class TrueMirrorInteraction : Interaction
{
	public override Player.State State => Player.State.LookAtMirror;
	
	[SerializeField] private EntityInfo returnEntity;
	[SerializeField] private BaseSequence playSequence;
	
	public override bool Interactable => !alreadyUsed;
	private bool alreadyUsed;

	public override void Interact(VariableSystem variableSystem)
	{
		if (alreadyUsed)
			return;
		
		StartCoroutine(MirrorCor(variableSystem));
	}

	private IEnumerator MirrorCor(VariableSystem variableSystem)
	{
		variableSystem.SetVariable(entity.info.variableName+ Entity.UsedSuffix, "true", true);
		yield return playSequence.Sequence();
		variableSystem.Inventory.AddItem(returnEntity);
		alreadyUsed = true;
	}

	public override void LoadState(VariableSystem variableSystem)
	{
		var variable = variableSystem.GetVariable(entity.info.variableName + Entity.UsedSuffix);
		if (variable != null && variable.Value == "true")
		{
			alreadyUsed = true;
		}
	}
}