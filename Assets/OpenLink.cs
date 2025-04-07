using UnityEngine;

public class OpenLink : MonoBehaviour
{
    public string url = "https://doc-hosting.flycricket.io/bard-plants-magick-forest-privacy-policy/79275674-4a7a-4f9e-9965-ecb7ee06f5bd/privacy";
    public void OpenUrlInBrowser()
    {
        if (!string.IsNullOrEmpty(url))
        {
            Application.OpenURL(url);
        }
    }
}
