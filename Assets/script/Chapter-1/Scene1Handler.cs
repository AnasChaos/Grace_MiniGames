using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
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
    [SerializeField] Button skipConvo;

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

    IEnumerator Start()
    {
        string steps = Resources.Load<TextAsset>("Dialouges").ToString();
        Debug.Log(steps);
        //selectCharacteImage.GetComponent<Image>().sprite = UIManager.instance.GetSelectedCharacter();
        this.steps = JsonConvert.DeserializeObject<Steps>(steps);
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1);
        StepCounter(0);
    }
    private void StepCounter(int stepCounter)
    {    
        Debug.Log($"Step counter: {stepCounter}");
        CancelInvoke("NextStepOverlay");
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
                nextStepOverlay.gameObject.SetActive(true);
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
                nextStepOverlay.gameObject.SetActive(false);                
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
        nextStepOverlay.interactable = false;
        LeanTween.moveLocalY(book.gameObject, -250, 1f).setOnComplete(() =>
        {
            LeanTween.moveY(inventory.gameObject, 0, 1f).setOnComplete(() =>
            {
                LeanTween.moveLocalY(book.gameObject, -800, 1f);
                LeanTween.scale(book.gameObject, Vector2.zero, 1f).setOnComplete(() =>
                {
                    LeanTween.alpha(inventoryBook.rectTransform, 1, 1f).setEase(LeanTweenType.easeInQuad).setOnComplete(() =>
                    {
                        LeanTween.moveY(inventory.gameObject, -100, 1f).setOnComplete(() =>
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
        LeanTween.moveY(dailougeHolder.gameObject, -100, 1f).setOnComplete(() =>
        {
            LeanTween.alpha(imageHover.rectTransform, 0, 1.5f).setEase(LeanTweenType.easeInQuad).setOnComplete(() =>
            {
                LeanTween.alpha(imageHover.rectTransform, 0.5f, 1.5f).setEase(LeanTweenType.easeInQuad).setOnComplete(() =>
                {
                    if (this.nextStepCounter >= 24) 
                    {
                        //Invoke("NextStepOverlay", 1f);
                        return;
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
        nextStepOverlay.interactable = false;        
        CameraPosAndSize(40);
    }
    private void Step24() 
    {
        nextStepOverlay.interactable = false;
        LeanTween.alpha(step24Key.rectTransform, 1f, 1f).setEase(LeanTweenType.easeInQuad).setOnComplete(() =>
        {
            //LeanTween.alpha(step24Key.rectTransform, 0f, 2f).setEase(LeanTweenType.easeInQuad).setOnComplete(() =>
            //{                                   
                LeanTween.moveY(inventory.gameObject, 50, 1f).setOnComplete(() =>
                {
                    openBook.transform.localScale = Vector3.zero;
                    LeanTween.moveLocalY(openBook, 50, 1f);
                    LeanTween.scale(openBook, Vector2.one, 1f).setEase(LeanTweenType.easeInQuad).setOnComplete(() =>
                    {
                        LeanTween.alpha(step24Key.rectTransform, 0f, 1f).setEase(LeanTweenType.easeInQuad);
                        LeanTween.alpha(bookKeyImage.rectTransform, 1f, 1f).setDelay(1).setEase(LeanTweenType.easeInQuad).setOnComplete(() =>
                        {
                            LeanTween.moveLocalY(openBook, -200, 1f);
                            LeanTween.scale(openBook, Vector2.zero, 1f).setOnComplete(() =>
                            {
                                LeanTween.moveY(inventory.gameObject, -100, 1f).setOnComplete(() =>
                                {
                                    CameraPosAndSize(80);
                                });
                            });

                        });

                    });
                });

            //});
        });

    }
    private void Step25()
    {
        SceneManager.LoadScene("Tower");
    }

    public void NextStep(Button btn = null)
    {
        StepCounter(nextStepCounter);
        if(btn != null) 
        {
            btn.interactable = false;
        }
    }
    private void ShowDialogue(int stepCounter)
    {
        queenImage.gameObject.transform.localPosition = (IsContinueDialouge(stepCounter)) ? new Vector2(queenImage.transform.localPosition.x, queenImage.transform.localPosition.y) : new Vector2(-1000, queenImage.transform.localPosition.y);
        selectCharacteImage.gameObject.transform.localPosition = new Vector2(1000, -100f);
        //dialougeText.gameObject.transform.localScale = Vector2.zero;
        nextStepOverlay.interactable = false;
        dialougeText.text = string.Empty;
        StopAllCoroutines();


        if (steps.steps[stepCounter].isQueen)
        {
            LeanTween.moveLocalX(queenImage.gameObject, 400, 1f).setOnComplete(() =>
            {
                StartCoroutine(WriteText(steps.steps[stepCounter].dailouge));
                //dialougeText.text = steps.steps[stepCounter].dailouge;
                //LeanTween.scale(dialougeText.gameObject, Vector2.one, 1f).setOnComplete(() =>
                //{   
                //    Invoke("NextStepOverlay", 1f);
                //});
            });
        }
        else
        {
            LeanTween.moveLocalX(selectCharacteImage.gameObject, -150, 1f).setOnComplete(() =>
            {
                StartCoroutine(WriteText(steps.steps[stepCounter].dailouge));
                //dialougeText.text = steps.steps[stepCounter].dailouge;
                //LeanTween.scale(dialougeText.gameObject, Vector2.one, 1f).setOnComplete(() =>
                //{
                //    Invoke("NextStepOverlay", 1f);
                //});
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
        if (!skipConvo.gameObject.activeInHierarchy && this.nextStepCounter < 22)
        {
            skipConvo.gameObject.SetActive(true);
        }
    }
    
    public void SkipConversation() 
    {   
        skipConvo.gameObject.SetActive(false);
        nextStepOverlay.interactable = false;
        nextStepOverlay.gameObject.SetActive(false);
        CancelInvoke("NextStepOverlay");
        if(this.nextStepCounter < 10) 
        {               
            ShowDialogue(10);
            this.nextStepCounter = 11;            
            NextStep();
        }
        else if(this.nextStepCounter > 10) 
        {
            ShowDialogue(21);
            this.nextStepCounter = 22;
            NextStep();
        }
    }

    IEnumerator WriteText(string fullText)
    {   
        int writtenCounter = 0;
        Invoke("NextStepOverlay", 1f);
        for (int i = 0; i < fullText.Length; i++)
        {           
            writtenCounter++;
            dialougeText.text += fullText[i];          
           
            yield return new WaitForSeconds(0.08f);
        }
    }
}
