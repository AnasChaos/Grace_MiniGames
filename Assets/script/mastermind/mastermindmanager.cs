using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mastermindmanager : MonoBehaviour
{


    bool started;
    bool win;
    public GameObject pannel;
    [SerializeField] public List<mastercolors> colors;//options
    private List<mastercolors> currentcolors;//question list 
    public int options;
    int currentattempts;
    public int maxattempts;
    public GameObject[] colorsholders;
    public GameObject[] ansholder;
    public mastercolors color;
    int ii;
    bool[] corr= new bool[4];

    void getrandomquestion()
    {
        int randomquestionindex;
        mastercolors newQuestion;


        for (int i = 0; i < options; i++)
        {

            randomquestionindex = Random.Range(0, colors.Count);
            newQuestion = colors[randomquestionindex];
            currentcolors.Add(newQuestion);
        }

    }

    public void submit()
    {
        if (!win)
        {
            bool allcorrects = true;
            ii = 0;
            GameObject holder = colorsholders[currentattempts];
            GameObject holderbulbs = ansholder[currentattempts];

            foreach (Transform child in holder.transform)// checks first if any option is correct first
            {
                //color = child.transform.GetComponent<colorsholders>().colors;
                color = child.transform.GetComponentInChildren<colorsolts>().colors;
                Debug.Log(color);
                if (currentcolors[ii].color == color.color)
                {
                    Debug.Log("correct");
                    currentcolors[ii].corrects = true;
                    corr[ii] = true;
                    holderbulbs.transform.GetChild(ii).GetComponent<Image>().color = new Color32(0, 255, 0, 255);
                }
                else
                {
                    allcorrects = false;
                    holderbulbs.transform.GetChild(ii).GetComponent<Image>().color = new Color32(0, 0, 0, 255);
                }


                ii += 1;
            }
            if (allcorrects)
            {
                win = true;
                pannel.SetActive(true);
                return;
            }


            ii = 0;
            foreach (Transform child in holder.transform)//checks if correct correct color and wrong place or not 
            {
                color = child.transform.GetComponentInChildren<colorsolts>().colors;
                for (int i = 0; i < currentcolors.Count; i++)
                {
                    if (color.color == currentcolors[i].color && !currentcolors[i].corrects && !corr[i])
                    {
                        holderbulbs.transform.GetChild(ii).GetComponent<Image>().color = new Color32(255, 0, 0, 255);
                        corr[i] = true;
                        break; // Break the loop once a correct color in the wrong position is found for the current slot
                    }
                }
                ii += 1;
            }

            for (int i = 0; i < corr.Length; i++)
            {
                corr[i] = false;
                currentcolors[i].corrects = false;
            }
            currentattempts += 1;
            holder.GetComponent<CanvasGroup>().blocksRaycasts = false;
            colorsholders[currentattempts].GetComponent<CanvasGroup>().blocksRaycasts = true;

        }
    }


    // Start is called before the first frame update
    void Start()
    {
        started = true;
        currentcolors = new List<mastercolors>();
        getrandomquestion();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
