using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene4Handler : MonoBehaviour
{
    [SerializeField] Steps steps;
    [SerializeField] int nextStepCounter = 38;
    [SerializeField] Button nextStepOverlay;
    [SerializeField] Animator animator;
    [SerializeField] GameObject inventory;

    [Header("Dailouge Box")]
    [SerializeField] Text dialougeText;
    [SerializeField] GameObject queenImage;
    [SerializeField] GameObject selectCharacteImage;

    [Header("Step 48")]
    [SerializeField] GameObject dailougeHolder;
    [SerializeField] GameObject food;
    [SerializeField] Image inventoryFood;
    [SerializeField] Image inventoryBeer;



    IEnumerator Start()
    {
        food.transform.localScale = Vector3.zero;
        food.transform.localScale = Vector3.zero;

        string steps = Resources.Load<TextAsset>("Dialouges").ToString();
        Debug.Log(steps);
        this.steps = JsonConvert.DeserializeObject<Steps>(steps);
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1);
        StepCounter(38);

    }

    private void StepCounter(int stepCounter)
    {
        Debug.Log($"Step counter: {stepCounter}");
        nextStepCounter++;
        switch (stepCounter)
        {
            case 38:
                ShowDialogue(stepCounter);
                break;
            case 39:
                ShowDialogue(stepCounter);
                break;
            case 40:
                ShowDialogue(stepCounter);
                break;
            case 41:
                ShowDialogue(stepCounter);
                break;
            case 42:
                ShowDialogue(stepCounter);
                break;
            case 43:
                ShowDialogue(stepCounter);
                break;
            case 44:
                ShowDialogue(stepCounter);
                break;
            case 45:
                ShowDialogue(stepCounter);
                break;
            case 46:
                ShowDialogue(stepCounter);
                break;
            case 47:
                ShowDialogue(stepCounter);
                break;
            case 48:
                Step48();
                break;
            case 49:
                Step49();
                break;
            default:
                break;

        }
    }

    private void Step48()
    {
        LeanTween.moveY(dailougeHolder.gameObject, -200, 1f).setOnComplete(() =>
        {
            LeanTween.moveY(inventory.gameObject, 20, 1f).setDelay(1f).setOnComplete(() =>
            {
                LeanTween.alpha(inventoryFood.rectTransform, 0, 1f);
                LeanTween.alpha(inventoryBeer.rectTransform, 0, 1f).setOnComplete(() =>
                {
                    LeanTween.scale(food.gameObject, Vector2.one, 1f).setDelay(1).setOnComplete(() =>
                    {
                        LeanTween.moveY(inventory.gameObject, -100, 1f).setDelay(1).setOnComplete(() => 
                        {
                            LeanTween.scale(food.gameObject, Vector2.zero, 1f).setDelay(1).setOnComplete(() =>
                            {
                                NextStep();
                            });

                        });

                    });

                });

            });

        });

    }

    private void Step49() 
    {
        SceneManager.LoadScene("Notes Speller");
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

    public void NextStep(Button btn = null)
    {
        StepCounter(nextStepCounter);
        if (btn != null)
        {
            btn.interactable = false;
        }
    }

    private void NextStepOverlay()
    {
        nextStepOverlay.interactable = true;
    }


}
