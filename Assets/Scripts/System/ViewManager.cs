using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager : MonoBehaviour
{
    public GameObject Manager;
    public GameObject Menu_Setup;
    public GameObject Menu_Main;
    public GameObject Menu_Scout;
    public GameObject Menu_Present;
    public GameObject Menu_Settings;
    public GameObject Menu_Manual;
    public GameObject Menu_Credits;
    public GameObject Menu_RSE;
    public GameObject Menu_CSV;
    public GameObject Menu_QR;
    public GameObject Menu_USB;
    public List<string> Windows;
    public int CurrentWindow;
    public bool OpenWithRS = false;
    public bool OpenWithRSE = false;

    void CLS()
    {
        Menu_Setup.SetActive(false);
        Menu_Main.SetActive(false);
        Menu_Scout.SetActive(false);
        Menu_Present.SetActive(false);
        Menu_Settings.SetActive(false);
        Menu_Manual.SetActive(false);
        Menu_Credits.SetActive(false);
        Menu_RSE.SetActive(false);
        Menu_CSV.SetActive(false);
        Menu_QR.SetActive(false);
        Menu_USB.SetActive(false);
    }

    void UpdatePanel(string lvl)
    {
        Manager.GetComponent<SystemUIManager>().ContentSwap(lvl);
    }

    public void Wake()
    {
        CLS();
        if (File.Exists(FilesDataClass.FilePath + "/Settings.rss"))
        {
            if (!OpenWithRS && !OpenWithRSE)
            {
                UpdatePanel("Main");
                Menu_Main.SetActive(true);
                Windows.Add("Main");
                CurrentWindow = 0;
            }
            else if (OpenWithRS)
            {
                UpdatePanel("Scout");
                Menu_Scout.SetActive(true);
                Windows.Add("main");
                Windows.Add("Scout");
                CurrentWindow = 1;
            }
            else if (OpenWithRSE)
            {
                UpdatePanel("Settings");
                Menu_Settings.SetActive(true);
                Windows.Add("main");
                Windows.Add("Settings");
                CurrentWindow = 1;
            }
        }
        else
        {
            UpdatePanel("Main");
            Menu_Main.SetActive(true);
            Windows.Add("Main");
            CurrentWindow = 0;
        }
    }

    public void GoBack()
    {
        string LastWindow;
        if (CurrentWindow != 0)
        {
            LastWindow = Windows[CurrentWindow - 1];
            CurrentWindow--;
        }
        else
        {
            LastWindow = "Main";
        }
        CLS();
        switch (LastWindow)
        {
            case "Main":
                UpdatePanel("Main");
                Menu_Main.SetActive(true);
                break;
            case "Scout":
                UpdatePanel("Scout");
                Menu_Scout.SetActive(true);
                break;
            case "Analyze":
                UpdatePanel("Analyze");
                Menu_Present.SetActive(true);
                break;
            case "Settings":
                UpdatePanel("Settings");
                Menu_Settings.SetActive(true);
                break;
            case "Manual":
                UpdatePanel("Manual");
                Menu_Manual.SetActive(true);
                break;
            case "Credits":
                UpdatePanel("Credits");
                Menu_Credits.SetActive(true);
                break;
            case "RSE":
                UpdatePanel("RSE");
                Menu_RSE.SetActive(true);
                break;
            case "CSV":
                UpdatePanel("CSV");
                Menu_CSV.SetActive(true);
                break;
            case "QR":
                UpdatePanel("QR");
                Menu_QR.SetActive(true);
                break;
            case "USB":
                UpdatePanel("USB");
                Menu_USB.SetActive(true);
                break;
            default:
                Debug.Log("Um...");
                ShowMain();
                break;
        }
    }

    public void GoForward()
    {
        string NextWindow;
        if (CurrentWindow < Windows.Count - 1)
        {
            CurrentWindow++;
            NextWindow = Windows[CurrentWindow];
        }
        else
        {
            NextWindow = Windows[CurrentWindow];
        }
        CLS();
        switch (NextWindow)
        {
            case "Main":
                UpdatePanel("Main");
                Menu_Main.SetActive(true);
                break;
            case "Scout":
                UpdatePanel("Scout");
                Menu_Scout.SetActive(true);
                break;
            case "Analyze":
                UpdatePanel("Analyze");
                Menu_Present.SetActive(true);
                break;
            case "Settings":
                UpdatePanel("Settings");
                Menu_Settings.SetActive(true);
                break;
            case "Manual":
                UpdatePanel("Manual");
                Menu_Manual.SetActive(true);
                break;
            case "Credits":
                UpdatePanel("Credits");
                Menu_Credits.SetActive(true);
                break;
            case "RSE":
                UpdatePanel("RSE");
                Menu_RSE.SetActive(true);
                break;
            case "CSV":
                UpdatePanel("CSV");
                Menu_CSV.SetActive(true);
                break;
            case "QR":
                UpdatePanel("QR");
                Menu_QR.SetActive(true);
                break;
            case "USB":
                UpdatePanel("USB");
                Menu_USB.SetActive(true);
                break;
            default:
                Debug.Log("Um...");
                ShowMain();
                break;
        }
    }

    public bool OpenWindow(string Window)
    {
        CLS();
        switch (Window)
        {
            case "Main":
                UpdatePanel("Main");
                Menu_Main.SetActive(true);
                return true;
            case "Scout":
                UpdatePanel("Scout");
                Menu_Scout.SetActive(true);
                return true;
            case "Analyze":
                UpdatePanel("Analyze");
                Menu_Present.SetActive(true);
                return true;
            case "Settings":
                UpdatePanel("Settings");
                Menu_Settings.SetActive(true);
                return true;
            case "Manual":
                UpdatePanel("Manual");
                Menu_Manual.SetActive(true);
                return true;
            case "Credits":
                UpdatePanel("Credits");
                Menu_Credits.SetActive(true);
                return true;
            case "RSE":
                UpdatePanel("RSE");
                Menu_RSE.SetActive(true);
                return true;
            case "CSV":
                UpdatePanel("CSV");
                Menu_CSV.SetActive(true);
                return true;
            case "QR":
                UpdatePanel("QR");
                Menu_QR.SetActive(true);
                return true;
            case "USB":
                UpdatePanel("USB");
                Menu_USB.SetActive(true);
                return true;
            default:
                ShowMain();
                return false;
        }
    }

    public void Frame()
    {
        
        if (Input.GetKeyDown(KeyCode.Mouse3))
        {
            GoBack();
        }
        if (Input.GetKeyDown(KeyCode.Mouse4))
        {
            GoForward();
        }
    }

    void Timeline(string NewWindow)
    {
        if (CurrentWindow + 1 != Windows.Count)
        {
            if (NewWindow != Windows[CurrentWindow])
            {
                for (int i = Windows.Count - 1; i >= CurrentWindow + 1; i--)
                {
                    Windows.RemoveAt(i);
                }
                Windows.Add(NewWindow);
                CurrentWindow++;
            }
            else
            {

            }
        }
        else
        {
            Windows.Add(NewWindow);
            CurrentWindow++;
        }
    }

    public void ShowMain()
    {
        CLS();
        UpdatePanel("Main");
        Menu_Main.SetActive(true);
        Timeline("Main");
    }

    public void ShowScout()
    {
        CLS();
        UpdatePanel("Scout");
        Menu_Scout.SetActive(true);
        Timeline("Scout");
    }

    public void ShowAnalyze()
    {
        CLS();
        UpdatePanel("Analyze");
        Menu_Present.SetActive(true);
        Timeline("Analyze");
    }

    public void ShowSettings()
    {
        CLS();
        UpdatePanel("Settings");
        Menu_Settings.SetActive(true);
        Timeline("Settings");
    }

    public void ShowManual()
    {
        CLS();
        UpdatePanel("Manual");
        Menu_Manual.SetActive(true);
        Timeline("Manual");
    }

    public void ShowCredits()
    {
        CLS();
        UpdatePanel("Credits");
        Menu_Credits.SetActive(true);
        Timeline("Credits");
    }

    public void ShowRSE()
    {
        CLS();
        UpdatePanel("RSE");
        Menu_RSE.SetActive(true);
        Timeline("RSE");
    }

    public void ShowCSV()
    {
        CLS();
        UpdatePanel("CSV");
        Menu_CSV.SetActive(true);
        Timeline("CSV");
    }

    public void ShowQR()
    {
        CLS();
        UpdatePanel("QR");
        Menu_QR.SetActive(true);
        Timeline("QR");
    }

    public void ShowUSB()
    {
        CLS();
        UpdatePanel("USB");
        Menu_USB.SetActive(true);
        Timeline("USB");
    }

    public void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
			Application.Quit ();
        #endif

    }
}
