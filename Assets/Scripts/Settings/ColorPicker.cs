using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    public Slider Red;
    public Slider Green;
    public Slider Blue;
    public Slider Alpha;
    public Text RedText;
    public Text GreenText;
    public Text BlueText;
    public Text AlphaText;
    public float RedFloat;
    public float GreenFloat;
    public float BlueFloat;
    public float AlphaFloat;

    public void UpdateRed(float Color)
    {
        RedText.text = Color.ToString();
        RedFloat = Color;
    }
    public void UpdateGreen(float Color)
    {
        GreenText.text = Color.ToString();
        GreenFloat = Color;
    }
    public void UpdateBlue(float Color)
    {
        BlueText.text = Color.ToString();
        BlueFloat = Color;
    }
    public void UpdateAlpha(float Color)
    {
        AlphaText.text = Color.ToString();
        AlphaFloat = Color;
    }

    public SettingsInterfaceColors GetColors()
    {
        SettingsInterfaceColors Color = new SettingsInterfaceColors();
        Color.Red = int.Parse(RedFloat.ToString());
        Color.Green = int.Parse(GreenFloat.ToString());
        Color.Blue = int.Parse(BlueFloat.ToString());
        Color.Alpha = int.Parse(AlphaFloat.ToString());
        return Color;
    }

    public void SetColors(SettingsInterfaceColors Colors)
    {
        Red.value = Colors.Red;
        Green.value = Colors.Green;
        Blue.value = Colors.Blue;
        Alpha.value = Colors.Alpha;
        RedText.text = Colors.Red.ToString();
        GreenText.text = Colors.Green.ToString();
        BlueText.text = Colors.Blue.ToString();
        AlphaText.text = Colors.Alpha.ToString();
        RedFloat = Colors.Red;
        GreenFloat = Colors.Green;
        BlueFloat = Colors.Blue;
        AlphaFloat = Colors.Alpha;
    }
}
