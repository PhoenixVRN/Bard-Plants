using UnityEngine;

public class ButtonHandlerAnalytics : MonoBehaviour
{
    public string NameEvent;

    public void SendEventAnalytics()
    {
        AnalyticsManager.instance.AnalyticsEvent(NameEvent);
    }
    
}
