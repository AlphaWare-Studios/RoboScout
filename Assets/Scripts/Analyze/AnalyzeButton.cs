using System;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnalyzeButton : MonoBehaviour
{
    public GameObject Manager;
    public GameObject MainManager;
    public RawImage Icon;
    public bool isDone;
    public bool isTeam;
    public Texture2D FIRST;
    public Text TeamTitle;
    int Team;
    int Year;

    private void Start()
    {
        isDone = false;
        Manager = GameObject.Find("_Manager");
        MainManager = GameObject.Find("Main Manager");
        TeamTitle = GameObject.Find("TeamTitle").GetComponent<Text>();
        SettingsClass Settings = Manager.GetComponent<FileManager>().Settings;
        ColorBlock colorBlock = new ColorBlock();
        colorBlock.normalColor = new Color((float)Settings.ButtonColor.Red / 255, (float)Settings.ButtonColor.Green / 255, (float)Settings.ButtonColor.Blue / 255, (float)Settings.ButtonColor.Alpha / 255);
        colorBlock.pressedColor = new Color(0, 0, 0);
        colorBlock.highlightedColor = new Color(1, 1, 1);
        colorBlock.selectedColor = new Color(1, 1, 1);
        colorBlock.disabledColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        colorBlock.colorMultiplier = 1;
        colorBlock.fadeDuration = 0.1f;
        this.GetComponent<Button>().colors = colorBlock;
        this.GetComponentInChildren<Text>().color = new Color((float)Settings.ButtonTextColor.Red / 255, (float)Settings.ButtonTextColor.Green / 255, (float)Settings.ButtonTextColor.Blue / 255, (float)Settings.ButtonTextColor.Alpha / 255);
        if (isTeam == true)
        {
            StartCoroutine(GetTeamMediaStatus());
        }
    }

    public void LoadTeam()
    {
        TeamTitle.text = this.GetComponentInChildren<Text>().text;
    }

    public void LoadRound()
    {
        TeamTitle.text = this.GetComponentInChildren<Text>().text;
    }

    IEnumerator GetTeamMediaStatus()
    {
        Team = int.Parse(this.GetComponentInChildren<Text>().text);
        Year = MainManager.GetComponent<Main>().Year;
        bool isCached = false;
        if (File.Exists(FilesDataClass.FilePathCacheImages + "/Avatar_" + Team + "_" + Year + ".png"))
        {
            isCached = true;
        }
        if (!isCached)
        {
            if (!MainManager.GetComponent<Main>().isDataDown)
            {
                Manager.GetComponent<TBAManager>().GetTeamMedia(Team, Year);
                while (Manager.GetComponent<TBAManager>().TeamMediaDone == false)
                {
                    yield return null;
                }
                TeamMediaClass Data = Manager.GetComponent<TBAManager>().TeamMediaData;
                if (Data != null)
                {
                    if (Data.Stuff.Length != 0)
                    {
                        if (Data.Stuff[0].details != null)
                        {
                            if (Data.Stuff[0].details.base64Image != null)
                            {
                                string name = Data.Stuff[0].foreign_key;
                                byte[] imageBytes = Convert.FromBase64String(Data.Stuff[0].details.base64Image);
                                Texture2D tex = new Texture2D(40, 40);
                                tex.filterMode = FilterMode.Point;
                                tex.LoadImage(imageBytes);
                                Icon.GetComponent<RawImage>().texture = tex;
                                CachePNG(tex);
                            }
                            else
                            {
                                Icon.GetComponent<RawImage>().texture = FIRST;
                                CachePNG(FIRST);
                            }
                        }
                        else
                        {
                            Icon.GetComponent<RawImage>().texture = FIRST;
                            CachePNG(FIRST);
                        }
                    }
                    else
                    {
                        Icon.GetComponent<RawImage>().texture = FIRST;
                        CachePNG(FIRST);
                    }
                }
                else
                {
                    Icon.GetComponent<RawImage>().texture = FIRST;
                    CachePNG(FIRST);
                }
            }
            else
            {
                Icon.GetComponent<RawImage>().texture = FIRST;
                CachePNG(FIRST);
            }
        }
        else
        {
            byte[] bytes = File.ReadAllBytes(FilesDataClass.FilePathCacheImages + "/Avatar_" + Team + "_" + Year + ".png");
            Texture2D tex = new Texture2D(40, 40);
            tex.filterMode = FilterMode.Point;
            tex.LoadImage(bytes);
            Icon.GetComponent<RawImage>().texture = tex;
        }
        isDone = true;
    }

    void CachePNG(Texture2D Image)
    {
        byte[] bytes = Image.EncodeToPNG();
        File.WriteAllBytes(FilesDataClass.FilePathCacheImages + "/Avatar_" + Team + "_" + Year + ".png", bytes);
    }
}
