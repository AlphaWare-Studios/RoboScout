using System.Collections.Generic;
using UnityEngine;

public class ProgramManager : MonoBehaviour
{
    public GameObject Programs;

    public void Frame()
    {
        float OffsetX = ResolutionManager.ScreenOffsetW;
        float OffsetY = ResolutionManager.ScreenOffsetH;
        string ProgramType;
        GameObject[] ProgramList = GetChildren(Programs).ToArray();
        GameObject Toolbar;
        Vector2 Pos;
        foreach (GameObject Program in ProgramList) {
            Toolbar = Program.transform.Find("Toolbar").gameObject;
            ProgramType = Program.name;
            switch (ProgramType)
            {
                case "Calculator":
                    if (!Program.GetComponent<Calculator>().isMinimized)
                    {
                        if (Toolbar.GetComponent<ToolbarManager>().dragging)
                        {
                            Program.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y - (275 * OffsetY));
                            Program.transform.SetAsLastSibling();
                        }
                        Pos = Program.transform.position;
                        Pos.x = Mathf.Clamp(Pos.x, 200, 1720);
                        Pos.y = Mathf.Clamp(Pos.y, -140, 675);
                        Program.transform.position = Pos;
                    }
                    else
                    {
                        if (Toolbar.GetComponent<ToolbarManager>().dragging)
                        {
                            Program.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y - OffsetY);
                            Program.transform.SetAsLastSibling();
                        }
                        Pos = Program.transform.position;
                        Pos.x = Mathf.Clamp(Pos.x, 200, 1720);
                        Pos.y = Mathf.Clamp(Pos.y, 132.5f, 947.5f);
                        Program.transform.position = Pos;
                    }
                    break;
                case "Notepad":
                    if (!Program.gameObject.GetComponent<Notepad>().isMinimized)
                    {
                        if (Toolbar.GetComponent<ToolbarManager>().dragging)
                        {
                            Program.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y - (225 * OffsetY));
                            Program.transform.SetAsLastSibling();
                        }
                        Pos = Program.transform.position;
                        Pos.x = Mathf.Clamp(Pos.x, 400, 1520);
                        Pos.y = Mathf.Clamp(Pos.y, -90, 725);
                        Program.transform.position = Pos;
                    }
                    else
                    {
                        if (Toolbar.GetComponent<ToolbarManager>().dragging)
                        {
                            Program.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y - OffsetY);
                            Program.transform.SetAsLastSibling();
                        }
                        Pos = Program.transform.position;
                        Pos.x = Mathf.Clamp(Pos.x, 400, 1520);
                        Pos.y = Mathf.Clamp(Pos.y, 132.5f, 947.5f);
                        Program.transform.position = Pos;
                    }
                    break;
                case "File Manager":
                    if (!Program.gameObject.GetComponent<FileExplorer>().isMinimized)
                    {
                        if (Toolbar.GetComponent<ToolbarManager>().dragging)
                        {
                            Program.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y - (350 * OffsetY));
                            Program.transform.SetAsLastSibling();
                        }
                        Pos = Program.transform.position;
                        Pos.x = Mathf.Clamp(Pos.x, 600, 1320);
                        Pos.y = Mathf.Clamp(Pos.y, 480, 600);
                        Program.transform.position = Pos;
                    }
                    else
                    {
                        if (Toolbar.GetComponent<ToolbarManager>().dragging)
                        {
                            Program.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y - OffsetY);
                            Program.transform.SetAsLastSibling();
                        }
                        Pos = Program.transform.position;
                        Pos.x = Mathf.Clamp(Pos.x, 600, 1320);
                        Pos.y = Mathf.Clamp(Pos.y, 132.5f, 947.5f);
                        Program.transform.position = Pos;
                    }
                    break;
                case "Music":
                    Program.GetComponent<Music>().Frame();
                    if (!Program.gameObject.GetComponent<Music>().isMinimized)
                    {
                        if (Toolbar.GetComponent<ToolbarManager>().dragging)
                        {
                            Program.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y - (55 * OffsetY));
                            Program.transform.SetAsLastSibling();
                        }
                        Pos = Program.transform.position;
                        Pos.x = Mathf.Clamp(Pos.x, 200, 1720);
                        Pos.y = Mathf.Clamp(Pos.y, 80, 875);
                        Program.transform.position = Pos;
                    }
                    else
                    {
                        if (Toolbar.GetComponent<ToolbarManager>().dragging)
                        {
                            Program.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y - OffsetY);
                            Program.transform.SetAsLastSibling();
                        }
                        Pos = Program.transform.position;
                        Pos.x = Mathf.Clamp(Pos.x, 200, 1720);
                        Pos.y = Mathf.Clamp(Pos.y, 132.5f, 947.5f);
                        Program.transform.position = Pos;
                    }
                    break;
                case "Console":
                    Program.GetComponent<Console>().Frame();
                    if (!Program.gameObject.GetComponent<Console>().isMinimized)
                    {
                        if (Toolbar.GetComponent<ToolbarManager>().dragging)
                        {
                            Program.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y - (225 * OffsetY));
                            Program.transform.SetAsLastSibling();
                        }
                        Pos = Program.transform.position;
                        Pos.x = Mathf.Clamp(Pos.x, 400, 1520);
                        Pos.y = Mathf.Clamp(Pos.y, -90, 725);
                        Program.transform.position = Pos;
                    }
                    else
                    {
                        if (Toolbar.GetComponent<ToolbarManager>().dragging)
                        {
                            Program.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y - OffsetY);
                            Program.transform.SetAsLastSibling();
                        }
                        Pos = Program.transform.position;
                        Pos.x = Mathf.Clamp(Pos.x, 400, 1520);
                        Pos.y = Mathf.Clamp(Pos.y, 132.5f, 947.5f);
                        Program.transform.position = Pos;
                    }
                    break;
                default:
                    break;
            }
        }
    }

    List<GameObject> GetChildren(GameObject Object)
    {
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in Object.transform)
        {
            children.Add(child.gameObject);
        }
        return children;
    }
}
