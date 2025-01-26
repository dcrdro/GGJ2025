using System.Collections;
using Game.Player;
using UnityEngine;

public class TrapInteraction : Interaction
{
	[SerializeField] private Transform target;
	[SerializeField] private Player player;
	
	public override Player.State State => Player.State.TeleportIn;

	public override bool Interactable => true;
	
	public override void Interact(VariableSystem variableSystem)
	{
		StartCoroutine(TrapCor());
	}

	protected override void Validate()
	{
		if (player == null)
			player = FindFirstObjectByType<Player>();

		if (target == null)
		{
			var spawner = FindFirstObjectByType<PlayerSpawner>();
			if (spawner != null)
				target = spawner.GetSpawnLocation("Default");
		}
	}

	private IEnumerator TrapCor()
	{
		//yield return new WaitForSeconds(interactionTime);
		player.TakeDamage(interactionTime);
		yield return new WaitForSeconds(interactionTime);
		player.Disappear();
		yield return new WaitForSeconds(interactionTime);
        AudioManager.PlayOneShot(FMODEvents.Instance.respawn);

        player.transform.position = target.position;
		player.Appear();
	}

	public override void LoadState(VariableSystem variableSystem)
	{
		
	}
}
