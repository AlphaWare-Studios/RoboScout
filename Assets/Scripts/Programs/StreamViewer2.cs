using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.IO;
using System;

//this is called StreamViewer2 rather than StreamViewer because unity or something dosn't like the name
//StreamViewer and refuses to believe that it could possible derive from MonoBehaviour.
public class StreamViewer2 : MonoBehaviour
{
	GameObject Manager;
	public GameObject StreamProgram;
	public RectTransform StreamRT;
	public GameObject Screen;
	public bool isMinimized;
	public InputField FileBox;

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
			StreamRT.sizeDelta = new Vector2(StreamRT.sizeDelta.x, 55);
			StreamProgram.transform.position = new Vector2(StreamProgram.transform.position.x, StreamProgram.transform.position.y + (222.5f * ResolutionManager.ScreenOffsetH));
		}
	}

	public void Maximize()
	{
		if (isMinimized)
		{
			isMinimized = false;
			Screen.SetActive(true);
			StreamRT.sizeDelta = new Vector2(StreamRT.sizeDelta.x, 500);
			StreamProgram.transform.position = new Vector2(StreamProgram.transform.position.x, StreamProgram.transform.position.y - (222.5f * ResolutionManager.ScreenOffsetH));
		}
	}

	public void Close()
	{
		GameObject.Destroy(StreamProgram);
	}
}
