using UnityEngine;

public class UIReloadManager : MonoBehaviour
{
    public static GameObject Manager;
    public static GameObject MainManager;
    public static GameObject ScoutManager;
    public GameObject AnalyzeManager;
    public GameObject SettingsManager;
    public GameObject ManualManager;
    public GameObject CreditsManager;
    public GameObject QRManager;

    public void Wake()
    {
        Manager = GameObject.Find("_Manager");
        MainManager = GameObject.Find("Main Manager");
        ScoutManager = GameObject.Find("Scout Manager");
        AnalyzeManager = GameObject.Find("Analyze Manager");
        SettingsManager = GameObject.Find("Settings Manager");
        QRManager = GameObject.Find("QR Manager");
    }

    public static void Reload()
    {
        Manager.GetComponent<ColorManager>().ReloadUI();
        Manager.GetComponent<SystemUIManager>().ReloadUI();
        Manager.GetComponent<Tooltip>().ReloadUI();
        MainManager.GetComponent<Main>().ReloadUI();
        ScoutManager.GetComponent<Scout>().ReloadUI();
    }
}
