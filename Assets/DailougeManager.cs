using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class DailougeManager : MonoBehaviour
{
    [SerializeField] Dailogues dailogues;
    private void Start()
    {
        Debug.Log(Resources.Load<TextAsset>("Dialouges").ToString());
        string steps = Resources.Load<TextAsset>("Dialouges").ToString();
        Debug.Log(steps);
        dailogues = JsonConvert.DeserializeObject<Dailogues>(steps); 
    }
}


[System.Serializable]
public class Dailogues
{
    public List<Step> steps = new List<Step>();
}

[System.Serializable]
public class Step
{
    public int step;
    public string dailouge = "";
}