using UnityEngine;

namespace Game.Core
{
	public class Entity : MonoBehaviour
	{
		public const string TakenSuffix = "_taken";
		public const string VisibleSuffix = "_visible";
		public const string UsedSuffix = "_used";

		public EntityInfo info;

#if UNITY_EDITOR
		private void Awake()
		{
			if (info == null)
			{
				Debug.LogError($"[Entity] {name} ItemInfo not set", gameObject);
			}
		}
#endif

		public void LoadState(VariableSystem variableSystem)
		{
			GameVar variable = variableSystem.GetVariable(info.variableName + VisibleSuffix);
			if (variable != null)
			{
				gameObject.SetActive(variable.Value == "true");
			}

			var interaction = GetComponent<Interaction>();
			if (interaction != null)
			{
				interaction.LoadState(variableSystem);
			}
		}
	}
}