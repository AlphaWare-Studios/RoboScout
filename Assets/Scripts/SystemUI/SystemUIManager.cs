using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Net.NetworkInformation;

public class SystemUIManager : MonoBehaviour
{
    public GameObject MainTop;
    public GameObject MainBottom;
    public GameObject ScoutTop;
    public GameObject ScoutBottom;
    public GameObject AnalyzeTop;
    public GameObject AnalyzeBottom;
    public GameObject SettingsTop;
    public GameObject SettingsBottom;
    public GameObject ManualTop;
    public GameObject ManualBottom;
    public GameObject CreditsTop;
    public GameObject CreditsBottom;
    public GameObject RSETop;
    public GameObject RSEBottom;
    public GameObject CSVTop;
    public GameObject CSVBottom;
    public GameObject QRTop;
    public GameObject QRBottom;
    public GameObject USBTop;
    public GameObject USBBottom;

    public GameObject Manager;
    public string Time;
    public string Date;
    bool is24Hour;
    public Text TimeText;
    public Text DateText;
    public bool hasBattery;
    public bool hasConnection;
    public float BatteryPercent;
    public Text FPSText;
    public Text FilesText;
    public int Frames;
    public bool isCalculated;
    public Image Battery;
    public Sprite Outlet;
    public Sprite BatteryNone;
    public Sprite Battery0;
    public Sprite Battery10;
    public Sprite Battery20;
    public Sprite Battery30;
    public Sprite Battery40;
    public Sprite Battery50;
    public Sprite Battery60;
    public Sprite Battery70;
    public Sprite Battery80;
    public Sprite Battery90;
    public Sprite Battery100;
    public Image Connection;
    public Sprite Ethernet;
    public Sprite ConnectionNone;
    public Sprite Tower;
    public Sprite Wifi;

    public void Wake()
    {
        ReloadUI();
        QualitySettings.vSyncCount = 0;
        isCalculated = false;
    }

    public void Frame()
    {
        Frames++;
    }

    public void ReloadUI()
    {
        SettingsClass Settings = Manager.GetComponent<FileManager>().Settings;
        is24Hour = Settings.is24Hour;
    }

    public void Second()
    {
        if (isCalculated)
        {
            FPSText.text = (Frames.ToString() + " FPS");
            Frames = 0;
        }
        else
        {
            FPSText.text = "? FPS";
            isCalculated = true;
            Frames = 0;
        }
        if (is24Hour)
        {
            Time = DateTime.Now.ToString("HH:mm");
        }
        else
        {
            Time = DateTime.Now.ToString("h:mm tt");
        }
        string matching = "*.rs";
        int Files = Directory.GetFiles(FilesDataClass.FilePathSaves, matching).Length;
        if (Files != 1)
        {
            FilesText.text = Files + " Files";
        }
        else
        {
            FilesText.text = Files + " File";
        } 
        Date = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString();
        TimeText.text = Time;
        DateText.text = Date;
        if (SystemInfo.batteryStatus != BatteryStatus.Unknown)
        {
            if (SystemInfo.batteryLevel == -1f)
            {
                Battery.sprite = Outlet;
                hasBattery = false;
            }
            else
            {
                hasBattery = true;
            }
        }
        else
        {
            Battery.sprite = BatteryNone;
            hasBattery = false;
        }
        if (hasBattery == true)
        {
            BatteryPercent = SystemInfo.batteryLevel * 100f;
            if (BatteryPercent == 100)
            {
                Battery.sprite = Battery100;
            }
            if (BatteryPercent > 90 && BatteryPercent < 100)
            {
                Battery.sprite = Battery90;
            }
            if (BatteryPercent > 80 && BatteryPercent < 91)
            {
                Battery.sprite = Battery80;
            }
            if (BatteryPercent > 70 && BatteryPercent < 81)
            {
                Battery.sprite = Battery70;
            }
            if (BatteryPercent > 60 && BatteryPercent < 71)
            {
                Battery.sprite = Battery60;
            }
            if (BatteryPercent > 50 && BatteryPercent < 61)
            {
                Battery.sprite = Battery50;
            }
            if (BatteryPercent > 40 && BatteryPercent < 51)
            {
                Battery.sprite = Battery40;
            }
            if (BatteryPercent > 30 && BatteryPercent < 41)
            {
                Battery.sprite = Battery30;
            }
            if (BatteryPercent > 20 && BatteryPercent < 31)
            {
                Battery.sprite = Battery20;
            }
            if (BatteryPercent > 10 && BatteryPercent < 21)
            {
                Battery.sprite = Battery10;
            }
            if (BatteryPercent > 0 && BatteryPercent < 11)
            {
                Battery.sprite = Battery0;
            }
            if (BatteryPercent == 0)
            {
                Battery.sprite = Battery0;
            }
        }
        if (hasBattery == true)
        {
            if (SystemInfo.batteryStatus == BatteryStatus.Discharging)
            {
                Application.targetFrameRate = 30;
            }
            else
            {
                Application.targetFrameRate = -1;
            }
        }
        else
        {
            Application.targetFrameRate = -1;
        }
        bool isEthernet = false;
        foreach (NetworkInterface netInterface in NetworkInterface.GetAllNetworkInterfaces())
        {
            if (netInterface.OperationalStatus == OperationalStatus.Up)
            {
                if (netInterface.NetworkInterfaceType.ToString() == "Ethernet")
                {
                    isEthernet = true;
                }
            }
        }
        switch (Application.internetReachability)
        {
            case NetworkReachability.NotReachable:
                Connection.sprite = ConnectionNone;
                hasConnection = false;
                break;
            case NetworkReachability.ReachableViaLocalAreaNetwork:
                if (isEthernet)
                {
                    Connection.sprite = Ethernet;
                }
                else
                {
                    Connection.sprite = Wifi;
                }
                hasConnection = true;
                break;
            case NetworkReachability.ReachableViaCarrierDataNetwork:
                Connection.sprite = Tower;
                hasConnection = true;
                break;
        }
    }

    void CLS()
    {
        MainTop.SetActive(false);
        MainBottom.SetActive(false);
        ScoutTop.SetActive(false);
        ScoutBottom.SetActive(false);
        AnalyzeTop.SetActive(false);
        AnalyzeBottom.SetActive(false);
        SettingsTop.SetActive(false);
        SettingsBottom.SetActive(false);
        ManualTop.SetActive(false);
        ManualBottom.SetActive(false);
        CreditsTop.SetActive(false);
        CreditsBottom.SetActive(false);
        RSETop.SetActive(false);
        RSEBottom.SetActive(false);
        CSVTop.SetActive(false);
        CSVBottom.SetActive(false);
        QRTop.SetActive(false);
        QRBottom.SetActive(false);
        USBTop.SetActive(false);
        USBBottom.SetActive(false);
    }

    public void ContentSwap(string lvl)
    {
        CLS();
        switch (lvl)
        {
            case "Main":
                MainTop.SetActive(true);
                MainBottom.SetActive(true);
                break;

            case "Scout":
                ScoutTop.SetActive(true);
                ScoutBottom.SetActive(true);
                break;

            case "Analyze":
                AnalyzeTop.SetActive(true);
                AnalyzeBottom.SetActive(true);
                break;

            case "Settings":
                SettingsTop.SetActive(true);
                SettingsBottom.SetActive(true);
                break;

            case "Manual":
                ManualTop.SetActive(true);
                ManualBottom.SetActive(true);
                break;

            case "Credits":
                CreditsTop.SetActive(true);
                CreditsBottom.SetActive(true);
                break;

            case "RSE":
                RSETop.SetActive(true);
                RSEBottom.SetActive(true);
                break;

            case "CSV":
                CSVTop.SetActive(true);
                CSVBottom.SetActive(true);
                break;

            case "QR":
                QRTop.SetActive(true);
                QRBottom.SetActive(true);
                break;

            case "USB":
                USBTop.SetActive(true);
                USBBottom.SetActive(true);
                break;

            default:
                Debug.Log("Error");
                break;
        }
    }
}
