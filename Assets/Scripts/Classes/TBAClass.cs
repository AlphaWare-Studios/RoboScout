using System;

[Serializable]
public class TBAClass { //status

    public bool is_datafeed_down;
    public int current_season;
}

[Serializable]
public class TeamDataClass { //team/ID

    public int team_number;
    public string nickname;
    public int rookie_year;
    public string website;
    public string city;
    public string state_prov;
    public string country;
}

[Serializable]
public class TeamMediaClass { //team/ID/media/YEAR
    public TeamMediaClassData[] Stuff;
}

[Serializable]
public class TeamMediaClassData { 

    public TeamMediaDetailsClass details;
    public string foreign_key;
}

[Serializable]
public class TeamMediaDetailsClass {

    public string base64Image;
}
