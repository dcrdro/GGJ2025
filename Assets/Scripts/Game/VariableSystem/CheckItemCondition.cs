
public class CheckItemCondition : BaseCondition
{
	public EntityInfo checkItem;
	public BaseCondition and;

	public override bool Satisfied()
	{
		var inventory = VariableSystem.Instance.Inventory;
		var items = inventory.GetItems();
		if (and != null && !and.Satisfied())
		{
			return false;
		}
		return items.Contains(checkItem);
	}
}
