using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    public GameObject ApplicationWindow;
    public GameObject Calculator;
    public GameObject Notepad;
    public GameObject Music;
    public GameObject Browser;
    public GameObject Console;
    public Transform Programs;
    public bool isStartMenuOpen;
    GameObject Program;
    float ResolutionX;
    float ResolutionY;
    float OffsetX;
    float OffsetY;
    bool hasCalculatored;
    bool hasNotepaded;
    bool hasMusiced;
    bool hasConsoled;

    public void Wake()
    {
        hasConsoled = false;
        ApplicationWindow.SetActive(false);
    }

    public void Frame()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftAlt))
        {
            if (Input.GetKey(KeyCode.Alpha1))
            {
                if (!hasCalculatored)
                {
                    hasCalculatored = true;
                    StartCoroutine(OpenProgramShortcut(Calculator, "Calculator", 1));
                }
            }
            if (Input.GetKey(KeyCode.Alpha2))
            {
                if (!hasNotepaded)
                {
                    hasNotepaded = true;
                    StartCoroutine(OpenProgramShortcut(Notepad, "Notepad", 2));
                }
            }
            if (Input.GetKey(KeyCode.Alpha3))
            {
                if (!hasMusiced)
                {
                    hasMusiced = true;
                    StartCoroutine(OpenProgramShortcut(Music, "Music", 3));
                }
            }
            if (Input.GetKey(KeyCode.C))
            {
                if (!hasConsoled)
                {
                    hasConsoled = true;
                    StartCoroutine(OpenProgramShortcut(Console, "Console", 4));
                }
            }
        }
    }

    void GetScreen()
    {
        ResolutionX = ResolutionManager.NativeW;
        ResolutionY = ResolutionManager.NativeH;
        OffsetX = ResolutionManager.ScreenOffsetW;
        OffsetY = ResolutionManager.ScreenOffsetH;
    }

    IEnumerator OpenProgramShortcut(GameObject ProgramType, string ProgramName, int ProgramBool)
    {
        GetScreen();
        Program = Instantiate(ProgramType) as GameObject;
        Program.name = ProgramName;
        Program.transform.SetParent(Programs.transform);
        Program.transform.localScale = new Vector2(1, 1);
        Program.transform.position = new Vector2((ResolutionX / 2) * OffsetX, (ResolutionY / 2) * OffsetY);
        switch (ProgramName)
        {
            case "Calculator":
                Program.GetComponent<Calculator>().Wake();
                break;
            case "Notepad":
                Program.GetComponent<Notepad>().Wake();
                break;
            case "Music":
                Program.GetComponent<Music>().Wake();
                break;
            case "Console":
                Program.GetComponent<Console>().Wake();
                break;
            default:
                break;
        }
        while (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftAlt))
        {
            yield return null;
        }
        if (ProgramBool == 1)
        {
            hasCalculatored = false;
        }
        if (ProgramBool == 2)
        {
            hasNotepaded = false;
        }
        if (ProgramBool == 3)
        {
            hasMusiced = false;
        }
        if (ProgramBool == 4)
        {
            hasConsoled = false;
        }
    }

    void OpenProgram(GameObject ProgramType, string ProgramName, string[] Arguments)
    {
        GetScreen();
        Program = Instantiate(ProgramType) as GameObject;
        Program.name = ProgramName;
        Program.transform.SetParent(Programs.transform);
        Program.transform.localScale = new Vector2(1, 1);
        Program.transform.position = new Vector2((ResolutionX / 2) * OffsetX, (ResolutionY / 2) * OffsetY);
        switch (ProgramName)
        {
            case "Calculator":
                Program.GetComponent<Calculator>().Wake();
                break;
            case "Notepad":
                Program.GetComponent<Notepad>().Wake();
                break;
            case "Music":
                Program.GetComponent<Music>().Wake();
                break;
            case "Console":
                Program.GetComponent<Console>().Wake();
                break;
            default:
                break;
        }
        if (Arguments != null) {
            switch (ProgramName)
            {
                case "Notepad":
                    Program.transform.Find("Screen").transform.Find("Filename").GetComponent<InputField>().text = Arguments[0];
                    Program.transform.Find("Screen").transform.Find("Text").GetComponent<InputField>().text = Arguments[1];
                    break;
                default:
                    break;
            }
        }
    }

    public void StartMenuClick()
    {
        if (!isStartMenuOpen)
        {
            StartMenuOpen();
        }
        else
        {
            StartMenuClose();
        }

    }

    void StartMenuOpen()
    {
        ApplicationWindow.SetActive(true);
        isStartMenuOpen = true;
    }

    void StartMenuClose()
    {
        ApplicationWindow.SetActive(false);
        isStartMenuOpen = false;
    }

    public void StartProgram(string ProgramName)
    {
        GetScreen();
        switch (ProgramName)
        {
            case "Calculator":
                OpenProgram(Calculator, "Calculator", null);
                break;
            case "Notepad":
                OpenProgram(Notepad, "Notepad", null);
                break;
            case "Music":
                OpenProgram(Music, "Music", null);
                break;
            case "Console":
                OpenProgram(Console, "Console", null);
                break;
            default:
                break;
        }
        StartMenuClose();
    }

    public bool StartProgramBool(string ProgramName, string[] Arguments)
    {
        GetScreen();
        switch (ProgramName)
        {
            case "Calculator":
                OpenProgram(Calculator, "Calculator", Arguments);
                return true;
            case "Notepad":
                OpenProgram(Notepad, "Notepad", Arguments);
                return true;
            case "Music":
                OpenProgram(Music, "Music", Arguments);
                return true;
            case "Console":
                OpenProgram(Console, "Console", Arguments);
                return true;
            default:
                return false;
        }
    }
}
