using UnityEngine;

public class FMODSetMusicArea : MonoBehaviour
{
    [Header("Area")]
    [SerializeField] private MusicParam musicParam;

    private void Start()
    {
        AudioManager.instance.SetMusicParam(musicParam);
        Debug.Log($"[AudioChanger] Set music param {musicParam}");
    }
}
