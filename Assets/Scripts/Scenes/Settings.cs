using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.IO;
using System.Text;

public class Settings : MonoBehaviour
{
	public GameObject Manager;
	public GameObject MainManager;
	public GameObject SettingsWindow;
	public AudioMixer AudioMixer;
	bool isDone;
	bool FullscreenBool;
	Resolution[] Resolutions;
	int ResolutionInt;
	int QualityInt;
	float VolumeFloat;
	public InputField TeamNumber;
	public InputField ScouterName;
	public Dropdown LangDropdown;
	public string CurrentLanguage;
	public Toggle isBetaEnabled;
	public Toggle isDataEnabled;
	public Toggle FSToggle;
	public Dropdown QualityDropdown;
	public Dropdown ResDropdown;
	public Slider VolumeSlider;
	public Text VolumeText;
	public Toggle is24Hour;
	public Toggle isTooltipsEnabled;
	public Toggle isDiscordEnabled;
	public Slider QuestionsSlider;
	int QuestionsCount;
	int[] QuestionsType;
	string[] QuestionsData;
	public Text QuestionsText;
	public GameObject TitleColor;
	public Toggle isParalaxEnabled;
	public GameObject BackColor;
	public GameObject TextColor;
	public GameObject ButtonColor;
	public GameObject ButtonTextColor;
	public GameObject ButtonHighlightColor;
	public GameObject PanelColor;
	public GameObject PanelTextColor;
	public GameObject FilesWarning;

	public GameObject Question;
	public Transform Questions;
	public Transform SpawnerQuestions;
	public GameObject Window;
	public Transform Content;
	public Scrollbar Scrollbar;
	float OffsetX;
	float OffsetY;

	public void Wake()
	{
		Load();
	}

	void Load()
	{
		isDone = false;
		SettingsClass Settings = Manager.GetComponent<FileManager>().Settings;
		TeamNumber.text = Settings.TeamNumber.ToString();
		ScouterName.text = Settings.ScouterName;
		CurrentLanguage = Settings.Language;
		isBetaEnabled.isOn = Settings.isBetaEnabled;
		isDataEnabled.isOn = true;
		VolumeSlider.value = Settings.Volume;
		VolumeText.text = ((Settings.Volume + 80) * 1.25f).ToString().Split('.')[0] + "%";
		AudioMixer.SetFloat("Volume", Settings.Volume);
		is24Hour.isOn = Settings.is24Hour;
		isTooltipsEnabled.isOn = Settings.isTooltipsEnabled;
		isDiscordEnabled.isOn = false;
		QuestionsSlider.value = Settings.QuestionAmmount;
		QuestionsText.text = Settings.QuestionAmmount.ToString();
		QuestionsType = Settings.QuestionType;
		QuestionsData = Settings.Questions;
		TitleColor.GetComponent<ColorPicker>().SetColors(Settings.TitleColor);
		isParalaxEnabled.isOn = true;
		BackColor.GetComponent<ColorPicker>().SetColors(Settings.BackColor);
		TextColor.GetComponent<ColorPicker>().SetColors(Settings.TextColor);
		ButtonColor.GetComponent<ColorPicker>().SetColors(Settings.ButtonColor);
		ButtonTextColor.GetComponent<ColorPicker>().SetColors(Settings.ButtonTextColor);
		ButtonHighlightColor.GetComponent<ColorPicker>().SetColors(Settings.ButtonHighlightColor);
		PanelColor.GetComponent<ColorPicker>().SetColors(Settings.PanelColor);
		PanelTextColor.GetComponent<ColorPicker>().SetColors(Settings.PanelTextColor);
		FilesWarning.SetActive(false);
		StopCoroutine(SettingsStart());
		StartCoroutine(SettingsStart());
	}

	IEnumerator SettingsStart()
	{
		while (!MainManager.GetComponent<Main>().isDone)
		{
			yield return null;
		}
		while (!SettingsWindow.activeSelf)
		{
			yield return null;
		}
		OffsetX = ResolutionManager.ScreenOffsetW;
		OffsetY = ResolutionManager.ScreenOffsetH;
		LangDropdown.ClearOptions();
		List<string> Languages = new List<string>();
		string matching = "*.lang";
		int Langi = 0;
		int CurrentLanguageIndex = 0;
		foreach (string file in Directory.GetFiles(FilesDataClass.FilePathLanguage, matching))
		{
			Langi++;
			byte[] bytes = File.ReadAllBytes(file);
			string JSON = Encoding.ASCII.GetString(bytes);
			LanguageClass Language;
			Language = JsonUtility.FromJson<LanguageClass>(JSON);
			if (Language.Version == "v1")
			{
				if (Language.LanguageName != null || Language.LanguageName != "")
				{
					Languages.Add(Language.LanguageName);
					if (Language.LanguageName == CurrentLanguage)
					{
						CurrentLanguageIndex = Langi;
					}
				}
			}
			if (Languages.Count == 0)
			{
				//Regenerate language files
			}
			LangDropdown.AddOptions(Languages);
			LangDropdown.value = Langi;
			LangDropdown.RefreshShownValue();
		}

		FSToggle.isOn = Screen.fullScreen;
		QualityDropdown.value = QualitySettings.GetQualityLevel();
		Resolutions = Screen.resolutions;
		ResDropdown.ClearOptions();
		List<string> options = new List<string>();
		int CurrentResolutionIndex = 0;
		for (int i = 0; i < Resolutions.Length; i++)
		{
			string option = Resolutions[i].width + "x" + Resolutions[i].height;// + "@" + Resolutions[i].refreshRate + "Hz";
			options.Add(option);
			if (Resolutions[i].width == Screen.currentResolution.width && Resolutions[i].height == Screen.currentResolution.height && Resolutions[i].refreshRate == 60)// && Resolutions[i].refreshRate == Screen.currentResolution.refreshRate)
			{
				CurrentResolutionIndex = i;
			}
		}
		ResDropdown.AddOptions(options);
		ResDropdown.value = CurrentResolutionIndex;
		ResDropdown.RefreshShownValue();
		isDone = true;
	}

	public void Cancel()
	{
		Load();
	}

	public void Apply()
	{
		Resolution Resolution = Resolutions[ResolutionInt];
		Screen.SetResolution(Resolution.width, Resolution.height, FullscreenBool, Resolution.refreshRate);
		QualitySettings.SetQualityLevel(QualityInt);
		AudioMixer.SetFloat("Volume", VolumeFloat);
		int[] QuestionType = new int[QuestionsCount];
		string[] QuestionData = new string[QuestionsCount];
		int[] QuestionValue = new int[QuestionsCount];
		for (int i = 0; i < QuestionsCount; i++)
		{
			if (i <= QuestionsCount)
			{
				QuestionType[i] = Questions.Find("Question " + (i + 1).ToString()).GetComponent<QuestionType>().TypeInt;
			}
			else
			{
				QuestionType[i] = 0;
			}
		}
		for (int i = 0; i < QuestionsCount; i++)
		{
			if (i <= QuestionsCount)
			{
				QuestionData[i] = Questions.Find("Question " + (i + 1).ToString()).GetComponent<QuestionType>().GetData()[0];
			}
			else
			{
				QuestionData[i] = "";
			}
		}
		SettingsClass Settings = new SettingsClass
		{
			SaveVersion = "v1",
			Version = Application.version,
			TeamNumber = int.Parse(TeamNumber.text),
			ScouterName = ScouterName.text,
			Language = "English",
			isBetaEnabled = isBetaEnabled.isOn,
			Volume = VolumeFloat,
			is24Hour = is24Hour.isOn,
			isTooltipsEnabled = isTooltipsEnabled.isOn,
			QuestionAmmount = QuestionsCount,
			QuestionType = QuestionType,
			Questions = QuestionData,
			QuestionValue = QuestionValue,
			UIStyle = "Standard",
			TitleColor = TitleColor.GetComponent<ColorPicker>().GetColors(),
			BackColor = BackColor.GetComponent<ColorPicker>().GetColors(),
			TextColor = TextColor.GetComponent<ColorPicker>().GetColors(),
			ButtonColor = ButtonColor.GetComponent<ColorPicker>().GetColors(),
			ButtonTextColor = ButtonTextColor.GetComponent<ColorPicker>().GetColors(),
			ButtonHighlightColor = ButtonHighlightColor.GetComponent<ColorPicker>().GetColors(),
			PanelColor = PanelColor.GetComponent<ColorPicker>().GetColors(),
			PanelTextColor = PanelTextColor.GetComponent<ColorPicker>().GetColors()
		};
		Manager.GetComponent<FileManager>().SetSettings(Settings);
		Manager.GetComponent<FileManager>().Save();
		ResolutionManager.UpdateResolution();
		UIReloadManager.Reload();
		Load();
	}

	public void SetFullscreen(bool isFullscreen)
	{
		FullscreenBool = isFullscreen;
	}

	public void SerResolution(int ResolutionIndex)
	{
		ResolutionInt = ResolutionIndex;
	}

	public void SetQuality(int QualityIndex)
	{
		QualityInt = QualityIndex;
	}

	public void SetVolume(float VolumeIndex)
	{
		VolumeFloat = VolumeIndex;
		VolumeText.text = ((VolumeIndex + 80) * 1.25f).ToString().Split('.')[0] + "%";
	}

	public void SetQuestions(float QuestionsIndex)
	{
		QuestionsCount = (int)QuestionsIndex;
		QuestionsText.text = QuestionsIndex.ToString();
		StopCoroutine(UpdateQuestions());
		StartCoroutine(UpdateQuestions());
	}

	IEnumerator UpdateQuestions()
	{
		while(!isDone || QuestionsSlider.GetComponent<SliderEnd>().isSliding)
		{
			yield return null;
		}
		foreach (Transform child in Questions.transform)
		{
			GameObject.Destroy(child.gameObject);
		}
		int Outsidei = 0;
		if (QuestionsType.Length != QuestionsData.Length)
		{
			Manager.GetComponent<ErrorManager>().Error("Unaligned question ammount, Attempt to load questions stopped");
			yield break;
		}
		Window.GetComponent<ScrollRect>().vertical = false;
		Scrollbar.enabled = false;
		for (int i = 1; i < QuestionsCount + 1; i++)
		{
			Outsidei = i;
			GameObject childObject = Instantiate(Question) as GameObject;
			childObject.name = "Question " + i;
			childObject.transform.SetParent(Questions.transform);
			if (i <= QuestionsType.Length && i <= QuestionsData.Length)
			{
				childObject.GetComponent<QuestionType>().SetData(QuestionsType[i - 1], QuestionsData[i - 1]);
			}
			else
			{
				childObject.GetComponent<QuestionType>().SetData(0, "");
			}
		}
		if (QuestionsCount > 9)
		{
			Window.GetComponent<ScrollRect>().vertical = true;
			Scrollbar.enabled = true;
		}
		yield return new WaitForSecondsRealtime(0.1f);
		Content.GetComponent<RectTransform>().sizeDelta = new Vector2(Content.GetComponent<RectTransform>().sizeDelta.x, 10 * OffsetY + (85 * (Outsidei + 1)));
		Content.position = new Vector2(970 * OffsetX, (970 - (42.5f * (Outsidei + 1))) * OffsetY);
	}

	public void LoadDefaultsFunction()
	{
		Manager.GetComponent<FileManager>().ResetSettings();
		Load();
	} 


	public void ClearCache()
	{
		Directory.Delete(FilesDataClass.FilePathCache, true);
		Directory.CreateDirectory(FilesDataClass.FilePathCache);
		Directory.CreateDirectory(FilesDataClass.FilePathCacheImages);
		Directory.CreateDirectory(FilesDataClass.FilePathCacheJson);
	}

	public void ClearFiles()
	{
		FilesWarning.SetActive(true);
	}

	public void ClearFilesCancel()
	{
		FilesWarning.SetActive(false);
	}

	public void ClearFilesActually()
	{
		Directory.Delete(FilesDataClass.FilePathSaves, true);
		Directory.CreateDirectory(FilesDataClass.FilePathSaves);
		FilesWarning.SetActive(false);
	}

	public void ShowDirectory()
	{
		if (!Directory.Exists(FilesDataClass.FilePath))
		{
			Directory.CreateDirectory(FilesDataClass.FilePath);
		}
		OpenInFileBrowser.Open(FilesDataClass.FilePath);
	}
}
