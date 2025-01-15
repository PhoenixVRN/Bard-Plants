using AudioSystem;
using UnityEngine;
using WebGame.AudioSystem;

public class AudioSettingsSource
{
    public AudioSource SetSettingsSoundESound(AudioSource source, ESound type, float volume)
    {
        source.outputAudioMixerGroup = AudioManager.Instance.GetMixerSound;
        source.clip = AudioManager.Instance.GetSound(type).soundClip;
        source.playOnAwake = false;
        source.volume = AudioManager.Instance.GetSound(type).volume;

        return source;
    }
    
    public AudioSource SetSettingsMusicEMusic(AudioSource source, EMusic type, float volume)
    {
        source.outputAudioMixerGroup = AudioManager.Instance.GetMixerMusic;
        source.clip = AudioManager.Instance.GetMusic(type).musicClip;
        source.volume = AudioManager.Instance.GetMusic(type).volume;

        return source;
    }
}
