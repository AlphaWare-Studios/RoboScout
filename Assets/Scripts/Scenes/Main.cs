using System;
using System.Net;
using System.Collections;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public GameObject Manager;
    public Text TeamNumber;
    public Text VersionText;
    public Text ScouterName;
    public Text YearText;
    public GameObject Changelog;
    public Text ChangelogTitle;
    public GameObject UpdateButton;
    public Text UpdateButtonText;
    public int Year;
    public bool isDataDown;
    public bool isDone;
    bool isBeta;
    readonly string Passcode = "9e596f22-abe7-4211-a4d5-720e20ae2d6b";

    public void Wake()
    {
        Changelog.SetActive(false);
        UpdateButton.SetActive(false);
        ReloadUI();
        isDone = false;
        VersionText.text = Application.version;
        YearText.text = "FIRST";
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            StartCoroutine(GetAWStatus());
            StartCoroutine(GetTBAStatus());
            StartCoroutine(Timeout());
        }
        else
        {
            isDataDown = true;
            Year = DateTime.Now.Year;
            YearText.text = "FIRST";
            isDone = true;
        }
    }

    public void ReloadUI()
    {
        SettingsClass Settings = Manager.GetComponent<FileManager>().Settings;
        TeamNumber.text = Settings.TeamNumber.ToString();
        ScouterName.text = Settings.ScouterName;
        isBeta = Settings.isBetaEnabled;
        if (Settings.Version != Application.version)
        {
            ChangelogTitle.text = "Welcome to RoboScout " + Application.version;
            Changelog.SetActive(true);
        }
        //StartCoroutine(SendTeamNum(Settings));
    }

    IEnumerator SendTeamNum(SettingsClass Settings)
    {
        using (AlphaWareWebHook AWWeb = new AlphaWareWebHook())
        {
            AWWeb.WebHook = "http://alphawarestudios.com/Data/RoboScoutTeamGetter.php";
            AWWeb.SendMessage(Passcode, Settings.TeamNumber);
            AWWeb.Dispose();
        }
        return null;
    }

    IEnumerator GetAWStatus()
    {
        Manager.GetComponent<AWManager>().GetAWStatus();
        while (Manager.GetComponent<AWManager>().AWStatusDone == false)
        {
            yield return null;
        }
        AWClass Data = Manager.GetComponent<AWManager>().AWStatusData;
        if (Data != null)
        {
            if (!isBeta)
            {
                if (Data.LatestVersion != Application.version)
                {
                    UpdateButtonText.text = "Version " + Data.LatestVersion + " out now!";
                    UpdateButton.SetActive(true);
                }
            }
            else
            {
                if (Data.LatestBetaVersion != Application.version)
                {
                    UpdateButtonText.text = "Beta Version " + Data.LatestBetaVersion + " out now!";
                    UpdateButton.SetActive(true);
                }
            }
            this.GetComponent<NewsManager>().SetNews(Data);
        }
    }

    IEnumerator GetTBAStatus()
    {
        Manager.GetComponent<TBAManager>().GetTBAStatus();
        while (Manager.GetComponent<TBAManager>().TBAStatusDone == false)
        {
            yield return null;
        }
        TBAClass Data = Manager.GetComponent<TBAManager>().TBAStatusData;
        if (Data != null)
        {
            isDataDown = Data.is_datafeed_down;
            Year = Data.current_season;
            YearText.text = "FIRST " + Year;
            isDone = true;
        }
    }

    IEnumerator Timeout()
    {
        yield return new WaitForSeconds(2.5f);
        if (!isDone)
        {
            isDataDown = true;
            Year = DateTime.Now.Year;
            StopCoroutine(GetAWStatus());
            StopCoroutine(GetTBAStatus());
            YearText.text = "FIRST";
            isDone = true;
        }
    }

    public void UpdateLink()
    {
        Manager.GetComponent<ExternalLinks>().ExternalLink("http://Alphawarestudios.com/RoboScout");
    }

    public void Confirm()
    {
        Manager.GetComponent<FileManager>().Save();
        Changelog.SetActive(false);
    }
}

public class AlphaWareWebHook : IDisposable
{
    private WebClient AWebClient;
    private static NameValueCollection AlphaWareValues = new NameValueCollection();
    public string WebHook { get; set; }

    public AlphaWareWebHook()
    {
        AWebClient = new WebClient();
    }

    public void SendMessage(string Passcode, int Team)
    {
        AlphaWareValues.Clear();
        AlphaWareValues.Add("Passcode", Passcode);
        AlphaWareValues.Add("Team", Team.ToString());

        AWebClient.UploadValues(WebHook, AlphaWareValues);
    }

    public void Dispose()
    {
        AWebClient.Dispose();
    }
}
