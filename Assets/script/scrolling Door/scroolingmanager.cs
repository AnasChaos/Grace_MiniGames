using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class scroolingmanager : MonoBehaviour
{
    public static scroolingmanager instance;
    public bool started;
    public bool win;
    Soundmanager Sou;
    public GameObject pannel;
    public GameObject[] noteholder;
    int current;
    int rounds;
    public int maxrounds;
    int previousindex;
    public intervaltype currenttype;
    public intervalss currentinterval;//current question
    //public List<intervaltype> ques = new List<intervaltype>();
    public List<intervalss> ques = new List<intervalss>();//list of questions
    //string[] noteNames = { "C4", "D4", "E4", "F4","G4", "A4", "B4", "C5", "D5", "E5", "F5" };
    public Button[] bu;
    bool first = true;
    bool answered =true;
    bool allowedtoclick;
    public GameObject buttons;
    public GameObject startss;
    public GameObject progresspannel;
    [SerializeField] private TMP_Text progress;
    int index;
    int index2;
    public bool easymode;

    public class intervalss
    {
        public intervaltype ques;
        public int number;
    }

    [System.Serializable]
    public enum intervaltype
    {
        step,
        skip,
        leap,
        repeat
    }


    //event system------------------------------------
    public delegate void MyEventDelegate(string status);

    public event MyEventDelegate MyEvent;

    public void RaiseEvent(string status)
    {

        MyEvent?.Invoke(status);
    }


    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        Sou = Soundmanager.instance;
        setups();
        getraindomquestion();
    }




    //starts the game
    public void starteds()
    {
        RaiseEvent("start");
        started = true;
    }

    public void resets()// reset the wround
    {
        clean();
        allowedtoclick = false;
        RaiseEvent("Resets");
        buttons.SetActive(false);
        startss.SetActive(true);
        first = true;
        answered = true;
        current = 0;
        currenttype = ques[current].ques;
    }

    public void next()// setup next wround
    {
        clean();
        allowedtoclick = false;
        progresspannel.SetActive(true);
        rounds += 1;
        progress.text = "Round "+ rounds.ToString() +" Completed";
        RaiseEvent("Resets");
        buttons.SetActive(false);
        startss.SetActive(true);
        first = true;
        answered = true;
        current = 0;
        RaiseEvent("Resets");
        ques = null;
        ques = new List<intervalss>();
        getraindomquestion();
    }

    public void clean()// clean up lights 
    {
        Debug.Log(index2);
        noteholder[0].transform.GetChild(index2).transform.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 255);

        for (int i = 1; i < noteholder.Length; i++)
        {
            Debug.Log(ques[(i-1)].number);
            noteholder[i].transform.GetChild(ques[(i-1)].number).transform.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 255);

        }

    }

    public void correctscolor()//color nodes as correct
    {
        Debug.Log(current);
        noteholder[0].transform.GetChild(index2).transform.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 0, 255);
        noteholder[current].transform.GetChild(ques[current-1].number).transform.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 0, 255);
    }

    // Check if the sum is within the desired range
    private bool IsSumInRange(int sum)
        {
            return sum >= 0 && sum <= 10;
        }


    //generate and position the nodes and questions
        void getraindomquestion()
        {

        for (int i = 0; i < noteholder.Length; i++)
        {
            foreach (Transform childTransform in noteholder[i].transform)
            {
                GameObject child = childTransform.gameObject;
                child.SetActive(false);
            }
        }


            index = Random.Range(0, 11);
            Debug.Log(index);
            noteholder[0].transform.GetChild(index).gameObject.SetActive(true);
            previousindex = index;
            index2 = index;

        for (int i = 1; i < noteholder.Length; i++)
        {
            intervaltype[] values = (intervaltype[])System.Enum.GetValues(typeof(intervaltype));
            int randomIndex = Random.Range(0, values.Length);
            currenttype = values[randomIndex];
            intervalss currentInterval = new intervalss();
            currentInterval.ques = currenttype;

            Debug.Log(currenttype);




            if (currenttype == intervaltype.repeat)
            {
                noteholder[i].transform.GetChild(previousindex).gameObject.SetActive(true);

            }



            if (currenttype == intervaltype.step)
            {
                int check;
                int leapFactor = 1;
                do
                {
                    index = Random.Range(-1, 2);
                    check = index * leapFactor + previousindex;
                } while (index == 0 || !IsSumInRange(check));
                Debug.Log(check);
                noteholder[i].transform.GetChild(check).gameObject.SetActive(true);
                previousindex = check;
            }

            if (currenttype == intervaltype.skip)
            {
                int check;
                int leapFactor = 2;
                do
                {
                    index = Random.Range(-1, 2);
                    check = index * leapFactor + previousindex;
                } while (index == 0 || !IsSumInRange(check));
                Debug.Log(check);
                noteholder[i].transform.GetChild(check).gameObject.SetActive(true);
                previousindex = check;
            }

            if (currenttype == intervaltype.leap)
            {
                int check;
                int leapFactor = 4;
                do
                {
                    index = Random.Range(-1, 2);
                    check = index * leapFactor + previousindex;
                } while (index == 0 || !IsSumInRange(check));
                Debug.Log(check);
                noteholder[i].transform.GetChild(check).gameObject.SetActive(true);
                previousindex = check;
            }
            currentInterval.number = previousindex;
            ques.Add(currentInterval);

            currenttype = ques[current].ques;
        }

        }


    public void currentnode(tunnelletters tt) // tells if the line moved over the node 
    {
        Sou.Play(tt.name);
        if (!first)
        {
            if (easymode)
            {
                RaiseEvent("stop");
                allowedtoclick = true;
            }
            else
            {
                if (!answered)
                {
                    resets();
                }
                else
                {
                    answered = false;
                    allowedtoclick = true;
                }
            }

        }
        else
        {
            first = false;
        }

    }


    public void setups()
    {
        for (int i = 0; i < bu.Length; i++)
        {
          intervaltype[] values = (intervaltype[])System.Enum.GetValues(typeof(intervaltype));
            intervaltype ee = values[i];
            bu[i].onClick.RemoveAllListeners(); // Remove previous listeners to avoid duplicates.
            bu[i].onClick.AddListener(() => checks(ee)); // Use the local variable.

        }
    }


    public void checks(intervaltype IT)// checks the answer 
    {
        Debug.Log(IT);
        if (allowedtoclick)
        {
            if (IT == currenttype)
            {
                current += 1;
                if (current <= ques.Count)
                {
                    correctscolor();
                    if (easymode)
                    {
                        RaiseEvent("start");
                    }

                }

                if (current >= ques.Count)
                {
                    if (rounds == maxrounds)
                    {
                        wins();
                        return;
                    }
                    else
                    {
                        next();
                    }

                }
                answered = true;
                currenttype = ques[current].ques;
                Debug.Log("correct");
            }
            else
            {
                Debug.Log("wrong");
                resets();
            }

        }


    }

    public void wins()
    {
        win = true;
        pannel.SetActive(true);
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
