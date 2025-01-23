using UnityEditor;
using UnityEngine;

namespace Game.Core
{
	[CustomEditor(typeof(EntityStateSystem))]
	public class EntityStateSystemEditor : Editor
	{
		private EntityStateSystem _stateSystem;
		private void OnEnable()
		{
			_stateSystem = (EntityStateSystem)target;
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			//GUILayout.Label($"Registered items: {_stateSystem.Entities.Count}");
			if (GUILayout.Button("Scan Entities"))
			{
				_stateSystem.ScanItemsOnScene();
			}
		}
	}
}