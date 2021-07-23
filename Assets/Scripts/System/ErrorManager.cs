using System;
using System.IO;
using System.Collections;
using System.Collections.Specialized;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class ErrorManager : MonoBehaviour
{
	public GameObject ErrorBox;
	public Text ErrorText;

	public void Wake()
	{
		ErrorText.text = "Error";
		ErrorBox.SetActive(false);
	}

	public void Custom(string Error, Color color)
	{
		StopAllCoroutines();
		StartCoroutine(ErrorDisplay(Error, color));
	}

	public void Log(string Error)
	{
		StopAllCoroutines();
		StartCoroutine(ErrorDisplay(Error, Color.white));
	}

	public void Warning(string Error)
	{
		StopAllCoroutines();
		StartCoroutine(ErrorDisplay(Error, Color.yellow));
	}

	public void Error(string Error)
	{
		StopAllCoroutines();
		StartCoroutine(ErrorDisplay(Error, Color.red));
	}

	public void Exception(string Log, string Trace, LogType Type)
	{
		if (Type == LogType.Exception)
		{
			DiscordErrorReport("Error recieved at " + DateTime.Now + "\nFrom " + Application.productName + " : " + Application.version + "\n-----\n" + Log + "\n-----\n" + Trace);
			StopAllCoroutines();
			StartCoroutine(ErrorDisplay(Log + "\n-----\n" + Trace, Color.red));
		}
	}

	IEnumerator ErrorDisplay(string Error, Color color)
	{
		yield return new WaitForSeconds(0.1f);
		Error = Error.Replace("\n", " ");
		ErrorText.text = Error;
		ErrorText.color = color;
		ErrorBox.SetActive(true);
		yield return new WaitForSeconds(5);
		ErrorText.text = "Error";
		ErrorBox.SetActive(false);
	}

	void DiscordErrorReport(string Message)
	{
		using (DiscordWebHook dcWeb = new DiscordWebHook())
		{
			dcWeb.SendMessage(Message);
			dcWeb.Dispose();
		}
	}
}

public class DiscordWebHook : IDisposable
{
	private WebClient dWebClient;

	public DiscordWebHook()
	{
		dWebClient = new WebClient();
	}

	public void SendMessage(string msgSend)
	{
		WebRequest WR = (HttpWebRequest)WebRequest.Create(Keys.DiscordErrorReport);
		WR.ContentType = "application/json";
		WR.Method = "POST";
		using (var SW = new StreamWriter(WR.GetRequestStream()))
		{
			embed embed = new embed
			{
				title = "Error recieved at " + DateTime.Now + "\nFrom " + Application.productName + " : " + Application.version,
				description = msgSend,
				color = "16738816"
			};
			embed[] EmbedArr = new embed[1] {embed};
			DiscordWebHookValues Values = new DiscordWebHookValues
			{
				username = "RoboScout Error Report",
				avatar_url = "https://alphawarestudios.com/Assets/Sprites/RoboScout-Logo.png",
				message = "",
				embeds = EmbedArr
			};
			string Json = JsonUtility.ToJson(Values);
			Debug.Log(Json);
			SW.Write(Json);
		}
		WebResponse Response = WR.GetResponse();
	}

	public void Dispose()
	{
		dWebClient.Dispose();
	}
}

[Serializable]
public class DiscordWebHookValues
{
	public string username;
	public string avatar_url;
	public string message;
	public embed[] embeds;
}

[Serializable]
public class embed
{
	public string title;
	public string description;
	public string color;
}
