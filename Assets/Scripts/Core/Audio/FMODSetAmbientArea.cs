using UnityEngine;

public class FMODSetAmbientArea : MonoBehaviour
{
    [SerializeField] private AmbientParam ambientParam;
    [SerializeField] private bool setOnStart = true;

    private void Start()
    {
        if (setOnStart)
        {
            SetAmbient();
        }
    }

    public void SetAmbient()
    {
        AudioManager.instance.SetAmbientParam(ambientParam);
    }
}
