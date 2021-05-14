using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TBAManager : MonoBehaviour
{
    //private readonly string UpdateURL = "";
    private readonly string TBAStatusURL = "https://www.thebluealliance.com/api/v3/status";
    private readonly string TeamURL = "https://www.thebluealliance.com/api/v3/team/frc";
    private readonly string Auth = "?X-TBA-Auth-Key=UmhU7XOG9pCzlOfempRGmK0pXNMnPd6PMjwL947HWiDAcBcYXJ48ZfhJOnR0k7pn";
    public TBAClass TBAStatusData;
    public bool TBAStatusDone = false;
    public TeamDataClass TeamDataData;
    public bool TeamInfoDone = false;
    public TeamMediaClass TeamMediaData;
    public bool TeamMediaDone = false;

    public void GetTBAStatus()
    {
        TBAStatusDone = false;
        StartCoroutine(GetTBAStatusCoroutine());
    }

    public void GetTeamInfo(int Team)
    {
        TeamInfoDone = false;
        StartCoroutine(GetTeamInfoCoroutine(Team));
    }

    public void GetTeamMedia(int Team, int Year)
    {
        TeamMediaDone = false;
        StartCoroutine(GetTeamMediaCoroutine(Team, Year));
    }

    IEnumerator GetRequest(string url, Action<UnityWebRequest> callback)
    {
        using UnityWebRequest Request = UnityWebRequest.Get(url);
        yield return Request.SendWebRequest();
        callback(Request);
    }

    IEnumerator GetTBAStatusCoroutine()
    {
        StartCoroutine(GetRequest(TBAStatusURL + Auth, (UnityWebRequest Request) =>
        {
            if (Request.result == UnityWebRequest.Result.Success)
            {
                string data = Request.downloadHandler.text;
                TBAStatusData = JsonUtility.FromJson<TBAClass>(data);
                TBAStatusDone = true;
            }
            else
            {
                TBAStatusData = null;
                TBAStatusDone = true;
            }
        }
        ));
        yield return null;
    }

    IEnumerator GetTeamInfoCoroutine(int Team)
    {
        StartCoroutine(GetRequest(TeamURL + Team + Auth, (UnityWebRequest Request) =>
        {
            if (Request.result == UnityWebRequest.Result.Success)
            {
                string data = Request.downloadHandler.text;
                TeamDataData = JsonUtility.FromJson<TeamDataClass>(data);
                TeamInfoDone = true;
            }
            else
            {
                TeamDataData = null;
                TeamInfoDone = true;
            }
        }));
        yield return null;
    }

    IEnumerator GetTeamMediaCoroutine(int Team, int Year)
    {
        StartCoroutine(GetRequest(TeamURL + Team + "/media/" + Year + Auth, (UnityWebRequest Request) =>
        {
            if (Request.result == UnityWebRequest.Result.Success)
            {
                string data = Request.downloadHandler.text;
                string[] Error = data.Split(' ');
                if (Error[0] != "{\"Errors\":")
                {
                    data = "{\"Stuff\":" + data + "}";
                    TeamMediaData = JsonUtility.FromJson<TeamMediaClass>(data);
                    TeamMediaDone = true;
                }
                else
                {
                    TeamMediaData = null;
                    TeamMediaDone = true;
                }
            }
            else
            {
                TeamMediaData = null;
                TeamMediaDone = true;
            }
        }));
        yield return null;
    }
}
