using Game.Core;

public class CheckItemUsedCondition : BaseCondition
{
	public EntityInfo checkItem;
	public EntityInfo onCheckItem;
	public BaseCondition and;

	public override bool Satisfied()
	{
		var variableSystem = VariableSystem.Instance;
		
		var variable = variableSystem.GetVariable(checkItem.variableName+"_ON_"+onCheckItem.variableName + Entity.UsedSuffix);
		if (variable != null && variable.Value == "true")
		{
			return and == null || and.Satisfied();
		}
		return false;
	}
}
