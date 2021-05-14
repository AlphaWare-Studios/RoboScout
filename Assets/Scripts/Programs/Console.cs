using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Console : MonoBehaviour
{
	public GameObject Manager;
	public GameObject StartManager;
	public GameObject ConsoleProgram;
	public RectTransform ConsoleRT;
	public GameObject Screen;
	public bool isMinimized;
	public bool hasCrashed;
	public GameObject TasksWindow;
	public GameObject ConsoleWindow;
	public GameObject UsageWindow;
	public GameObject ChatWindow;

	public InputField CommandInput;
	public Text Text1;
	public Text Text2;
	public Text Text3;
	public Text Text4;
	public Text Text5;
	public Text Text6;
	string[] CommandList = new string[5] {"Clear", "Help", "Request", "Start", "Window"};

	public InputField Teams;

	public void Wake()
	{
		Manager = GameObject.Find("_Manager");
		StartManager = GameObject.Find("Start");
		TasksWindow.SetActive(true);
		ConsoleWindow.SetActive(false);
		UsageWindow.SetActive(false);
		ChatWindow.SetActive(false);
		Text1.text = "";
		Text2.text = "";
		Text3.text = "";
		Text4.text = "";
		Text5.text = "";
		Text6.text = "";
	}

	public void SetWindow(string Window)
	{
		TasksWindow.SetActive(false);
		ConsoleWindow.SetActive(false);
		UsageWindow.SetActive(false);
		ChatWindow.SetActive(false);
		switch (Window)
		{
			case "Tasks":
				TasksWindow.SetActive(true);
				break;
			case "Console":
				ConsoleWindow.SetActive(true);
				break;
			case "Usage":
				UsageWindow.SetActive(true);
				StartCoroutine(SetUsage());
				break;
			case "Chat":
				ChatWindow.SetActive(true);
				break;
			default:
				break;
		}
	}

	public void SubmitCommand()
	{
		if (CommandInput.text != "")
		{
			string[] Data = CommandInput.text.Split(' ');
			bool Admin;
			string Command;
			string Argument;
			bool Pipe;
			string PipeTo;
			if (Data[0] == "Sudo")
			{
				if (Data.Length > 1)
				{
					Command = Data[1];
					if (Data.Length > 2)
					{
						Argument = Data[2];
						if (Data.Length > 4)
						{
							if (Data[3] == "|")
							{
								Pipe = true;
								PipeTo = Data[4];
							}
							else
							{
								Pipe = false;
								PipeTo = "";
							}
						}
						else
						{
							Pipe = false;
							PipeTo = "";
						}
					}
					else
					{
						Argument = "Null";
						Pipe = false;
						PipeTo = "";
					}
				}
				else
				{
					Command = "Null";
					Argument = "Null";
					Pipe = false;
					PipeTo = "";
				}
				Admin = true;
			}
			else
			{
				Command = Data[0];
				if (Data.Length > 1)
				{
					Argument = Data[1];
					if (Data.Length > 3)
					{
						if (Data[2] == "|")
						{
							Pipe = true;
							PipeTo = Data[3];
						}
						else
						{
							Pipe = false;
							PipeTo = "";
						}
					}
					else
					{
						Pipe = false;
						PipeTo = "";
					}
				}
				else
				{
					Argument = "Null";
					Pipe = false;
					PipeTo = "";
				}
				Admin = false;
			}
			switch (Command)
			{
				case "Clear":
					MoveText(6);
					break;
				case "Help":
					MoveText(1);
					Text1.text = "Showing Help Page 1/1";
					foreach (string CommandType in CommandList)
					{
						MoveText(1);
						Text1.text = CommandType;
					}
					break;
				case "Request":
					MoveText(1);
					StartCoroutine(GetRequest(Argument, (UnityWebRequest Request) =>
					{
						if (Request.result == UnityWebRequest.Result.Success)
						{
							string Data = Request.downloadHandler.text;
							Text1.text = "Successfully retrieved data from \"" + Argument + "\"";
							MoveText(1);
							if (Pipe)
							{
								string[] Arguments = new string[2] { Argument, Data };
								bool Program = StartManager.GetComponent<StartManager>().StartProgramBool(PipeTo, Arguments);
								if (Program)
								{
									Text1.text = "Data piped to \"" + PipeTo + "\"";
								}
								else
								{
									Text1.text = "Unable to pipe to \"" + PipeTo + "\"";
								}
							}
							else
							{
								Text1.text = "Pipe destination not set, unable to pipe";
							}
						}
						else
						{
							Text1.text = "Unable to fetch data from \"" + Argument + "\"";
						}
					}
		));
					break;
				case "Start":
					MoveText(1);
					bool Program = StartManager.GetComponent<StartManager>().StartProgramBool(Argument, null);
					if (Program)
					{
						Text1.text = "Started program \"" + Argument + "\"";
					}
					else
					{
						Text1.text = "Unknown program \"" + Argument + "\" unable to start";
					}
					break;
				case "Window":
					MoveText(1);
					bool Window = Manager.GetComponent<ViewManager>().OpenWindow(Argument);
					if (Window)
					{
						Text1.text = "Loaded window \"" + Argument + "\"";
					}
					else
					{
						Text1.text = "Unknown window \"" + Argument + "\" going to Main";
					}
					break;
				default:
					MoveText(1);
					Text1.text = "Unknown Command \"" + Command + "\" type \"Help\" for a list of commands";
					break;
			}
			CommandInput.text = "";
		}
	}

	void MoveText(int Ammount)
	{
		for (int i = 0; i < Ammount; i++) {
			Text6.text = Text5.text;
			Text5.text = Text4.text;
			Text4.text = Text3.text;
			Text3.text = Text2.text;
			Text2.text = Text1.text;
			Text1.text = "";
		}
	}

	IEnumerator GetRequest(string url, Action<UnityWebRequest> callback)
	{
		using UnityWebRequest Request = UnityWebRequest.Get(url);
		yield return Request.SendWebRequest();
		callback(Request);
	}

	IEnumerator SetUsage()
	{
		Manager.GetComponent<AWManager>().GetAWTeams();
		while (!Manager.GetComponent<AWManager>().AWTeamsDone)
		{
			yield return null;
		}
		Teams.text = Manager.GetComponent<AWManager>().AWTeamsData;
	}

	public void Frame()
	{
		if(Input.GetKeyDown(KeyCode.Return))
		{
			SubmitCommand();
		}
	}

	public void Minimize()
	{
		if (!isMinimized)
		{
			isMinimized = true;
			Screen.SetActive(false);
			ConsoleRT.sizeDelta = new Vector2(ConsoleRT.sizeDelta.x, 55);
			ConsoleProgram.transform.position = new Vector2(ConsoleProgram.transform.position.x, ConsoleProgram.transform.position.y + (222.5f * ResolutionManager.ScreenOffsetH));
		}
	}

	public void Maximize()
	{
		if (isMinimized)
		{
			isMinimized = false;
			Screen.SetActive(true);
			ConsoleRT.sizeDelta = new Vector2(ConsoleRT.sizeDelta.x, 500);
			ConsoleProgram.transform.position = new Vector2(ConsoleProgram.transform.position.x, ConsoleProgram.transform.position.y - (222.5f * ResolutionManager.ScreenOffsetH));
		}
	}

	public void Close()
	{
		GameObject.Destroy(ConsoleProgram);
	}
}