using System;

[Serializable]
public class AWClass
{
    public string LatestVersion;
    public string LatestBetaVersion;
    public News News1;
    public News News2;
    public News News3;
    public News News4;
    public News News5;
}

[Serializable]
public class News
{
    public string Data;
    public string Title;
    public string Desc;
    public string Link;
}