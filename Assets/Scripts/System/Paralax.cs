using UnityEngine;

public class Paralax : MonoBehaviour
{
    public GameObject BackgroundMain;
    bool UseParalax;
    float ParalaxValue = 50;

    public void Wake()
    {
        UseParalax = true;
        ReloadUI();
    }

    public void ReloadUI()
    {
        if (UseParalax)
        {
            BackgroundMain.transform.localScale = new Vector2(BackgroundMain.transform.localScale.x + BackgroundMain.transform.localScale.x / ParalaxValue, BackgroundMain.transform.localScale.y + BackgroundMain.transform.localScale.y / ParalaxValue);
        }
        else
        {
            BackgroundMain.transform.localScale = new Vector2(1, 1);
        }
    }

    public void Frame()
    {
        if (UseParalax)
        {
            float X = Input.mousePosition.x / ParalaxValue;
            float Y = Input.mousePosition.y / ParalaxValue;
            X = Mathf.Clamp(X, 0, Screen.width / ParalaxValue);
            Y = Mathf.Clamp(Y, 0, Screen.height / ParalaxValue);
            BackgroundMain.transform.position = new Vector2(X + Screen.width / 2 - (Screen.width / (ParalaxValue * 2)), Y + Screen.height / 2 - (Screen.height / (ParalaxValue * 2)));
        }
        else
        {
            BackgroundMain.transform.position = new Vector2(Screen.width / 2, Screen.width / 2);
        }
    }
}
