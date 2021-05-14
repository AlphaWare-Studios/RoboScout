using UnityEngine;
using UnityEngine.UI;

public class QuestionType : MonoBehaviour
{
    public GameObject Manager;
    public Text TypeText;
    public GameObject StringQuestion;
    public GameObject IntQuestion;
    public GameObject BoolQuestion;
    public GameObject Buttonx2Question;
    public GameObject Buttonx3Question;
    public GameObject MenuQuestion;
    public int TypeInt;
    public int Value;
    public bool isDone;

    void Start()
    {
        isDone = false;
        Manager = GameObject.Find("_Manager");
        SettingsClass Settings = Manager.GetComponent<FileManager>().Settings;
        ColorBlock colorBlock = new ColorBlock
        {
            normalColor = new Color((float)Settings.ButtonColor.Red / 255, (float)Settings.ButtonColor.Green / 255, (float)Settings.ButtonColor.Blue / 255, (float)Settings.ButtonColor.Alpha / 255),
            pressedColor = new Color(0, 0, 0),
            highlightedColor = new Color((float)Settings.ButtonHighlightColor.Red / 255, (float)Settings.ButtonHighlightColor.Green / 255, (float)Settings.ButtonHighlightColor.Blue / 255, (float)Settings.ButtonHighlightColor.Alpha / 255),
            selectedColor = new Color(1, 1, 1),
            disabledColor = new Color(1, 0, 0),
            colorMultiplier = 1,
            fadeDuration = 0.1f
        };
        this.gameObject.transform.Find("Type").GetComponent<Button>().colors = colorBlock;
        TypeText.color = new Color((float)Settings.TextColor.Red / 255, (float)Settings.TextColor.Green / 255, (float)Settings.TextColor.Blue / 255, (float)Settings.TextColor.Alpha / 255);
        foreach (Transform child in this.transform)
        {
            if (child.name != "Type" && child.name != "QuestionType" && child.name != "ValueTracker")
            {
                foreach (Transform childchild in child.transform)
                {
                    childchild.GetComponent<InputField>().colors = colorBlock;
                    foreach (Transform childchildchild in childchild.transform)
                    {
                        if (childchildchild.name != "Question Input Caret" && childchildchild.name != "Button1 Input Caret" && childchildchild.name != "Button2 Input Caret" && childchildchild.name != "Button3 Input Caret")
                        {
                            childchildchild.GetComponent<Text>().color = new Color((float)Settings.ButtonTextColor.Red / 255, (float)Settings.ButtonTextColor.Green / 255, (float)Settings.ButtonTextColor.Blue / 255, (float)Settings.ButtonTextColor.Alpha / 255);
                        }
                    }
                }
            }
        }
        CLS();
        ShowType();
        isDone = true;
    }

    public string[] GetData()
    {
        string Question;
        string Button1;
        string Button2;
        string Button3;
        string[] Data = new string[2];
        switch (TypeInt)
        {
            case 0:
                Question = MenuQuestion.transform.Find("Question").GetComponent<InputField>().text;
                Data[0] = Question;
                break;
            case 1:
                Question = StringQuestion.transform.Find("Question").GetComponent<InputField>().text;
                Data[0] = Question;
                break;
            case 2:
                Question = IntQuestion.transform.Find("Question").GetComponent<InputField>().text;
                Data[0] = Question;
                break;
            case 3:
                Question = BoolQuestion.transform.Find("Question").GetComponent<InputField>().text;
                Data[0] = Question;
                break;
            case 4:
                Question = Buttonx2Question.transform.Find("Question").GetComponent<InputField>().text;
                Button1 = Buttonx2Question.transform.Find("Button1").GetComponent<InputField>().text;
                Button2 = Buttonx2Question.transform.Find("Button2").GetComponent<InputField>().text;
                Data[0] = Question + "|" + Button1 + "|" + Button2;
                break;
            case 5:
                Question = Buttonx3Question.transform.Find("Question").GetComponent<InputField>().text;
                Button1 = Buttonx3Question.transform.Find("Button1").GetComponent<InputField>().text;
                Button2 = Buttonx3Question.transform.Find("Button2").GetComponent<InputField>().text;
                Button3 = Buttonx3Question.transform.Find("Button3").GetComponent<InputField>().text;
                Data[0] = Question + "|" + Button1 + "|" + Button2 + "|" + Button3;
                break;
            default:
                Data[0] = "Error";
                Manager.GetComponent<ErrorManager>().Warning("Unknown Question type, Some questions may have failed to save");
                break;
        }
        return Data;
    }

    public void SetData(int Type, string Data)
    {
        TypeInt = Type;
        CLS();
        ShowType();
        switch (TypeInt)
        {
            case 0:
                MenuQuestion.transform.Find("Question").GetComponent<InputField>().text = Data;
                break;
            case 1:
                StringQuestion.transform.Find("Question").GetComponent<InputField>().text = Data;
                break;
            case 2:
                IntQuestion.transform.Find("Question").GetComponent<InputField>().text = Data;
                break;
            case 3:
                BoolQuestion.transform.Find("Question").GetComponent<InputField>().text = Data;
                break;
            case 4:
                string[] SDataX2 = Data.Split('|');
                try
                {
                    Buttonx2Question.transform.Find("Question").GetComponent<InputField>().text = SDataX2[0];
                    Buttonx2Question.transform.Find("Button1").GetComponent<InputField>().text = SDataX2[1];
                    Buttonx2Question.transform.Find("Button2").GetComponent<InputField>().text = SDataX2[2];
                }
                catch
                {
                    Manager.GetComponent<ErrorManager>().Warning("Error spliting multidata, Some questions may have failed to load");
                }
                break;
            case 5:
                try
                {
                    string[] SDataX3 = Data.Split('|');
                    Buttonx3Question.transform.Find("Question").GetComponent<InputField>().text = SDataX3[0];
                    Buttonx3Question.transform.Find("Button1").GetComponent<InputField>().text = SDataX3[1];
                    Buttonx3Question.transform.Find("Button2").GetComponent<InputField>().text = SDataX3[2];
                    Buttonx3Question.transform.Find("Button3").GetComponent<InputField>().text = SDataX3[3];
                }
                catch
                {
                    Manager.GetComponent<ErrorManager>().Warning("Error spliting multidata, Some questions may have failed to load");
                }
                break;
            default:
                Manager.GetComponent<ErrorManager>().Warning("Unknown Question type, Some questions may have failed to load");
                break;
        }
    }

    public void TypeChange()
    {
        TypeInt++;
        if (TypeInt > 5)
        {
            TypeInt = 0;
        }
        CLS();
        ShowType();
    }

    void CLS()
    {
        StringQuestion.SetActive(false);
        IntQuestion.SetActive(false);
        BoolQuestion.SetActive(false);
        Buttonx2Question.SetActive(false);
        Buttonx3Question.SetActive(false);
        MenuQuestion.SetActive(false);
    }
    
    void ShowType()
    {
        switch(TypeInt)
        {
            case 0:
                MenuQuestion.SetActive(true);
                TypeText.text = "Title";
                break;
            case 1:
                StringQuestion.SetActive(true);
                TypeText.text = "Text";
                break;
            case 2:
                IntQuestion.SetActive(true);
                TypeText.text = "Number";
                break;
            case 3:
                BoolQuestion.SetActive(true);
                TypeText.text = "Toggle";
                break;
            case 4:
                Buttonx2Question.SetActive(true);
                TypeText.text = "Button x2";
                break;
            case 5:
                Buttonx3Question.SetActive(true);
                TypeText.text = "Button x3";
                break;
            default:
                TypeText.text = "Unknown";
                break;
        }   
    }
}
