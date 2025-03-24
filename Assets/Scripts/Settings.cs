using System;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Localization.Components;
using UnityEngine.UI;
using WebGame.AudioSystem;

public class Settings : MonoBehaviour
{
    public LocalzationSelector localzationSelector;
    public LocalizeStringEvent localizeStringEvent;
    public LocalizeStringEvent localizeStringEventUI;

    public Image SpriteMusic;
    public Image SpriteMusicSprout;

    public Image SpriteUISound;
    public Image SpriteUISoundSprout;

    public Sprite musicOn;
    public Sprite musicOff;
    public Sprite musicSproutOn;
    public Sprite musicSproutOnOff;

    private int ID;
    private bool onMusic;
    private bool onUISound;

    private void Start()
    {
        ID = PlayerPrefs.GetInt("LocaleKey", 0);
        onMusic = PlayerPrefs.GetInt("MusicOn", 1) > 0;
        Debug.Log($"onMusic {onMusic}");
        onUISound = PlayerPrefs.GetInt("onUISound", 1) > 0;
      Debug.Log($"onUISound {onUISound}");
        if (onMusic)
            AudioManager.Instance.mixer.audioMixer.SetFloat("Music", 0);
        else AudioManager.Instance.mixer.audioMixer.SetFloat("Music", -80);

      
        if (onUISound) AudioManager.Instance.mixer.audioMixer.SetFloat("Sound", 0);
        else AudioManager.Instance.mixer.audioMixer.SetFloat("Sound", -80);

        // localizeStringEvent.RefreshString();
    }

    public void Init()
    {
        localizeStringEvent.StringReference.TableEntryReference = onMusic ? "on_setting" : "off_setting";
        SpriteMusic.sprite = onMusic ? musicOn : musicOff;
        SpriteMusicSprout.sprite = onMusic ? musicSproutOn : musicSproutOnOff;

        localizeStringEventUI.StringReference.TableEntryReference = onUISound ? "on_setting" : "off_setting";
        SpriteUISound.sprite = onUISound ? musicOn : musicOff;
        SpriteUISoundSprout.sprite = onUISound ? musicSproutOn : musicSproutOnOff;
        localizeStringEvent.RefreshString();
    }

    public void SwitchLanguage()
    {
        ID = (ID + 1) % 3; // Увеличиваем значение и зацикливаем от 0 до 2
        PlayerPrefs.SetInt("LocaleKey", ID);
        localzationSelector.ChangeLocale(ID);
    }

    public void SetMusic()
    {
        if (onMusic)
        {
            AnalyticsManager.instance.AnalyticsEvent("count_off_music");
            localizeStringEvent.StringReference.TableEntryReference = "off_setting";
            SpriteMusic.sprite = musicOff;
            SpriteMusicSprout.sprite = musicSproutOnOff;
            onMusic = false;
            PlayerPrefs.SetInt("MusicOn", 0);

            AudioManager.Instance.mixer.audioMixer.SetFloat("Music", -80);
            // PlayerPrefs.SetFloat("MasterVolume1", 1);
        }
        else
        {
            localizeStringEvent.StringReference.TableEntryReference = "on_setting";
            SpriteMusic.sprite = musicOn;
            SpriteMusicSprout.sprite = musicSproutOn;
            onMusic = true;
            PlayerPrefs.SetInt("MusicOn", 1);
            AudioManager.Instance.mixer.audioMixer.SetFloat("Music", 0);
            // PlayerPrefs.SetFloat("MasterVolume1", 0);
        }

        localizeStringEvent.RefreshString();
    }

    public void SetUISound()
    {
        if (onUISound)
        {
            AnalyticsManager.instance
                .AnalyticsEvent("count_off_music"); // TODO добавить евент в юнити аналитикс и подвязать его
            localizeStringEventUI.StringReference.TableEntryReference = "off_setting";
            SpriteUISound.sprite = musicOff;
            SpriteUISoundSprout.sprite = musicSproutOnOff;
            onUISound = false;
            PlayerPrefs.SetInt("onUISound", 0);

            AudioManager.Instance.mixer.audioMixer.SetFloat("Sound", -80);
            // PlayerPrefs.SetFloat("MasterVolume1", 1);
        }
        else
        {
            localizeStringEventUI.StringReference.TableEntryReference = "on_setting";
            SpriteUISound.sprite = musicOn;
            SpriteUISoundSprout.sprite = musicSproutOn;
            onUISound = true;
            PlayerPrefs.SetInt("onUISound", 1);
            AudioManager.Instance.mixer.audioMixer.SetFloat("Sound", 0);
            // PlayerPrefs.SetFloat("MasterVolume1", 0);
        }

        localizeStringEventUI.RefreshString();
    }
}