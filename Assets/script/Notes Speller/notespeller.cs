using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;


public class notespeller : MonoBehaviour
{
    public notescripts[] scriptss;
    private static List<notescripts> unanweredquestion;
    private notescripts currentquestion;
    public GameObject noteholder;
    [SerializeField] private Button nexts;
    [SerializeField] private TMP_Text question;
    [SerializeField] private TMP_Text progress;
    [SerializeField] private Slider slide;
    private float correct = 0;
    private bool answered = false;

    private bool firstatemt = true;
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text result;
    // Start is called before the first frame update

    public Animator animator;
    Soundmanager Sou;

    void Start()
    {


        progress.text = ((correct / scriptss.Length) * 100f).ToString() + "%";
        unanweredquestion = scriptss.ToList<notescripts>();
        getraindomquestion();

        Sou = Soundmanager.instance;
    }

    IEnumerator  Result()
    {

        GameObject tex = noteholder.transform.GetChild(1).gameObject;

        for (int i = 0; i <= tex.transform.childCount; i++)
        {

            try
            {
                if (tex.transform.GetChild(i).GetComponent<slots>())
                {


                        tex.transform.GetChild(i).gameObject.SetActive(false);

                    

                    
                }
                else
                {
                    tex.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
            catch
            {
            }



        }

        yield return new WaitForSeconds(1.5f);


        panel.SetActive(true);
        //result.text = "you got " + correct + " out of " + scriptss.Length.ToString();
        //if ((correct / scriptss.Length) > 0.5 && (correct / scriptss.Length) < 0.74)
        //{
           // animator.Play("onestar");
        //}
        //else if ((correct / scriptss.Length) > 0.75 && (correct / scriptss.Length) < 1)
        //{
            //animator.Play("twostar");
        //}
        //else if ((correct / scriptss.Length) == 1)
        //{
            //animator.Play("threestar");
        //}


    }



    void getraindomquestion()
    {
        if (unanweredquestion.Count == 0)
        {
            // Handle the case when there are no more questions.
            Debug.Log("No more questions.");
            StartCoroutine(Result());
            return;
        }

        for (int i = 0; i < noteholder.transform.childCount; i++)
        {
            Destroy(noteholder.transform.GetChild(i).gameObject);
        }

        int randomquestionindex = Random.Range(0, unanweredquestion.Count);
        currentquestion = unanweredquestion[randomquestionindex];
        unanweredquestion.RemoveAt(randomquestionindex);
        GameObject uiElement = Instantiate(currentquestion.scripts);
        GameObject uiElement2 = Instantiate(currentquestion.questioninfo);
        uiElement.transform.SetParent(noteholder.transform, false);
        uiElement2.transform.SetParent(noteholder.transform, false);
        answered = false;
    }


    public void CheckAnswer()
    {

        bool allCorrect = true;

        if (answered == false)
        {
            int chack = 0;
            int cor = 0;

            GameObject tex = noteholder.transform.GetChild(1).gameObject;
            for (int i = 0; i <= tex.transform.childCount; i++)
            {

                try
                {
                    if (tex.transform.GetChild(i).GetComponent<slots>())
                    {

                        alphabet alphabet = tex.transform.GetChild(i).GetComponent<slots>().alphabet;
                        if (alphabet)
                        {
                            //Debug.Log(alphabet.letter.ToString());
                            //Debug.Log(currentquestion.correctans[chack].letter.ToString());
                            if (alphabet.letter != currentquestion.correctans[chack].letter)
                            {
                                allCorrect = false;
                                Sou.Play("wrong");
                                firstatemt = false;
                                Debug.Log(firstatemt);
                                tex.transform.GetChild(i).GetComponent<Image>().color = new Color32(255, 167, 167, 255);
                            }
                            chack += 1;
                        }
                        else
                        {
                            allCorrect = false;
                        }
                    }
                }
                catch
                {
                }
            }
            chack = 0;
            for (int i = 0; i <= tex.transform.childCount; i++)
            {

                try
                {
                    alphabet alphabet = tex.transform.GetChild(i).GetComponent<slots>().alphabet;
                    if (alphabet)
                    {
                        //Debug.Log(alphabet.letter.ToString());
                        //Debug.Log(currentquestion.correctans[chack].letter.ToString());
                        if (alphabet.letter == currentquestion.correctans[chack].letter)
                        {
                            cor += 1;
                            Sou.Play("correct");
                            Debug.Log("answered");
                            tex.transform.GetChild(i).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                        }
                        chack += 1;
                    }
                    if (allCorrect)
                    {
                        answered = true; // Set answered to true only if all questions are answered correctly
                    }



                }
                catch
                {
                }

                Debug.Log(correct);
                Debug.Log(scriptss.Length);


            }

            if (firstatemt && answered)
            {
                correct += 1;
                slide.value = correct / scriptss.Length;
                progress.text = ((correct / scriptss.Length) * 100f).ToString("0") + "%";
            }
            if (unanweredquestion.Count == 0 && answered)
            {
                // Handle the case when there are no more questions.
                Debug.Log("No more questions.");
                StartCoroutine(Result());
                return;
            }




        }

    }




    public void next()
    {
        CheckAnswer();
        if (answered)
        {
            getraindomquestion();
            answered = false;
            firstatemt = true;
        }
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
