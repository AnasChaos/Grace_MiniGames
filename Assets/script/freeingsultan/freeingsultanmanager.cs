using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class freeingsultanmanager : MonoBehaviour
{

    public bool started;
    public bool win;
    public GameObject pannel;
    private List<key> keys = new List<key>();
    public interval currentkeys = new interval();
    public Image[] whitekeys;
    public Image[] blackkeys;
    int current;
    int correct;
    public int total;
    Soundmanager Sou;
    bool loops;

    public int step;//move in steps
    public int direction;//back or forward
    [SerializeField] private TMP_Text ste;
    [SerializeField] private TMP_Text score;

    [System.Serializable]
    public class interval
    {

        public int step = 2;//move in steps
        public int direction;//back or forward
        public key key1;
        public key key2;

    }

    [System.Serializable]
    public class key
    {

        public int number;
        public string value;
        public Image bu;

    }

    public void starter()
    {
        InitializePianoKeys();
        randomqa();
        StartCoroutine(currenttune());

    }

    // Start is called before the first frame update
    void Start()
    {
        Sou = Soundmanager.instance;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void checks()
    {
        if (step == currentkeys.step && direction == currentkeys.direction)
        {
            current ++;
            score.text = current.ToString()+"/"+total.ToString();
            if (current>=total)
            {
                wins();
            }
            else
            {

                randomqa();
                clener();
                StartCoroutine(currenttune());

            }
            Debug.Log("correct");

        }
    }


    public void setdirections(int ee)//set directon
    {
        direction = ee;

        //dir.text = direction.ToString();
    }

    public void stepsets(int ee)//set step
    {
        if (ee == 1) {
            step = step + ee;
            if (step > 5)
            {
                step = 2;
            }
        }
        else
        {
            step = step + ee;
            if (step < 2)
            {
                step = 5;
            }

        }
        ste.text = step.ToString()+"D";
    }

    
    IEnumerator currenttune()//playes tunes
    {
        yield return new WaitForSeconds(1f);

        Sou.Play(currentkeys.key1.value);
        currentkeys.key1.bu.color = new Color32(100, 100, 100, 255);

        yield return new WaitForSeconds(1f);
        Sou.Play(currentkeys.key2.value);
        currentkeys.key2.bu.color = new Color32(100, 100, 100, 255);


    }

    public void clener()// cleans up the lights 
    {
        for (int x = 0; x < keys.Count; x++)
        {
            keys[x].bu.color = new Color32(255, 255, 255, 255);
        }

    }




    //makes random question 
    void randomqa()
    {
       
       int steps = Random.Range(2, 6);
       int dir = Random.Range(0, 2);
       int key1;
       int key2 = 0;
       loops = true;
        do
        {

            key1 = Random.Range(0, keys.Count);
            if (dir == 1)//forward
            {
                if ((key1+steps-1) <= keys.Count)
                {
                    loops = false;
                    key2 = (key1 + steps - 1);
                } 

            }
            else//backward
            {
                if ((key1 - steps + 1) >= 0)
                {
                    loops = false;
                    key2 = (key1 - steps + 1);
                }
            }

            if (keys[key1] == currentkeys.key1 && keys[key2] == currentkeys.key2)
            {
                loops = true;
            }


        } while (loops);

       
       


       currentkeys.step = steps;
       currentkeys.direction = dir;
       currentkeys.key1 = keys[key1];
       currentkeys.key2 = keys[key2];

    }


    //setup piano key list with it letters  
    void InitializePianoKeys()
    {
        bool loop = true;
        string[] noteNames = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
        int nu = 0;
        int wh = 0;//counts whightkeys
        int bl = 0;//counts blackkeys
        // Create 88 piano keys
        for (int octave = 3; octave < 6; octave++) // Assuming a standard piano with 7 octaves
        {
            foreach (string note in noteNames)
            {
                if (octave == 3 && !(note == "F" || note == "F#" || note == "G" || note == "G#" || note == "A" || note == "A#" || note == "B"))
                {
                    continue; // Skip notes other than A, A#, and B for octave 0
                }

                if (loop)
                {
                    nu += 1;
                    string fullNoteName = note + octave.ToString();
                    bool hasSharp = fullNoteName.Contains("#");
                    //Debug.Log(fullNoteName);

                    if (!hasSharp)
                    {
                        key button1 = new key();
                        button1.value = fullNoteName;
                        button1.bu = whitekeys[wh];
                        button1.number = nu;
                        keys.Add(button1);
                        wh += 1;
                    }

                }
            }

            // Reset the loop variable for the next octave
            loop = true;
        }
        // You now have a list of piano keys in the pianoKeys variable
    }

    public void wins()
    {
        win = true;
        pannel.SetActive(true);
    }




}
