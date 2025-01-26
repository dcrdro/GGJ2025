using FMODUnity;
using UnityEngine;

public class FMODEvents : Singleton<FMODEvents>
{
    [Header("SFX")] 
    public EventReference grabItem;
    public EventReference key;
    public EventReference teleport;
    public EventReference death;
    public EventReference respawn;

    // [Header("Cutscene")]
    // public EventReference intro;
    // public EventReference final;
    
    [Header("UI")] 
    
    public EventReference buttonHover;
    public EventReference buttonClick;
    public EventReference buttonMenu;
    [Header("Music")] 
    public EventReference mainTheme;
    public EventReference ambience;
}
