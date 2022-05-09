using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using System;

public class Scout : MonoBehaviour
{
	public GameObject Manager;
	public GameObject Legacy;
	public GameObject ScoutWindow;
	public GameObject DeleteButton;
	public GameObject DeleteScreen;
	public GameObject OverwriteScreen;
	public InputField MatchNumber;
	public InputField TeamNumber;

	public GameObject Question;
	public Transform Questions;
	public Transform SpawnerQuestions;
	public GameObject Window;
	public Transform Content;
	public Scrollbar Scrollbar;
	SettingsClass Settings;
	int QuestionsCount;
	int[] QuestionsType;
	string[] QuestionsData;
	string[] AnswersData;

	public string Match;
	public string Team;
	public string MatchCache;
	public string TeamCache;
	public string CreatedWithCache;
	public string ScouterCache;
	public bool NewFileCheck;
	public bool OpenWithCheck = false;
	public string OpenWithDirectory = "";
	public bool isDone;
	float OffsetX;
	float OffsetY;

	void Start()
	{
		Load();
	}

	public void ReloadUI()
	{
		Load();
	}

	void Load()
	{
		isDone = false;
		if (!OpenWithCheck)
		{
			MatchNumber.text = "0";
			TeamNumber.text = "0000";
			Match = "";
			Team = "";
			NewFileCheck = true;
			DeleteButton.SetActive(false);
			SettingsClass Settings = Manager.GetComponent<FileManager>().Settings;
			QuestionsCount = Settings.QuestionAmmount;
			QuestionsType = Settings.QuestionType;
			QuestionsData = Settings.Questions;
			AnswersData = null;
			StartCoroutine(LoadQuestions(false));
		}
		else
		{
			string[] Parts = OpenWithDirectory.Split('\\');
			string[] Numbers = Parts[Parts.Length - 1].Split('-');
			MatchNumber.text = Numbers[0];
			TeamNumber.text = Numbers[1].Split('.')[0];
			NewFileCheck = false;
			DeleteButton.SetActive(true);
			OpenFileWith();
			OpenWithCheck = false;
		}
		OffsetX = ResolutionManager.ScreenOffsetW;
		OffsetY = ResolutionManager.ScreenOffsetH;
		isDone = true;
	}

	IEnumerator LoadQuestions(bool Open)
	{
		while (!isDone)
		{
			yield return null;
		}
		foreach (Transform child in Questions.transform)
		{
			GameObject.Destroy(child.gameObject);
		}
		int Outsidei = 0;
		Window.GetComponent<ScrollRect>().vertical = false;
		Scrollbar.enabled = false;
		for (int i = 1; i < QuestionsCount + 1; i++)
		{
			Outsidei = i;
			GameObject childObject = Instantiate(Question) as GameObject;
			childObject.name = "Question " + i;
			childObject.transform.SetParent(Questions.transform);
			childObject.GetComponent<ScoutType>().isDone = false;
			while (!childObject.GetComponent<ScoutType>().isDone && Open)
			{
				yield return null;
			}
			if (i <= QuestionsType.Length)
			{
				childObject.GetComponent<ScoutType>().SetData(QuestionsType[i - 1], QuestionsData[i - 1]);
			}
			else
			{
				childObject.GetComponent<ScoutType>().SetData(0, "");
			}
			if (Open)
			{
				if (i <= QuestionsType.Length)
				{
					childObject.GetComponent<ScoutType>().SetValue(AnswersData[i - 1]);
				}
				else
				{
					childObject.GetComponent<ScoutType>().SetValue("");
				}
			}
		}
		if (QuestionsCount > 10)
		{
			Window.GetComponent<ScrollRect>().vertical = true;
			Scrollbar.enabled = true;
		}
		yield return new WaitForSecondsRealtime(0.1f);
		Content.GetComponent<RectTransform>().sizeDelta = new Vector2(Content.GetComponent<RectTransform>().sizeDelta.x, 10 * OffsetY + (85 * (Outsidei)));
		Content.position = new Vector2(970 * OffsetX, (970 - (42.5f * (Outsidei))) * OffsetY);
	}

	public void OpenFile()
	{
		if (MatchNumber.text != "")
		{
			if (TeamNumber.text != "")
			{
				if (File.Exists(FilesDataClass.FilePathSaves + "/" + MatchNumber.text + "-" + TeamNumber.text + ".rs"))
				{
					Open();
				}
				else
				{
					Manager.GetComponent<ErrorManager>().Warning("Scouting file " + MatchNumber.text + "-" + TeamNumber.text + ".rs does not exist");
				}
			}
			else
			{
				Manager.GetComponent<ErrorManager>().Warning("Team ID blank, Unable to load");
			}
		}
		else
		{
			Manager.GetComponent<ErrorManager>().Warning("Match ID blank, Unable to load");
		}
	}

	public void OpenFileWith()
	{
		if (File.Exists(OpenWithDirectory))
		{
			OpenWith();
		}
		else
		{
			Manager.GetComponent<ErrorManager>().Warning("Scouting file " + MatchNumber.text + "-" + TeamNumber.text + ".rs does not exist");
		}
	}

	public void SaveFile()
	{
		if (MatchNumber.text != "")
		{
			if (TeamNumber.text != "")
			{
				if (File.Exists(FilesDataClass.FilePathSaves + "/" + MatchNumber.text + "-" + TeamNumber.text + ".rs"))
				{
					if (!NewFileCheck)
					{
						if (Match == MatchNumber.text && Team == TeamNumber.text)
						{
							Save(MatchNumber.text, TeamNumber.text);
						}
						else
						{
							OverwriteProtection();
						}
					}
					else
					{
						OverwriteProtection();
					}
				}
				else
				{
					Save(MatchNumber.text, TeamNumber.text);
				}
			}
			else
			{
				Manager.GetComponent<ErrorManager>().Warning("Team ID blank, Unable to save");
			}
		}
		else
		{
			Manager.GetComponent<ErrorManager>().Warning("Match ID blank, Unable to save");
		}
	}

	public void NewFile()
	{
		Load();
	}

	void Open()
	{
		string Directory = FilesDataClass.FilePathSaves + "/" + MatchNumber.text + "-" + TeamNumber.text + ".rs";
		string check = FileTest.FileCheck(Directory);
		string[] ScoutFiles;
		switch (check)
		{
			case "Future":
				QuestionsCount = 0;
				QuestionsType = null;
				QuestionsData = null;
				AnswersData = null;
				StartCoroutine(LoadQuestions(true));
				SetFile();
				Manager.GetComponent<ErrorManager>().Warning(MatchNumber.text + "-" + TeamNumber.text + ".rs is from a future version and is unreadable in " + Application.version);
				break;
			case "RS2":
				byte[] bytes = File.ReadAllBytes(Directory);
				string JSON = Encoding.ASCII.GetString(bytes);
				FilesClass Files;
				Files = JsonUtility.FromJson<FilesClass>(JSON);
				CreatedWithCache = Files.CreatedWith;
				ScouterCache = Files.Scouter;
				QuestionsCount = Files.QuestionsInt;
				QuestionsType = Files.QuestionsType;
				QuestionsData = Files.Questions;
				AnswersData = Files.Answers;
				StartCoroutine(LoadQuestions(true));
				SetFile();
				Manager.GetComponent<ErrorManager>().Log("Scouting file " + MatchNumber.text + "-" + TeamNumber.text + ".rs loaded");
				break;
			case "RS1.5":
				/*ScoutFiles = RS1Files.LoadRS1Files(Directory);
				CreatedWithCache = "RS1.5";
				ScouterCache = "Unknown";
				QuestionsCount = 11;
				foreach (string Part in ScoutFiles)
				{
					Debug.Log(Part);
				}
				string[] Type = new string[7] {ScoutFiles[1][0].ToString(), ScoutFiles[1][1].ToString(), ScoutFiles[1][2].ToString(), ScoutFiles[1][3].ToString(), ScoutFiles[1][4].ToString(), ScoutFiles[1][5].ToString(), ScoutFiles[1][6].ToString(), };
				QuestionsType = new int[11] { 0, int.Parse(Type[0]), int.Parse(Type[1]), 0, int.Parse(Type[2]), int.Parse(Type[3]), int.Parse(Type[4]), 0, int.Parse(Type[5]), int.Parse(Type[6]), 1 };
				QuestionsData = new string[11] { "Unknown Title", "Unknown Question", "Unknown Question", "Unknown Title", "Unknown Question", "Unknown Question", "Unknown Question", "Unknown Title", "Unknown Question", "Unknown Question", "Comment" };
				AnswersData = new string[11] { "", ScoutFiles[0], ScoutFiles[1], "", ScoutFiles[2], ScoutFiles[3], ScoutFiles[4], "", ScoutFiles[5], ScoutFiles[6], ScoutFiles[7] };
				StartCoroutine(LoadQuestions(true));
				SetFile();
				Manager.GetComponent<ErrorManager>().Log(MatchNumber.text + "-" + TeamNumber.text + ".rs has been loaded in RS1.5 compatibility mode");*/
				QuestionsCount = 0;
				QuestionsType = null;
				QuestionsData = null;
				AnswersData = null;
				StartCoroutine(LoadQuestions(true));
				SetFile();
				Manager.GetComponent<ErrorManager>().Warning(MatchNumber.text + "-" + TeamNumber.text + ".rs is a RS1.5 scouting file and is incompatible with RS2");
				break;
			case "RS1":
				ScoutFiles = RS1Files.LoadRS1Files(Directory);
				CreatedWithCache = "RS1";
				ScouterCache = "Unknown";
				QuestionsCount = 11;
				QuestionsType = new int[11] {0, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1};
				QuestionsData = new string[11] { "Unknown Title", "Unknown Question", "Unknown Question", "Unknown Title", "Unknown Question", "Unknown Question", "Unknown Question", "Unknown Title", "Unknown Question", "Unknown Question", "Comment" };
				AnswersData = new string[11] { "", ScoutFiles[0], ScoutFiles[1], "", ScoutFiles[2], ScoutFiles[3], ScoutFiles[4], "", ScoutFiles[5], ScoutFiles[6], ScoutFiles[7] };
				StartCoroutine(LoadQuestions(true));
				SetFile();
				Manager.GetComponent<ErrorManager>().Log(MatchNumber.text + "-" + TeamNumber.text + ".rs has been loaded in RS1 compatibility mode");
				break;
			case "Error":
				QuestionsCount = 0;
				QuestionsType = null;
				QuestionsData = null;
				AnswersData = null;
				StartCoroutine(LoadQuestions(true));
				SetFile();
				Manager.GetComponent<ErrorManager>().Error(MatchNumber.text + "-" + TeamNumber.text + ".rs is unreadable");
				break;
		}
	}

	void OpenWith()
	{
		string check = FileTest.FileCheck(OpenWithDirectory);
		switch (check)
		{
			case "RS2":
				byte[] bytes = File.ReadAllBytes(OpenWithDirectory);
				string JSON = Encoding.ASCII.GetString(bytes);
				FilesClass Files;
				Files = JsonUtility.FromJson<FilesClass>(JSON);
				CreatedWithCache = Files.CreatedWith;
				ScouterCache = Files.Scouter;
				QuestionsCount = Files.QuestionsInt;
				QuestionsType = Files.QuestionsType;
				QuestionsData = Files.Questions;
				AnswersData = Files.Answers;
				StartCoroutine(LoadQuestions(true));
				Match = MatchNumber.text;
				Team = TeamNumber.text;
				DeleteButton.SetActive(true);
				NewFileCheck = true;
				Manager.GetComponent<ErrorManager>().Log("Scouting file " + MatchNumber.text + "-" + TeamNumber.text + ".rs loaded");
				break;
			case "RS1.5":
				Manager.GetComponent<ErrorManager>().Error(MatchNumber.text + "-" + TeamNumber.text + ".rs is a RS1.5 scouting file and is unsupported in RS2");
				break;
			case "RS1":
				string[] ScoutFiles = RS1Files.LoadRS1Files(OpenWithDirectory);
				Manager.GetComponent<ErrorManager>().Log(MatchNumber.text + "-" + TeamNumber.text + ".rs has been loaded in RS1 compatibility mode");
				break;
			case "Error":
				Manager.GetComponent<ErrorManager>().Error(MatchNumber.text + "-" + TeamNumber.text + ".rs is unreadable");
				break;
		}
	}

	void Save(string SaveMatch, string SaveTeam)
	{
		FilesClass Files;
		int[] Type = new int[QuestionsCount];
		string[] Question = new string[QuestionsCount];
		string[] Answer = new string[QuestionsCount];
		for (int i = 0; i < QuestionsCount; i++)
		{
			if (i <= QuestionsCount)
			{
				Type[i] = Questions.Find("Question " + (i + 1).ToString()).GetComponent<ScoutType>().TypeInt;
			}
			else
			{
				Type[i] = 0;
			}
		}
		for (int i = 0; i < QuestionsCount; i++)
		{
			if (i <= QuestionsCount)
			{
				Question[i] = Questions.Find("Question " + (i + 1).ToString()).GetComponent<ScoutType>().GetData();
			}
			else
			{
				Question[i] = "";
			}
		}
		for (int i = 0; i < QuestionsCount; i++)
		{
			if (i <= QuestionsCount)
			{
				Answer[i] = Questions.Find("Question " + (i + 1).ToString()).GetComponent<ScoutType>().GetValue();
			}
			else
			{
				Answer[i] = "";
			}
		}
		if (NewFileCheck)
		{
			Settings = Manager.GetComponent<FileManager>().Settings;
			Files = new FilesClass
			{
				SaveVersion = "v1",
				CreatedWith = Application.version,
				LastOpenedWith = Application.version,
				Scouter = Settings.ScouterName,
				QuestionsInt = QuestionsCount,
				QuestionsType = Type,
				Questions = Question,
				Answers = Answer,
				Comment = ""
			};
		}
		else
		{
			Files = new FilesClass
			{
				SaveVersion = "v1",
				CreatedWith = CreatedWithCache,
				LastOpenedWith = Application.version,
				Scouter = ScouterCache,
				QuestionsInt = QuestionsCount,
				QuestionsType = Type,
				Questions = Question,
				Answers = Answer,
				Comment = ""
			};
		}
		string JSON = JsonUtility.ToJson(Files, true);
		byte[] bytes = Encoding.ASCII.GetBytes(JSON);
		File.WriteAllBytes(FilesDataClass.FilePathSaves + "/" + SaveMatch + "-" + SaveTeam + ".rs", bytes);
		SetFile();
		Manager.GetComponent<ErrorManager>().Log("Scouting file saved as " + SaveMatch + "-" + SaveTeam + ".rs");
	}

	void SetFile()
	{
		Match = MatchNumber.text;
		Team = TeamNumber.text;
		DeleteButton.SetActive(true);
		NewFileCheck = false;
	}

	void OverwriteProtection()
	{
		MatchCache = MatchNumber.text;
		TeamCache = TeamNumber.text;
		OverwriteScreen.transform.Find("Text").GetComponent<Text>().text = "You're about to overwrite " + MatchCache + "-" + TeamCache + ".rs\n\nAre you sure you would like to procede?";
		OverwriteScreen.SetActive(true);
	}

	public void DeleteFile()
	{
		MatchCache = Match;
		TeamCache = Team;
		DeleteScreen.transform.Find("Text").GetComponent<Text>().text = "You're about to delete "+ MatchCache + "-" + TeamCache + ".rs\n\nAre you sure you would like to procede?";
		DeleteScreen.SetActive(true);
	}

	public void CancelDelete()
	{
		DeleteScreen.SetActive(false);
	}

	public void ConfirmDelete()
	{
		if (File.Exists(FilesDataClass.FilePathSaves + "/" + MatchCache + "-" + TeamCache + ".rs"))
		{
			File.Delete(FilesDataClass.FilePathSaves + "/" + MatchCache + "-" + TeamCache + ".rs");
			DeleteScreen.SetActive(false);
			NewFile();
		}
		else
		{
			Manager.GetComponent<ErrorManager>().Warning(MatchCache + "-" + TeamCache + ".rs does not exist, Unable to delete file");
			DeleteScreen.SetActive(false);
			NewFile();
		}
	}

	public void CancelOverwrite()
	{
		OverwriteScreen.SetActive(false);
	}

	public void ConfirmOverwrite()
	{
		Save(MatchCache, TeamCache);
		OverwriteScreen.SetActive(false);
	}
}
