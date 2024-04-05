using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class quizsmanager : MonoBehaviour
{

    public quizs[] quizs;
    private static List<quizs> unanweredquestion;
    private quizs currentquestion;
    [SerializeField] private List<Button> imagebuttons;
    [SerializeField] private List<Image> imagess;
    [SerializeField] private TMP_Text question;
    [SerializeField] private List<Button> options;
    [SerializeField] private Image questionImage;
    [SerializeField] private TMP_Text result;
    [SerializeField] private GameObject panel;
    //[SerializeField] private Slider slide;
    [SerializeField] private float correct = 0;
    [SerializeField] private Button nexts;
    private bool answered = false;
    private bool firstatemt = true;
    private int answerselected;
    [SerializeField] private TMP_Text progress;
    public int totalquestions;


    // Start is called before the first frame update

    public Animator animator;
    Soundmanager Sou;
    void Start()
    {
      unanweredquestion = null;
      progress.text = correct.ToString() + "/" + totalquestions.ToString();
      unanweredquestion = quizs.ToList();
      getraindomquestion();
      Sou = Soundmanager.instance;            
    }

    void getraindomquestion()
    {
        if (correct== totalquestions)
        {
            // Handle the case when there are no more questions.
            Debug.Log("No more questions.");
            Result();
            return;
        }

        int randomquestionindex;
        quizs newQuestion;

        do
        {
            randomquestionindex = Random.Range(0, unanweredquestion.Count);
            newQuestion = unanweredquestion[randomquestionindex];
        } while (newQuestion == currentquestion);

        currentquestion = newQuestion;

        //unanweredquestion.RemoveAt(randomquestionindex);
        question.text = currentquestion.questioninfo;

        if (currentquestion.questiontype == questiontype.image)
        {
            questionImage.sprite = currentquestion.questionimage;
            questionImage.gameObject.SetActive(true);
        }
        else
        {
            questionImage.gameObject.SetActive(false);
        }

        if (currentquestion.questiontype == questiontype.imageanswer)
        {


            for (int i = 0; i < imagebuttons.Count; i++)
            {
                int buttonIndex = i;
                string optionText = currentquestion.options[i]; // Capture the option text in a local variable.
                imagess[i].sprite = currentquestion.answersimage[i];
                imagebuttons[i].onClick.RemoveAllListeners(); // Remove previous listeners to avoid duplicates.
                imagebuttons[i].onClick.AddListener(() => CheckAnswer(optionText, buttonIndex)); // Use the local variable.

            }


        }
        else
        {

            for (int i = 0; i < options.Count; i++)
            {
                int buttonIndex = i;
                string optionText = currentquestion.options[i]; // Capture the option text in a local variable.
                options[i].GetComponentInChildren<TextMeshProUGUI>().text = optionText;
                options[i].onClick.RemoveAllListeners(); // Remove previous listeners to avoid duplicates.
                options[i].onClick.AddListener(() => CheckAnswer(optionText, buttonIndex)); // Use the local variable.
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
        //result.text = "you got " + correct + " out of "+ quizs.Length.ToString();
        //if((correct/ quizs.Length)>0.5 && (correct / quizs.Length) < 0.74)
        //{
        //    animator.Play("onestar");
        //}
        //else if ((correct / quizs.Length) > 0.75 && (correct / quizs.Length) < 1)
        //{
        //    animator.Play("twostar");
        //}
        //else if ((correct / quizs.Length) == 1)
        //{
        //    animator.Play("threestar");
        //}
    }

    void CheckAnswer(string selectedOption,int button)
    {
        if (!answered)
        {
            Debug.Log(button);
            if (selectedOption == currentquestion.correctans)
            {
                Sou.Play(currentquestion.correctans);
                // The selected option is the correct answer.
                Debug.Log("Correct Answer!");

                if (currentquestion.questiontype == questiontype.imageanswer) 
                {
                    imagebuttons[button].GetComponent<Image>().color = new Color32(0, 255, 0, 50);
                }
                else
                {
                    options[button].GetComponent<Image>().color = new Color32(0, 255, 0, 100);
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
                    next();
                }
            }
            else
            {
                // The selected option is not correct.
                Debug.Log("Wrong Answer!");
                Sou.Play("wrong");
                if (currentquestion.questiontype == questiontype.imageanswer)
                {
                    imagebuttons[button].GetComponent<Image>().color = new Color32(255, 0, 0, 50);
                    
                }
                else
                {
                    options[button].GetComponent<Image>().color = new Color32(255, 0, 0, 100);
                }

                firstatemt = false;
            }
            progress.text = correct.ToString() +"/"+ totalquestions.ToString();

            // Call getRandomQuestion to display the next question.
            //getraindomquestion();
        }
    }

    public void next()
    {
        if (answered)
        {
            for (int i = 0; i < imagebuttons.Count; i++)
            {
                imagebuttons[i].interactable = true;
            }
            if (currentquestion.questiontype == questiontype.imageanswer)
            {
                for (int i = 0; i < imagebuttons.Count; i++)
                {
                    imagebuttons[i].GetComponent<Image>().color = new Color32(255, 255, 255, 0);
                }
            }
            else
            {
                for (int i = 0; i < options.Count; i++)
                {
                    options[i].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                }
            }


            getraindomquestion();
            answered = false;
            firstatemt = true;
        }
    }

}
