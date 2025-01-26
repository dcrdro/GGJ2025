using UnityEngine;

public class FMODSetGameProgress : MonoBehaviour
{
    [Header("Area")]
    [SerializeField] private GameProgress gameProgress;
    [SerializeField] private bool setProgressOnStart;

    private void Start()
    {
        if (setProgressOnStart)
        {
            AudioManager.instance.SetMusicProgress(gameProgress);

            Debug.Log($"[AudioChanger] Set music param {gameProgress}");
        }
    }

    public void SetInitialMode()
    {
        AudioManager.instance.SetMusicProgress(GameProgress.Initial);
    }
    
    public void SetPastComplete()
    {
        AudioManager.instance.SetMusicProgress(GameProgress.PastComplete);
    }

    public void SetPresentComplete()
    {
        AudioManager.instance.SetMusicProgress(GameProgress.PresentComplete);
    }

    public void SetFutureComplete()
    {
        AudioManager.instance.SetMusicProgress(GameProgress.FutureComplete);
    }
}
