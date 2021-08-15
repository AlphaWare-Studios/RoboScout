using System.Collections;
using UnityEngine;

public class MethodManager : MonoBehaviour
{
	public GameObject Manager;
	public GameObject MainManager;
	public GameObject ScoutManager;
	public GameObject AnalyzeManager;
	public GameObject SettingsManager;
	public GameObject QRManager;
	public GameObject USBManager;
	public GameObject StartManager;
	public bool Running;

	public void StartManaging()
	{
		Running = true;
		Wake();
		StartCoroutine(Second(true));
	}

	void Wake()
	{
		Manager.GetComponent<FileManager>().Wake();
		Manager.GetComponent<ResolutionManager>().Wake();
		Manager.GetComponent<ViewManager>().Wake();
		Manager.GetComponent<SystemUIManager>().Wake();
		Manager.GetComponent<UIReloadManager>().Wake();
		Manager.GetComponent<ErrorManager>().Wake();
		Manager.GetComponent<ColorManager>().Wake();
		Manager.GetComponent<Tooltip>().Wake();
		Manager.GetComponent<ExportManager>().Wake();
		Manager.GetComponent<Paralax>().Wake();
		MainManager.GetComponent<Main>().Wake();
		MainManager.GetComponent<Splash>().Wake();
		MainManager.GetComponent<NewsManager>().Wake();
		AnalyzeManager.GetComponent<Analyze>().Wake();
		SettingsManager.GetComponent<Settings>().Wake();
		SettingsManager.GetComponent<SettingSelect>().Wake();
		QRManager.GetComponent<QR>().Wake();
		StartManager.GetComponent<StartManager>().Wake();
	}

	IEnumerator Second(bool Init)
	{
		if (Running) {
			if (Init)
			{
				yield return new WaitForSeconds(1);
			}
			Manager.GetComponent<SystemUIManager>().Second();
			MainManager.GetComponent<Splash>().Second();
			yield return new WaitForSeconds(1);
			StartCoroutine(Second(false));
		}
	}

	void Update()
	{
		if (Running)
		{
			Manager.GetComponent<ViewManager>().Frame();
			Manager.GetComponent<SystemUIManager>().Frame();
			Manager.GetComponent<Tooltip>().Frame();
			Manager.GetComponent<Paralax>().Frame();
			Manager.GetComponent<ProgramManager>().Frame();
			Manager.GetComponent<Screenshot>().Frame();
			MainManager.GetComponent<Splash>().Frame();
			QRManager.GetComponent<QR>().Frame();
			StartManager.GetComponent<StartManager>().Frame();
		}
	}
}
