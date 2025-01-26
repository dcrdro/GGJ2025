using DG.Tweening;
using Game.Player;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

namespace Game.Mirror
{
	public class MirrorKill : Interaction
	{
		[SerializeField] private float delayBeforeKill;
        [SerializeField] private Transform target;

		public Animator handAnim;
		public MirrorTeleport teleport;

		public override Player.Player.State State => Player.Player.State.Idle;
		public override bool Interactable => true;

		private float currentTime;
		private bool isPlayerTriggered, isPlayerCatched;

		private void Update()
		{
			if (!isPlayerTriggered || isPlayerCatched || (!teleport?.IsTeleportedTo ?? false))
				return;

			currentTime += Time.deltaTime;

			if (currentTime >= delayBeforeKill)
			{
				isPlayerCatched = true;
                StartCoroutine(TrapCor());
            }
		}

		private IEnumerator TrapCor()
		{
			//yield return new WaitForSeconds(interactionTime);
			Player.Player.Instance.TakeDamage(1f);
			yield return new WaitForSeconds(interactionTime);
			Player.Player.Instance.Disappear();
            OnTriggerExit(null);
			yield return new WaitForSeconds(interactionTime);
            AudioManager.PlayOneShot(FMODEvents.Instance.respawn);

            Player.Player.Instance.transform.position = target.position;
			Player.Player.Instance.Appear();

        }

        private void OnTriggerEnter(Collider other)
        {
			handAnim.Play("In");
            isPlayerTriggered = true;
        }

        private void OnTriggerExit(Collider other)
		{
			isPlayerTriggered = false;
            isPlayerCatched = false;
            
			handAnim.Play("Out");
            currentTime = 0;

        }

		public override void Interact(VariableSystem variableSystem)
		{
		}

		public override void LoadState(VariableSystem variableSystem)
		{
		}
	}
}