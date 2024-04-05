using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class quizs 
{
    public string questioninfo;
    public questiontype questiontype;
    public Sprite questionimage;
    public AudioClip AudioClip;
    public UnityEngine.Video.VideoClip VideoClip;
    public List<string> options;
    public string correctans;
    public List<Sprite> answersimage;

}
[System.Serializable]
public enum questiontype
{
    truefals,
    text,
    imageanswer,
    image,
    video,
    audio
}

