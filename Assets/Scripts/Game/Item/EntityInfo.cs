using UnityEngine;

[CreateAssetMenu]
public class EntityInfo : ScriptableObject
{
	public string variableName;
	public string screenName;
	public string description;
	public Sprite icon;

	private void OnValidate()
	{
		variableName = name;
	}
}