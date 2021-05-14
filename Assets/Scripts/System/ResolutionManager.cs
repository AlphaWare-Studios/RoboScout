using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    public static float NativeW = 1920;
    public static float NativeH = 1080;
    public static float ScreenOffsetW;
    public static float ScreenOffsetH;

    public void Wake()
    {
        UpdateResolution();
    }

    public static void UpdateResolution()
    {
        ScreenOffsetW = Screen.width / NativeW;
        ScreenOffsetH = Screen.height / NativeH;
    }
}
