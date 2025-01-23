using System;
using UnityEngine;

[Serializable]
public class InteractionInfo
{
	public enum EInteractionMode
	{
		Radius,
		FixedPoint,
		Instant,
	}
	
	public EInteractionMode interactionMode;
	public Transform interactionSpot;
	public float interactionRadius;
}