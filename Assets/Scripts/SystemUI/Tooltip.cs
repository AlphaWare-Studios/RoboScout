using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class Tooltip : MonoBehaviour
{
    public GameObject Manager;
    public Canvas Canvas;
    public GameObject TooltipWindow;
    public Text TooltipText;
    public int TooltipLines;
    public bool isTooltipEnabled;
    public float OffsetX;
    public float OffsetY;

    public void Wake()
    {
        ReloadUI();
    }

    public void ReloadUI()
    {
        float x = Canvas.GetComponent<RectTransform>().rect.width;
        float y = Canvas.GetComponent<RectTransform>().rect.height;
        OffsetX = x / 1920;
        OffsetY = y / 1080;
        SettingsClass Settings = Manager.GetComponent<FileManager>().Settings;
        isTooltipEnabled = Settings.isTooltipsEnabled;
        TooltipWindow.SetActive(false);
        TooltipText.text = "";
    }

    public void Frame()
    {
        if (isTooltipEnabled)
        {
            switch (TooltipLines)
            {
                case 5:
                    TooltipWindow.transform.position = new Vector2((Input.mousePosition.x + 270), Input.mousePosition.y - 83.125f);
                    TooltipWindow.GetComponent<RectTransform>().sizeDelta = new Vector2(540, 166.25f);
                    break;
                case 4:
                    TooltipWindow.transform.position = new Vector2((Input.mousePosition.x + 270), Input.mousePosition.y - 67.5f);
                    TooltipWindow.GetComponent<RectTransform>().sizeDelta = new Vector2(540, 135);
                    break;
                case 3:
                    TooltipWindow.transform.position = new Vector2((Input.mousePosition.x + 270), Input.mousePosition.y - 51.875f);
                    TooltipWindow.GetComponent<RectTransform>().sizeDelta = new Vector2(540, 103.75f);
                    break;
                case 2:
                    TooltipWindow.transform.position = new Vector2((Input.mousePosition.x + 270), Input.mousePosition.y - 36.25f);
                    TooltipWindow.GetComponent<RectTransform>().sizeDelta = new Vector2(540, 72.5f);
                    break;
                case 1:
                    TooltipWindow.transform.position = new Vector2((Input.mousePosition.x + 270), Input.mousePosition.y - 20.625f);
                    TooltipWindow.GetComponent<RectTransform>().sizeDelta = new Vector2(540, 41.25f);
                    break;
                default:
                    TooltipWindow.transform.position = new Vector2((Input.mousePosition.x + 270), Input.mousePosition.y - 5);
                    TooltipWindow.GetComponent<RectTransform>().sizeDelta = new Vector2(540, 10);
                    break;
            }
        }
    }
}
