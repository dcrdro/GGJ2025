using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

namespace Game.Sequence
{
	public class FocusOnTargetSequence : BaseSequence
	{
		public override float TotalTime => focusTime + delayTime + (ChainedSequence != null ? ChainedSequence.TotalTime : 0f);
		
		[SerializeField] private Transform target;
		[SerializeField] private BaseSequence ChainedSequence;
		[SerializeField] private float focusTime = 1f;
		[SerializeField] private float delayTime = 1f;
		[SerializeField] private CinemachineCamera cinemachineCamera;
		
		private Transform currentTarget;
		
		private bool _isRunning = false;
		
		public override IEnumerator Sequence()
		{
			if (_isRunning)
				yield break;

			_isRunning = true;

			currentTarget = cinemachineCamera.Target.TrackingTarget;
			cinemachineCamera.Target.TrackingTarget = target;
			yield return new WaitForSeconds(focusTime);
			if (ChainedSequence != null)
				yield return ChainedSequence.Sequence();
			cinemachineCamera.Target.TrackingTarget = currentTarget;
			yield return new WaitForSeconds(delayTime);
			_isRunning = false;
		}
	}
}