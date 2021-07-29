using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.IO;

public class Splash : MonoBehaviour
{
    public Text splash;
    public GameObject Manager;
    public GameObject MainManager;
    public string[] texts;
    public int SID;

    public void Wake()
    {
        string ThenString = "2018-10-11";
        DateTime Then = DateTime.Parse(ThenString);
        string matching = "*.RS";
        int Files = Directory.GetFiles(FilesDataClass.FilePathSaves, matching).Length;
        string Characters = " abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        string Random = "";
        int[] RandomLegnth = new int[25];
        foreach (int woo in RandomLegnth)
        {
            Random += Characters[UnityEngine.Random.Range(0, 62)];
        }
        texts = new string[56];
        texts[0] = "This will never be seen :(";
        texts[1] = "Unknown lines of code";
        texts[2] = "Yes";
        texts[3] = "2";
        texts[4] = "01001000 01100101 01101100 01101100 01101111 00100000 01010111 01101111 01110010 01101100 01100100";
        texts[5] = "Spying on robots since 2018";
        texts[6] = "Possibly Dennis proof";
        texts[7] = "Sure beats pen and paper";
        texts[8] = "Made by the Corner Crew";
        texts[9] = "Donley the Buc n` Gear was here";
        texts[10] = "<color=#ff0000>C</color><color=#ff8800>O</color><color=#ffff00>L</color><color=#88ff00>O</color><color=#00ff00>R</color><color=#00ff88>M</color><color=#00ffff>A</color><color=#0088ff>T</color><color=#0000ff>I</color><color=#8800ff>C</color><color=#ff00ff>!</color><color=#ff0088>!</color>";
        texts[11] = "I'VE GOT A JAR OF DIRT";
        texts[12] = "Just Monika";
        texts[13] = "I reject your reality and substitute my own";
        texts[14] = "SPLASH";
        texts[15] = "Deja Vu";
        texts[16] = "Deja Vu";
        texts[17] = "https://www.youtube.com/watch?v=dQw4w9WgXcQ";
        texts[18] = "RoboScout gets updates so make sure to connect it to internet every so often to check";
        texts[19] = "Generating ablation cascade";
        texts[20] = "Please use gracious professionalism!";
        texts[21] = "I wanna visit null island";
        texts[22] = "Task failed successfully";
        texts[23] = "Much more betterer";
        texts[24] = "Rest in peace, Woodie Flowers";
        texts[25] = "Just do it";
        texts[26] = "HEY DUDE! You're getting a dell!";
        texts[27] = "(◕ヮ◕)";
        texts[28] = "[Insert windows xp startup sound here]";
        texts[29] = "I cant think of any splash to put here";
        texts[30] = "NullReferenceException";
        texts[31] = "Error 403 splash text denied";
        texts[32] = "Error 404 splash text not found";
        texts[33] = "Error 418 I'm a teapot";
        texts[34] = "October 25, 2001 - April 8, 2014";
        texts[35] = "October 22, 2009 - January 14, 2020";
        texts[36] = "すばらし";
        texts[37] = "Harder";
        texts[38] = "Better";
        texts[39] = "Faster";
        texts[40] = "Stronger";
        texts[41] = "Celebrating " + GetDifferenceInYears(Then,DateTime.Now) + " years of RoboScout!";
        texts[42] = "MY DEPENDENCIES";
        texts[43] = "R.I.P AlphaWare-Server1";
        texts[44] = "aHR0cHM6Ly93d3cueW91dHViZS5jb20vd2F0Y2g/dj1kUXc0dzlXZ1hjUQ==";
        texts[45] = "Battery Data";
        texts[46] = "Mechanical problems require Software solutions";
        texts[47] = "Dell servers make a good foot rest";
        texts[48] = "You have " + Files + " scouting files";
        texts[49] = Random;
        texts[50] = "RoboScout 2 Electric Boogaloo";
        texts[51] = "OUT OF STOCK";
        texts[52] = "FIRST Season";
        texts[53] = "Pickup that can";
        texts[54] = "Any computer is a laptop if your brave enough";
        texts[55] = "Rise and shine, Mister Freeman.";
        //texts[64] = "A stack of spash";
        //texts[65] = "texts[65]";
        //texts[66] = "Execute order 66";
        //texts[69] = "nice";
        SID = UnityEngine.Random.Range(1, 56);
        splash.text = texts[SID];
        if (SID == 52)
        {
            StartCoroutine(Season());
        }
    }

    public void Second()
    {
        if (SID == 45)
        {
            if (Manager.GetComponent<SystemUIManager>().hasBattery)
            {
                splash.text = "Your battery is at " + Manager.GetComponent<SystemUIManager>().BatteryPercent + "%";
            }
            else
            {
                splash.text = "You don't have a battery";
            }
        }
    }

    public void Frame()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.S))
        {
            Wake();
        }
    }

    IEnumerator Season()
    {
        while (!MainManager.GetComponent<Main>().isDone)
        {
            yield return null;
        }
        splash.text = "Welcome to the <i>FIRST</i> Robotics " + MainManager.GetComponent<Main>().Year + " Season";
    }

    public int GetDifferenceInYears(DateTime startDate, DateTime endDate)
    {
        int years = endDate.Year - startDate.Year;

        if (startDate.Month == endDate.Month && endDate.Day < startDate.Day || endDate.Month < startDate.Month)
        {
            years--;
        }

        return years;
    }
}
