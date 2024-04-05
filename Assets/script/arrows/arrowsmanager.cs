using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class arrowsmanager : MonoBehaviour
{

    //new------------------
    public Button[] whitekeys;
    public Button[] blackkeys;
    public List<Octivebuttons> oc = new List<Octivebuttons>();
    private List<Octivebuttons> currentoc = new List<Octivebuttons>();
    bool start;
    bool wins;
    int round;
    public int maxround;
    private int current;
    public Soundmanager Sou;
    string[] noteNames = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
    int[] moves = {1,1,0,1,1,1,0};
    [SerializeField] private TMP_Text question;
    public GameObject panel;
    public GameObject roundpannel;
    [SerializeField] private TMP_Text roundtext;
    public SpriteRenderer fether;
    public Sprite[] fetersimg;
    int previous;
    int ran;
    bool loop;
    //Octive class------------------
    [System.Serializable]
    public class Octivebuttons
    {
        public int octives;
        public string value;
        public Button bu;
        public int number;
    }


    // Start is called before the first frame update
    void Start()
    {
        Sou = Soundmanager.instance;
        InitializePianoKeys();//setup keys
        setups();//setup buttons
        makeque();//makes question 
    }

    public IEnumerator NextOC()
    {
        yield return new WaitForSeconds(2f);
        fether.sprite = null;
        roundpannel.SetActive(true);
        roundtext.text = "Round " + round.ToString() + " Completed";
        Invoke("close", 2);
        current = 0;
        currentoc = null;
        currentoc = new List<Octivebuttons>();
        makeque();//makes question 

    }

    public void close()
    {
        roundpannel.SetActive(false);
    }

    public void Resets()
    {
        current = 0;
    }

    public void CheckOC(string key, int but)
    {
        Debug.Log(key);
        Sou.Play(key);
        if (currentoc[current].value == key)
        {
            current += 1;
            if (current == 3)
            {
                fether.sprite = fetersimg[0];
            }
            if (current == 6)
            {
                fether.sprite = fetersimg[1];
            }
            if(current == 8)
            {
                fether.sprite = fetersimg[2];
            }
            oc[but].bu.GetComponent<Image>().color = new Color32(0, 255, 0, 255);
            if (current >= 8)
            {
                //fether.sprite = fetersimg[round];
                round += 1;
                StartCoroutine(clean());
                if (round == maxround)
                {
                    Debug.Log("win");
                    panel.SetActive(true);
                    wins = true;
                }
                else
                {
                    StartCoroutine(NextOC());
                }
            }
        }
        else
        {
            Debug.Log("wrong");
            oc[but].bu.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
            StartCoroutine(clean());
            Resets();
            fether.sprite = null;
        }

    }

    public IEnumerator clean()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < oc.Count; i++)
        {
            oc[i].bu.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }
    public void makeque()
    {
        do 
        {
            loop = false;
            ran = Random.Range(0, noteNames.Length);
            if(ran == previous)
            {
                loop = true;
            }

        }while(loop);
        previous = ran;
        string name = noteNames[ran];
        currentoc.Add(oc[ran]);
        for (int i = 0; i < 7; i++)
        {
            currentoc.Add(oc[(ran + moves[i]+1)]);
            ran = (ran + moves[i] + 1);
        }
        question.text = name+" Major";
    }


    public void setups()
    {

        for (int i = 0; i < oc.Count; i++)
        {
            int buttonIndex = i;
            string optionText = oc[i].value;
            oc[i].bu.onClick.RemoveAllListeners(); // Remove previous listeners to avoid duplicates.
            oc[i].bu.onClick.AddListener(() => CheckOC(optionText, buttonIndex)); // Use the local variable.

        }
    }



    void InitializePianoKeys()
    {
        bool loop = true;
        int nu = 0;
        int wh = 0;
        int bl = 0;
        // Create 88 piano keys
        for (int octave = 4; octave < 6; octave++) // Assuming a standard piano with 7 octaves
        {
            foreach (string note in noteNames)
            {

                if (loop)
                {
                    nu += 1;
                    string fullNoteName = note + octave.ToString();
                    bool hasSharp = fullNoteName.Contains("#");
                    //Debug.Log(fullNoteName);

                    if (!hasSharp)
                    {
                        Octivebuttons button1 = new Octivebuttons();
                        button1.octives = octave;
                        button1.value = fullNoteName;
                        button1.bu = whitekeys[wh];
                        button1.number = nu;
                        oc.Add(button1);
                        wh += 1;
                    }
                    else
                    {
                        Octivebuttons button1 = new Octivebuttons();
                        button1.octives = octave;
                        button1.value = fullNoteName;
                        button1.bu = blackkeys[bl];
                        button1.number = nu;
                        oc.Add(button1);
                        bl += 1;
                    }
                }
            }

            // Reset the loop variable for the next octave
            loop = true;
        }
        // You now have a list of piano keys in the pianoKeys variable
    }





    // Update is called once per frame
    void Update()
    {
        
    }
}
