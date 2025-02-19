using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;

public class AnalyticsManager : MonoBehaviour
{
    public static AnalyticsManager instance;
    
    private bool _isInitialized;
    private GameModel _gameModel;
    


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    async void Start()
    {
        await UnityServices.InitializeAsync();
        AnalyticsService.Instance.StartDataCollection();
        _isInitialized = true;
        Debug.Log($"isInitialized");
        _gameModel = Reference.GameModel;
        _gameModel.LevelGame.Subscribe(ChangeGameLevel);
    }

    public void AnalyticsEvent(string nameEvent)
    {
        if (!_isInitialized) return;
        AnalyticsService.Instance.RecordEvent(nameEvent);
        AnalyticsService.Instance.Flush();
        Debug.Log($"Recording event {nameEvent}");
    }

    private void ChangeGameLevel(int value)
    {
        string text = "count_UP_levels_" + value;
        AnalyticsEvent(text);
    }
}