using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class VolumeSlider : MonoBehaviour
    {
        public enum VolumeType
        {
            MASTER,
            MUSIC,
            SFX
        }

        [Header("Type")] [SerializeField] VolumeType volumeType;

        private Slider volumeSlider;

        private void Awake()
        {
            volumeSlider = this.GetComponent<Slider>();
        }

        private void Update()
        {
            switch (volumeType)
            {
                case VolumeType.MASTER:
                    volumeSlider.value = AudioManager.instance.masterVolume;
                    break;
                case VolumeType.MUSIC:
                    volumeSlider.value = AudioManager.instance.musicVolume;
                    break;
                case VolumeType.SFX:
                    volumeSlider.value = AudioManager.instance.sfxVolume;
                    break;
            }
        }

        public void OnSliderValueChanged()
        {
            switch (volumeType)
            {
                case VolumeType.MASTER:
                    AudioManager.instance.masterVolume = volumeSlider.value;
                    break;
                    break;
                case VolumeType.MUSIC:
                    AudioManager.instance.musicVolume = volumeSlider.value;
                    break;
                case VolumeType.SFX:
                    AudioManager.instance.sfxVolume = volumeSlider.value;
                    break;
            }
        }
    }
}