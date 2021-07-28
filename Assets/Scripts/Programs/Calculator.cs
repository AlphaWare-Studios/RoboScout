using UnityEngine;
using UnityEngine.UI;
using System;

public class Calculator : MonoBehaviour
{
    public GameObject Manager;
    public GameObject CalcProgram;
    public RectTransform CalcRT;
    public GameObject TopMenu;
    public GameObject Screen;
    public GameObject Buttons;
    public GameObject Border1;
    public GameObject Border2;
    public bool isMinimized;
    public bool hasCrashed;
    public Text Main;
    public Text Secondary;
    public string Operator;
    string Temp3;
    public bool hasDecimal;
    public bool Value1Neg;
    public bool Value2Neg;
    public bool hasMovedUp;
    public string Value1Str;
    public double Value1;
    public double Value2;
    public double Value3;
    public bool isDone;
    public GameObject Row1;
    public GameObject Row2;
    public GameObject Row3;
    public GameObject Row4;
    public GameObject Row5;
    string CalcType;
    public Text Title;
    public GameObject SideMenu;
    bool isMenu;

    public void Wake()
    {
        Manager = GameObject.Find("_Manager");
        SettingsClass Settings = Manager.GetComponent<FileManager>().Settings;
        ColorBlock colorBlock = new ColorBlock();
        colorBlock.normalColor = new Color((float)Settings.ButtonColor.Red / 255, (float)Settings.ButtonColor.Green / 255, (float)Settings.ButtonColor.Blue / 255, (float)Settings.ButtonColor.Alpha / 255);
        colorBlock.pressedColor = new Color(0, 0, 0);
        colorBlock.highlightedColor = new Color((float)Settings.ButtonHighlightColor.Red / 255, (float)Settings.ButtonHighlightColor.Green / 255, (float)Settings.ButtonHighlightColor.Blue / 255, (float)Settings.ButtonHighlightColor.Alpha / 255);
        colorBlock.selectedColor = new Color((float)Settings.ButtonHighlightColor.Red / 255, (float)Settings.ButtonHighlightColor.Green / 255, (float)Settings.ButtonHighlightColor.Blue / 255, (float)Settings.ButtonHighlightColor.Alpha / 255);
        colorBlock.disabledColor = new Color(1, 0, 0);
        colorBlock.colorMultiplier = 1;
        colorBlock.fadeDuration = 0.1f;
        this.GetComponent<Image>().color = new Color((float)Settings.PanelColor.Red / 255, (float)Settings.PanelColor.Green / 255, (float)Settings.PanelColor.Blue / 255, (float)Settings.PanelColor.Alpha / 255);
        foreach (Transform child in Row1.transform)
        {
            child.GetComponent<Button>().colors = colorBlock;
            child.GetComponent<Button>().GetComponentInChildren<Text>().color = new Color((float)Settings.ButtonTextColor.Red / 255, (float)Settings.ButtonTextColor.Green / 255, (float)Settings.ButtonTextColor.Blue / 255, (float)Settings.ButtonTextColor.Alpha / 255);
        }
        foreach (Transform child in Row2.transform)
        {
            child.GetComponent<Button>().colors = colorBlock;
            child.GetComponent<Button>().GetComponentInChildren<Text>().color = new Color((float)Settings.ButtonTextColor.Red / 255, (float)Settings.ButtonTextColor.Green / 255, (float)Settings.ButtonTextColor.Blue / 255, (float)Settings.ButtonTextColor.Alpha / 255);
        }
        foreach (Transform child in Row3.transform)
        {
            child.GetComponent<Button>().colors = colorBlock;
            child.GetComponent<Button>().GetComponentInChildren<Text>().color = new Color((float)Settings.ButtonTextColor.Red / 255, (float)Settings.ButtonTextColor.Green / 255, (float)Settings.ButtonTextColor.Blue / 255, (float)Settings.ButtonTextColor.Alpha / 255);
        }
        foreach (Transform child in Row4.transform)
        {
            child.GetComponent<Button>().colors = colorBlock;
            child.GetComponent<Button>().GetComponentInChildren<Text>().color = new Color((float)Settings.ButtonTextColor.Red / 255, (float)Settings.ButtonTextColor.Green / 255, (float)Settings.ButtonTextColor.Blue / 255, (float)Settings.ButtonTextColor.Alpha / 255);
        }
        foreach (Transform child in Row5.transform)
        {
            child.GetComponent<Button>().colors = colorBlock;
            child.GetComponent<Button>().GetComponentInChildren<Text>().color = new Color((float)Settings.ButtonTextColor.Red / 255, (float)Settings.ButtonTextColor.Green / 255, (float)Settings.ButtonTextColor.Blue / 255, (float)Settings.ButtonTextColor.Alpha / 255);
        }
        foreach (Transform child in Screen.transform)
        {
            if (child.gameObject.name != "Border")
            {
                child.GetComponentsInChildren<Text>()[0].color = new Color((float)Settings.PanelTextColor.Red / 255, (float)Settings.PanelTextColor.Green / 255, (float)Settings.PanelTextColor.Blue / 255, (float)Settings.PanelTextColor.Alpha / 255);
            }
        }
        foreach (Transform child in TopMenu.transform)
        {
            if (child.name == "Menu")
            {
                child.GetComponent<Button>().colors = colorBlock;
            }
        }
        SideMenu.GetComponent<Image>().color = new Color((float)Settings.PanelColor.Red / 255, (float)Settings.PanelColor.Green / 255, (float)Settings.PanelColor.Blue / 255, (float)Settings.PanelColor.Alpha / 255);
        Title.color = new Color((float)Settings.ButtonTextColor.Red / 255, (float)Settings.ButtonTextColor.Green / 255, (float)Settings.ButtonTextColor.Blue / 255, (float)Settings.ButtonTextColor.Alpha / 255);
        isMenu = false;
        SideMenu.SetActive(false);
        Clear();
    }

    public void Menu()
    {
        switch (isMenu)
        {
            case false:
                SideMenu.SetActive(true);
                isMenu = true;
                break;

            case true:
                SideMenu.SetActive(false);
                isMenu = false;
                break;
        }
    }

    void Clear()
    {
        Operator = "None";
        hasDecimal = false;
        Value1Neg = false;
        Value2Neg = false;
        Value1Str = "0";
        Value1 = 0f;
        Value2 = 0f;
        Value3 = 0f;
        hasMovedUp = false;
        isDone = false;
        Render();
    }

    void Finish()
    {
        hasMovedUp = false;
        Main.text = Value3.ToString();
        Operator = "None";
    }

    public void Calculate()
    {
        if (Operator != "None")
        {
            Temp3 = Operator;
        }
        if (isDone)
        {
            string Temp1 = Value1Str;
            string Temp2 = Value3.ToString();
            Clear();
            Value1Str = Temp2;
            Operator = Temp3;
            MoveUp();
            Value1Str = Temp1;
        }
        Value1 = double.Parse(Value1Str);
        if (Value1Neg)
        {
            Value1 *= -1;
        }
        double Answer;
        switch (Operator)
        {
            case "Plus":
                Answer = Value2 + Value1;
                Value3 = Answer;
                Finish();
                break;
            case "Minus":
                Answer = Value2 - Value1;
                Value3 = Answer;
                Finish();
                break;
            case "Multiply":
                Answer = Value2 * Value1;
                Value3 = Answer;
                Finish();
                break;
            case "Divide":
                if (Value1 == 0 || Value2 == 0)
                {
                    Main.text = "Cannot divide by zero";
                }
                else
                {
                    Answer = Value2 / Value1;
                    Value3 = Answer;
                    Finish();
                }
                break;
            case "None":
                Value3 = Value1;
                Finish();
                break;
            default:
                Main.text = "Error";
                break;
        }
        isDone = true;
        Render();
    }

    public void InputValue(string Input)
    {
        if (!isDone)
        {
            if (hasDecimal)
            {
                Value1Str += "." + Input;
                hasDecimal = false;
            }
            else
            {
                if (Value1Str != "0")
                {
                    Value1Str += Input;
                }
                else
                {
                    Value1Str = Input;
                }
            }
            Render();
        }
        else
        {
            Clear();
            if (Input == "0")
            {
                Value1Str = "0";
            }
            else
            {
                Value1Str = Input;
            }
            Render();
        }
    }

    public void InputFunction(string Input)
    {
        switch (Input)
        {
            case "Negative":
                if (!isDone)
                {
                    Value1Neg = !Value1Neg;
                }
                else
                {
                    double temp = Value3;
                    Clear();
                    Value1Str = temp.ToString();
                    Value1Neg = !Value1Neg;
                }
                break;
            case ".":
                if (isDone)
                {
                    Clear();
                }
                hasDecimal = true;
                break;
            case "Clear":
                Clear();
                break;
            case "ClearEntry":
                if (!isDone)
                {
                    if (Value1Str != "0")
                    {
                        Value1Neg = false;
                        Value1Str = "0";
                    }
                    else
                    {
                        Clear();
                    }
                }
                else
                {
                    Clear();
                }
                break;
            case "Backspace":
                if (!isDone)
                {
                    if (Value1Str.Length != 1)
                    {
                        Value1Str = Value1Str.Substring(0, Value1Str.Length - 1);
                    }
                    else
                    {
                        Value1Str = "0";
                    }
                }
                else
                {
                    Clear();
                }
                break;
        }
        Render();
    }

    public void InputOperator(string Input)
    {
        if (Operator != "None")
        {
            Calculate();
        }
        if (isDone)
        {
            Value1Str = Value3.ToString();
            isDone = false;
        }
        Operator = Input;
        if (!hasMovedUp)
        {
            MoveUp();
        }
        Render();
    }

    void MoveUp()
    {
        hasMovedUp = true;
        try
        {
            Value1 = Double.Parse(Value1Str);
        }
        catch (OverflowException)
        {
            Value1 = 0;
            Operator = "Error";
        }
        Value1Str = "0";
        if (!Value1Neg)
        {
            Value2 = Value1;
        }
        else
        {
            Value2 = Value1 * -1;
        }

        Value2Neg = Value1Neg;
        Value1Neg = false;
        Value1 = 0;
        Render();
    }

    void Render()
    {
        string Temp;
        if (!isDone)
        {
            if (!Value1Neg)
            {
                Main.text = Value1Str;
            }
            else
            {
                Main.text = "-" + Value1Str;
            }
        }
        if (Operator != "None")
        {
            Temp = Value2.ToString();
            switch (Operator)
            {
                case "Plus":
                    Temp += " + ";
                    break;
                case "Minus":
                    Temp += " - ";
                    break;
                case "Multiply":
                    Temp += " × ";
                    break;
                case "Divide":
                    Temp += " ÷ ";
                    break;
                case "Error":
                    Temp = "";
                    Main.text = "Error";
                    break;
            }
            if (isDone)
            {
                Temp += Value1 + " =";
            }
            Secondary.text = Temp;
        }
        else
        {
            if (isDone)
            {
                Secondary.text += Value1 + " = ";
            }
            else
            {
                Secondary.text = "";
            }
        }
        if (hasDecimal)
        {
            Main.text += ".";
        }
    }

    public void Minimize()
    {
        if (!isMinimized)
        {
            isMinimized = true;
            TopMenu.SetActive(false);
            Screen.SetActive(false);
            Buttons.SetActive(false);
            Border1.SetActive(false);
            Border2.SetActive(false);
            SideMenu.SetActive(false);
            isMenu = false;
            CalcRT.sizeDelta = new Vector2(CalcRT.sizeDelta.x, 55);
            CalcProgram.transform.position = new Vector2(CalcProgram.transform.position.x, CalcProgram.transform.position.y + (297.5f * ResolutionManager.ScreenOffsetH));
        }
    }

    public void Maximize()
    {
        if (isMinimized)
        {
            isMinimized = false;
            TopMenu.SetActive(true);
            Screen.SetActive(true);
            Buttons.SetActive(true);
            Border1.SetActive(true);
            Border2.SetActive(true);
            CalcRT.sizeDelta = new Vector2(CalcRT.sizeDelta.x, 650);
            CalcProgram.transform.position = new Vector2(CalcProgram.transform.position.x, CalcProgram.transform.position.y - (297.5f * ResolutionManager.ScreenOffsetH));
        }
    }

    public void Close()
    {
        GameObject.Destroy(CalcProgram);
    }
}
