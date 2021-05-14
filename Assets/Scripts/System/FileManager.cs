using UnityEngine;
using System.IO;
using System.Text;

public class FileManager : MonoBehaviour
{
	public SettingsClass Settings;

	public void Wake()
	{
		Open();
	}

	public void Open()
	{
		if (File.Exists(FilesDataClass.FilePath + "/Settings.rss"))
		{
			byte[] bytes = File.ReadAllBytes(FilesDataClass.FilePath + "/Settings.rss");
			string JSON = Encoding.ASCII.GetString(bytes);
			Settings = JsonUtility.FromJson<SettingsClass>(JSON);
		}
		else
        {
			Settings = new SettingsClass
			{
				SaveVersion = "v1",
				Version = Application.version,
				TeamNumber = 0,
				ScouterName = "",
				Language = "English",
				isBetaEnabled = false,
				Volume = 0,
				is24Hour = false,
				isTooltipsEnabled = true,
				QuestionAmmount = 5,
				QuestionType = new int[5] {1, 1, 1, 1, 1},
				Questions = new string[5] { "", "", "", "", ""},
				QuestionValue = new int[5] { 1, 1, 1, 1, 1 },
				UIStyle = "Standard",
				TitleColor = new SettingsInterfaceColors {Red = 255, Green = 255, Blue = 255, Alpha = 255},
				BackColor = new SettingsInterfaceColors {Red = 100, Green = 100, Blue = 100, Alpha = 255},
				TextColor = new SettingsInterfaceColors { Red = 255, Green = 255, Blue = 255, Alpha = 255 },
				ButtonColor = new SettingsInterfaceColors { Red = 50, Green = 50, Blue = 50, Alpha = 255 },
				ButtonTextColor = new SettingsInterfaceColors { Red = 255, Green = 255, Blue = 255, Alpha = 255 },
				ButtonHighlightColor = new SettingsInterfaceColors { Red = 125, Green = 125, Blue = 125, Alpha = 255 },
				PanelColor = new SettingsInterfaceColors { Red = 70, Green = 70, Blue = 70, Alpha = 255 },
				PanelTextColor = new SettingsInterfaceColors { Red = 255, Green = 255, Blue = 255, Alpha = 255 },
			};
		}
	}
	
	public void Save()
	{
		SettingsClass NewSettings = new SettingsClass
		{
			SaveVersion = "v1",
			Version = Application.version,
			TeamNumber = Settings.TeamNumber,
			ScouterName = Settings.ScouterName,
			Language = "English",
			isBetaEnabled = Settings.isBetaEnabled,
			Volume = Settings.Volume,
			is24Hour = Settings.is24Hour,
			isTooltipsEnabled = Settings.isTooltipsEnabled,
			QuestionAmmount = Settings.QuestionAmmount,
			QuestionType = Settings.QuestionType,
			Questions = Settings.Questions,
			QuestionValue = Settings.QuestionValue,
			UIStyle = "Standard",
			TitleColor = Settings.TitleColor,
			BackColor = Settings.BackColor,
			TextColor = Settings.TextColor,
			ButtonColor = Settings.ButtonColor,
			ButtonTextColor = Settings.ButtonTextColor,
			ButtonHighlightColor = Settings.ButtonHighlightColor,
			PanelColor = Settings.PanelColor,
			PanelTextColor = Settings.PanelTextColor
		};
		string JSON = JsonUtility.ToJson(NewSettings, true);
		byte[] bytes = Encoding.ASCII.GetBytes(JSON);
		File.WriteAllBytes(FilesDataClass.FilePath + "/Settings.rss", bytes);
	}

	public void SetSettings(SettingsClass NewSettings)
	{
		Settings = NewSettings;
	}

	public void ResetSettings()
    {
		if (File.Exists(FilesDataClass.FilePath + "/Settings.rss")) {
			File.Delete(FilesDataClass.FilePath + "/Settings.rss");
		}
		Open();
    }
}
