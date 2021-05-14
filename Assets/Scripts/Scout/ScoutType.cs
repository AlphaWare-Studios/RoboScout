using UnityEngine;
using UnityEngine.UI;

public class ScoutType : MonoBehaviour
{
    public GameObject Manager;
    public Text QuestionText;
    public Text AnswerText;
    public GameObject MenuQuestion;
    public GameObject StringQuestion;
    public GameObject IntQuestion;
    public GameObject BoolQuestion;
    public GameObject Buttonx2Question;
    public GameObject Buttonx3Question;
    public string QuestionData;
    public int TypeInt;
    public bool isDone;

    void Start()
    {
        isDone = false;
        QuestionData = "";
        Manager = GameObject.Find("_Manager");
        SettingsClass Settings = Manager.GetComponent<FileManager>().Settings;
        ColorBlock colorBlock = new ColorBlock
        {
            normalColor = new Color((float)Settings.ButtonColor.Red / 255, (float)Settings.ButtonColor.Green / 255, (float)Settings.ButtonColor.Blue / 255, (float)Settings.ButtonColor.Alpha / 255),
            pressedColor = new Color(0, 0, 0),
            highlightedColor = new Color((float)Settings.ButtonHighlightColor.Red / 255, (float)Settings.ButtonHighlightColor.Green / 255, (float)Settings.ButtonHighlightColor.Blue / 255, (float)Settings.ButtonHighlightColor.Alpha / 255),
            selectedColor = new Color((float)Settings.ButtonHighlightColor.Red / 255, (float)Settings.ButtonHighlightColor.Green / 255, (float)Settings.ButtonHighlightColor.Blue / 255, (float)Settings.ButtonHighlightColor.Alpha / 255),
            disabledColor = new Color(1, 0, 0),
            colorMultiplier = 1,
            fadeDuration = 0.1f
        };
        QuestionText.color = new Color((float)Settings.TextColor.Red / 255, (float)Settings.TextColor.Green / 255, (float)Settings.TextColor.Blue / 255, (float)Settings.TextColor.Alpha / 255);
        AnswerText.color = new Color((float)Settings.TextColor.Red / 255, (float)Settings.TextColor.Green / 255, (float)Settings.TextColor.Blue / 255, (float)Settings.TextColor.Alpha / 255);
        MenuQuestion.transform.Find("Question").GetComponent<Text>().color = new Color((float)Settings.TextColor.Red / 255, (float)Settings.TextColor.Green / 255, (float)Settings.TextColor.Blue / 255, (float)Settings.TextColor.Alpha / 255);
        StringQuestion.transform.Find("Question").GetComponent<InputField>().colors = colorBlock;
        StringQuestion.transform.Find("Question").transform.Find("Placeholder").GetComponent<Text>().color = new Color((float)Settings.ButtonTextColor.Red / 255, (float)Settings.ButtonTextColor.Green / 255, (float)Settings.ButtonTextColor.Blue / 255, (float)Settings.ButtonTextColor.Alpha / 255);
        StringQuestion.transform.Find("Question").transform.Find("Text").GetComponent<Text>().color = new Color((float)Settings.ButtonTextColor.Red / 255, (float)Settings.ButtonTextColor.Green / 255, (float)Settings.ButtonTextColor.Blue / 255, (float)Settings.ButtonTextColor.Alpha / 255);
        IntQuestion.transform.Find("Minus").GetComponent<Button>().colors = colorBlock;
        IntQuestion.transform.Find("Stats").GetComponent<InputField>().colors = colorBlock;
        IntQuestion.transform.Find("Plus").GetComponent<Button>().colors = colorBlock;
        IntQuestion.transform.Find("Stats").transform.Find("Placeholder").GetComponent<Text>().color = new Color((float)Settings.ButtonTextColor.Red / 255, (float)Settings.ButtonTextColor.Green / 255, (float)Settings.ButtonTextColor.Blue / 255, (float)Settings.ButtonTextColor.Alpha / 255);
        IntQuestion.transform.Find("Stats").transform.Find("Text").GetComponent<Text>().color = new Color((float)Settings.ButtonTextColor.Red / 255, (float)Settings.ButtonTextColor.Green / 255, (float)Settings.ButtonTextColor.Blue / 255, (float)Settings.ButtonTextColor.Alpha / 255);
        BoolQuestion.transform.Find("Question").GetComponent<Toggle>().colors = colorBlock;
        Buttonx2Question.transform.Find("Button1").GetComponent<Button>().colors = colorBlock;
        Buttonx2Question.transform.Find("Button2").GetComponent<Button>().colors = colorBlock;
        Buttonx2Question.transform.Find("Button1").transform.Find("Text").GetComponent<Text>().color = new Color((float)Settings.ButtonTextColor.Red / 255, (float)Settings.ButtonTextColor.Green / 255, (float)Settings.ButtonTextColor.Blue / 255, (float)Settings.ButtonTextColor.Alpha / 255);
        Buttonx2Question.transform.Find("Button2").transform.Find("Text").GetComponent<Text>().color = new Color((float)Settings.ButtonTextColor.Red / 255, (float)Settings.ButtonTextColor.Green / 255, (float)Settings.ButtonTextColor.Blue / 255, (float)Settings.ButtonTextColor.Alpha / 255);
        Buttonx3Question.transform.Find("Button1").GetComponent<Button>().colors = colorBlock;
        Buttonx3Question.transform.Find("Button2").GetComponent<Button>().colors = colorBlock;
        Buttonx3Question.transform.Find("Button3").GetComponent<Button>().colors = colorBlock;
        Buttonx3Question.transform.Find("Button1").transform.Find("Text").GetComponent<Text>().color = new Color((float)Settings.ButtonTextColor.Red / 255, (float)Settings.ButtonTextColor.Green / 255, (float)Settings.ButtonTextColor.Blue / 255, (float)Settings.ButtonTextColor.Alpha / 255);
        Buttonx3Question.transform.Find("Button2").transform.Find("Text").GetComponent<Text>().color = new Color((float)Settings.ButtonTextColor.Red / 255, (float)Settings.ButtonTextColor.Green / 255, (float)Settings.ButtonTextColor.Blue / 255, (float)Settings.ButtonTextColor.Alpha / 255);
        Buttonx3Question.transform.Find("Button3").transform.Find("Text").GetComponent<Text>().color = new Color((float)Settings.ButtonTextColor.Red / 255, (float)Settings.ButtonTextColor.Green / 255, (float)Settings.ButtonTextColor.Blue / 255, (float)Settings.ButtonTextColor.Alpha / 255);
        isDone = true;
    }

    public void SetData(int Type, string Data)
    {
        TypeInt = Type;
        CLS();
        ShowType();
        switch (TypeInt)
        {
            case 0:
                QuestionText.text = "";
                MenuQuestion.transform.Find("Question").GetComponent<Text>().text = Data;
                break;
            case 1:
                QuestionText.text = Data;
                break;
            case 2:
                QuestionText.text = Data;
                break;
            case 3:
                QuestionText.text = Data;
                if (Data == "true")
                {
                    BoolQuestion.transform.Find("Question").GetComponent<Toggle>().isOn = true;
                }
                else
                {
                    BoolQuestion.transform.Find("Question").GetComponent<Toggle>().isOn = false;
                }
                break;
            case 4:
                string[] SDataX2 = Data.Split('|');
                QuestionText.text = SDataX2[0];
                Buttonx2Question.transform.Find("Button1").transform.Find("Text").GetComponent<Text>().text = SDataX2[1];
                Buttonx2Question.transform.Find("Button2").transform.Find("Text").GetComponent<Text>().text = SDataX2[2];
                break;
            case 5:
                string[] SDataX3 = Data.Split('|');
                QuestionText.text = SDataX3[0];
                Buttonx3Question.transform.Find("Button1").transform.Find("Text").GetComponent<Text>().text = SDataX3[1];
                Buttonx3Question.transform.Find("Button2").transform.Find("Text").GetComponent<Text>().text = SDataX3[2];
                Buttonx3Question.transform.Find("Button3").transform.Find("Text").GetComponent<Text>().text = SDataX3[3];
                break;
            default:
                QuestionText.text = Data;
                Manager.GetComponent<ErrorManager>().Warning("Unknown Question type, Some questions may have failed to load");
                break;
        }
    }

    public string GetData()
    {
        string Data;
        switch (TypeInt)
        {
            case 0:
                Data = MenuQuestion.transform.Find("Question").GetComponent<Text>().text;
                break;
            case 1:
                Data = QuestionText.text;
                break;
            case 2:
                Data = QuestionText.text;
                break;
            case 3:
                Data = QuestionText.text;
                break;
            case 4:
                string[] SDataX2 = new string[4];
                SDataX2[0] = QuestionText.text;
                SDataX2[1] = Buttonx2Question.transform.Find("Button1").transform.Find("Text").GetComponent<Text>().text;
                SDataX2[2] = Buttonx2Question.transform.Find("Button2").transform.Find("Text").GetComponent<Text>().text;
                Data = SDataX2[0] + "|" + SDataX2[1] + "|" + SDataX2[2];
                break;
            case 5:
                string[] SDataX3 = new string[4];
                SDataX3[0] = QuestionText.text;
                SDataX3[1] = Buttonx3Question.transform.Find("Button1").transform.Find("Text").GetComponent<Text>().text;
                SDataX3[2] = Buttonx3Question.transform.Find("Button2").transform.Find("Text").GetComponent<Text>().text;
                SDataX3[3] = Buttonx3Question.transform.Find("Button3").transform.Find("Text").GetComponent<Text>().text;
                Data = SDataX3[0] + "|" + SDataX3[1] + "|" + SDataX3[2] + "|" + SDataX3[3];
                break;
            default:
                Data = QuestionData;
                Manager.GetComponent<ErrorManager>().Warning("Unknown Question type, Some questions may have failed to save");
                break;
        }
        return Data;
    }

    public void SetValue(string Data)
    {
        switch (TypeInt)
        {
            case 0:
                break;
            case 1:
                StringQuestion.transform.Find("Question").GetComponent<InputField>().text = Data;
                break;
            case 2:
                IntQuestion.transform.Find("Stats").GetComponent<InputField>().text = Data;
                break;
            case 3:
                if (Data == "true")
                {
                    BoolQuestion.transform.Find("Question").GetComponent<Toggle>().isOn = true;
                }
                else
                {
                    BoolQuestion.transform.Find("Question").GetComponent<Toggle>().isOn = false;
                }
                break;
            case 4:
                AnswerText.text = Data;
                break;
            case 5:
                AnswerText.text = Data;
                break;
            default:
                QuestionData = Data;
                AnswerText.text = Data;
                Manager.GetComponent<ErrorManager>().Warning("Unknown Question type, Some questions may have failed to load");
                break;
        }
    }

    public string GetValue()
    {
        string Question;
        string Data;
        switch (TypeInt)
        {
            case 0:
                Data = "";
                break;
            case 1:
                Question = StringQuestion.transform.Find("Question").GetComponent<InputField>().text;
                Data = Question;
                break;
            case 2:
                Question = IntQuestion.transform.Find("Stats").GetComponent<InputField>().text;
                Data = Question;
                break;
            case 3:
                if (BoolQuestion.transform.Find("Question").GetComponent<Toggle>().isOn)
                {
                    Question = "true";
                }
                else
                {
                    Question = "false";
                }
                Data = Question;
                break;
            case 4:
                Data = AnswerText.text;
                break;
            case 5:
                Data = AnswerText.text;
                break;
            default:
                Data = QuestionData;
                Manager.GetComponent<ErrorManager>().Warning("Unknown Question type, Some questions may have failed to save");
                break;
        }
        return Data;
    }

    public void Minus()
    {
        int Ammount = int.Parse(IntQuestion.transform.Find("Stats").GetComponent<InputField>().text);
        Ammount--;
        IntQuestion.transform.Find("Stats").GetComponent<InputField>().text = Ammount.ToString();
    }

    public void Plus()
    {
        int Ammount = int.Parse(IntQuestion.transform.Find("Stats").GetComponent<InputField>().text);
        Ammount++;
        IntQuestion.transform.Find("Stats").GetComponent<InputField>().text = Ammount.ToString();
    }

    public void Buttonx2B1()
    {
        QuestionData = Buttonx2Question.transform.Find("Button1").transform.Find("Text").GetComponent<Text>().text;
        AnswerText.text = QuestionData;
    }

    public void Buttonx2B2()
    {
        QuestionData = Buttonx2Question.transform.Find("Button2").transform.Find("Text").GetComponent<Text>().text;
        AnswerText.text = QuestionData;
    }

    public void Buttonx3B1()
    {
        QuestionData = Buttonx3Question.transform.Find("Button1").transform.Find("Text").GetComponent<Text>().text;
        AnswerText.text = QuestionData;
    }

    public void Buttonx3B2()
    {
        QuestionData = Buttonx3Question.transform.Find("Button2").transform.Find("Text").GetComponent<Text>().text;
        AnswerText.text = QuestionData;
    }

    public void Buttonx3B3()
    {
        QuestionData = Buttonx3Question.transform.Find("Button3").transform.Find("Text").GetComponent<Text>().text;
        AnswerText.text = QuestionData;
    }

    void CLS()
    {
        MenuQuestion.SetActive(false);
        StringQuestion.SetActive(false);
        IntQuestion.SetActive(false);
        BoolQuestion.SetActive(false);
        Buttonx2Question.SetActive(false);
        Buttonx3Question.SetActive(false);
    }

    void ShowType()
    {
        switch (TypeInt)
        {
            case 0:
                MenuQuestion.SetActive(true);
                AnswerText.text = "";
                break;
            case 1:
                StringQuestion.SetActive(true);
                AnswerText.text = "";
                break;
            case 2:
                IntQuestion.SetActive(true);
                AnswerText.text = "";
                break;
            case 3:
                BoolQuestion.SetActive(true);
                AnswerText.text = "";
                break;
            case 4:
                Buttonx2Question.SetActive(true);
                AnswerText.text = "Empty";
                break;
            case 5:
                Buttonx3Question.SetActive(true);
                AnswerText.text = "Empty";
                break;
            default:
                AnswerText.text = "Unknown";
                break;
        }
    }
}
