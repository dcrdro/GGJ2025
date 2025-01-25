using System;
using Game.Player;
using Game.Core;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Entity))]
public abstract class Interaction : MonoBehaviour
{
	
#if UNITY_EDITOR
	private void OnValidate()
	{
		entity = gameObject.GetComponent<Entity>();
	}
#endif
	
	[HideInInspector] [SerializeField] public Entity entity;
	public float interactionTime = 1f;
	
	[Obsolete]
	[SerializeField] protected UnityEvent onInteract;
	
	public abstract Player.State State { get; }
	public abstract bool Interactable { get; }
	public abstract void Interact(VariableSystem variableSystem);
	public abstract void LoadState(VariableSystem variableSystem);
}
