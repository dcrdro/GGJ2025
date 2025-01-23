using Game.Player;
using Game.Core;

public class TakeInteraction : Interaction
{
	public override Player.InteractState InteractState => Player.InteractState.TakeItem;
	public override bool Interactable { get; protected set; }

	private void Awake()
	{
		Interactable = true;
	}

	public override void Interact(VariableSystem variableSystem)
	{
		variableSystem.SetVariable(entity.info.itemName + Entity.VisibleSuffix, "false", true);
		variableSystem.Inventory.AddItem(entity.info);
		onInteract.Invoke();
		entity.gameObject.SetActive(false);
	}

	public override void LoadState(VariableSystem variableSystem)
	{
	}
}