using FMODUnity;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.UI
{
    public class PointerSound : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private EventReference enterEvent;
        [SerializeField] private EventReference exitEvent;
        [SerializeField] private EventReference clickEvent;

        public void OnPointerEnter(PointerEventData pointerEventData)
        {
            if (!enterEvent.IsNull)
            {
                AudioManager.PlayOneShot(enterEvent);       
            }
        }

        public void OnPointerExit(PointerEventData pointerEventData)
        {
            if (!exitEvent.IsNull)
            {
                AudioManager.PlayOneShot(exitEvent);        
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!clickEvent.IsNull)
            {
                AudioManager.PlayOneShot(clickEvent);        
            }
        }
    }
}