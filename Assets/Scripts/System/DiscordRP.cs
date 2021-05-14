using System.Collections;
using UnityEngine;
using Discord;

public class DiscordRP : MonoBehaviour
{
	public static Discord.Discord discord;
	static ActivityManager activityManager;
	static Activity activity;
	static ActivityAssets Icon = new ActivityAssets{LargeImage = "roboscout", LargeText = "RoboScout", SmallImage = "alphaware_mini", SmallText = "AlphaWare" };
	static string Status1;
	static string Status2;
	public static string Status2Default;
	static bool hasDiscord;
	static bool Init;
	static DiscordRP Instance;


	public void Wake()
	{
		Init = false;
		Instance = this;
		hasDiscord = true;
		try
		{
			discord = new Discord.Discord(831301928431386694, (ulong)CreateFlags.Default);
			activityManager = discord.GetActivityManager();
		}
		catch (ResultException)
		{
			hasDiscord = false;
		}
		Init = true;
	}

	public static IEnumerator UpdateActivity(string S1, string S2)
	{
		while (!Init)
		{
			yield return null;
		}
		if (hasDiscord)
		{
			if (S1 != null)
			{
				Status1 = S1;
			}
			if (S2 != "Default")
			{
				if (S2 != null)
				{
					Status2 = S2;
				}
			}
			else
			{
				Status2 = Status2Default;
			}
			activity = new Activity
			{
				Details = Status1,
				State = Status2,
				Assets = Icon
			};
			activityManager.UpdateActivity(activity, (result) =>
			{
				if (result != Result.Ok)
				{
					Debug.Log("Failed");
				}
			});
		}
	}

	public void Frame()
	{
		if (hasDiscord)
		{
			discord.RunCallbacks();
		}
	}

	void OnDisable()
	{
		if (hasDiscord)
		{
			discord.Dispose();
		}
	}
}
