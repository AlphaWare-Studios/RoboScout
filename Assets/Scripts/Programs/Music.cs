using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Music : MonoBehaviour
{
    public GameObject Manager;
    public GameObject MusicProgram;
    public RectTransform MusicRT;
    public GameObject Screen;
    public bool isMinimized;
    public bool hasCrashed;
    List<string> Songs;
    List<string> SongsType;
    public Text NextSong;
    public Text Title;
    public Toggle LoopOneToggle;
    public Toggle LoopAllToggle;
    public Image PlayButton;
    public Sprite PlayIcon;
    public Sprite PauseIcon;
    public AudioSource Source;
    int CurrentSongInt;
    bool isPaused;
    bool hasStarted;
    bool LoopOne;
    bool LoopAll;

    public void Wake()
    {
        Manager = GameObject.Find("_Manager");
        LoopOne = false;
        LoopAll = false;
        CurrentSongInt = 0;
        hasStarted = false;
        Source = Manager.GetComponent<AudioSource>();
        SettingsClass Settings = Manager.GetComponent<FileManager>().Settings;
        ColorBlock colorBlock = new ColorBlock();
        colorBlock.normalColor = new Color((float)Settings.ButtonColor.Red / 255, (float)Settings.ButtonColor.Green / 255, (float)Settings.ButtonColor.Blue / 255, (float)Settings.ButtonColor.Alpha / 255);
        colorBlock.pressedColor = new Color(0, 0, 0);
        colorBlock.highlightedColor = new Color((float)Settings.ButtonHighlightColor.Red / 255, (float)Settings.ButtonHighlightColor.Green / 255, (float)Settings.ButtonHighlightColor.Blue / 255, (float)Settings.ButtonHighlightColor.Alpha / 255);
        colorBlock.selectedColor = new Color((float)Settings.ButtonHighlightColor.Red / 255, (float)Settings.ButtonHighlightColor.Green / 255, (float)Settings.ButtonHighlightColor.Blue / 255, (float)Settings.ButtonHighlightColor.Alpha / 255);
        colorBlock.disabledColor = new Color(1, 0, 0);
        colorBlock.colorMultiplier = 1;
        colorBlock.fadeDuration = 0.1f;
        this.GetComponent<Image>().color = new Color((float)Settings.PanelColor.Red / 255, (float)Settings.PanelColor.Green / 255, (float)Settings.PanelColor.Blue / 255, (float)Settings.PanelColor.Alpha / 255);
        Screen.transform.Find("Next").GetComponent<Text>().color = new Color((float)Settings.PanelTextColor.Red / 255, (float)Settings.PanelTextColor.Green / 255, (float)Settings.PanelTextColor.Blue / 255, (float)Settings.PanelTextColor.Alpha / 255);
        Screen.transform.Find("Title").GetComponent<Text>().color = new Color((float)Settings.PanelTextColor.Red / 255, (float)Settings.PanelTextColor.Green / 255, (float)Settings.PanelTextColor.Blue / 255, (float)Settings.PanelTextColor.Alpha / 255);
        Screen.transform.Find("Buttons").transform.Find("Stop").GetComponent<Button>().colors = colorBlock;
        Screen.transform.Find("Buttons").transform.Find("Last").GetComponent<Button>().colors = colorBlock;
        Screen.transform.Find("Buttons").transform.Find("Play").GetComponent<Button>().colors = colorBlock;
        Screen.transform.Find("Buttons").transform.Find("Next").GetComponent<Button>().colors = colorBlock;
        Screen.transform.Find("Buttons").transform.Find("LoopOne").GetComponent<Toggle>().colors = colorBlock;
        Screen.transform.Find("Buttons").transform.Find("LoopAll").GetComponent<Toggle>().colors = colorBlock;
        GetSongs();
        Title.text = "No songs available";
        NextSong.text = "";
        if (Songs.Count == 0)
        {
            return;
        }
        Title.text = "Press play";
        NextSong.text = "Ready to play -  " + Songs[0];
    }

    public void Play()
    {
        if (Songs.Count == 0)
        {
            return;
        }
        if (!hasStarted)
        {
            StopCoroutine(Loop());
            StartCoroutine(GetSong());
            hasStarted = true;
        }
        else
        {
            if (!isPaused)
            {
                Source.Pause();
                isPaused = true;
            }
            else
            {
                Source.UnPause();
                isPaused = false;
            }
        }
    }

    IEnumerator GetSong()
    {
        Debug.Log("GetSong");
        Title.text = "Loading...";
        string path = "file://" + FilesDataClass.FilePathMusic + "/" + Songs[CurrentSongInt];
        if (!File.Exists(path))
        {
            
        }
        Debug.Log(path);
        if (SongsType[CurrentSongInt] == "mp3")
        {
            Debug.Log("mp3");
            using var Request = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.MPEG);
            yield return Request.SendWebRequest();
            if (Request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(Request.error);
                yield break;
            }
            yield return null;

            AudioClip clip = DownloadHandlerAudioClip.GetContent(Request);
            while (clip.loadState != AudioDataLoadState.Loaded)
            {
                yield return Request;
            }
            clip.name = Path.GetFileNameWithoutExtension(path);
            Title.text = "Playing - " + Path.GetFileNameWithoutExtension(path);
            Source.clip = clip;
            Source.Play();
        }
        if (SongsType[CurrentSongInt] == "wav")
        {
            Debug.Log("wav");
            using var Request = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.WAV);
            yield return Request.SendWebRequest();
            if (Request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(Request.error);
                yield break;
            }
            yield return null;

            AudioClip clip = DownloadHandlerAudioClip.GetContent(Request);
            while (clip.loadState != AudioDataLoadState.Loaded)
            {
                yield return Request;
            }
            clip.name = Path.GetFileNameWithoutExtension(path);
            Title.text = "Playing - " + Path.GetFileNameWithoutExtension(path);
            Source.clip = clip;
            Source.Play();
        }
        if (SongsType[CurrentSongInt] == "ogg")
        {
            Debug.Log("ogg");
            using var Request = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.OGGVORBIS);
            yield return Request.SendWebRequest();
            if (Request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(Request.error);
                yield break;
            }
            yield return null;

            AudioClip clip = DownloadHandlerAudioClip.GetContent(Request);
            while (clip.loadState != AudioDataLoadState.Loaded)
            {
                yield return Request;
            }
            clip.name = Path.GetFileNameWithoutExtension(path);
            Title.text = "Playing - " + Path.GetFileNameWithoutExtension(path);
            Source.clip = clip;
            Source.Play();
        }
        StartCoroutine(DiscordRP.UpdateActivity(null, "Listening to music(" + Path.GetFileNameWithoutExtension(path) + ")"));
        LoopCheck();
        StartCoroutine(Loop());
    }

    IEnumerator Loop()
    {
        Debug.Log("Loop");
        if (LoopOne || LoopAll)
        {
            PauseBreak:
            while (Source.isPlaying)
            {
                yield return null;
            }
            if (isPaused)
            {
                yield return null;
                goto PauseBreak;
            }
            if (LoopOne)
            {
                if (hasStarted)
                {
                    if (!isPaused)
                    {
                        StartCoroutine(GetSong());
                    }
                }
            }
            else if (LoopAll)
            {
                CurrentSongInt++;
                if (CurrentSongInt > Songs.Count - 1)
                {
                    CurrentSongInt = 0;
                }
                if (hasStarted)
                {
                    if (!isPaused)
                    {
                        StartCoroutine(GetSong());
                    }
                }
            }
        }
        else
        {
            while (Source.isPlaying)
            {
                yield return null;
            }
            Title.text = "Press play";
            NextSong.text = "Ready to play -  " + Songs[CurrentSongInt];
        }
    }

    public void Stop()
    {
        StartCoroutine(DiscordRP.UpdateActivity(null, "Default"));
        StopCoroutine(Loop());
        hasStarted = false;
        isPaused = false;
        Source.Stop();
    }

    public void Prev()
    {
        if (Songs.Count != 0)
        {
            CurrentSongInt--;
            if (CurrentSongInt < 0)
            {
                CurrentSongInt = Songs.Count - 1;
            }
            if (hasStarted)
            {
                if (!isPaused)
                {
                    StartCoroutine(GetSong());
                }
            }
            else
            {
                NextSong.text = "Ready to play -  " + Songs[CurrentSongInt];
            }
        }
    }

    public void Next()
    {
        if (Songs.Count != 0)
        {
            CurrentSongInt++;
            if (CurrentSongInt > Songs.Count - 1)
            {
                CurrentSongInt = 0;
            }
            if (hasStarted)
            {
                if (!isPaused)
                {
                    StartCoroutine(GetSong());
                }
            }
            else
            {
                NextSong.text = "Ready to play -  " + Songs[CurrentSongInt];
            }
        }
    }

    public void BoolLoadOne()
    {
        StopCoroutine(Loop());
        if (LoopOneToggle.isOn)
        {
            LoopOne = true;
            LoopAll = false;
            LoopAllToggle.isOn = false;
            StartCoroutine(Loop());
        }
        else
        {
            LoopOne = false;
        }
        LoopCheck();
    }

    public void BoolLoadAll()
    {
        StopCoroutine(Loop());
        if (LoopAllToggle.isOn)
        {
            LoopAll = true;
            LoopOne = false;
            LoopOneToggle.isOn = false;
            StartCoroutine(Loop());
        }
        else
        {
            LoopAll = false;
        }
        LoopCheck();

    }

    void LoopCheck()
    {
        if (hasStarted)
        {
            string path = "file://" + FilesDataClass.FilePathMusic + "/" + Songs[CurrentSongInt];
            string path2 = "file://" + FilesDataClass.FilePathMusic + "/" + Songs[CurrentSongInt + 1];
            if (LoopAll)
            {
                if (CurrentSongInt + 1 != Songs.Count)
                {
                    NextSong.text = "Next up - " + Path.GetFileNameWithoutExtension(path2);
                }
                else
                {
                    NextSong.text = "Next up - " + Path.GetFileNameWithoutExtension(path);
                }
            }
            else
            if (LoopOne)
            {
                NextSong.text = "Looping " + Path.GetFileNameWithoutExtension(path);
            }
            else
            {
                NextSong.text = "";
            }
        }
    }

    public void Frame()
    {
        if (hasStarted)
        {
            if (!isPaused)
            {
                PlayButton.sprite = PauseIcon;
            }
            else
            {
                PlayButton.sprite = PlayIcon;
            }
        }
        else
        {
            PlayButton.sprite = PlayIcon;
        }
    }

    public void GetSongs()
    {
        Songs = new List<string>();
        SongsType = new List<string>();
        string[] matching = new string[3];
        matching[0] = "*.mp3";
        matching[1] = "*.wav";
        matching[2] = "*.ogg";
        var allFilesMp3 = Directory.GetFiles(FilesDataClass.FilePathMusic, matching[0]).Select(Path.GetFileName);
        foreach (var file in allFilesMp3)
        {
            string Song = file;
            if (!Songs.Contains(Song))
            {
                Songs.Add(Song);
                SongsType.Add("mp3");
            }
        }
        var allFilesWav = Directory.GetFiles(FilesDataClass.FilePathMusic, matching[1]).Select(Path.GetFileName);
        foreach (var file in allFilesWav)
        {
            string Song = file;
            if (!Songs.Contains(Song))
            {
                Songs.Add(Song);
                SongsType.Add("wav");
            }
        }
        var allFilesOgg = Directory.GetFiles(FilesDataClass.FilePathMusic, matching[2]).Select(Path.GetFileName);
        foreach (var file in allFilesOgg)
        {
            string Song = file;
            if (!Songs.Contains(Song))
            {
                Songs.Add(Song);
                SongsType.Add("ogg");
            }
        }
    }

    public void Minimize()
    {
        if (!isMinimized)
        {
            isMinimized = true;
            Screen.SetActive(false);
            MusicRT.sizeDelta = new Vector2(MusicRT.sizeDelta.x, 55);
            MusicProgram.transform.position = new Vector2(MusicProgram.transform.position.x, MusicProgram.transform.position.y + (52.5f * ResolutionManager.ScreenOffsetH));
        }
    }

    public void Maximize()
    {
        if (isMinimized)
        {
            isMinimized = false;
            Screen.SetActive(true);
            MusicRT.sizeDelta = new Vector2(MusicRT.sizeDelta.x, 160);
            MusicProgram.transform.position = new Vector2(MusicProgram.transform.position.x, MusicProgram.transform.position.y - (52.5f * ResolutionManager.ScreenOffsetH));
        }
    }

    public void Close()
    {
        StartCoroutine(DiscordRP.UpdateActivity(null, "Default"));
        Source.Stop();
        GameObject.Destroy(MusicProgram);
    }
}