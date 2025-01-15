using System;
using Core.Pooling;
using UnityEngine;
using UnityEngine.UI;
using WebGame.AudioSystem;

namespace AudioSystem
{
    public class ButtonSwitching : MonoBehaviour
    {
        [SerializeField] private ESoundType type;
        [SerializeField] private Image stateImage;
        [SerializeField] private Sprite soundOn;
        [SerializeField] private Sprite soundOff;
        
        private bool _state;
        private Button _button;

        private void OnEnable()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Switching);
        }

        private void Start()
        { 
            LoadState();
        }

        private void LoadState()
        {
            switch (type)
            {
                case ESoundType.Sound:
                    _state = AudioSetting.GetInstance.LoadSound();
                    break;

                case ESoundType.Music:
                    _state = AudioSetting.GetInstance.LoadMusic();
                    break;
            }

            LoadView();
        }

        private void SaveState()
        {
            switch (type)
            {
                case ESoundType.Sound:
                    AudioSetting.GetInstance.SaveSound(_state);
                    PlayerPrefs.SetInt("Sound", _state ? 1 : 0);
                    break;

                case ESoundType.Music:
                    AudioSetting.GetInstance.SaveMusic(_state);
                    PlayerPrefs.SetInt("Music", _state ? 1 : 0);
                    break;
            }
        }

        private void Switching()
        {
            _state = !_state;
            PoolingAudio.Instance.PlayAudio(ESound.Click);
            
            SwitchingImage();
            SwitchingSoundVolume();
            SaveState();
        }

        private void LoadView()
        {
            SwitchingImage();
            SwitchingSoundVolume();
            SaveState();
        }
    
        private void SwitchingSoundVolume()
        {
            switch (type)
            {
                case ESoundType.Sound:
                    AudioManager.Instance.SetVolumeSound(_state ? 0 : -80);
                    break;

                case ESoundType.Music:
                    AudioManager.Instance.SetVolumeMusic(_state ? 0 : -80);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        private void SwitchingImage() => stateImage.sprite = _state ? soundOn : soundOff;
        private void OnDisable()
        {
            _button.onClick.AddListener(Switching);
        }
    }
}

