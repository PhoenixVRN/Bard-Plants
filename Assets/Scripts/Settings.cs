using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;
using WebGame.AudioSystem;

public class Settings : MonoBehaviour
{
    public LocalzationSelector localzationSelector;
    public LocalizeStringEvent localizeStringEvent;
    public Image SpriteMusic;
    public Image SpriteMusicSprout;

    public Sprite musicOn;
    public Sprite musicOff;
    public Sprite musicSproutOn;
    public Sprite musicSproutOnOff;

    private int ID;
    private bool onMusic;

    private void OnEnable()
    {
        ID = PlayerPrefs.GetInt("LocaleKey", 0);
        onMusic = PlayerPrefs.GetInt("MusicOn", 1) > 0;
        localizeStringEvent.StringReference.TableEntryReference = onMusic ? "on_setting" : "off_setting";
        SpriteMusic.sprite = onMusic ? musicOn : musicOff;
        SpriteMusicSprout.sprite = onMusic ? musicSproutOn : musicSproutOnOff;
        // var volume = onMusic ? 0 : -80;
        // AudioManager.Instance.mixer.audioMixer.SetFloat("Master",volume);
        localizeStringEvent.RefreshString();
    }

    public void SwitchLanguage()
    {
        ID = (ID + 1) % 3; // Увеличиваем значение и зацикливаем от 0 до 2
        localzationSelector.ChangeLocale(ID);
    }

    public void SetMusic()
    {
        if (onMusic)
        {
            localizeStringEvent.StringReference.TableEntryReference = "off_setting";
            SpriteMusic.sprite = musicOff;
            SpriteMusicSprout.sprite = musicSproutOnOff;
            onMusic = false;
            PlayerPrefs.SetInt("MusicOn", 0);
            
            AudioManager.Instance.mixer.audioMixer.SetFloat("Master", -80);
            PlayerPrefs.SetFloat("MasterVolume1", 1);
        }
        else
        {
            localizeStringEvent.StringReference.TableEntryReference = "on_setting";
            SpriteMusic.sprite = musicOn;
            SpriteMusicSprout.sprite = musicSproutOn;
            onMusic = true;
            PlayerPrefs.SetInt("MusicOn", 1);
            AudioManager.Instance.mixer.audioMixer.SetFloat("Master", 0);
            PlayerPrefs.SetFloat("MasterVolume1", 0);
        }

        localizeStringEvent.RefreshString();
    }
}