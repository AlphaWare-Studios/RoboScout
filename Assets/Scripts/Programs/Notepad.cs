using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.IO;
using System;

public class Notepad : MonoBehaviour
{
	GameObject Manager;
	public GameObject NoteProgram;
	public RectTransform NoteRT;
	public GameObject Screen;
	public bool isMinimized;
	public bool hasCrashed;
	public InputField TextBox;
	public InputField FileBox;

	public void Wake()
	{
		Manager = GameObject.Find("_Manager");
		SettingsClass Settings = Manager.GetComponent<FileManager>().Settings;
		ColorBlock colorBlock = new ColorBlock
        {
            normalColor = new Color((float)Settings.ButtonColor.Red / 255, (float)Settings.ButtonColor.Green / 255, (float)Settings.ButtonColor.Blue / 255, (float)Settings.ButtonColor.Alpha / 255),
            pressedColor = new Color(0, 0, 0),
            highlightedColor = new Color((float)Settings.ButtonHighlightColor.Red / 255, (float)Settings.ButtonHighlightColor.Green / 255, (float)Settings.ButtonHighlightColor.Blue / 255, (float)Settings.ButtonHighlightColor.Alpha / 255),
            selectedColor = new Color((float)Settings.ButtonHighlightColor.Red / 255, (float)Settings.ButtonHighlightColor.Green / 255, (float)Settings.ButtonHighlightColor.Blue / 255, (float)Settings.ButtonHighlightColor.Alpha / 255),
            disabledColor = new Color(1, 0, 0),
            colorMultiplier = 1,
            fadeDuration = 0.1f
        };
        this.GetComponent<Image>().color = new Color((float)Settings.PanelColor.Red / 255, (float)Settings.PanelColor.Green / 255, (float)Settings.PanelColor.Blue / 255, (float)Settings.PanelColor.Alpha / 255);
		Screen.transform.Find("Filename").GetComponent<InputField>().colors = colorBlock;
		Screen.transform.Find("Filename").transform.Find("Placeholder").GetComponentInChildren<Text>().color = new Color((float)Settings.ButtonTextColor.Red / 255, (float)Settings.ButtonTextColor.Green / 255, (float)Settings.ButtonTextColor.Blue / 255, (float)Settings.ButtonTextColor.Alpha / 255);
		Screen.transform.Find("Filename").transform.Find("Text").GetComponentInChildren<Text>().color = new Color((float)Settings.ButtonTextColor.Red / 255, (float)Settings.ButtonTextColor.Green / 255, (float)Settings.ButtonTextColor.Blue / 255, (float)Settings.ButtonTextColor.Alpha / 255);
		Screen.transform.Find("Open").GetComponent<Button>().colors = colorBlock;
		Screen.transform.Find("Save").GetComponent<Button>().colors = colorBlock;
		Screen.transform.Find("New").GetComponent<Button>().colors = colorBlock;
		Screen.transform.Find("Text").GetComponent<InputField>().colors = colorBlock;
		Screen.transform.Find("Text").transform.Find("Placeholder").GetComponentInChildren<Text>().color = new Color((float)Settings.ButtonTextColor.Red / 255, (float)Settings.ButtonTextColor.Green / 255, (float)Settings.ButtonTextColor.Blue / 255, (float)Settings.ButtonTextColor.Alpha / 255);
		Screen.transform.Find("Text").transform.Find("Text").GetComponentInChildren<Text>().color = new Color((float)Settings.ButtonTextColor.Red / 255, (float)Settings.ButtonTextColor.Green / 255, (float)Settings.ButtonTextColor.Blue / 255, (float)Settings.ButtonTextColor.Alpha / 255);
	}

	public void Open()
	{
		if (File.Exists(FilesDataClass.FilePathNotepad + "/" + FileBox.text + ".txt"))
		{
			byte[] bytes = File.ReadAllBytes(FilesDataClass.FilePathNotepad + "/" + FileBox.text + ".txt");
			TextBox.text = Encoding.ASCII.GetString(bytes);
			Manager.GetComponent<ErrorManager>().Log("Text file " + FileBox.text + ".txt has been loaded");
		}
		else
		{
			Manager.GetComponent<ErrorManager>().Warning("Text file " + FileBox.text + ".txt does not exist");
		}
	}

	public void Save()
	{
		if (FileBox.text != "") {
			byte[] bytes = Encoding.ASCII.GetBytes(TextBox.text);
			try
			{
				File.WriteAllBytes(FilesDataClass.FilePathNotepad + "/" + FileBox.text + ".txt", bytes);
				Manager.GetComponent<ErrorManager>().Log("Text file saved as " + FileBox.text + ".txt");
					Manager.GetComponent<ErrorManager>().Error("Unsupported characters in filename");
			}
			catch (Exception E)
			{
				if (E is DirectoryNotFoundException)
                {
					Manager.GetComponent<ErrorManager>().Error("That directory does not exist");
				}
				else if (E is IOException)
                {
					Manager.GetComponent<ErrorManager>().Error("Unsupported characters in filename");
				}
				else
                {
					Manager.GetComponent<ErrorManager>().Error("An unknown error occurred while attempting to save");
				}
			}
		}
		else
		{
			Manager.GetComponent<ErrorManager>().Warning("Filename cannot be blank");
		}
	}

	public void New()
	{
		FileBox.text = "";
		TextBox.text = "";
	}

	public void Minimize()
	{
		if (!isMinimized)
		{
			isMinimized = true;
			Screen.SetActive(false);
			NoteRT.sizeDelta = new Vector2(NoteRT.sizeDelta.x, 55);
			NoteProgram.transform.position = new Vector2(NoteProgram.transform.position.x, NoteProgram.transform.position.y + (222.5f * ResolutionManager.ScreenOffsetH));
		}
	}

	public void Maximize()
	{
		if (isMinimized)
		{
			isMinimized = false;
			Screen.SetActive(true);
			NoteRT.sizeDelta = new Vector2(NoteRT.sizeDelta.x, 500);
			NoteProgram.transform.position = new Vector2(NoteProgram.transform.position.x, NoteProgram.transform.position.y - (222.5f * ResolutionManager.ScreenOffsetH));
		}
	}

	public void Close()
	{
		GameObject.Destroy(NoteProgram);
	}
}
