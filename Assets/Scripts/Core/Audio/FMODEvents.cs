using FMODUnity;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    [Header("SFX")] 
    //public EventReference footsteps;
    public EventReference grabItem;
    public EventReference teleportEnter;
    public EventReference teleportExit;

    [Header("Cutscene")]
    public EventReference intro;
    [Header("UI")] 
    
    public EventReference buttonHover;
    public EventReference buttonClick;
    public EventReference buttonMenu;
    [Header("Music")] 
    public EventReference mainTheme;


    // public static FMODEvents instance { get; private set; }
    //
    // private void Awake()
    // {
    //     if (instance != null)
    //     {
    //         Debug.LogError("Found more than one FMODEvents instance in scene");
    //     }
    //     instance = this;
    // }
}
