using System.Linq;
using AudioSystem;
using UnityEngine;
using UnityEngine.Audio;

namespace WebGame.AudioSystem
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [SerializeField] private AudioDataBase dataBase;
        public AudioMixerGroup mixer;
        [SerializeField] private AudioMixerGroup mixerSound;
        [SerializeField] private AudioMixerGroup mixerMusic;
        public AudioMixerGroup GetMixer => mixer;
        public AudioMixerGroup GetMixerSound => mixerSound;
        public AudioMixerGroup GetMixerMusic => mixerMusic;

        private void Awake()
        {
            if (Instance) Destroy(gameObject);
            else Instance = this;
        }

        void Start()
        {
            var musicVolumeSlaider = PlayerPrefs.GetInt("MusicOn", 1) > 0 ? 0 : -80;
            // Debug.Log($"SetFloat {musicVolumeSlaider}/{ Mathf.Log10(musicVolumeSlaider) * 20}");
            // mixer.audioMixer.SetFloat("Master", Mathf.Log10(musicVolumeSlaider) * 20);
            mixer.audioMixer.SetFloat("Master", musicVolumeSlaider);
        }
        
        public void Initialize()
        {
            SetVolumeSound(AudioSetting.GetInstance.LoadSound() ? 0 : -80);
            SetVolumeMusic(AudioSetting.GetInstance.LoadMusic() ? 0 : -80);
        }
        
        public void SetVolumeSound(float value) => mixer.audioMixer.SetFloat("Sound", value);
        public void SetVolumeMusic(float value) => mixer.audioMixer.SetFloat("Music", value);

        public SettingMusic GetMusic(EMusic music) => (from t in dataBase.SettingMusic where t.eMusic == music select t).FirstOrDefault();
        public SettingSound GetSound(ESound sound) => (from t in dataBase.SettingSounds where t.eSound == sound select t).FirstOrDefault();
    }
}

