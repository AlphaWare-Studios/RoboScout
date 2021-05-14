using UnityEngine;

public class Language : MonoBehaviour
{
	public GameObject Manager;

	void Wake()
	{
		Load();
	}

	void Load()
	{
		SettingsClass Settings = Manager.GetComponent<FileManager>().Settings;
	}

	void SetLanguage()
    {

    }
}
