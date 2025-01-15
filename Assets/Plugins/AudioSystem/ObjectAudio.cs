using System;
using AudioSystem;
using UnityEngine;
using WebGame.AudioSystem;

[RequireComponent(typeof(AudioSource))]
public class ObjectAudio : MonoBehaviour
{
    [SerializeField] private ESound soundType;
    [SerializeField] private float volumeValue;
    private Action RayAction;
    private AudioSource _source;
    private AudioSettingsSource _sourceSettings = new AudioSettingsSource();

    private void OnEnable()
    {
        RayAction = PlayObjectSound;
    }

    private void OnDisable()
    {
        RayAction -= PlayObjectSound;
    }

    private void Start() => _source = _sourceSettings.SetSettingsSoundESound(GetComponent<AudioSource>(), soundType, volumeValue);

    public void PlayObjectSound() => _source.Play();

}
