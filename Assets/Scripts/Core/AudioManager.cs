using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public enum SoundEvent
{
    _test1,
}

public class AudioManager : Singleton<AudioManager>
{
    [System.Serializable]
    private class SoundEventConfig
    {
        public SoundEvent soundEvent; 
        public StudioEventEmitter eventEmitter; 
    }

    [SerializeField]
    private List<SoundEventConfig> soundConfigs; 

    private Dictionary<SoundEvent, StudioEventEmitter> soundEmitters;

    protected override void OnAwake()
    {
        base.OnAwake();
        InitializeAudio();
    }

    private void InitializeAudio()
    {
        soundEmitters = new Dictionary<SoundEvent, StudioEventEmitter>();

        foreach (var config in soundConfigs)
        {
            soundEmitters.Add(config.soundEvent, config.eventEmitter);
        }
    }

    public void PlaySound(SoundEvent soundEvent)
    {
        if (soundEmitters.ContainsKey(soundEvent))
        {
            var emitter = soundEmitters[soundEvent];
            emitter.Play();
        }
        else
        {
            Debug.LogWarning($"No FMOD event configured for {soundEvent}");
        }
    }

    public void StopSound(SoundEvent soundEvent)
    {
        if (soundEmitters.ContainsKey(soundEvent))
        {
            var emitter = soundEmitters[soundEvent];
            emitter.Stop();
        }
        else
        {
            Debug.LogWarning($"No FMOD event configured for {soundEvent}");
        }
    }

#if UNITY_EDITOR
    [ContextMenu("Play1")]
    public void Play1()
    {
        PlaySound(SoundEvent._test1);
    }
#endif
}
