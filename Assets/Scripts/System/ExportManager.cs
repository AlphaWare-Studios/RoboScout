using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text;
using System.Linq;
using SFB;

public class ExportManager : MonoBehaviour
{
	public GameObject Manager;
	public GameObject PackageManager;
	public bool inProgress;
	bool hasAnswered;
	bool WillOverwrite;
	public Text Title;
	public Text Percent;
	public Text Total;
	public Text DateInfo;
	public Text CurrentFile;
	public Text Errors;
	public GameObject IgnoreButton;
	public GameObject OverwriteButton;
	public GameObject ConfirmButton;
	public Toggle IgnoreQuestions;

	public void Wake()
	{
		PackageManager.SetActive(false);
		IgnoreQuestions.isOn = false;
	}

	public void Import(string Type)
	{
		switch (Type)
		{
			case "rse":
				string[] paths = StandaloneFileBrowser.OpenFilePanel("Import rse file", "", "rse", false);
				if (paths.Length > 0)
				{
					foreach (string path in paths)
					{
						StartCoroutine(ImportRSE(path));
					}
				}
				break;
			case "csv":
				break;
		}
	}

	public void Export(string Type)
	{
		switch (Type)
		{
			case "rse":
				string rsepath = StandaloneFileBrowser.SaveFilePanel("Export rse file", "", "Scouting Files", "rse");
				if (rsepath != null && rsepath != "")
				{
					StartCoroutine(ExportRSE(rsepath));
				}
				break;
			case "csv":
				string csvpath = StandaloneFileBrowser.SaveFilePanel("Export csv file", "", "Scouting Files", "csv");
				if (csvpath != null && csvpath != "")
				{
					StartCoroutine(ExportCSV(csvpath));
				}
				break;
		}
	}

	IEnumerator ImportRSE(string path)
	{
		byte[] bytesfile = File.ReadAllBytes(path);
		string JSONfile = Encoding.ASCII.GetString(bytesfile);
		FilesPackageClass Package;
		Package = JsonUtility.FromJson<FilesPackageClass>(JSONfile);
		if (Package.SaveVersion == "v1")
		{
			Debug.Log(Package.FilesCount);
			for (int i = 0; i < Package.FilesCount; i++)
			{
				if (!File.Exists(Package.FilesInfo[i] + ".rs"))
				{

				}
				else
				{

				}
			}
			while (!hasAnswered)
			{
				yield return null;
			}
		}
		else
		{
			Manager.GetComponent<ErrorManager>().Warning("Scouting package " + Path.GetFileName(path) + " is from a future version and cannot be opened");
		}
		yield return null;
	}

	IEnumerator ExportRSE(string path)
	{
		List<string> AllFiles = GetFiles();
		List<string> InfoFiles = new List<string>();
		List<FilesClass> CompFiles = new List<FilesClass>();
		int ErrorsCount = 0;
		IgnoreButton.SetActive(false);
		OverwriteButton.SetActive(false);
		ConfirmButton.SetActive(false);
		PackageManager.SetActive(true);
		Title.text = "Exporting " + Path.GetFileName(path);
		Total.text = "Scouting files to export: " + AllFiles.Count;
		DateInfo.text = "Created on " + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString() + ":v1";
		int Current = 1;
		foreach (string Directory in AllFiles)
		{
			Percent.text = (Current / AllFiles.Count) * 100 + "%";
			CurrentFile.text = "Exporting file " + Current + "/" + AllFiles.Count;
			string check = FileTest.FileCheck(Directory);
			switch (check)
			{
				case "Future":
					ErrorsCount++;
					break;
				case "RS2":
					byte[] bytesfile = File.ReadAllBytes(Directory);
					string JSONfile = Encoding.ASCII.GetString(bytesfile);
					FilesClass Files;
					Files = JsonUtility.FromJson<FilesClass>(JSONfile);
					InfoFiles.Add(Path.GetFileNameWithoutExtension(Directory));
					CompFiles.Add(Files);
					break;
				case "RS1.5":
					ErrorsCount++;
					break;
				case "RS1":
					string[] ScoutFiles = RS1Files.LoadRS1Files(Directory);
					FilesClass FilesRS1 = new FilesClass();
					FilesRS1.CreatedWith = "RS1";
					FilesRS1.LastOpenedWith = "Unknown";
					FilesRS1.Scouter = "Unknown";
					FilesRS1.QuestionsInt = 11;
					FilesRS1.QuestionsType = new int[11] { 0, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1 };
					FilesRS1.Questions = new string[11] { "Unknown Title", "Unknown Question", "Unknown Question", "Unknown Title", "Unknown Question", "Unknown Question", "Unknown Question", "Unknown Title", "Unknown Question", "Unknown Question", "Comment" };
					FilesRS1.Answers = new string[11] { "", ScoutFiles[0], ScoutFiles[1], "", ScoutFiles[2], ScoutFiles[3], ScoutFiles[4], "", ScoutFiles[5], ScoutFiles[6], ScoutFiles[7] };
					InfoFiles.Add(Path.GetFileNameWithoutExtension(Directory));
					CompFiles.Add(FilesRS1);
					break;
				case "Error":
					ErrorsCount++;
					break;
			}
			Current++;
			Errors.text = "Exported: " + CompFiles.Count + "   Errors: " + ErrorsCount.ToString();
		}
		FilesPackageClass Package = new FilesPackageClass
		{
			SaveVersion = "v1",
			Date = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString(),
			FilesCount = InfoFiles.Count,
			FilesInfo = InfoFiles.Select(i => i).ToArray(),
			Files = CompFiles.Select(i => i).ToArray()
		};
		string JSON = JsonUtility.ToJson(Package, false);
		byte[] bytes = Encoding.ASCII.GetBytes(JSON);
		File.WriteAllBytes(path, bytes);
		if (ErrorsCount == 0)
		{
			Manager.GetComponent<ErrorManager>().Log("Scouting package exported as " + Path.GetFileName(path));
		}
		else
		{
			Manager.GetComponent<ErrorManager>().Warning("Scouting package exported as " + Path.GetFileName(path) + " with " + ErrorsCount + " Errors");
		}
		ConfirmButton.SetActive(true);
		yield return null;
	}

	IEnumerator ExportCSV(string path)
	{
		List<string> AllFiles = GetFiles();
		List<string> InfoFiles = new List<string>();
		List<FilesClass> CompFiles = new List<FilesClass>();
		int ErrorsCount = 0;
		IgnoreButton.SetActive(false);
		OverwriteButton.SetActive(false);
		ConfirmButton.SetActive(false);
		PackageManager.SetActive(true);
		Title.text = "Exporting " + Path.GetFileName(path);
		Total.text = "Scouting files to export: " + AllFiles.Count;
		DateInfo.text = "Created on " + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString() + ":v1";
		int Current = 1;
		foreach (string Directory in AllFiles)
		{
			Percent.text = (Current / AllFiles.Count) * 100 + "%";
			CurrentFile.text = "Exporting file " + Current + "/" + AllFiles.Count;
			string check = FileTest.FileCheck(Directory);
			switch (check)
			{
				case "Future":
					ErrorsCount++;
					break;
				case "RS2":
					byte[] bytesfile = File.ReadAllBytes(Directory);
					string JSONfile = Encoding.ASCII.GetString(bytesfile);
					FilesClass Files;
					Files = JsonUtility.FromJson<FilesClass>(JSONfile);
					InfoFiles.Add(Path.GetFileNameWithoutExtension(Directory));
					CompFiles.Add(Files);
					break;
				case "RS1.5":
					ErrorsCount++;
					break;
				case "RS1":
					string[] ScoutFiles = RS1Files.LoadRS1Files(Directory);
					FilesClass FilesRS1 = new FilesClass();
					FilesRS1.CreatedWith = "RS1";
					FilesRS1.LastOpenedWith = "Unknown";
					FilesRS1.Scouter = "Unknown";
					FilesRS1.QuestionsInt = 11;
					FilesRS1.QuestionsType = new int[11] { 0, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1 };
					FilesRS1.Questions = new string[11] { "Unknown Title", "Unknown Question", "Unknown Question", "Unknown Title", "Unknown Question", "Unknown Question", "Unknown Question", "Unknown Title", "Unknown Question", "Unknown Question", "Comment" };
					FilesRS1.Answers = new string[11] { "", ScoutFiles[0], ScoutFiles[1], "", ScoutFiles[2], ScoutFiles[3], ScoutFiles[4], "", ScoutFiles[5], ScoutFiles[6], ScoutFiles[7] };
					InfoFiles.Add(Path.GetFileNameWithoutExtension(Directory));
					CompFiles.Add(FilesRS1);
					break;
				case "Error":
					ErrorsCount++;
					break;
			}
			Current++;
			Errors.text = "Exported: " + CompFiles.Count + "   Errors: " + ErrorsCount.ToString();
		}
		List<string> Output = new List<string>();
		string[] Rounds = new string[InfoFiles.Count];
		string[] Teams = new string[InfoFiles.Count];
		int Filei = 0;
		foreach (string File in InfoFiles)
		{
			Rounds[Filei] = InfoFiles[Filei].Split('-')[0];
			Teams[Filei] = InfoFiles[Filei].Split('-')[1];
			Filei++;
		}
		Rounds = Rounds.Distinct().ToArray();
		Teams = Teams.Distinct().ToArray();
		List<FilesClass> QuestionVariants = CompFiles.GroupBy(x => x.Questions).Select(y => y.First()).Distinct().ToList();
		int Matchi = 0;
		foreach (string Match in Rounds)
		{
			Output.Add(GenerateHeader(Match, Teams, 1));
			string[] VariantBuilder = new string[25];
			int Varianti = 0;
			string[] MatchString = new string[25];
			foreach (FilesClass File in CompFiles)
			{
				MatchString = new string[25];
				int Questioni = 0;
				foreach (string Question in File.Questions)
				{
					int Teami = 0;
					foreach (string Team in Teams)
					{
						int i = InfoFiles.IndexOf(Match + "-" + Team);
						if (i != -1)
						{
							if (Teami == 0)
							{
								MatchString[Questioni] += Question.Split('|')[0] + ",";
							}
							if (CompFiles[i].QuestionsType[Questioni] != 0)
							{
								MatchString[Questioni] += CompFiles[i].Answers[Questioni] + ",";
							}
							else
							{
								MatchString[Questioni] += ",";
							}
							/*if (CompFiles[i].Questions == File.Questions)
							{
								if (CompFiles[i].QuestionsType[Questioni] != 0)
								{
									VariantBuilder[Questioni] += CompFiles[i].Answers[Questioni] + ",";
								}
								else
								{
									VariantBuilder[Questioni] += ",";
								}
							}
							else
							{
								VariantBuilder[Questioni] += ",";
							}*/
						}
						else
						{
							MatchString[Questioni] += ",";
						}
						Teami++;
					}
					MatchString[Questioni] += ",";
					Questioni++;
				}
				Varianti++;
			}
			foreach (string T in MatchString)
			{
				if (T != null)
				{
					Output.Add(T);
				}
			}
			Output.Add("");
			Matchi++;
		}
		

		StreamWriter outputFile = new StreamWriter(path);
		foreach (string line in Output)
		{
			outputFile.WriteLine(line);
		}
		outputFile.Close();
		if (ErrorsCount == 0)
		{
			Manager.GetComponent<ErrorManager>().Log("Scouting package exported as " + Path.GetFileName(path));
		}
		else
		{
			Manager.GetComponent<ErrorManager>().Warning("Scouting package exported as " + Path.GetFileName(path) + " with " + ErrorsCount + " Errors");
		}
		ConfirmButton.SetActive(true);
		yield return null;
	}

	string GenerateHeader(string Match, string[] Teams, int Questions)
	{
		string Output = "";
		for (int i = 0; i < Questions; i++)
		{
			Output += "Match " + Match + ",";
			foreach (string Team in Teams)
			{
				Output = Output + Team + ",";
			}
			Output += ",";
		}
		return Output;
	}

	public void Ignore()
	{
		hasAnswered = true;
		WillOverwrite = false;
	}

	public void Overwrite()
	{
		hasAnswered = true;
		WillOverwrite = true;
	}

	public void Confirm()
	{
		PackageManager.SetActive(false);
	}

	public List<string> GetFiles()
	{
		List<string> Files = new List<string>();
		string Matching = "*.rs";
		string[] allFiles = Directory.GetFiles(FilesDataClass.FilePathSaves, Matching);
		foreach (string File in allFiles)
		{
			Files.Add(File);
		}
		return Files;
	}
}
