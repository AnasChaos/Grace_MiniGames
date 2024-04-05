using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MusicalSimon : MonoBehaviour
{
    public static MusicalSimon instance;
    public bool started;
    public bool win;
    Soundmanager Sou;
    public GameObject pannel;
    public GameObject[] keys;
    public Button[] bu;
    int current;
    int currentmusicnote;
    int rounds;
    public int maxrounds;
    int previousindex;
    int index;
    int index2;
    string[] noteNames = { "C4", "D4", "E4", "F4", "G4", "A4", "B4", "C5" };
    public int tuneoptions; // number of tunes
    public intervaltype currenttype;
    public intervalss currentinterval;
    public List<intervalss> intervals = new List<intervalss>();
    bool allowedtoclick = true;
    public GameObject buttons;
    public GameObject startss;
    public GameObject progresspannel;
    [SerializeField] private TMP_Text progress;



    [System.Serializable]
    public class intervalss
    {
        public intervaltype ques;
        public int number;
        public string note;
    }

    [System.Serializable]
    public enum intervaltype
    {
        nun,
        step,
        skip,
        leap,
        repeat
    }

    public void starteds()
    {
        Debug.Log("called");
        //RaiseEvent("start");
        started = true;
        StartCoroutine(currenttune());
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
        getraindomquestion();
        setups();
    }


    IEnumerator currenttune()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < intervals.Count; i++)
        {
            Sou.Play(intervals[i].note);
            keys[intervals[i].number].transform.GetComponent<Image>().color = new Color32(100, 100, 100, 255);
            yield return new WaitForSeconds(1f);
            keys[intervals[i].number].transform.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        yield return new WaitForSeconds(1f);
        select();
        allowedtoclick = true;

    }

    public void clean() // cleans up lighted notes
    {
        for (int i = 0; i < keys.Length; i++)
        {
            Debug.Log(i);
            keys[i].transform.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }

    }
    public void select()//light up nodes
    {
        keys[intervals[(current-1)].number].transform.GetComponent<Image>().color = new Color32(255, 255, 0, 255);
        keys[intervals[current].number].transform.GetComponent<Image>().color = new Color32(255, 255, 0, 255);
    }




    public void next()//starts up next round
    {
        allowedtoclick = false;
        progresspannel.SetActive(true);
        rounds += 1;
        progress.text = "Round " + rounds.ToString() + " Completed";

        buttons.SetActive(false);
        startss.SetActive(true);
        current = 0;

        intervals = null;
        intervals = new List<intervalss>();
        getraindomquestion();
    }


    public void resets()//reset round
    {

        allowedtoclick = false;
        buttons.SetActive(false);
        startss.SetActive(true);
        current = 1;
        currentinterval = intervals[current];
    }



    public void checks(intervaltype IT)// checks answers
    {
        Debug.Log(IT);
        if (allowedtoclick)
        {
            if (IT == currentinterval.ques)
            {

                current += 1;
                clean();
                if (current <= intervals.Count)
                {
                    //correctscolor();
                }

                if (current >= intervals.Count)
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
                else
                {
                    currentinterval = intervals[current];
                    clean();
                    select();
                    Debug.Log("correct");
                }
                //answered = true;
                

            }
            else
            {
                clean();
                Debug.Log("wrong");
                resets();
            }

        }


    }






    public void setups()//setup buttons
    {
        for (int i = 0; i < bu.Length; i++)
        {
            intervaltype[] values = (intervaltype[])System.Enum.GetValues(typeof(intervaltype));
            intervaltype ee = values[(i + 1)];
            bu[i].onClick.RemoveAllListeners(); // Remove previous listeners to avoid duplicates.
            bu[i].onClick.AddListener(() => checks(ee)); // Use the local variable.

        }
    }


    // Check if the sum is within the desired range
    private bool IsSumInRange(int sum)
    {
        return sum >= 0 && sum <= 7;
    }

    void getraindomquestion()//generate questions
    {
        intervaltype[] values = (intervaltype[])System.Enum.GetValues(typeof(intervaltype));
        index = Random.Range(0, 8);
        Debug.Log(index);
        previousindex = index;
        index2 = index;

        currentinterval.ques = values[0];
        currentinterval.number = index;
        currentinterval.note = noteNames[index];
        intervals.Add(currentinterval);



        for (int i = 1; i < tuneoptions; i++)
        {

            int randomIndex = Random.Range(1, values.Length);
            currenttype = values[randomIndex];
            intervalss currentInterval = new intervalss();
            currentInterval.ques = currenttype;


            if (currenttype == intervaltype.repeat)
            {
                //noteholder[i].transform.GetChild(previousindex).gameObject.SetActive(true);

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
                //noteholder[i].transform.GetChild(check).gameObject.SetActive(true);
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
                //noteholder[i].transform.GetChild(check).gameObject.SetActive(true);
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
                //noteholder[i].transform.GetChild(check).gameObject.SetActive(true);
                previousindex = check;
            }
            currentInterval.number = previousindex;
            currentInterval.note = noteNames[previousindex];
            intervals.Add(currentInterval);
        }

        current = 1;
        currentinterval = intervals[current];
    }

    public void wins()
    {
        win = true;
        pannel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            if (Sou == null) 
            {
                Sou = Soundmanager.instance;
            }

        }
    }
}
