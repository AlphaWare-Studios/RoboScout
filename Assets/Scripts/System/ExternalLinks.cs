using UnityEngine;

public class ExternalLinks: MonoBehaviour
{
    public void ExternalLink(string Link)
    {
        Application.OpenURL(Link);
    }
}