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
	public GameObject ButtonsUI;
	public GameObject Border1;
	public GameObject Border2;
	public bool isMinimized;
	public Text Main;
	public Text Secondary;
	public Text Up;
	public Text Down;
	public Dropdown UpDropdown;
	public Dropdown DownDropdown;
	public string Operator;
	string Temp3;
	public bool hasDecimalCalc;
	public bool hasDecimalTemp;
	public bool Value1Neg;
	public bool Value2Neg;
	public bool hasMovedUp;
	public string Value1Str;
	public double Value1;
	public double Value2;
	public double Value3;
	public bool isDone;
	public bool Temp1Neg;
	public string Temp1Str;
	public Text[] Texts = new Text[0];
	public Button[] Buttons = new Button[0];
	public InputField[] Fields = new InputField[0];
	public Toggle[] Toggles = new Toggle[0];
	public Dropdown[] Dropdowns = new Dropdown[0];
	public Image[] Images = new Image[0];
	public Text[] ButtonTexts = new Text[0];
	public Image[] Panels = new Image[0];
	public Text[] PanelTexts = new Text[0];
	string CalcType;
	public Text Title;
	public GameObject SideMenu;
	bool isMenu;
	public GameObject ScreenStandard;
	public GameObject ScreenTemperature;
	public GameObject ButtonsStandard;
	public GameObject ButtonsTemperature;

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
		foreach (Text Text in Texts)
		{
			if (Text != null)
				Text.color = new Color((float)Settings.TextColor.Red / 255, (float)Settings.TextColor.Green / 255, (float)Settings.TextColor.Blue / 255, (float)Settings.TextColor.Alpha / 255);
		}
		foreach (Button Button in Buttons)
		{
			if (Button != null)
				Button.colors = colorBlock;
		}
		foreach (InputField Field in Fields)
		{
			if (Field != null)
				Field.colors = colorBlock;
		}
		foreach (Toggle Toggle in Toggles)
		{
			if (Toggle != null)
				Toggle.colors = colorBlock;
		}
		foreach (Dropdown Dropdown in Dropdowns)
		{
			if (Dropdown != null)
				Dropdown.colors = colorBlock;
		}
		foreach (Image Image in Images)
		{
			if (Image != null)
				Image.color = new Color((float)Settings.ButtonColor.Red / 255, (float)Settings.ButtonColor.Green / 255, (float)Settings.ButtonColor.Blue / 255, (float)Settings.ButtonColor.Alpha / 255);
		}
		foreach (Text Text in ButtonTexts)
		{
			if (Text != null)
				Text.color = new Color((float)Settings.ButtonTextColor.Red / 255, (float)Settings.ButtonTextColor.Green / 255, (float)Settings.ButtonTextColor.Blue / 255, (float)Settings.ButtonTextColor.Alpha / 255);
		}
		foreach (Image Image in Panels)
		{
			if (Image != null)
				Image.color = new Color((float)Settings.PanelColor.Red / 255, (float)Settings.PanelColor.Green / 255, (float)Settings.PanelColor.Blue / 255, (float)Settings.PanelColor.Alpha / 255);
		}
		foreach (Text Text in PanelTexts)
		{
			if (Text != null)
				Text.color = new Color((float)Settings.PanelTextColor.Red / 255, (float)Settings.PanelTextColor.Green / 255, (float)Settings.PanelTextColor.Blue / 255, (float)Settings.PanelTextColor.Alpha / 255);
		}
		SetMenu("Standard");
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

	public void SetMenu(string Mode)
	{
		Clear();
		Menu();
		Title.text = Mode;
		CalcType = Mode;
		ScreenStandard.SetActive(false);
		ScreenTemperature.SetActive(false);
		ButtonsStandard.SetActive(false);
		ButtonsTemperature.SetActive(false);
		switch (Mode)
		{
			case "Standard":
				ScreenStandard.SetActive(true);
				ButtonsStandard.SetActive(true);
				break;

			case "Temperature":
				ScreenTemperature.SetActive(true);
				ButtonsTemperature.SetActive(true);
				break;

			default:
				break;
		}
	}

	void Clear()
	{
		Operator = "None";
		hasDecimalCalc = false;
		hasDecimalTemp = false;
		Value1Neg = false;
		Value2Neg = false;
		Value1Str = "0";
		Temp1Neg = false;
		Temp1Str = "0";
		Value1 = 0f;
		Value2 = 0f;
		Value3 = 0f;
		hasMovedUp = false;
		isDone = false;
		RenderCalc();
		RenderTemp();
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
		RenderCalc();
	}

	public void InputValue(string Input)
	{
		if (!isDone)
		{
			if (hasDecimalCalc)
			{
				Value1Str += "." + Input;
				hasDecimalCalc = false;
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
			RenderCalc();
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
			RenderCalc();
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
				hasDecimalCalc = true;
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
		RenderCalc();
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
		RenderCalc();
	}

	public void InputValueTemp(string Input)
	{
		if (hasDecimalTemp)
		{
			Temp1Str += "." + Input;
			hasDecimalTemp = false;
		}
		else
		{
			if (Temp1Str != "0")
			{
				Temp1Str += Input;
			}
			else
			{
				Temp1Str = Input;
			}
		}
		RenderTemp();
	}

	public void InputTempFunction(string Input)
	{
		switch (Input)
		{
			case "Negative":
				Temp1Neg = !Temp1Neg;
				break;
			case ".":
				hasDecimalTemp = true;
				break;
			case "ClearEntry":
				Temp1Neg = false;
				Temp1Str = "0";
				break;
			case "Backspace":
				if (Temp1Str.Length != 1)
				{
					Temp1Str = Temp1Str.Substring(0, Temp1Str.Length - 1);
				}
				else
				{
					Temp1Str = "0";
				}
				break;
		}
		RenderTemp();
	}

	public void TempDropdown()
    {
		RenderTemp();
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
		RenderCalc();
	}

	void RenderCalc()
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
		if (hasDecimalCalc)
		{
			Main.text += ".";
		}
	}

	void RenderTemp()
	{
		if (!Temp1Neg)
		{
			Up.text = Temp1Str;
		}
		else
		{
			Up.text = "-" + Temp1Str;
		}
		if (hasDecimalTemp)
		{
			Up.text += ".";
		}
		double Temp = double.Parse(Temp1Str);
		if (Temp1Neg)
		{
			Temp *= -1;
		}
		switch (UpDropdown.value, DownDropdown.value) //012FCK
        {
			case (0, 0):
				Down.text = Temp.ToString();
				break;
			case (0, 1):
				Down.text = ((Temp - 32) * 5/9).ToString();
				break;
			case (0, 2):
				Down.text = (((Temp - 32) * 5/9) + 273.15).ToString();
				break;
			case (1, 0):
				Down.text = ((Temp * 9/5) + 32).ToString();
				break;
			case (1, 1):
				Down.text = Temp.ToString();
				break;
			case (1, 2):
				Down.text = (Temp + 273.15).ToString();
				break;
			case (2, 0):
				Down.text = (((Temp - 273.15) * 9/5) + 32).ToString();
				break;
			case (2, 1):
				Down.text = (Temp + 273.15).ToString();
				break;
			case (2, 2):
				Down.text = Temp.ToString();
				break;
		}
	}

	public void Minimize()
	{
		if (!isMinimized)
		{
			isMinimized = true;
			TopMenu.SetActive(false);
			Screen.SetActive(false);
			ButtonsUI.SetActive(false);
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
			ButtonsUI.SetActive(true);
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
