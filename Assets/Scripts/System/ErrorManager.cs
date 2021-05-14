using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ErrorManager : MonoBehaviour
{
    public GameObject ErrorBox;
    public Text ErrorText;

    public void Wake()
    {
        ErrorText.text = "Error";
        ErrorBox.SetActive(false);
    }

    public void Custom(string Error, Color color)
    {
        StopAllCoroutines();
        StartCoroutine(ErrorDisplay(Error, color));
    }

    public void Log(string Error)
    {
        StopAllCoroutines();
        StartCoroutine(ErrorDisplay(Error, Color.white));
    }

    public void Warning(string Error)
    {
        StopAllCoroutines();
        StartCoroutine(ErrorDisplay(Error, Color.yellow));
    }

    public void Error(string Error)
    {
        StopAllCoroutines();
        StartCoroutine(ErrorDisplay(Error, Color.red));
    }

    public void Exception(string Log, string Trace, LogType Type)
    {
        if (Type == LogType.Exception)
        {
            StopAllCoroutines();
            StartCoroutine(ErrorDisplay(Log + " " + Trace, Color.red));
        }
    }

    IEnumerator ErrorDisplay(string Error, Color color)
    {
        yield return new WaitForSeconds(0.1f);
        Error = Error.Replace("\n", " ");
        ErrorText.text = Error;
        ErrorText.color = color;
        ErrorBox.SetActive(true);
        yield return new WaitForSeconds(5);
        ErrorText.text = "Error";
        ErrorBox.SetActive(false);
    }
}
