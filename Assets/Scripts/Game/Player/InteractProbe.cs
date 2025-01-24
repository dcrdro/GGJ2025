using System;
using UnityEngine;

namespace Game.Player
{
    public class InteractProbe : MonoBehaviour
    {
        public event Action<Player.State, float> OnInteract;
        private void OnTriggerEnter(Collider other)
        {
            //Debug.Log($"Interact enter {name} with {other.name}");
            var interaction = other.GetComponent<Interaction>();
            if (interaction != null && interaction.Interactable)
            {
                interaction.Interact(VariableSystem.Instance);
                OnInteract?.Invoke(interaction.State, interaction.interactionTime);
            }
        }
		
        private void OnTriggerExit(Collider other)
        {
            //Debug.Log($"Interact exit {name} with {other.name}");
        }
    }
}