using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using Unity.VisualScripting;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    //public EventInstance PlayerFootsteps { get; private set; }

    public FMODEvents events;
    [SerializeField] private StudioBankLoader bankLoaderPrefab;
    private string _sceneName;
    private EventInstance ost;
    private EventInstance amb;
    //private EventInstance ambience;
    private EventInstance buttonHandler;
    private EventInstance buttonClick;
    private EventInstance buttonsClick;
    private EventInstance lockClick;
    private EventInstance lockFail;
    private EventInstance lockDone;
    private EventInstance lockOpen;
    private EventInstance fabricatorAnim;
    private EventInstance extinguish;
    private EventInstance rootGrowth;
    private EventInstance shortCircuit;
    private EventInstance shortCircuitDone;
    private StudioBankLoader _bank;


    [SerializeField] private MusicParam ambientLevel;

    [Header("Volume")]
    [Range(0, 1)]
    public float masterVolume =1;
    [Range(0, 1)]
    public float musicVolume =1;
    //[Range(0, 1)] public float ambienceVolume = 1;
    [Range(0, 1)]
    public float sfxVolume = 1;

    private Bus masterBus;
    //private Bus ambienceBus;
    private Bus musicBus;
    private Bus sfxBus;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            _bank = Instantiate(bankLoaderPrefab, transform);
            //InitializePlayerFootsteps();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        masterBus = RuntimeManager.GetBus("bus:/");
        //ambienceBus = RuntimeManager.GetBus("bus:/Ambience");
        musicBus = RuntimeManager.GetBus("bus:/Music");
        sfxBus = RuntimeManager.GetBus("bus:/Sfx");

        // if (instance != null)
        // {
        //     Debug.LogError("Found more than one Audio Manager in scene");
        // }
        // instance = this; 
        // DontDestroyOnLoad(gameObject);
    }

    
    private void OnDestroy()
    {
        if (instance == this)
        {
            if (_bank != null)
            {
                _bank.Unload();
                Destroy(_bank.gameObject);
                _bank = null;
            }
            //Debug.Log($"Clear instance OnDestroy {gameObject.name}");
            instance = null;
        }
    }

    private void OnApplicationQuit()
    {
        //Debug.Log($"Clear instance OnApplicationQuit {gameObject.name}");
        if (_bank != null)
        {
            _bank.Unload();
            Destroy(_bank.gameObject);
            _bank = null;
        }
        instance = null;
    }

    private void Start()
    {
        InitializeOst(events.mainTheme);
        InitializeAmb(events.ambience);
        SetAmbientParam(ambientLevel);
    }

    public void Update()
    {
        masterBus.setVolume(masterVolume);
        //ambienceBus.setVolume(ambienceVolume);
        musicBus.setVolume(musicVolume);
        sfxBus.setVolume(sfxVolume);
    }
    
    public static void PlayOneShot(EventReference sound)
    {
        RuntimeManager.PlayOneShot(sound);
    }

    public static void PlayOneShot(EventReference sound, Vector3 worldPosition)
    {
        RuntimeManager.PlayOneShot(sound, worldPosition);
    }

    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        return eventInstance;
    }

    // public void InitializeAmbience(EventReference ambienceEventReference)
    // {
    //     ambience = CreateInstance(ambienceEventReference);
    //     ambience.start();
    // }

    public void InitializeOst(EventReference ostReference)
    {
        ost = CreateInstance(ostReference);
        ost.start();
    }
    public void InitializeAmb(EventReference ostReference)
    {
        amb = CreateInstance(ostReference);
        amb.start();
    }
    
    public void SetMusicParam(MusicParam param)
    {
        //ambience.setParameterByName("AMBIENCE", (float)area);
        ost.setParameterByName("Dynamic Music", (int)param);
    }
    
    public void SetAmbientParam(MusicParam param)
    {
        //ambience.setParameterByName("AMBIENCE", (float)area);
        ost.setParameterByName("Ambient Select", (int)param);
    }

    public void InitializeMenuButtonHandler() 
    {
        buttonHandler = CreateInstance(events.buttonHover);
        buttonHandler.start();
    }

    public void InitializeMenuButtonClick()
    {
        buttonClick = CreateInstance(events.buttonClick);
        buttonClick.start();
    }

    public void InitializeMenuClick()
    {
        buttonsClick = CreateInstance(events.buttonMenu);
        buttonsClick.start();
    }

}
