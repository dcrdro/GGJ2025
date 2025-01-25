using UnityEngine;

namespace Game.Mirror
{
	public class MirrorKill : Interaction
	{
		[SerializeField] private float delayBeforeKill;

		public override Player.Player.State State => Player.Player.State.Idle;
		public override bool Interactable => true;

		private float currentTime;
		private bool isPlayerTriggered;

		private void Update()
		{
			if (!isPlayerTriggered)
				return;

			currentTime += Time.deltaTime;

			if (currentTime >= delayBeforeKill)
			{
				currentTime = 0;
				Debug.Log("Kill by mirror here");
			}
		}

		private void OnTriggerEnter(Collider other) =>
			isPlayerTriggered = true;

		private void OnTriggerExit(Collider other)
		{
			isPlayerTriggered = false;
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