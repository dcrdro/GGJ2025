using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Sequence
{
	public class UnityEventSequence : BaseSequence
	{
		public override float TotalTime => delayTime + (chainedSequence != null ? chainedSequence.TotalTime : 0f);
		[SerializeField] private UnityEvent action;
		[SerializeField] private float delayTime = 1f;
		[SerializeField] private BaseSequence chainedSequence;
		
		public override IEnumerator Sequence()
		{
			action.Invoke();
			yield return new WaitForSeconds(delayTime);
			if (chainedSequence != null)
				yield return chainedSequence.Sequence();
			
		}
	}
}