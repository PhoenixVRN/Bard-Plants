using AudioSystem;
using UnityEngine;
using WebGame;
using WebGame.AudioSystem;
using Random = UnityEngine.Random;

namespace Core.Pooling
{
    public class PoolingAudio : PoolingSystem<AudioSource>
    {
        public static PoolingAudio Instance;

        private void Awake()
        {
            if (Instance) Destroy(gameObject);
            else Instance = this;
        }

        
        public void PlayTakeDamageAudio()
        {
            int index = Random.Range(0, 6);
            switch (index)
            {
                case 0:
                    PlayAudio(ESound.TakeDamage1);
                    break;
                case 1:
                    PlayAudio(ESound.TakeDamage2);
                    break;
                case 2:
                    PlayAudio(ESound.TakeDamage3);
                    break;
                case 3:
                    PlayAudio(ESound.TakeDamage4);
                    break;
                case 4:
                    PlayAudio(ESound.TakeDamage5);
                    break;
                case 5:
                    PlayAudio(ESound.TakeDamage6);
                    break;
            }
        }
        
        public void PlaySwishAudio()
        {
            int index = Random.Range(0, 5);
            switch (index)
            {
                case 0:
                    PlayAudio(ESound.Swish1);
                    break;
                case 1:
                    PlayAudio(ESound.Swish2);
                    break;
                case 2:
                    PlayAudio(ESound.Swish3);
                    break;
                case 3:
                    PlayAudio(ESound.Swish4);
                    break;
                case 4:
                    PlayAudio(ESound.Swish5);
                    break;
            }
        }
        
        public void PlayWeaponStrikeAudio()
        {
            int index = Random.Range(0, 3);
            switch (index)
            {
                case 0:
                    PlayAudio(ESound.WeaponStrike1);
                    break;
                case 1:
                    PlayAudio(ESound.WeaponStrike2);
                    break;
                case 2:
                    PlayAudio(ESound.WeaponStrike3);
                    break;
            }
        }
        
        public void PlayAudio(ESound eSound)
        {
            AudioSource audioSource = GetItem();
            if (audioSource == null) return;
            audioSource.gameObject.SetActive(true);
            audioSource.clip = AudioManager.Instance.GetSound(eSound).soundClip;
            audioSource.volume = AudioManager.Instance.GetSound(eSound).volume;
            audioSource.pitch = Random.Range(AudioManager.Instance.GetSound(eSound).pitch.x, AudioManager.Instance.GetSound(eSound).pitch.y);
            audioSource.Play();
            MonoCustom.Instance.StartCoroutineWithArg(CloseSound, audioSource.clip.length, audioSource);
        }

        private void CloseSound(AudioSource audioSource) => SetItem(audioSource);
    }
}