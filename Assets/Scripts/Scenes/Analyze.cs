using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Analyze : MonoBehaviour
{
    public Text TeamTitle;
    public Text TeamTotal;
    public Text RoundTotal;
    public int TeamsTotal;
    public int RoundsTotal;
    public GameObject Manager;
    public GameObject MainManager;
    public Transform Teams;
    public Transform SpawnerTeams;
    public GameObject Team;
    public Transform Rounds;
    public Transform SpawnerRounds;
    public GameObject Round;
    public GameObject Left;
    public GameObject Right;
    public Scrollbar LeftScroll;
    public Scrollbar RightScroll;
    public GameObject AnalyzeGO;
    float OffsetX;
    float OffsetY;

    public void Wake()
    {
        OffsetX = ResolutionManager.ScreenOffsetW;
        OffsetY = ResolutionManager.ScreenOffsetH;
        TeamTitle.text = "";
        TeamTotal.text = "0 Files";
        RoundTotal.text = "0 Matches";
        Left.GetComponent<ScrollRect>().vertical = false;
        Right.GetComponent<ScrollRect>().vertical = false;
        LeftScroll.enabled = false;
        RightScroll.enabled = false;
        StartCoroutine(LoadTeamsCoroutine());
        StartCoroutine(LoadRoundsCoroutine());
    }

    public void Reload()
    {
        OffsetX = ResolutionManager.ScreenOffsetW;
        OffsetY = ResolutionManager.ScreenOffsetH;
        TeamTitle.text = "";
        TeamTotal.text = "0 Files";
        RoundTotal.text = "0 Matches";
        Left.GetComponent<ScrollRect>().vertical = false;
        Right.GetComponent<ScrollRect>().vertical = false;
        LeftScroll.enabled = false;
        RightScroll.enabled = false;
        StopAllCoroutines();
        StartCoroutine(LoadTeamsCoroutine());
        StartCoroutine(LoadRoundsCoroutine());
    }

    IEnumerator LoadTeamsCoroutine()
    {
        foreach (Transform child in Teams.transform)
        {
            if (child.gameObject.name != "ScrollFull")
            {
                GameObject.Destroy(child.gameObject);
            }
        }
        while (!MainManager.GetComponent<Main>().isDone)
        {
            yield return null;
        }
        TeamsTotal = 0;
        var teams = GetTeams();
        teams.Sort();
        int Total = teams.Count;
        while (!AnalyzeGO.activeSelf)
        {
            yield return null;
        }
        foreach (var team in teams)
        {
            Teams.GetComponent<RectTransform>().sizeDelta = new Vector2(Teams.GetComponent<RectTransform>().sizeDelta.x, 5 + ((45f * Total)));
            Teams.position = new Vector2(66.5f * OffsetX, (942.5f * OffsetY) - ((22.5f * Total) * OffsetY));
            GameObject childObject = Instantiate(Team) as GameObject;
            childObject.GetComponent<AnalyzeButton>().isTeam = true;
            childObject.name = team.ToString();
            childObject.transform.SetParent(Teams.transform);
            childObject.GetComponentInChildren<Text>().text = team.ToString();
            TeamsTotal++;
            if (TeamsTotal != 1)
            {
                TeamTotal.text = TeamsTotal.ToString() + " Teams";
            }
            else
            {
                TeamTotal.text = TeamsTotal.ToString() + " Team";
            }
            while (!childObject.GetComponent<AnalyzeButton>().isDone)
            {
                yield return null;
            }
        }
        if (Total > 18)
        {
            Left.GetComponent<ScrollRect>().vertical = true;
            LeftScroll.enabled = true;
        }
    }

    IEnumerator LoadRoundsCoroutine()
    {
        foreach (Transform child in Rounds.transform)
        {
            if (child.gameObject.name != "ScrollFull") {
                GameObject.Destroy(child.gameObject);
            }
        }
        while (!MainManager.GetComponent<Main>().isDone)
        {
            yield return null;
        }
        RoundsTotal = 0;
        var rounds = GetRounds();
        rounds.Sort();
        int Total = rounds.Count;
        while (!AnalyzeGO.activeSelf)
        {
            yield return null;
        }
        foreach (var round in rounds)
        {
            Rounds.GetComponent<RectTransform>().sizeDelta = new Vector2(Rounds.GetComponent<RectTransform>().sizeDelta.x, 5 * OffsetY + (45f * Total));
            Rounds.position = new Vector2(178f * OffsetX, (942.5f * OffsetY) - ((22.5f * Total) * OffsetY));
            GameObject childObject = Instantiate(Round) as GameObject;
            childObject.GetComponent<AnalyzeButton>().isTeam = false;
            childObject.name = round;
            childObject.transform.SetParent(Rounds.transform);
            childObject.GetComponentInChildren<Text>().text = "Match " + round;
            RoundsTotal++;
            if (RoundsTotal != 1)
            {
                RoundTotal.text = RoundsTotal.ToString() + " Matches";
            }
            else
            {
                RoundTotal.text = RoundsTotal.ToString() + " Matches";
            }
        }
        if (Total > 18)
        {
            Right.GetComponent<ScrollRect>().vertical = true;
            RightScroll.enabled = true;
        }
    }

    public List<int> GetTeams()
    {
        List<int> teams = new List<int>();
        string matching = "*.rs";
        var allFiles = Directory.GetFiles(FilesDataClass.FilePathSaves, matching).Select(Path.GetFileName);
        foreach (var file in allFiles)
        {
            int team = int.Parse(file.Split('-')[1].Split('.')[0]);
            if (!teams.Contains(team))
                teams.Add(team);
        }
        return teams;
    }

    public List<string> GetRounds()
    {
        List<string> rounds = new List<string>();
        string matching = "*.rs";
        var allFiles = Directory.GetFiles(FilesDataClass.FilePathSaves, matching).Select(Path.GetFileName);
        foreach (var file in allFiles)
        {
            string round = file.Split('-')[0];
            if (!rounds.Contains(round))
                rounds.Add(round);
        }
        return rounds;
    }
}