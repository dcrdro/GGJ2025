using System.Collections;
using UnityEngine;

namespace Game.Sequence
{
	public class ObjectVisibleSequence : BaseSequence
	{
		public override float TotalTime => delayTime;
		
		[SerializeField] private GameObject target;
		[SerializeField] private bool visible;
		[SerializeField] private float delayTime = 1f;
		
		public override IEnumerator Sequence()
		{
			target.SetActive(visible);
			yield return new WaitForSeconds(delayTime);
		}
	}
}