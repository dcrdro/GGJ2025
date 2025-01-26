using FMODUnity;
using UnityEngine;

public class FMODEvents : Singleton<FMODEvents>
{
    [Header("SFX")] 
    //public EventReference footsteps;
    public EventReference grabItem;
    public EventReference key;
    public EventReference teleport;
    public EventReference death;
    public EventReference respawn;

    [Header("Cutscene")]
    public EventReference intro;
    [Header("UI")] 
    
    public EventReference buttonHover;
    public EventReference buttonClick;
    public EventReference buttonMenu;
    [Header("Music")] 
    public EventReference mainTheme;
    public EventReference ambience;


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
