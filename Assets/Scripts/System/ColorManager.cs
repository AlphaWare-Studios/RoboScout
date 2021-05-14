using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ColorManager : MonoBehaviour
{
	public GameObject Manager;
	public Text Title;
	public RawImage[] Backgrounds = new RawImage[0];
	public Text[] Texts = new Text[0];
	public Button[] Buttons = new Button[0];
	public InputField[] Fields = new InputField[0];
	public Toggle[] Toggles = new Toggle[0];
	public Dropdown[] Dropdowns = new Dropdown[0];
	public Image[] Images = new Image[0];
	public Text[] ButtonTexts = new Text[0];
	public Image[] Panels = new Image[0];
	public Text[] PanelTexts = new Text[0];

	public void Wake()
	{
		ReloadUI();
	}

	public void ReloadUI()
	{
		SettingsClass Settings = Manager.GetComponent<FileManager>().Settings;
		ColorBlock colorBlock = new ColorBlock();
		Title.color = new Color((float)Settings.TitleColor.Red / 255, (float)Settings.TitleColor.Green / 255, (float)Settings.TitleColor.Blue / 255, (float)Settings.TitleColor.Alpha / 255);
		colorBlock.normalColor = new Color((float)Settings.ButtonColor.Red / 255, (float)Settings.ButtonColor.Green / 255, (float)Settings.ButtonColor.Blue / 255, (float)Settings.ButtonColor.Alpha / 255);
		colorBlock.pressedColor = new Color(0, 0, 0);
		colorBlock.highlightedColor = new Color((float)Settings.ButtonHighlightColor.Red / 255, (float)Settings.ButtonHighlightColor.Green / 255, (float)Settings.ButtonHighlightColor.Blue / 255, (float)Settings.ButtonHighlightColor.Alpha / 255);
		colorBlock.selectedColor = new Color((float)Settings.ButtonHighlightColor.Red / 255, (float)Settings.ButtonHighlightColor.Green / 255, (float)Settings.ButtonHighlightColor.Blue / 255, (float)Settings.ButtonHighlightColor.Alpha / 255);
		colorBlock.disabledColor = new Color(1, 0, 0);
		colorBlock.colorMultiplier = 1;
		colorBlock.fadeDuration = 0.1f;
		bool hasBackground = false;
		Texture2D tex = new Texture2D(0, 0);
		if (File.Exists(FilesDataClass.FilePath + "/Background.png"))
		{
			byte[] backbytes = File.ReadAllBytes(FilesDataClass.FilePath + "/Background.png");
			tex = new Texture2D(Screen.width, Screen.height);
			tex.filterMode = FilterMode.Point;
			tex.LoadImage(backbytes);
			hasBackground = true;
		}
		foreach (RawImage Background in Backgrounds)
		{
			if (Background != null)
			{
				if (hasBackground)
				{
					Background.GetComponent<RawImage>().texture = tex;
				}
				Background.color = new Color((float)Settings.BackColor.Red / 255, (float)Settings.BackColor.Green / 255, (float)Settings.BackColor.Blue / 255, (float)Settings.BackColor.Alpha / 255);
			}
		}
		foreach (Text Text in Texts)
		{
			if (Text != null)
				Text.color = new Color((float)Settings.TextColor.Red / 255, (float)Settings.TextColor.Green / 255, (float)Settings.TextColor.Blue / 255, (float)Settings.TextColor.Alpha / 255);
		}
		foreach (Button Button in Buttons)
		{
			if (Button != null)
				Button.colors = colorBlock;
		}
		foreach (InputField Field in Fields)
		{
			if (Field != null)
				Field.colors = colorBlock;
		}
		foreach (Toggle Toggle in Toggles)
		{
			if (Toggle != null)
				Toggle.colors = colorBlock;
		}
		foreach (Dropdown Dropdown in Dropdowns)
		{
			if (Dropdown != null)
				Dropdown.colors = colorBlock;
		}
		foreach (Image Image in Images)
		{
			if (Image != null)
				Image.color = new Color((float)Settings.ButtonColor.Red / 255, (float)Settings.ButtonColor.Green / 255, (float)Settings.ButtonColor.Blue / 255, (float)Settings.ButtonColor.Alpha / 255);
		}
		foreach (Text Text in ButtonTexts)
		{
			if (Text != null)
				Text.color = new Color((float)Settings.ButtonTextColor.Red / 255, (float)Settings.ButtonTextColor.Green / 255, (float)Settings.ButtonTextColor.Blue / 255, (float)Settings.ButtonTextColor.Alpha / 255);
		}
		foreach (Image Image in Panels)
		{
			if (Image != null)
				Image.color = new Color((float)Settings.PanelColor.Red / 255, (float)Settings.PanelColor.Green / 255, (float)Settings.PanelColor.Blue / 255, (float)Settings.PanelColor.Alpha / 255);
		}
		foreach (Text Text in PanelTexts)
		{
			if (Text != null)
				Text.color = new Color((float)Settings.PanelTextColor.Red / 255, (float)Settings.PanelTextColor.Green / 255, (float)Settings.PanelTextColor.Blue / 255, (float)Settings.PanelTextColor.Alpha / 255);
		}
	}
}
