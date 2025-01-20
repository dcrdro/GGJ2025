using System.Collections.Generic;
using UnityEngine;

public enum UIPanel
{
    Game,
    PauseMenu,
    WinScreen,
    LoseScreen,
}

public abstract class UIManager<T> : Singleton<T> where T : MonoBehaviour
{
    [System.Serializable]
    public class UIElementConfig
    {
        public UIPanel uiElement;
        public GameObject uiObject;
    }

    [SerializeField]
    private List<UIElementConfig> uiConfigs;

    private Dictionary<UIPanel, GameObject> uiElements;

    protected override void OnAwake()
    {
        base.OnAwake();
        InitializeUI();
    }

    private void InitializeUI()
    {
        uiElements = new Dictionary<UIPanel, GameObject>();

        foreach (var config in uiConfigs)
        {
            if (config.uiObject != null)
            {
                uiElements.Add(config.uiElement, config.uiObject);
                //config.uiObject.SetActive(false);
            }
            else
            {
                Debug.LogWarning($"UIObject for {config.uiElement} is not assigned.");
            }
        }
    }

    public void ShowUI(UIPanel uiElement)
    {
        if (uiElements.ContainsKey(uiElement))
        {
            uiElements[uiElement].SetActive(true);
        }
        else
        {
            Debug.LogWarning($"No UI object configured for {uiElement}");
        }
    }
    
    public void HideUI(UIPanel uiElement)
    {
        if (uiElements.ContainsKey(uiElement))
        {
            uiElements[uiElement].SetActive(false);
        }
        else
        {
            Debug.LogWarning($"No UI object configured for {uiElement}");
        }
    }

    public void ToggleUI(UIPanel uiElement)
    {
        if (uiElements.ContainsKey(uiElement))
        {
            var uiObject = uiElements[uiElement];
            uiObject.SetActive(!uiObject.activeSelf);
        }
        else
        {
            Debug.LogWarning($"No UI object configured for {uiElement}");
        }
    }

    public void HideAllUI()
    {
        foreach (var uiObject in uiElements.Values)
        {
            uiObject.SetActive(false);
        }
    }
}