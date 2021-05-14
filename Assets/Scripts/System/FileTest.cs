using UnityEngine;
using System;
using System.IO;
using System.Text;

public static class FileTest
{

    public static string FileCheck(string Directory)
    {
        string Test;

        string CreatedWith;
        string Scouter;
        int QuestionsCount;
        int[] QuestionsType;
        string[] QuestionsData;
        string[] AnswersData;

        byte[] bytes = File.ReadAllBytes(Directory);
        string JSON = Encoding.ASCII.GetString(bytes);
        FilesClass Files;
        try
        {
            Files = JsonUtility.FromJson<FilesClass>(JSON);
            try
            {
                if (Files.SaveVersion == "v1")
                {
                    CreatedWith = Files.CreatedWith;
                    Scouter = Files.Scouter;
                    QuestionsCount = Files.QuestionsInt;
                    QuestionsType = Files.QuestionsType;
                    QuestionsData = Files.Questions;
                    AnswersData = Files.Answers;
                    Test = "RS2";
                }
                else
                {
                    Test = "Future";
                }
            }
            catch (Exception)
            {
                Test = "Error";
            }
        }
        catch (ArgumentException)
        {
            try
            {
                string[] ScoutFiles = RS1Files.LoadRS1Files(Directory);
                if (ScoutFiles[0] != "RSFSV1")
                {
                    Test = "RS1";
                }
                else
                {
                    Test = "RS1.5";
                }
            }
            catch (Exception)
            {
                Test = "Error";
            }
        }
        return Test;
    }

    public static string QRCheck(string Data)
    {
        string Test;

        bool Master;
        string UUID;
        int FilesT;
        QRClass Files;
        try
        {
            Files = JsonUtility.FromJson<QRClass>(Data);
            try
            {
                if (Files.SaveVersion == "v1")
                {
                    Master = Files.Master;
                    UUID = Files.UUID;
                    FilesT = Files.MaxFiles;
                    Test = "RS2";
                }
                else
                {
                    Test = "Future";
                }
            }
            catch (Exception)
            {
                Test = "Error";
            }
        }
        catch (ArgumentException)
        {
            /*try
            {
                string[] ScoutFiles = RS1Files.LoadRS1Files(Directory);
                if (ScoutFiles[0] != "RSFSV1")
                {
                    Test = "RS1";
                }
                else
                {
                    Test = "RS1.5";
                }
            }
            catch (Exception)
            {
                Test = "Error";
            }*/
            Test = "Error";
        }
        return Test;
    }

}
