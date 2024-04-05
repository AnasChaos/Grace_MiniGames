using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class takmanager : MonoBehaviour
{
    public quizs[] quizs;
    private static List<quizs> unanweredquestion;
    private quizs currentquestion;
    [SerializeField] private List<Button> imagebuttons;
    [SerializeField] private List<Image> imagess;
    [SerializeField] private List<Animator> Ani;

    [SerializeField] private TMP_Text question;

    [SerializeField] private Image questionImage;
    [SerializeField] private TMP_Text result;
    [SerializeField] private GameObject panel;
    private float correct = 0;
    [SerializeField] private Button nexts;
    private bool answered = false;
    private bool firstatemt = true;
    private int answerselected;
    [SerializeField] private TMP_Text progress;
    public int totalquestions;
    [SerializeField] private List<string> str;
    [SerializeField] private TMP_Text comment;
    [SerializeField] private GameObject commentpanel;

    // Start is called before the first frame update

    public Animator animator;
    Soundmanager Sou;
    void Start()
    {

        unanweredquestion = null;
        //progress.text = correct.ToString() + "/" + totalquestions.ToString();
        unanweredquestion = quizs.ToList<quizs>();
        getraindomquestion();
        Sou = Soundmanager.instance;

    }

    void getraindomquestion()
    {
        if (unanweredquestion.Count == 0)
        {
            // Handle the case when there are no more questions.
            Debug.Log("No more questions.");
            Result();
            return;
        }




        currentquestion = unanweredquestion[0]; 

        unanweredquestion.RemoveAt(0);
        question.text = currentquestion.questioninfo;


        if (currentquestion.questiontype == questiontype.imageanswer)
        {


            for (int i = 0; i < imagebuttons.Count; i++)
            {

                int buttonIndex = i;
                string optionText = currentquestion.options[i]; // Capture the option text in a local variable.
                imagess[i].sprite = currentquestion.answersimage[i];
                imagebuttons[i].onClick.RemoveAllListeners(); // Remove previous listeners to avoid duplicates.
                imagebuttons[i].onClick.AddListener(() => CheckAnswer(optionText, buttonIndex)); // Use the local variable.
                Ani[i].Play("displays");
            }


        }



    }

    private void Result()
    {
        for (int i = 0; i < imagebuttons.Count; i++)
        {
            imagebuttons[i].interactable = false;
        }
        panel.SetActive(true);

    }

    void CheckAnswer(string selectedOption, int button)
    {
        if (!answered)
        {
            Debug.Log(button);
            if (selectedOption == currentquestion.correctans)
            {
                Sou.Play("correct");
                // The selected option is the correct answer.
                Debug.Log("Correct Answer!");


                if (currentquestion.questiontype == questiontype.imageanswer)
                {
                    imagess[button].color = new Color32(0, 255, 0, 255);
                }

                for (int i = 0; i < imagebuttons.Count; i++)
                {
                    imagebuttons[i].interactable = false;
                }

                answered = true;
                if (firstatemt)
                {
                    correct += 1;
                    firstatemt = false;
                }
                if (correct == totalquestions)
                {
                    Result();
                    return;
                }
                else
                {
                    StartCoroutine(next());
                }

            }
            else
            {
                // The selected option is not correct.
                Debug.Log("Wrong Answer!");
                Sou.Play("wrong");
                if (currentquestion.questiontype == questiontype.imageanswer)
                {
                    imagess[button].color = new Color32(255, 0, 0, 255);

                }
                StartCoroutine(restarts());

                firstatemt = false;
            }
           // progress.text = correct.ToString() + "/" + totalquestions.ToString();

            // Call getRandomQuestion to display the next question.
            //getraindomquestion();
        }

    }

    IEnumerator next()
    {
        if (answered)
        {
            yield return new WaitForSeconds(1.5f);
            for (int i = 0; i < imagebuttons.Count; i++)
            {
                imagebuttons[i].interactable = true;
            }
            if (currentquestion.questiontype == questiontype.imageanswer)
            {
                for (int i = 0; i < imagebuttons.Count; i++)
                {
                    imagess[i].color = new Color32(255, 255, 255, 255);
                }
            }



            getraindomquestion();
            answered = false;
            firstatemt = true;
        }
    }
    IEnumerator restarts()
    {
        for (int i = 0; i < imagebuttons.Count; i++)
        {
            imagebuttons[i].interactable = false;
        }
        yield return new WaitForSeconds(1.5f);


        commentpanel.SetActive(true);
        int indexs = Random.Range(0, str.Count);
        string com = str[indexs];
        comment.text = com;

        yield return new WaitForSeconds(5f);
        for (int i = 0; i < imagebuttons.Count; i++)
        {
            imagebuttons[i].interactable = true;
        }

        commentpanel.SetActive(false);

        if (currentquestion.questiontype == questiontype.imageanswer)
        {
            for (int i = 0; i < imagebuttons.Count; i++)
            {
                imagess[i].color = new Color32(255, 255, 255, 255);
            }
        }
        correct = 0;
        unanweredquestion = null;
        //progress.text = correct.ToString() + "/" + totalquestions.ToString();
        unanweredquestion = quizs.ToList<quizs>();
        getraindomquestion();
    }



}
