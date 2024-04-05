using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class sharpsflat : MonoBehaviour
{

    private bool started;
    private bool win;
    [SerializeField] private GameObject panel;
    public Button[] bu;
    public List<key> keys = new List<key>();
    [SerializeField] private TMP_Text question;
    List<string> currentans = new List<string>();
    public string qa1;
    public string qa2;
    public List<string> answers1 = new List<string>();
    public List<string> answers2 = new List<string>();
    int current;
    public int total;
    public Soundmanager Sou;


    [SerializeField] private GameObject cpanel;
    [SerializeField] private TMP_Text cpaneltext;

    [System.Serializable]
    public class key
    {
        public int keys;
        public string note;
        public Button bu;

    }


    void InitializePianoKeys()
    {
        string[] noteNames = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
        for (int i = 0; i < bu.Length; i++)
        {
            key button1 = new key();
            button1.keys = i;
            button1.note = noteNames[i];
            button1.bu = bu[i];
            keys.Add(button1);

        }

    }


    public void starteds()
    {
        started = true;
        question.text = qa1;
    }
    public void wins()
    {
        panel.SetActive(true);
    }

    public void close()
    {
        cpanel.SetActive(false);
    }

    public void next()
    {
        currentans = null;
        currentans = new List<string>();
        Invoke("close", 2);
        cpanel.SetActive(true);
        cpaneltext.text = "Correct";
        question.text = qa2;
    }


    public void wrong()
    {
        currentans = null;
        currentans = new List<string>();
        Invoke("close", 2);
        cpanel.SetActive(true);
        cpaneltext.text = "Wrong Try Again";

    }


    public void setups()
    {


        for (int i = 0; i < keys.Count; i++)
        {
            int buttonIndex = i;
            string optionText = keys[i].note;
            keys[i].bu.onClick.RemoveAllListeners(); // Remove previous listeners to avoid duplicates.
            keys[i].bu.onClick.AddListener(() => CheckOC(optionText, buttonIndex)); // Use the local variable.

        }


    }




    public void CheckOC(string key, int but)
    {
        Debug.Log(key);
        Sou.Play(key + 4.ToString());
        currentans.Add(key);

        if (current == 0)
        {
            for (int i = 0; i < currentans.Count; i++)
            {
                Debug.Log(i);
                if (currentans[i] != answers1[i])
                {
                    Debug.Log(answers1[i]);
                    wrong();
                    return;
                }

            }
            if (currentans.Count == answers1.Count)
            {
                current++;
                if (current == total)
                {
                    wins();
                }
                else
                {
                    next();

                }
            }
        }
        else
        {
            for (int i = 0; i < currentans.Count; i++)
            {
                if (currentans[i] != answers2[i])
                {
                    wrong();
                    return;
                }

            }
            if (currentans.Count == answers2.Count)
            {
                current++;
                if (current == total)
                {
                    wins();
                }
                else
                {

                }
            }

        }
    }




    // Start is called before the first frame update
    void Start()
    {
        Sou = Soundmanager.instance;
        InitializePianoKeys();
        setups();
        starteds();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
