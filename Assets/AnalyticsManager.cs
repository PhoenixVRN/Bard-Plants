using System;
using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;

public class AnalyticsManager : MonoBehaviour
{
    private bool _isInitialized;

    async void Start()
    {
        await UnityServices.InitializeAsync();
        AnalyticsService.Instance.StartDataCollection();
        _isInitialized = true;
        Debug.Log($"isInitialized");
    }

    public void TestAnalytics()
    {
        if (!_isInitialized) return;
        CustomEvent myEvent = new CustomEvent("my_custom_event")
        {
            {"my_custom_parametr", "this is testing parametr"},
        };
        AnalyticsService.Instance.RecordEvent(myEvent);
        Debug.Log($"Recording event");
    }
}