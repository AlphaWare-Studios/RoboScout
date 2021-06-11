using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ImageViewer : MonoBehaviour
{
	GameObject Manager;
	public GameObject ImageProgram;
	public RectTransform ImageRT;
	public GameObject Screen;
	public bool isMinimized;
	public Dropdown Directory;
	public InputField FileBox;
	public RawImage Image;

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
		Screen.transform.Find("Directory").GetComponent<Dropdown>().colors = colorBlock;
		Screen.transform.Find("Directory").transform.Find("Placeholder").GetComponentInChildren<Text>().color = new Color((float)Settings.ButtonTextColor.Red / 255, (float)Settings.ButtonTextColor.Green / 255, (float)Settings.ButtonTextColor.Blue / 255, (float)Settings.ButtonTextColor.Alpha / 255);
		Screen.transform.Find("Directory").transform.Find("Text").GetComponentInChildren<Text>().color = new Color((float)Settings.ButtonTextColor.Red / 255, (float)Settings.ButtonTextColor.Green / 255, (float)Settings.ButtonTextColor.Blue / 255, (float)Settings.ButtonTextColor.Alpha / 255);
	}

	public void Open()
	{
		string ImageLocation = FilesDataClass.FilePath + "/" + Directory.itemText + "/" + FileBox.text;
		if (File.Exists(ImageLocation))
		{
			byte[] bytes = File.ReadAllBytes(ImageLocation);
			Manager.GetComponent<ErrorManager>().Log("Image file " + FileBox.text + " has been loaded");
		}
		else
		{
			Manager.GetComponent<ErrorManager>().Warning("Image file " + FileBox.text + " does not exist");
		}
	}

	public void Minimize()
	{
		if (!isMinimized)
		{
			isMinimized = true;
			Screen.SetActive(false);
			ImageRT.sizeDelta = new Vector2(ImageRT.sizeDelta.x, 55);
			ImageProgram.transform.position = new Vector2(ImageProgram.transform.position.x, ImageProgram.transform.position.y + (222.5f * ResolutionManager.ScreenOffsetH));
		}
	}

	public void Maximize()
	{
		if (isMinimized)
		{
			isMinimized = false;
			Screen.SetActive(true);
			ImageRT.sizeDelta = new Vector2(ImageRT.sizeDelta.x, 500);
			ImageProgram.transform.position = new Vector2(ImageProgram.transform.position.x, ImageProgram.transform.position.y - (222.5f * ResolutionManager.ScreenOffsetH));
		}
	}

	public void Close()
	{
		GameObject.Destroy(ImageProgram);
	}
}
