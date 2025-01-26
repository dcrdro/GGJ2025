using DG.Tweening;
using Game.Player;
using System.Collections;
using UnityEngine;

namespace Game.Mirror
{
	public class MirrorTeleport : Interaction
	{
		[SerializeField] private MirrorTeleport pairMirror;

		[SerializeField] private Transform teleportPoint;
		
		[SerializeField] private Player.Player.State interactState = Player.Player.State.Idle;

		public GameObject hint;


		public override Player.Player.State State => interactState;
		public override bool Interactable => false;
		public bool IsTeleportedTo { get; set; }

		private bool entered;

	


		private void OnTriggerEnter(Collider other)
		{
            var player = other.GetComponentInParent<Player.Player>();

            if (!player)
				return;

			entered = true;

			hint.SetActive(true);
			hint.transform.DOScale(1, 0.2f).From(0);

		}

        private void OnTriggerExit(Collider other)
        {
            //IsTeleportedTo = false;
            entered = false;
            hint.SetActive(false);

        }

        private void Update()
        {
            if (entered && !IsTeleportedTo && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) )
			{
				StartCoroutine(TeleportCor());
			}
        }

        private IEnumerator TeleportCor()
		{
			IsTeleportedTo = true;

            Player.Player.Instance.Disappear();

            yield return new WaitForSeconds(1f);
            Player.Player.Instance.transform.position = pairMirror.teleportPoint.position;
			Player.Player.Instance.Appear();
			
			IsTeleportedTo = false ;
		}

		public override void Interact(VariableSystem variableSystem)
		{
			
		}

		public override void LoadState(VariableSystem variableSystem)
		{
		}
	}
}