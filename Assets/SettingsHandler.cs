using UnityEngine;

public class SettingsHandler : MonoBehaviour
{
    [SerializeField] private Settings _settings;

    private void OnEnable()
    {
        _settings.Init();
    }
}