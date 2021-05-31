using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class AWManager : MonoBehaviour
{
    string AWURL = "alphawarestudios.com";
    private readonly string AWStatusURL = "/Data/RoboScout.json";
    private readonly string AWTeamsURL = "/Data/RoboScoutTeams.txt";
    public AWClass AWStatusData;
    public string AWTeamsData;
    public bool AWStatusDone = false;
    public bool AWTeamsDone = false;

    public void GetAWStatus()
    {
        AWStatusDone = false;
        StartCoroutine(GetAWStatusCoroutine());
    }

    public void GetAWTeams()
    {
        AWTeamsDone = false;
        StartCoroutine(GetAWTeamsCoroutine());
    }

    IEnumerator GetRequest(string url, Action<UnityWebRequest> callback)
    {
        using UnityWebRequest Request = UnityWebRequest.Get(url);
        yield return Request.SendWebRequest();
        callback(Request);
    }

    IEnumerator GetAWStatusCoroutine()
    {
        StartCoroutine(GetRequest("https://" + AWURL + AWStatusURL, (UnityWebRequest Request) =>
        {
            if (Request.result == UnityWebRequest.Result.Success)
            {
                string data = Request.downloadHandler.text;
                try
                {
                    AWStatusData = JsonUtility.FromJson<AWClass>(data);
                    AWStatusDone = true;
                }
                catch (ArgumentException)
                {
                    AWStatusData = null;
                    AWStatusDone = true;
                }
            }
            else
            {
                AWStatusData = null;
                AWStatusDone = true;
            }
        }
        ));
        yield return null;
    }

    IEnumerator GetAWTeamsCoroutine()
    {
        StartCoroutine(GetRequest("https://" + AWURL + AWTeamsURL, (UnityWebRequest Request) =>
        {
            if (Request.result == UnityWebRequest.Result.Success)
            {
                string data = Request.downloadHandler.text;
                AWTeamsData = data;
                AWTeamsDone = true;
            }
            else
            {
                AWTeamsData = "";
                AWTeamsDone = true;
            }
        }
        ));
        yield return null;
    }
}
