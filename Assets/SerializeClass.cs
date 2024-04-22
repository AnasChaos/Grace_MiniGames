using System.Collections.Generic;


[System.Serializable]
public class Users 
{
    public List<UserProfile> userProfiles= new List<UserProfile>();
}

[System.Serializable]
public class UserProfile
{
    public string name;
    public string password;
    public string character;
    public string saveState;
}

[System.Serializable]
public class Steps
{
    public List<Step> steps = new List<Step>();
}

[System.Serializable]
public class Step
{
    public int step;
    public string dailouge = "";
    public bool isQueen;
    public bool isTask;
}