using System;
using System.Collections.Generic;
using UnityEngine;

namespace AudioSystem
{
    public enum ESound
    {
        Click,
        CoinAdded,
        TakeDamage2,
        TakeDamage3,
        TakeDamage4,
        TakeDamage5,
        TakeDamage6,
        Gnom_sound_1,
        Gnom_sound_2,
        Owl_sound_1,
        Owl_sound_2,
        Swish1,
        Swish2,
        Swish3,
        Swish4,
        Swish5,
        WeaponStrike1,
        WeaponStrike2,
        WeaponStrike3,
    }
    
    public enum EMusic
    {
        BG
    }

    [Serializable]
    public class SettingSound
    {
        public ESound eSound;
        public AudioClip soundClip;
        [Range(0f, 1f), Min(0f)]
        public float volume;
        [Min(0f)]
        public Vector2 pitch;
    }

    [Serializable]
    public class SettingMusic
    {
        public EMusic eMusic;
        public AudioClip musicClip;
        [Range(0f, 1f), Min(0f)]
        public float volume;
        [Min(0)]
        public Vector2 pitch;
    }

    [CreateAssetMenu(fileName = "AudioDataBase", menuName = "Settings/AudioDataBase", order = 0)]
    public class AudioDataBase: ScriptableObject
    {
        [SerializeField] private List<SettingSound> sound;
        [SerializeField] private List<SettingMusic> music;

        public List<SettingSound> SettingSounds => sound;
        public List<SettingMusic> SettingMusic => music;
    }
}