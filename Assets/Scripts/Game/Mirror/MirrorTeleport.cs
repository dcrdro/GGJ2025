using System.Collections;
using UnityEngine;

namespace Game.Mirror
{
	public class MirrorTeleport : Interaction
	{
		[SerializeField] private MirrorTeleport pairMirror;

		[SerializeField] private Transform teleportPoint;
		
		[SerializeField] private Player.Player.State interactState = Player.Player.State.TeleportIn;

		public override Player.Player.State State => interactState;
		public override bool Interactable => true;
		public bool IsTeleportedTo { get; set; }

		private void OnTriggerEnter(Collider other)
		{
			if (IsTeleportedTo)
				return;

			StartCoroutine(TeleportCor(other));
		}

		private void OnTriggerExit(Collider other) =>
			IsTeleportedTo = false;

		private IEnumerator TeleportCor(Collider other)
		{
			var player = other.GetComponentInParent<Player.Player>();
			if (player != null)
			{
				yield return new WaitForSeconds(1f);
				pairMirror.IsTeleportedTo = true;
				other.transform.parent.position = pairMirror.teleportPoint.position;
				player.Appear();
			}
			else
			{
				pairMirror.IsTeleportedTo = true;
				other.transform.parent.position = pairMirror.teleportPoint.position;
			}
		}

		public override void Interact(VariableSystem variableSystem)
		{
			
		}

		public override void LoadState(VariableSystem variableSystem)
		{
		}
	}
}