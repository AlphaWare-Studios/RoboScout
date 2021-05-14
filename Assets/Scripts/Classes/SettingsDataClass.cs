using System;

[Serializable]
public class SettingsClass
{
    public string SaveVersion;
    public string Version;
    public int TeamNumber;
    public string ScouterName;
    public bool isBetaEnabled;
    public float DisplayScaling;
    public float Volume;
    public bool is24Hour;
    public bool isTooltipsEnabled;
    public int QuestionAmmount;
    public int[] QuestionType;
    public string[] Questions;
    public int[] QuestionValue;
    public string Language;
    public string UIStyle;
    public SettingsInterfaceColors TitleColor;
    public SettingsInterfaceColors BackColor;
    public SettingsInterfaceColors TextColor;
    public SettingsInterfaceColors ButtonColor;
    public SettingsInterfaceColors ButtonTextColor;
    public SettingsInterfaceColors ButtonHighlightColor;
    public SettingsInterfaceColors PanelColor;
    public SettingsInterfaceColors PanelTextColor;
}

[Serializable]
public class SettingsInterfaceColors
{
    public int Red;
    public int Green;
    public int Blue;
    public int Alpha;
}

