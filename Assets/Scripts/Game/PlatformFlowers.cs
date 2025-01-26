using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformFlowers : MonoBehaviour
{
	[SerializeField] private List<MeshRenderer> renderers;
	
	private void Start()
	{
		foreach (var meshRenderer in renderers)
		{
			if (meshRenderer == null)
				continue;
			
			meshRenderer.enabled = Random.value > 0.4f;
		}
	}
}
