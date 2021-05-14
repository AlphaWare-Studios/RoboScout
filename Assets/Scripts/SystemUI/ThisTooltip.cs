using UnityEngine;
using UnityEngine.EventSystems;

public class ThisTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    GameObject Manager;
    public string TooltipText;
    public int TooltipLines;
    bool isHighlighting;
    bool isTooltipsEnabled;

    void Start()
    {
        Manager = GameObject.Find("_Manager");
    }

    public void OnPointerEnter(PointerEventData e)
    {
        isHighlighting = true;
    }

    public void OnPointerExit(PointerEventData e)
    {
        isHighlighting = false;
        if (isTooltipsEnabled)
        {
            Manager.GetComponent<Tooltip>().TooltipText.text = "";
            Manager.GetComponent<Tooltip>().TooltipLines = 0;
            Manager.GetComponent<Tooltip>().TooltipWindow.SetActive(false);
        }
    }

    void OnDisable()
    {
        if (isTooltipsEnabled && isHighlighting)
        {
            Manager.GetComponent<Tooltip>().TooltipText.text = "";
            Manager.GetComponent<Tooltip>().TooltipLines = 0;
            Manager.GetComponent<Tooltip>().TooltipWindow.SetActive(false);
        }
        isHighlighting = false;
    }

    void Update()
    {
        isTooltipsEnabled = Manager.GetComponent<Tooltip>().isTooltipEnabled;
        if (isHighlighting && isTooltipsEnabled)
        {
            if (TooltipText != "")
            {
                Manager.GetComponent<Tooltip>().TooltipText.text = TooltipText;
                Manager.GetComponent<Tooltip>().TooltipLines = TooltipLines;
            }
            else
            {
                Manager.GetComponent<Tooltip>().TooltipText.text = this.name;
                Manager.GetComponent<Tooltip>().TooltipLines = 1;
            }
            Manager.GetComponent<Tooltip>().TooltipWindow.SetActive(true);
        }
    }
}
