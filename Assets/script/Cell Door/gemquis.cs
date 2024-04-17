using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class gemquis : MonoBehaviour
{

    public gemscripts[] gemscriptss;
    private static List<gemscripts> unanweredquestion;
    private gemscripts currentquestion;
    public GameObject notehigh;
    public GameObject noteLow;
    [SerializeField] private TMP_Text question;
    [SerializeField] private TMP_Text progress;
    [SerializeField] private Slider slide;
    private float correct = 0;

    //private bool correctanswer = false;
    private bool answered = false;
    private bool firstatemt = true;
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text result;
    GameObject tex;
    public Animator animator;
    Soundmanager Sou;
    //new
    string[] high = { "D4", "E4", "F4", "G4", "A4", "B4", "C5", "D5", "E5", "F5", "G5" };
    string[] low = { "F2", "G2", "A2", "B2", "C3", "D3", "E3", "F3", "G3", "A3", "B3" };
    public int options;
    public GameObject qholder;
    public int maxrounds;
    public float totalcurrent = 0;
    private float current = 0;
    [SerializeField]int rounds;
    bool win;
    public GameObject roundpannel;
    [SerializeField] private TMP_Text roundtext;
    int Hd;
    int Ld;
    int dd;
    private void Result()
    {
        panel.SetActive(true);

    }

    //create random questions
    void generateque()
    {
        gemtype TY;
        gemtype[] values = (gemtype[])System.Enum.GetValues(typeof(gemtype));
        gemscripts creator = new gemscripts();
        int randomIndex = Random.Range(0, values.Length);
        TY = values[randomIndex];
        creator.notes = TY;

        if (TY == gemtype.high)
        {
            for (int i = 0; i < options; i++)
            {
                do
                {
                   dd = Random.Range(0, high.Length);
                   
                } while (Hd == dd);
                Hd = dd;
                creator.questioninfo.Add(high[dd]);
                creator.correctans.Add((1 + dd).ToString());
            }
        }
        if (TY == gemtype.low)
        {
            for (int i = 0; i < options; i++)
            {

                do
                {
                    dd = Random.Range(0, low.Length);

                } while (Ld == dd);
                Ld = dd;
                creator.questioninfo.Add(low[dd]);
                creator.correctans.Add((1+dd).ToString());
            }

        }

        currentquestion = creator;
        setquestion();
    }
    //setup questions------
    public void setquestion()
    {
        if (currentquestion.notes == gemtype.high)
        {
            tex = notehigh.transform.GetChild(2).gameObject;
            for (int i = 0; i < tex.transform.childCount; i++)
            {
                tex.transform.GetChild(i).GetComponent<slotholdermanager>().destroysall();
            }

        }
        else
        {
            tex = noteLow.transform.GetChild(2).gameObject;
            for (int i = 0; i < tex.transform.childCount; i++)
            {
                tex.transform.GetChild(i).GetComponent<slotholdermanager>().destroysall();
            }
        }

        if (currentquestion.notes == gemtype.high)
        {
            notehigh.SetActive(true);
            noteLow.SetActive(false);
        }
        else
        {
            notehigh.SetActive(false);
            noteLow.SetActive(true);
        }


        for (int i = 0; i < qholder.transform.childCount; i++)
        {
            qholder.transform.GetChild(i).GetComponent<TMP_Text>().text = currentquestion.questioninfo[i];
        }


    }
    public void ByPassMission() 
    {
        rounds = 5;
        current = 5;
        checkround();
    }
    // check if round is clered
    void checkround()
    {
        if (answered)
        {
            current += 1;
            firstatemt = true;
            answered = false;
            progress.text = current.ToString() + "/" + gemscriptss.Length.ToString();
        }
        if(current>= totalcurrent)
        {
            rounds += 1;

            if (rounds>= maxrounds)
            {
                win = true;
                SceneManager.LoadScene("Scene4");
                //Result();
                return;
            }
            if (!win)
            {
                roundpannel.SetActive(true);
                roundtext.text = "Round " + rounds.ToString() + " Completed";
            }
            current = 0;
            progress.text = current.ToString() + "/" + gemscriptss.Length.ToString();
            Invoke("close", 2);
        }

    }
    public void close()
    {
        roundpannel.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        progress.text = current.ToString() + "/" + gemscriptss.Length.ToString();
        unanweredquestion = gemscriptss.ToList<gemscripts>();
        Sou = Soundmanager.instance;
        generateque();

    }

    // checks answers
    public void CheckAnswer()
    {
        bool allCorrect = true;
        if (!answered)
        {
            if (currentquestion.notes == gemtype.high)
            {
                Debug.Log("checking high");
                tex = notehigh.transform.GetChild(2).gameObject;

                for (int i = 0; i < tex.transform.childCount; i++)
                {

                    int sel = tex.transform.GetChild(i).GetComponent<slotholdermanager>().currentslot;
                    string selgem = tex.transform.GetChild(i).GetComponent<slotholdermanager>().selection;
                    Debug.Log(selgem);
                    Debug.Log(sel);

                    if (sel != 0 && sel.ToString() != currentquestion.correctans[i] || selgem == "low")
                    {
                        Debug.Log(sel);
                        Sou.Play("wrong");
                        firstatemt = false;
                        tex.transform.GetChild(i).GetComponent<slotholdermanager>().wrong();
                        allCorrect = false;
                        return;
                    }
                    else if (sel == 0)
                    {
                        allCorrect = false;
                    }
                    else if (sel != 0)
                    {
                        Sou.Play("correct");
                        tex.transform.GetChild(i).GetComponent<slotholdermanager>().right();
                    }
                }

                if (allCorrect)
                {
                    answered = true; // Set answered to true only if all questions are answered correctly
                }
                if (firstatemt && answered)
                {
                    correct += 1;
                    //slide.value = correct / gemscriptss.Length;
                    progress.text = current.ToString() + "/" + gemscriptss.Length.ToString();
                }

            }
            else if(currentquestion.notes == gemtype.low)
            {
                tex = noteLow.transform.GetChild(2).gameObject;
                Debug.Log("checking low");

                for (int i = 0; i < tex.transform.childCount; i++)
                {

                    int sel = tex.transform.GetChild(i).GetComponent<slotholdermanager>().currentslot;
                    string selgem = tex.transform.GetChild(i).GetComponent<slotholdermanager>().selection;

                    Debug.Log(selgem);
                    Debug.Log(sel);
                    if (sel != 0 && sel.ToString() != currentquestion.correctans[i] || selgem == "high")
                    {
                        Debug.Log(sel);
                        Sou.Play("wrong");
                        firstatemt = false;
                        tex.transform.GetChild(i).GetComponent<slotholdermanager>().wrong();
                        allCorrect = false;
                        return;
                    }
                    else if (sel == 0)
                    {
                        allCorrect = false;
                    }
                    else if (sel != 0)
                    {
                        Sou.Play("correct");
                        tex.transform.GetChild(i).GetComponent<slotholdermanager>().right();
                    }
                }
                if (allCorrect)
                {
                    answered = true; // Set answered to true only if all questions are answered correctly
                }


                if (firstatemt && answered)
                {
                    correct += 1;
                    //slide.value = correct / gemscriptss.Length;
                    progress.text = current.ToString() + "/" + gemscriptss.Length.ToString();
                }


            }



        }



        if (unanweredquestion.Count == 0 && answered)
        {
            // Handle the case when there are no more questions.
            Debug.Log("No more questions.");
            Result();
        }
    }

    //moves next
        public void next()
        {
            CheckAnswer();
            if (answered)
            {
               StartCoroutine(waits());
            }
        }

    IEnumerator waits()
    {
        yield return new WaitForSeconds(3f);
        checkround();
        generateque();
    }


}
