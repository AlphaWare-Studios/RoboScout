using System;
using System.Collections;
using UnityEngine;
using System.IO;

public class Screenshot : MonoBehaviour
{
    public GameObject Manager;

    public void Frame()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            TakeScreenshot();
        }
    }

    public void TakeScreenshot()
    {
        Texture2D Texture;
        StartCoroutine(RecordFrame(Tex =>
        {
            Texture = Tex;
            Texture.Apply();
            byte[] Bytes = Texture.EncodeToPNG();
            File.WriteAllBytes(FilesDataClass.FilePathScreenshots + "/" + DateTime.Now.ToString("M-dd-yyyy-HH.mm.ss") + ".png", Bytes);
            Manager.GetComponent<ErrorManager>().Log("Screenshot Saved as " + DateTime.Now.ToString("M-dd-yyyy-HH.mm.ss") + ".png");
        }));
    }

    static IEnumerator RecordFrame(System.Action<Texture2D> callback)
    {
        yield return new WaitForEndOfFrame();
        var Texture = ScreenCapture.CaptureScreenshotAsTexture();
        callback(Texture);
    }
}
