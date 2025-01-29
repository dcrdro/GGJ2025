using UnityEngine;

public class FMODSetAmbientArea : MonoBehaviour
{
    [SerializeField] private AmbientParam ambientParam;

    private void Start()
    {
        AudioManager.instance.SetAmbientParam(ambientParam);
    }
}
