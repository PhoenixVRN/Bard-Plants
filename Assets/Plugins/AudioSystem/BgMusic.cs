using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AudioSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace WebGame.AudioSystem
{
    [RequireComponent(typeof(AudioSource))]
    public class BgMusic : MonoBehaviour
    {
        public static BgMusic Instance;

        [SerializeField] private EMusic music;
        private List<EMusic> _musicList = new();
        private float _musicTime;
        private int _currentCountOfTrack = 0;
        private AudioSource _audioSource;

        private void Awake()
        {
            if (Instance) Destroy(gameObject);
            else Instance = this;
            
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            CheckTrackPlaying();
        }

        public void CheckTrackPlaying() => StartCoroutine(CheckTrack());

        private IEnumerator CheckTrack()
        {
            float musicPoint = 0;
            for (int i = 0; i < 5; i++)
            {
                musicPoint += _audioSource.time;
                yield return MonoCustomHelpers.GetWait(0.1f);
            }

            if (musicPoint / 5 == _audioSource.time) Initialize();
        }

        private void Update()
        {
            UpdateSound();
        }

        private void UpdateSound()
        {
            _musicTime = _audioSource.time;
            if (!_audioSource.clip) return;
            if (_musicTime + 0.1f >= _audioSource.clip.length)
            {
                _currentCountOfTrack++;
                if (_currentCountOfTrack == Enum.GetValues(typeof(EMusic)).Length) _currentCountOfTrack = 0;
                _audioSource.clip = AudioManager.Instance.GetMusic(_musicList[_currentCountOfTrack]).musicClip;
                _audioSource.time = 0;
                _audioSource.Play();
            }
        }
        
        public void Initialize()
        {
            for (int i = 0; i < Enum.GetValues(typeof(EMusic)).Length; i++)
                _musicList.Add((EMusic)Enum.GetValues(typeof(EMusic)).GetValue(i));
            
            _audioSource.volume = AudioManager.Instance.GetMusic(_musicList[0]).volume;
            _musicList = _musicList.OrderBy(x => Random.value).ToList();
            _audioSource.clip = AudioManager.Instance.GetMusic(_musicList[_currentCountOfTrack]).musicClip;
            _musicTime = 0;
            _audioSource.Play();
        }

        private void OnApplicationFocus(bool focus)
        {
            if (focus)
            {
                _audioSource.time = _musicTime;
                _audioSource.Play();
            }
            else _musicTime = _audioSource.time;
        }
    }
}
