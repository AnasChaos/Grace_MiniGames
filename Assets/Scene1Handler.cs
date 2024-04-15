using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene1Handler : MonoBehaviour
{
    [SerializeField] Steps steps;
    [SerializeField] int nextStepCounter;
    [SerializeField] Button nextStepOverlay;
    [SerializeField] Animator animator;
    [SerializeField] GameObject inventory;
    
    [Header("Dailouge Box")]
    [SerializeField] Text dialougeText;
    [SerializeField] GameObject queenImage;
    [SerializeField] GameObject selectCharacteImage;
    
    [Header("Step 11")]
    [SerializeField] Image inventoryBook;
    [SerializeField] GameObject book;

    [Header("Step 22")]
    [SerializeField] GameObject dailougeHolder;
    [SerializeField] Image imageHover;
    
    [Header("Step 23")]
    [SerializeField] Camera cameraPos;

    [Header("Step 24")]
    [SerializeField] Image step24Key;
    [SerializeField] GameObject openBook;
    [SerializeField] Image bookKeyImage;


    //public static Scene1Handler Instance
    //{
    //    get
    //    {
    //        if (instance == null)
    //        {
    //            instance = FindObjectOfType<Scene1Handler>();

    //            if (instance == null)
    //            {
    //                GameObject dailougeManager = new GameObject(typeof(Scene1Handler).Name);
    //                instance = dailougeManager.AddComponent<Scene1Handler>();
    //            }
    //        }
    //        return instance;
    //    }
    //}
    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //        Debug.Log("Dont Destroy");
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}
    IEnumerator Start()
    {
        string steps = Resources.Load<TextAsset>("Dialouges").ToString();
        Debug.Log(steps);
        this.steps = JsonConvert.DeserializeObject<Steps>(steps);
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1);
        StepCounter(0);
    }
    private void StepCounter(int stepCounter)
    {   
        this.nextStepCounter++;
        switch (stepCounter)
        {
            case 0:
                ShowDialogue(stepCounter);
                break;
            case 1:
                ShowDialogue(stepCounter);
                break;
            case 2:
                ShowDialogue(stepCounter);
                break;
            case 3:
                ShowDialogue(stepCounter);
                break;
            case 4:
                ShowDialogue(stepCounter);
                break;
            case 5:
                ShowDialogue(stepCounter);
                break;
            case 6:
                ShowDialogue(stepCounter);
                break;
            case 7:
                ShowDialogue(stepCounter);
                break;
            case 8:
                ShowDialogue(stepCounter);
                break;
            case 9:
                ShowDialogue(stepCounter);
                break;
            case 10:
                ShowDialogue(stepCounter);
                break;
            case 11:
                Step11();
                break;
            case 12:
                ShowDialogue(stepCounter);
                break;
            case 13:
                ShowDialogue(stepCounter);
                break;
            case 14:
                ShowDialogue(stepCounter);
                break;
            case 15:
                ShowDialogue(stepCounter);
                break;
            case 16:
                ShowDialogue(stepCounter);
                break;
            case 17:
                ShowDialogue(stepCounter);
                break;
            case 18:
                ShowDialogue(stepCounter);
                break;
            case 19:
                ShowDialogue(stepCounter);
                break;
            case 20:
                ShowDialogue(stepCounter);
                break;
            case 21:
                ShowDialogue(stepCounter);
                break;
            case 22:
                nextStepOverlay.interactable = false;
                Step22();
                break;
            case 23:
                Step23();
                break;
            case 24:
                Step24();
                break;
            case 25:
                Step25();
                break;                
            default:
                break;
        }
    }
    private void Step11()
    {
        LeanTween.moveLocalY(book.gameObject, -250, 1.5f).setOnComplete(() =>
        {
            LeanTween.moveY(inventory.gameObject, 0, 2f).setOnComplete(() =>
            {
                LeanTween.scale(book.gameObject, Vector2.zero, 2f).setOnComplete(() =>
                {
                    LeanTween.alpha(inventoryBook.rectTransform, 1, 2f).setEase(LeanTweenType.easeInQuad).setOnComplete(() =>
                    {
                        LeanTween.moveY(inventory.gameObject, -1000, 3f).setOnComplete(() =>
                        {
                            NextStep();
                        });
                    });
                });
            });
        });
    }
    private void Step22()
    {       
        LeanTween.moveY(dailougeHolder.gameObject, -1000, 1f).setOnComplete(() =>
        {
            LeanTween.alpha(imageHover.rectTransform, 0, 1.5f).setEase(LeanTweenType.easeInQuad).setOnComplete(() =>
            {
                LeanTween.alpha(imageHover.rectTransform, 0.5f, 1.5f).setEase(LeanTweenType.easeInQuad).setOnComplete(() =>
                {
                    if (this.nextStepCounter == 24) 
                    {
                        Invoke("NextStepOverlay", 1f);
                    }
                    else
                    {
                        Step22();
                        nextStepOverlay.interactable = false;

                    }                    
                    Debug.Log("Debug");
                });
            });
        });
    }
    private void Step23()
    {
        CameraPosAndSize(40);
    }
    private void Step24() 
    {
        LeanTween.alpha(step24Key.rectTransform, 1f, 2f).setEase(LeanTweenType.easeInQuad).setOnComplete(() =>
        {
            LeanTween.alpha(step24Key.rectTransform, 0f, 2f).setEase(LeanTweenType.easeInQuad).setOnComplete(() =>
            {   
                                
                LeanTween.moveY(inventory.gameObject, 50, 2f).setOnComplete(() =>
                {
                    openBook.transform.localScale = Vector3.zero;
                    LeanTween.scale(openBook, Vector2.one, 2f).setEase(LeanTweenType.easeInQuad).setOnComplete(() =>
                    {
                        LeanTween.alpha(bookKeyImage.rectTransform, 1f, 2f).setEase(LeanTweenType.easeInQuad).setOnComplete(() =>
                        {
                            LeanTween.scale(openBook, Vector2.zero, 2f).setEase(LeanTweenType.easeInQuad).setOnComplete(() =>
                            {
                                LeanTween.moveY(inventory.gameObject, -1000, 2f).setOnComplete(() =>
                                {
                                    CameraPosAndSize(80);
                                });
                            });

                        });

                    });
                });

            });
        });

    }
    private void Step25()
    {
        SceneManager.LoadScene("Tower");
    }

    public void NextStep()
    {
        StepCounter(nextStepCounter);
    }
    private void ShowDialogue(int stepCounter)
    {
        queenImage.gameObject.transform.localPosition = (IsContinueDialouge(stepCounter)) ? new Vector2(queenImage.transform.localPosition.x, queenImage.transform.localPosition.y) : new Vector2(-1000, queenImage.transform.localPosition.y);
        selectCharacteImage.gameObject.transform.localPosition = new Vector2(1000, -100f);
        dialougeText.gameObject.transform.localScale = Vector2.zero;
        nextStepOverlay.interactable = false;


        if (steps.steps[stepCounter].isQueen)
        {
            LeanTween.moveLocalX(queenImage.gameObject, 400, 1f).setOnComplete(() =>
            {
                dialougeText.text = steps.steps[stepCounter].dailouge;
                LeanTween.scale(dialougeText.gameObject, Vector2.one, 1f).setOnComplete(() =>
                {
                    Invoke("NextStepOverlay", 1f);
                });
            });
        }
        else
        {
            LeanTween.moveLocalX(selectCharacteImage.gameObject, -150, 1f).setOnComplete(() =>
            {
                Debug.Log(selectCharacteImage.gameObject.transform.position);
                dialougeText.text = steps.steps[stepCounter].dailouge;
                LeanTween.scale(dialougeText.gameObject, Vector2.one, 1f).setOnComplete(() =>
                {
                    Invoke("NextStepOverlay", 1f);
                });
            });
        }
    }
    private bool IsContinueDialouge(int stepCounter)
    {
        try
        {
            if (steps.steps[stepCounter].isQueen == steps.steps[stepCounter - 1].isQueen)
            {
                return true;
            }

        }
        catch (System.Exception ex)
        {

            Debug.Log(ex.Message);
        }


        return false;
    }
    private void CameraPosAndSize(int cameraSizeVal) 
    {
        LeanTween.value(cameraPos.orthographicSize, cameraSizeVal, 2f)
            .setOnUpdate((float value) =>
            {
                cameraPos.orthographicSize = value;
                imageHover.gameObject.SetActive(false);
            }).setOnComplete(() =>
            {
                NextStep(); 
            });
    }

    private void NextStepOverlay() 
    {
        nextStepOverlay.interactable = true;
    }
}


[System.Serializable]
public class Steps
{
    public List<Step> steps = new List<Step>();
}

[System.Serializable]
public class Step
{
    public int step;
    public string dailouge = "";
    public bool isQueen;
    public bool isTask;
}