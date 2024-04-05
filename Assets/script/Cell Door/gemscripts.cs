using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class gemscripts 
{
    public gemtype notes;
    public List<string> questioninfo = new List<string>();
    public List<string> correctans = new List<string>();
}
[System.Serializable]
public enum gemtype
{
    high,
    low
}