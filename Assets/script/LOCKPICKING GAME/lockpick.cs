using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class lockpick : MonoBehaviour
{
    bool started;
    bool win;
    bool pinmove;
    public int[] locations;
    public Image[] icons;
    private int currentlocaion;
    public GameObject pannel;
    public GameObject pin;
    public Animator pinan;
    public Animator[] lockpinan;
    public int options;
    [SerializeField] public List<tunnelletters> nodes;
    private List<tunnelletters> currentnodes;
    tunnelletters previouspin;
    public float[] currentlockpinvalue;
    public int maxrounds;
    int rounds;
    public GameObject roundpannel;
    [SerializeField] private TMP_Text roundtext;



    public void wins()
    {
        pannel.SetActive(true);
    }

    public void round()
    {
        rounds += 1;
        if (rounds >= maxrounds)
        {
            wins();
            return;
        }

        roundpannel.SetActive(true);
        roundtext.text = "Round " + rounds.ToString() + " Completed";
        Invoke("closed", 2);
        resets();
    }
    public  void closed()
    {
        roundpannel.SetActive(false);
    }

    public void resets() 
    {
        currentnodes = null;
        changelocation(0);
        currentnodes = new List<tunnelletters>();
        getrandomquestion();
        for (int i = 0; i < lockpinan.Length; i++)
        {
            lockpinan[i].Play("0");
        }
        for (int i = 0; i < currentlockpinvalue.Length; i++)
        {
            currentlockpinvalue[i] = 0;
        }
        changelocation(0);
    }


    void getrandomquestion()
    {
        int randomquestionindex;
        tunnelletters newQuestion;


        for (int i = 0; i<options; i++)
        {
            do
            {
                randomquestionindex = Random.Range(0, nodes.Count);
                newQuestion = nodes[randomquestionindex];
            } while (newQuestion == previouspin);
            previouspin = newQuestion;
            currentnodes.Add(newQuestion);
        }

        for (int i = 0; i < icons.Length; i++)
        {
            icons[i].sprite = currentnodes[i].sprite;
        }

    }

    public void changepinvalue(int mo)
    {
        if (!pinmove)
        {
            float newvalue = currentlockpinvalue[currentlocaion] + mo;
            if (newvalue >= 0 && newvalue < nodes.Count)
            {
                lockpinan[currentlocaion].Play(currentlockpinvalue[currentlocaion].ToString() + "to" + newvalue.ToString());
                //lockpinan1.Play(currentlockpinvalue[currentlocaion].ToString()+"to" + newvalue.ToString());
                Debug.Log(currentlockpinvalue[currentlocaion].ToString() + "to" + newvalue.ToString());
                currentlockpinvalue[currentlocaion] = newvalue;
                pinan.Play(newvalue.ToString());
                
                //LeanTween.moveLocalX(pin, locations[currentlocaion], 1f);
                //Debug.Log(locations[currentlocaion]);
            }
        }
    }

    public void changelocation(int mo)
    {
        pinmove = true;
        int newLocation = currentlocaion + mo;
        if (newLocation >= 0 && newLocation < options)
        {
            currentlocaion = newLocation;
            LeanTween.moveLocalX(pin, locations[currentlocaion], 1f).setOnComplete(() => pinmove = false);
            Debug.Log(locations[currentlocaion]);
        }
    }

    public void submit()
    {
        for (int i = 0; i < options; i++)
        {
            if(currentnodes[i].space!= currentlockpinvalue[i])
            {
                return;
            }
        }
        round();
    }

    // Start is called before the first frame update
    void Start()
    {
        started = true;
        currentnodes = new List<tunnelletters>();
        getrandomquestion();
        
        changelocation(0);

    }

    


    // Update is called once per frame
    void Update()
    {
        
    }
}
