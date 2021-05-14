using System.IO;
using System.Diagnostics;
using UnityEngine;

public static class OpenInFileBrowser
{
	public static bool IsInWindows
	{
		get
		{
			if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
            {
				return true;
            }
			else
            {
				return false;
            }
		}
	}

	public static bool IsInMacOS
	{
		get
		{
			if (Application.platform == RuntimePlatform.OSXPlayer)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}

	public static bool IsInLinux
	{
		get
		{
			if (Application.platform == RuntimePlatform.LinuxPlayer)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}

	public static void OpenInWin(string path)
	{
		bool openInsidesOfFolder = false;
		string winPath = path.Replace("/", "\\");

		if (Directory.Exists(winPath))
		{
			openInsidesOfFolder = true;
		}

		try
		{
			Process.Start("explorer.exe", (openInsidesOfFolder ? "/root," : "/select,") + winPath);
		}
		catch (System.ComponentModel.Win32Exception)
		{

		}
	}

	public static void OpenInMac(string path)
	{
		bool openInsidesOfFolder = false;
		string macPath = path.Replace("\\", "/");
		if (Directory.Exists(macPath))
		{
			openInsidesOfFolder = true;
		}
		if (!macPath.StartsWith("\""))
		{
			macPath = "\"" + macPath;
		}
		if (!macPath.EndsWith("\""))
		{
			macPath += "\"";
		}
		string arguments = (openInsidesOfFolder ? "" : "-R ") + macPath;

		try
		{
			Process.Start("open", arguments);
		}
		catch (System.ComponentModel.Win32Exception)
		{

		}
	}

	public static void OpenInLinux(string path)
	{
		bool openInsidesOfFolder = false;
		string winPath = path.Replace("/", "\\");

		if (Directory.Exists(winPath))
		{
			openInsidesOfFolder = true;
		}

		try
		{
			Process.Start("mimeopen", (openInsidesOfFolder ? "/root," : "/select,") + winPath);
		}
		catch (System.ComponentModel.Win32Exception)
		{

		}
	}

	public static void Open(string path)
	{
		if (IsInWindows)
		{
			OpenInWin(path);
		}
		else if (IsInMacOS)
		{
			OpenInMac(path);
		}
		else if (IsInLinux)
		{
			OpenInLinux(path);
		}
		else // couldn't determine OS
		{
			
		}
	}
}