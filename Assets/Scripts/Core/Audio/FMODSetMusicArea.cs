using UnityEngine;

public class FMODSetMusicArea : MonoBehaviour
{
    [Header("Area")]
    [SerializeField] private MusicParam musicParam;
    [SerializeField] private bool setMusicArea;

    private void Start()
    {
        if (setMusicArea)
        {
            AudioManager.instance.SetMusicParam(musicParam);
            AudioManager.instance.SetAmbientParam(musicParam);

            Debug.Log($"[AudioChanger] Set music param {musicParam}");
        }
    }

    public void SetMode0()
    {
        AudioManager.instance.SetMusicParam(MusicParam.Hub);
        AudioManager.instance.SetAmbientParam(MusicParam.Hub);
    }
    
    public void SetMode1()
    {
        AudioManager.instance.SetMusicParam(MusicParam.PastRoom);
        AudioManager.instance.SetAmbientParam(MusicParam.PastRoom);
    }

    public void SetMode2()
    {
        AudioManager.instance.SetMusicParam(MusicParam.PresentRoom);
        AudioManager.instance.SetAmbientParam(MusicParam.PresentRoom);
    }

    public void SetMode3()
    {
        AudioManager.instance.SetMusicParam(MusicParam.FutureRoom);
        AudioManager.instance.SetAmbientParam(MusicParam.FutureRoom);
    }
}
