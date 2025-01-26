using System;
using System.Collections;
using Game.Player;
using UnityEngine;

namespace Game.Sequence
{
	public class MirroredPlayerSequence : BaseSequence
	{
		[SerializeField] Animator playerAnimator;
		[SerializeField] private PlayerDissolveEffect dissolveEffect;
		[SerializeField] private GameObject sphere;

		private void Start()
		{
			dissolveEffect.Set(1f);
			playerAnimator.SetBool(Player.Player.IsMoving, false);
		}
		
		public override IEnumerator Sequence()
		{
			dissolveEffect.RunAppear();
			yield return new WaitForSeconds(1f);
			playerAnimator.SetTrigger(Player.Player.UseItem);
			yield return new WaitForSeconds(0.5f);
			sphere.gameObject.SetActive(true);
			yield return new WaitForSeconds(1.5f);
			sphere.gameObject.SetActive(false);
			dissolveEffect.RunDisappear();
			yield return new WaitForSeconds(1f);
		}

		public override float TotalTime => 3;
	}
}