using System;
using System.IO;
using UnityEngine;

public class FilesDataClass : MonoBehaviour
{
    public static string FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "RoboScout");
    public static string FilePathSaves = Path.Combine(FilePath, "Saves");
    public static string FilePathLanguage = Path.Combine(FilePath, "Languages");
    public static string FilePathCache = Path.Combine(FilePath, "Cache");
    public static string FilePathCacheJson = Path.Combine(FilePathCache, "Json");
    public static string FilePathCacheImages = Path.Combine(FilePathCache, "Images");
    public static string FilePathPlugins = Path.Combine(FilePath, "Plugins");
    public static string FilePathNotepad = Path.Combine(FilePath, "Notepad");
    public static string FilePathMusic = Path.Combine(FilePath, "Music");
}

[Serializable]
public class FilesClass {
    public string SaveVersion;
    public string CreatedWith;
    public string LastOpenedWith;
    public string Scouter;
    public int QuestionsInt;
    public int[] QuestionsType;
    public string[] Questions;
    public string[] Answers;
    public string Comment;
}

[Serializable]
public class FilesPackageClass
{
    public string SaveVersion;
    public string Date;
    public int FilesCount;
    public string[] FilesInfo;
    public FilesClass[] Files;
}

[Serializable]
public class QRClass
{
    public string SaveVersion;
    public bool Master;
    public string UUID;
    public int MaxFiles;
    public int ThisQR;
    public bool Compression;
    public string Data;
}

[Serializable]
public class FilesQRClass
{
    public int FilesCount;
    public string[] FilesInfo;
    public FilesClass[] Files;
}
