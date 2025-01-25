using System;
using FMODUnity;
using UnityEngine;

namespace Game.Player
{
	public class PlayerDissolveEffect : MonoBehaviour
	{
		private static readonly int Dissolve = Shader.PropertyToID("_Dissolve");

		[SerializeField] private Material dissolveMaterial;
		[SerializeField] private SkinnedMeshRenderer dissolveMesh;
		[SerializeField] private EventReference appearEvent;
		[SerializeField] private EventReference disappearEvent;
		//[SerializeField] private float duration;

		private float targetValue;
		private float currentValue;

		private Material _dissolveInstance;

		private void Awake()
		{
			//_dissolveInstance = Instantiate(dissolveMaterial);
			//_dissolveInstance = dissolveMaterial;
			_dissolveInstance = dissolveMesh.materials[0];
			//dissolveMesh.materials[0] = _dissolveInstance;
		}

		private void OnDestroy()
		{
			Destroy(_dissolveInstance);
			_dissolveInstance = null;
		}

		private void Update()
		{
			if (currentValue <= targetValue && targetValue >= 1f)
			{
				currentValue += Time.deltaTime;
				_dissolveInstance.SetFloat(Dissolve, currentValue);
			}
			if (currentValue > targetValue && targetValue <= 0.1f)
			{
				currentValue -= Time.deltaTime;
				_dissolveInstance.SetFloat(Dissolve, currentValue);
			}
		}

		public void Set(float value)
		{
			currentValue = value;
			targetValue = value;
			_dissolveInstance.SetFloat(Dissolve, value);
		}

		[ContextMenu("Appear")]
		public void RunAppear()
		{
			targetValue = 0.1f;
			if (!appearEvent.IsNull)
			{
				AudioManager.PlayOneShot(appearEvent, transform.position);
			}
			//var tween = new 
			//dissolveMaterial.SetFloat();
		}

		[ContextMenu("Disappear")]
		public void RunDisappear()
		{
			targetValue = 1f;
			if (!disappearEvent.IsNull)
			{
				AudioManager.PlayOneShot(disappearEvent, transform.position);
			}
		}
	}
}