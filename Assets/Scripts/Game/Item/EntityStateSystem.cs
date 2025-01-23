using System.Collections.Generic;
using UnityEngine;
using Game.Core;

public class EntityStateSystem : MonoBehaviour
{
	public IReadOnlyList<Entity> Entities => entities;
	[SerializeField] private List<Entity> entities;

#if UNITY_EDITOR
	[ContextMenu("Scan Entities On Scene")]
	public void ScanItemsOnScene()
	{
		entities.Clear();
		var sceneItems = FindObjectsByType<Entity>(FindObjectsSortMode.None);
		entities.AddRange(sceneItems);
		UnityEditor.EditorUtility.SetDirty(gameObject);
	}
#endif

	public void Load()
	{
		var variableSystem = VariableSystem.Instance;
		foreach (Entity ent in entities)
		{
			if (ent == null)
				continue;
			
			ent.LoadState(variableSystem);
		}
	}
	
}
