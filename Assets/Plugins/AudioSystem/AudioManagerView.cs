using AudioSystem;
using UnityEngine;
using WebGame.AudioSystem;

[RequireComponent(typeof(AudioSource))]
public class AudioManagerView : MonoBehaviour
{
    public ESound soundClic;
    public static AudioManagerView Instance;
    public static readonly Vector2 GetClassicPitch = new(0.98f, 1.02f);
    public static readonly float GetClassicVolume = 0.12f;
    private AudioSource _audioSource;
    private bool OnStart;

    private void Awake()
    {
        if (Instance) Destroy(gameObject);
        else Instance = this;

        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = false;
        _audioSource.playOnAwake = false;
        // Invoke("InitStart", 0.5f);
        OnStart = true;
    }

    private void InitStart()
    {
        OnStart = true;
    }

    public void PlaySound(ESound sound)
    {
        Debug.Log($"PlaySound {sound.ToString()}");
        if (!OnStart) return;
        _audioSource.clip = AudioManager.Instance.GetSound(sound).soundClip;
        _audioSource.pitch = Random.Range(AudioManager.Instance.GetSound(sound).pitch.x,
            AudioManager.Instance.GetSound(sound).pitch.y);
        _audioSource.volume = AudioManager.Instance.GetSound(sound).volume;
        _audioSource.Play();
    }

    public void PlaySoundClick(AudioClip sound)
    {
        Debug.Log($"PlaySoundClick");
        var pitch = Random.Range(0.7f, 1.3f);
        AudioManager.Instance.mixer.audioMixer.SetFloat("PitchSound", pitch);
        _audioSource.PlayOneShot(sound);
    }

    // public void PlayClic()
    // {
    //     _audioSource.clip = AudioManager.Instance.GetSound(soundClic).soundClip;
    //     _audioSource.pitch = Random.Range(AudioManager.Instance.GetSound(soundClic).pitch.x, AudioManager.Instance.GetSound(soundClic).pitch.y);
    //     _audioSource.volume = AudioManager.Instance.GetSound(soundClic).volume;
    //     _audioSource.Play();
    // }
}