using UnityEngine;

public class OpenShopToAnalytics : MonoBehaviour
{
    public string NameEvent;
   
    private void OnEnable()
    {
        AnalyticsManager.instance.AnalyticsEvent(NameEvent);
    }
}