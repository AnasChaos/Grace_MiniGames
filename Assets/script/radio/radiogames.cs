using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class radiogames : MonoBehaviour
{
    bool started;
    bool win;
    bool allcorrect;
    [SerializeField] public Dictionary<int, string > variableDictionary = new Dictionary<int, string>();
    public int[] answers;
    int[] currentChoices = new int[5];
    public GameObject pannel;
    public Image[] ims;
    public GameObject[] but;

    public void submits()
    {
        allcorrect = true;
        for (int i = 0; i < currentChoices.Length; i++)
        {
            if (currentChoices[i] != answers[i])
            {
                allcorrect = false;
                ims[i].color = new Color32(255, 0, 0, 255);

            }
            else
            {
                ims[i].color = new Color32(0, 255, 0, 255);
            }
        }
        if (allcorrect)
        {
            pannel.SetActive(true);
        }

    }

    public void setvalue(int bu)
    {
        if (currentChoices[bu] == 3)
        {
            currentChoices[bu] = 0;
            LeanTween.rotateZ(but[bu], -360, 0.5f).setOnComplete(() => LeanTween.rotateZ(but[bu], 0, 0));
        }
        else
        {
            LeanTween.rotateAroundLocal(but[bu], Vector3.forward, -90, 0.5f);
            currentChoices[bu] += 1;
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        variableDictionary[0] = "north";
        variableDictionary[1] = "east";
        variableDictionary[2] = "south";
        variableDictionary[3] = "west";
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
