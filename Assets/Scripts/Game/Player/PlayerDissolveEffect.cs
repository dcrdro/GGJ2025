using FMODUnity;
using UnityEngine;

namespace Game.Player
{
	public class PlayerDissolveEffect : MonoBehaviour
	{
		private static readonly int Dissolve = Shader.PropertyToID("_Dissolve");

		[SerializeField] private Material dissolveMaterial;
		[SerializeField] private EventReference appearEvent;
		[SerializeField] private EventReference disappearEvent;
		//[SerializeField] private float duration;

		private float targetValue;
		private float currentValue;

		private void Update()
		{
			if (currentValue <= targetValue && targetValue >= 1f)
			{
				currentValue += Time.deltaTime;
				dissolveMaterial.SetFloat(Dissolve, currentValue);
			}
			if (currentValue > targetValue && targetValue <= 0.1f)
			{
				currentValue -= Time.deltaTime;
				dissolveMaterial.SetFloat(Dissolve, currentValue);
			}
		}

		public void Set(float value)
		{
			currentValue = value;
			targetValue = value;
			dissolveMaterial.SetFloat(Dissolve, value);
		}

		[ContextMenu("Appear")]
		public void RunAppear()
		{
			targetValue = 0.1f;
			if (!appearEvent.IsNull)
			{
				AudioManager.instance.PlayOneShot(appearEvent, transform.position);
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
				AudioManager.instance.PlayOneShot(disappearEvent, transform.position);
			}
		}
	}
}