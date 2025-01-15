using System.Collections;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LocalzationSelector : MonoBehaviour
{
    private bool _active;
    public LocalizedString localizedString;

    private void Start()
    {
        int ID = PlayerPrefs.GetInt("LocaleKey", 0);
        ChangeLocale(ID);
        localizedString.StringChanged += OnStringChanged;

        // Получение значения сразу
        string value = localizedString.GetLocalizedString();
        // Debug.Log("Полученное значение: " + value);
    }

    public void ChangeLocale(int localeID)
    {
        if (_active) return;
        StartCoroutine(SetLocale(localeID));
    }

    IEnumerator SetLocale(int localeID)
    {
        _active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
        _active = false;
        PlayerPrefs.SetInt("LocaleKey", localeID);
    }
    private void OnStringChanged(string newValue)
    {
        // Debug.Log("Измененная строка: " + newValue);
    }
    
}