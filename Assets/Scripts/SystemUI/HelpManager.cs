using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpManager : MonoBehaviour
{
    public GameObject Manager;
    public GameObject HelpWindow;
    public bool isHelpMenuOpen;
    void Start()
    {
        HelpWindow.SetActive(false);
    }

    public void HelpMenuClick()
    {
        if (!isHelpMenuOpen)
        {
            HelpMenuOpen();
        }
        else
        {
            HelpMenuClose();
        }

    }

    void HelpMenuOpen()
    {
        HelpWindow.SetActive(true);
        isHelpMenuOpen = true;
    }

    void HelpMenuClose()
    {
        HelpWindow.SetActive(false);
        isHelpMenuOpen = false;
    }

    public void OpenWindow(string WindowName)
    {
        switch (WindowName)
        {
            case "Manual":
                Manager.GetComponent<ViewManager>().ShowManual();
                break;
            case "Credits":
                Manager.GetComponent<ViewManager>().ShowCredits();
                break;
            case "Feedback":
                Manager.GetComponent<ExternalLinks>().ExternalLink("https://alphawarestudios.com/Feedback");
                break;
        }
        HelpMenuClose();
    }
}
