using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class peddlercardmanager : MonoBehaviour
{
    bool start;
    bool win;
    int round;
    public int maxround;
    private int current;
    public Soundmanager Sou;
    string[] noteNames = { "C4", "D4", "E4", "F4", "G4", "A4", "B4", "C5" };
    public Button[] whitekeys;
    public List<interval> inv = new List<interval>();
    public List<interval> RandomsQA = new List<interval>();
    interval currentinterval = new interval();
    public GameObject panel;
    public GameObject[] holders;
    public GameObject panelround;
    [SerializeField] private TMP_Text roundtext;



    public Sprite[] cardssprites;
    public Animator cards;
    public Animator[] Yflip;
    public Animator[] Gflip;

    [System.Serializable]
    public class interval
    {
        public int intervals;
        //public Sprite bu;
        public string key1;
        public string key2;
        public int ikey1;
        public int ikey2;

    }



    public void starter()
    {
        Initializeintervals();
        Randompickinv();
        setups();
        start = true;
        cards.Play("move");

    }

    public void wins()
    {
        win = true;
        panel.SetActive(true);
    }


    public void Randompickinv()
    {
        int randomquestionindex;

        RandomsQA = null;
        RandomsQA = new List<interval>();

        for (int i = 0; i < 3; i++)
        {
            randomquestionindex = Random.Range(0, inv.Count);
            RandomsQA.Add(inv[randomquestionindex]);
            inv.RemoveAt(randomquestionindex);
        }


    }

    public void setups()
    {
        for (int i = 0; i < whitekeys.Length; i++)
        {
            int buttonIndex = i;
            string optionText = noteNames[i];
            whitekeys[i].onClick.RemoveAllListeners(); // Remove previous listeners to avoid duplicates.
            whitekeys[i].onClick.AddListener(() => CheckOC(optionText, buttonIndex)); // Use the local variable.

        }

    }

    public void CheckOC(string key, int but)
    {

        Sou.Play(key);
        if (current == 0) 
        {
            if (key == currentinterval.key1)
            {
                current++;
                Debug.Log("correct");
                whitekeys[but].GetComponent<Image>().color = new Color32(0, 255, 0, 200);
            }
            else
            {
                current = 0;
                whitekeys[but].GetComponent<Image>().color = new Color32(255, 0, 0, 200);
                StartCoroutine(clean());
                panelround.SetActive(true);
                roundtext.text = "Wrong";
                StartCoroutine(resets());
            }
        }
        else
        {
            if (key == currentinterval.key2)
            {
                current = 0;
                Debug.Log("correct");
                whitekeys[but].GetComponent<Image>().color = new Color32(0,255, 0, 200);
                StartCoroutine(clean());
                panelround.SetActive(true);
                roundtext.text = "correct";
                StartCoroutine(nexts());
            }
            else
            {
                current = 0;
                whitekeys[but].GetComponent<Image>().color = new Color32(255, 0, 0, 200);
                StartCoroutine(clean());
                panelround.SetActive(true);
                roundtext.text = "Wrong";
                StartCoroutine(resets());

            }

        }



    }

    public IEnumerator clean()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < whitekeys.Length; i++)
        {
            whitekeys[i].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        for (int i = 0; i < Gflip.Length; i++)
        {
            Gflip[i].Play("unflip");
        }

        for (int i = 0; i < holders.Length; i++)
        {
            foreach (Transform childTransform in holders[i].transform.GetChild(1))
            {
                childTransform.gameObject.SetActive(false);
            }
            foreach (Transform childTransform in holders[i].transform.GetChild(2))
            {
                childTransform.gameObject.SetActive(false);
            }

        }


        yield return new WaitForSeconds(0.5f);
        cards.Play("movein");

    }


    //flips selected card and make it current interval
    public void selected(int ii)
    {

        Gflip[ii].Play("cardflip");
        currentinterval = RandomsQA[ii];
        holders[ii].transform.GetChild(1).transform.GetChild(currentinterval.ikey1).gameObject.SetActive(true);
        holders[ii].transform.GetChild(2).transform.GetChild(currentinterval.ikey2).gameObject.SetActive(true);
        StartCoroutine(show(ii));
    }


    IEnumerator show(int ii)
    {
        yield return new WaitForSeconds(0.4f);
        holders[ii].transform.GetChild(1).gameObject.SetActive(true);
        holders[ii].transform.GetChild(2).gameObject.SetActive(true);

    }

    IEnumerator nexts()
    {
        yield return new WaitForSeconds(1f);
        panelround.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        Randompickinv();
        cards.Play("move");

    }
    IEnumerator resets()
    {
        yield return new WaitForSeconds(1f);
        panelround.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        Randompickinv();
        cards.Play("move");

    }


    //set the card interval list 
    void Initializeintervals()
    {
        bool loop = true;
        int cu = 0;//current card
        int inter = 1;//interval

        while (inter < 5)
        {
            for (int i = 0; i < noteNames.Length; i++)
            {
                if ((i + inter) >= noteNames.Length)
                {
                    continue;
                }
                Debug.Log((i + inter));
                interval curr = new interval();
                curr.intervals = inter + 1;
                curr.key1 = noteNames[i];
                curr.ikey1 = i;
                curr.key2 = noteNames[(i + inter)];
                curr.ikey2 = (i + inter);
                //curr.bu = cardssprites[cu];
                inv.Add(curr);
                cu++;
            }
            for (int i = noteNames.Length - 1; i >= 0; i--)
            {
                if ((i - inter) < 0)
                {
                    continue;
                }
                Debug.Log((i - inter));
                Debug.Log(noteNames[i]);
                interval curr = new interval();
                curr.intervals = inter + 1;
                curr.key1 = noteNames[i];
                curr.ikey1 = i;
                curr.key2 = noteNames[(i - inter)];
                curr.ikey2 = (i - inter);
                //curr.bu = cardssprites[cu];
                inv.Add(curr);
                cu++;
            }

            inter++;
        }



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
}
