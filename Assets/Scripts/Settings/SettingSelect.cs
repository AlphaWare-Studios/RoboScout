using UnityEngine;

public class SettingSelect : MonoBehaviour {

	public GameObject Menu1;
	public GameObject Menu2;
	public GameObject Menu3;
	public GameObject Menu4;
	public GameObject Menu1Toggle;
	public GameObject Menu2Toggle;
	public GameObject Menu3Toggle;
	public GameObject Menu4Toggle;

	public void Wake()
	{
		MenuSwap("General");
	}
	void CLS()
    {
		Menu1.SetActive(false);
		Menu2.SetActive(false);
		Menu3.SetActive(false);
		Menu4.SetActive(false);
		Menu1Toggle.SetActive(false);
		Menu2Toggle.SetActive(false);
		Menu3Toggle.SetActive(false);
		Menu4Toggle.SetActive(false);
	}

	public void MenuSwap(string lvl) {
		CLS();
		switch (lvl)
        {
			case "General":
				Menu1.SetActive(true);
				Menu1Toggle.SetActive(true);
				break;
			case "Questions":
				Menu2.SetActive(true);
				Menu2Toggle.SetActive(true);
				break;
			case "Files":
				Menu3.SetActive(true);
				Menu3Toggle.SetActive(true);
				break;
			case "Interface":
				Menu4.SetActive(true);
				Menu4Toggle.SetActive(true);
				break;
			case "Dev":
				break;
			default:
				Debug.Log("Error");
				break;
		}
	}
}
