
public class CheckItemCondition : BaseCondition
{
	public EntityInfo checkItem;

	public override bool Satisfied()
	{
		var inventory = VariableSystem.Instance.Inventory;
		var items = inventory.GetItems();
		return items.Contains(checkItem);
	}
}
