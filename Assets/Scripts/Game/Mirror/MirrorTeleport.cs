using UnityEngine;

namespace Game.Mirror
{
  public class MirrorTeleport : Interaction
  {
    [SerializeField]
    private MirrorTeleport pairMirror;

    [SerializeField]
    private Transform teleportPoint;

    public override Player.Player.State State => Player.Player.State.TeleportIn;
    public override bool Interactable { get; protected set; }
    public bool IsTeleportedTo { get; set; }

    private void Awake() => 
      Interactable = true;

    private void OnTriggerEnter(Collider other)
    {
      if (IsTeleportedTo)
        return;

      pairMirror.IsTeleportedTo = true;
      other.transform.parent.position = pairMirror.teleportPoint.position;
    }

    private void OnTriggerExit(Collider other) =>
      IsTeleportedTo = false;

    public override void Interact(VariableSystem variableSystem) { }

    public override void LoadState(VariableSystem variableSystem) { }
  }
}