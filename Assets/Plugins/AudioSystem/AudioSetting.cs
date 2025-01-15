using UnityEngine;

namespace AudioSystem
{
    public class AudioSetting
    {
        public static AudioSetting GetInstance { get; } = new();

        public bool LoadSound() => PlayerPrefs.GetInt("Sound", 1) == 1;
        public bool LoadMusic() => PlayerPrefs.GetInt("Music", 1) == 1;
        public void SaveSound(bool state) => PlayerPrefs.SetInt("Sound", state ? 1 : 0);
        public void SaveMusic(bool state) => PlayerPrefs.SetInt("Music", state ? 1 : 0);
    }
}