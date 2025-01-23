using UnityEngine;

namespace Game.Player
{
    public class InteractProbe : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            //Debug.Log($"Interact enter {name} with {other.name}");
            var interaction = other.GetComponent<Interaction>();
            if (interaction != null && interaction.Interactable)
            {
                interaction.Interact(VariableSystem.Instance);
            }
        }
		
        private void OnTriggerExit(Collider other)
        {
            //Debug.Log($"Interact exit {name} with {other.name}");
        }
    }
}