using UnityEngine;

public class FMODSetMusicArea : MonoBehaviour
{
    [Header("Area")]
    [SerializeField] private MusicArea area;

    private void Start()
    {
        AudioManager.instance.SetMusicArea(area);
        Debug.Log($"[AudioChanger] Set music area {area}");
    }
}
