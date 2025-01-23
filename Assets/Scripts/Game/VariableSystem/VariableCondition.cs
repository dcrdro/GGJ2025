using System.Collections.Generic;

public class VariableCondition : BaseCondition
{
	public List<VariableContext> checkVariables;

	public override bool Satisfied()
	{
		var variableSystem = VariableSystem.Instance;
		foreach (VariableContext variableDesc in checkVariables)
		{
			var gameVar = variableSystem.GetVariable(variableDesc.name);
			if (gameVar != null)
			{
				if (gameVar.Value != variableDesc.value)
				{
					return false;
				}
			}
			else
				return false;
		}

		return true;
	}
}
