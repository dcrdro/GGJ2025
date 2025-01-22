using System.Collections.Generic;
using UnityEngine;
using Game.Core;

public class ItemStateSystem : MonoBehaviour
{
	[SerializeField] private List<Entity> items;

#if UNITY_EDITOR
	[ContextMenu("Scan Entities On Scene")]
	private void ScanItemsOnScene()
	{
		items.Clear();
		var sceneItems = FindObjectsOfType<Entity>(true);
		items.AddRange(sceneItems);
		UnityEditor.EditorUtility.SetDirty(gameObject);
	}
#endif

	public void Load()
	{
		var variableSystem = VariableSystem.Instance;
		foreach (Entity item in items)
		{
			if (item == null)
				continue;
			
			item.LoadState(variableSystem);
		}
	}
	
}
