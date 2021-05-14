using System;
using System.IO;
using System.Text;
using UnityEngine;

public class Init : MonoBehaviour
{
    public GameObject Manager;
    public GameObject ScoutManager;
    public GameObject SettingsManager;

    void Awake()
    {
        Physics.autoSimulation = false;
        Application.targetFrameRate = -1;
        Application.logMessageReceivedThreaded += Manager.GetComponent<ErrorManager>().Exception;
        if (File.Exists(FilesDataClass.FilePath + "/Settings.rss"))
        {
            byte[] bytes = File.ReadAllBytes(FilesDataClass.FilePath + "/Settings.rss");
            string JSON = Encoding.ASCII.GetString(bytes);
            SettingsClass Settings;
            try
            {
                Settings = JsonUtility.FromJson<SettingsClass>(JSON);
            }
            catch (ArgumentException)
            {
                File.Delete(FilesDataClass.FilePath + "/Settings.rss");
                Manager.GetComponent<ErrorManager>().Error("Corrupt Settings File, Settings have been reset.");
            }
        }
        if (!Directory.Exists(FilesDataClass.FilePath))
        {
            Directory.CreateDirectory(FilesDataClass.FilePath);
        }
        if (!Directory.Exists(FilesDataClass.FilePathSaves))
        {
            Directory.CreateDirectory(FilesDataClass.FilePathSaves);
        }
        if (!Directory.Exists(FilesDataClass.FilePathLanguage))
        {
            Directory.CreateDirectory(FilesDataClass.FilePathLanguage);
        }
        if (!Directory.Exists(FilesDataClass.FilePathCache))
        {
            Directory.CreateDirectory(FilesDataClass.FilePathCache);
        }
        if (!Directory.Exists(FilesDataClass.FilePathCacheJson))
        {
            Directory.CreateDirectory(FilesDataClass.FilePathCacheJson);
        }
        if (!Directory.Exists(FilesDataClass.FilePathCacheImages))
        {
            Directory.CreateDirectory(FilesDataClass.FilePathCacheImages);
        }
        if (!Directory.Exists(FilesDataClass.FilePathPlugins))
        {
            Directory.CreateDirectory(FilesDataClass.FilePathPlugins);
        }
        if (!Directory.Exists(FilesDataClass.FilePathNotepad))
        {
            Directory.CreateDirectory(FilesDataClass.FilePathNotepad);
        }
        if (!Directory.Exists(FilesDataClass.FilePathMusic))
        {
            Directory.CreateDirectory(FilesDataClass.FilePathMusic);
        }
        string[] args = Environment.GetCommandLineArgs();
        if (args.Length > 0)
        {
            if (File.Exists(args[1]))
            {
                if (args[1].EndsWith(".rs"))
                {
                    Manager.GetComponent<ViewManager>().OpenWithRS = true;
                    ScoutManager.GetComponent<Scout>().OpenWithCheck = true;
                    ScoutManager.GetComponent<Scout>().OpenWithDirectory = args[1];
                }
                else if (args[1].EndsWith(".rse"))
                {
                    Manager.GetComponent<ViewManager>().OpenWithRSE = true;
                }
                else if (args[1].EndsWith(".rss"))
                {

                }
            }
        }
        if (Application.isEditor)
        {
            DiscordRP.Status2Default = "Developing";
            StartCoroutine(DiscordRP.UpdateActivity(null, "Developing"));
        }
        else
        {
            DiscordRP.Status2Default = "";
        }
        Manager.GetComponent<MethodManager>().StartManaging();
    }
}
