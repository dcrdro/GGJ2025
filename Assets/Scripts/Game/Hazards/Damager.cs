using Game.Player;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        var player = collision.GetComponentInParent<Player>();
        if (player != null)
        {
            player.TakeDamage();
        }
    }
}
