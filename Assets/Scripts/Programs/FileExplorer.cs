using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.IO;
using System;

public class FileExplorer : MonoBehaviour
{
	GameObject Manager;
	public GameObject FileProgram;
	public RectTransform FileRT;
	public GameObject Screen;
	public bool isMinimized;
	public bool hasCrashed;

	public void Wake()
	{
		Manager = GameObject.Find("_Manager");
	}

	public void Minimize()
	{
		if (!isMinimized)
		{
			isMinimized = true;
			Screen.SetActive(false);
			FileRT.sizeDelta = new Vector2(FileRT.sizeDelta.x, 55);
			FileProgram.transform.position = new Vector2(FileProgram.transform.position.x, FileProgram.transform.position.y + (347.5f * ResolutionManager.ScreenOffsetH));
		}
	}

	public void Maximize()
	{
		if (isMinimized)
		{
			isMinimized = false;
			Screen.SetActive(true);
			FileRT.sizeDelta = new Vector2(FileRT.sizeDelta.x, 750);
			FileProgram.transform.position = new Vector2(FileProgram.transform.position.x, FileProgram.transform.position.y - (347.5f * ResolutionManager.ScreenOffsetH));
		}
	}

	public void Close()
	{
		GameObject.Destroy(FileProgram);
	}
}
