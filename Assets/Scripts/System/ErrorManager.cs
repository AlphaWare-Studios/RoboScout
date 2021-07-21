using System;
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
			StartCoroutine(ErrorDisplay(Log + " " + Trace, Color.red));
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

	void DiscordErrorReport(string Error)
	{
<<<<<<< master
		using (DiscordWebHook dcWeb = new DiscordWebHook())
		{
			dcWeb.ProfilePicture = "https://alphawarestudios.com/Assets/Sprites/RoboScout-Logo.png";
			dcWeb.UserName = "RoboScout Error Report";
			dcWeb.WebHook = "https://discord.com/api/webhooks/782645093906776074/pTbpLXHqeovb_BDuSwcWohfX9CZSWn6J1ssC5S08UGsjeuWL4xkJcktc7SgVnacAc8JR";
			string Message = Error;

			dcWeb.SendMessage(Message);
			dcWeb.Dispose();
		}
	}
}

public class DiscordWebHook : IDisposable
{
	private WebClient dWebClient;
	private static NameValueCollection discordValues = new NameValueCollection();
	public string WebHook { get; set; }
	public string UserName { get; set; }
	public string ProfilePicture { get; set; }

	public DiscordWebHook()
	{
		dWebClient = new WebClient();
	}

	public void SendMessage(string msgSend)
	{
		discordValues.Clear();
		discordValues.Add("username", UserName);
		discordValues.Add("avatar_url", ProfilePicture);
		discordValues.Add("content", msgSend);

		dWebClient.UploadValues(WebHook, discordValues);
	}

	public void Dispose()
	{
		dWebClient.Dispose();
=======
		string Username = "RoboScout Error Report";
		string PFP = "https://alphawarestudios.com/Assets/Sprites/RoboScout-Logo.png";
		string WebHook = Keys.DiscordErrorReport;
		WebRequest WR = (HttpWebRequest)WebRequest.Create(WebHook);
		WR.ContentType = "application/json";
		WR.Method = "POST";
		using (var SW = new StreamWriter(WR.GetRequestStream()))
        {
			string Json = JsonConvert.SerializeObject(new
			{
				username = Username,
				avatar_url = PFP,
				message = "",
				embeds = new[]
                {
					new
                    {
						title = "Error recieved at " + DateTime.Now + "\nFrom " + Application.productName + " : " + Application.version,
						description = Error,
						color = "16738816"
					}
                }
			});
			SW.Write(Json);
        }
		WebResponse Response = WR.GetResponse();
>>>>>>> local
	}
}
