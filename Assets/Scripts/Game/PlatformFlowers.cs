using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformFlowers : MonoBehaviour
{
	[SerializeField] private List<MeshRenderer> renderers;
	[SerializeField] private int visible;
	
	private void Start()
	{
		foreach (var meshRenderer in renderers)
		{
			
		}

		foreach (var meshRenderer in renderers)
		{
			meshRenderer.enabled = Random.value > 0.5f;
		}
	}
}
