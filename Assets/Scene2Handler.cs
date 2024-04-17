using Newtonsoft.Json;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene2Handler : MonoBehaviour
{
    [SerializeField] Steps steps;
    [SerializeField] int nextStepCounter = 26;
    [SerializeField] Camera cameraPos;
    [SerializeField] GameObject inventory;

    [Header("Step 26")]
    [SerializeField] Image effectHover;

    [Header("Step 27")]
    [SerializeField] Animator animatorPapers;
    [SerializeField] GameObject openBook;
    [SerializeField] Image key2;

    [Header("Step 28")]
    [SerializeField] Image effectHover2;

    [Header("Step 29")]
    [SerializeField] GameObject chest;
    [SerializeField] Animator animatorChest;
    [SerializeField] Image daimonds;




    private void Start()
    {
        cameraPos.orthographicSize = 40f;
        string steps = Resources.Load<TextAsset>("Dialouges").ToString();
        Debug.Log(steps);
        this.steps = JsonConvert.DeserializeObject<Steps>(steps);
        CameraPosAndSize(80);
        openBook.transform.localScale = Vector3.zero;
        animatorPapers.transform.localScale = Vector3.zero;
        ImageAlpha(effectHover, 0);
        effectHover.gameObject.SetActive(false);
        effectHover2.gameObject.SetActive(false);
        ImageAlpha(effectHover2, 0);
        ImageAlpha(daimonds, 0);
        animatorChest.transform.localScale = Vector3.zero;
    }

    private void StepCounter(int stepCounter)
    {
        Debug.Log($"Step counter: {stepCounter}");
        nextStepCounter++;
        switch (stepCounter)
        {
            case 26:
                Step26();
                break;
            case 27:
                StartCoroutine(Step27());
                break;
            case 28:
                Step28();
                break;
            case 29:
                StartCoroutine(Step29());
                break;
            case 30:
                Step30();
                break;
            default:
                break;

        }
    }

    private void Step26()
    {   
        effectHover.gameObject.SetActive(true);
        LeanTween.alpha(effectHover.rectTransform, 0, 1.5f).setEase(LeanTweenType.easeInQuad).setOnComplete(() =>
        {
            LeanTween.alpha(effectHover.rectTransform, 1f, 1.5f).setEase(LeanTweenType.easeInQuad).setOnComplete(() =>
            {
                if (this.nextStepCounter >= 28)
                {
                    effectHover.gameObject.SetActive(false);
                    return;
                }
                else
                {
                    Step26();                   
                }
                Debug.Log("Debug");
            });
        });

    }

    private IEnumerator Step27()
    {
        animatorPapers.enabled = false;
        animatorPapers.gameObject.SetActive(true);
        LeanTween.scale(animatorPapers.gameObject, Vector2.one,2f).setOnComplete(() =>
        {
            animatorPapers.enabled = true;
        });
        yield return new WaitUntil(() => animatorPapers.GetCurrentAnimatorStateInfo(0).normalizedTime > 1);
        LeanTween.moveY(inventory.gameObject, 10, 2f).setOnComplete(() =>
        {   
            openBook.SetActive(true);
            LeanTween.scale(openBook, Vector3.one, 2f).setOnComplete(() =>
            {
                LeanTween.moveY(inventory.gameObject, -50, 2f).setOnComplete(() =>
                {
                    LeanTween.alpha(key2.rectTransform, 1f, 2f).setEase(LeanTweenType.easeInQuad).setOnComplete(() =>
                    {
                        LeanTween.scale(animatorPapers.gameObject, Vector3.zero, 2f).setOnComplete(() =>
                        {
                            LeanTween.moveY(inventory.gameObject, 10, 2f).setOnComplete(() =>
                            {
                                LeanTween.scale(openBook.gameObject, Vector3.zero, 2f).setOnComplete(() =>
                                {
                                    LeanTween.moveY(inventory.gameObject, -50, 2f).setOnComplete(() =>
                                    {
                                        NextStep();
                                    });
                                });
                            });
                        });
                    });
                });               

            });
        });
    }

    private void Step28()
    {
        effectHover2.gameObject.SetActive(true);  
        LeanTween.alpha(effectHover2.rectTransform, 0, 1.5f).setEase(LeanTweenType.easeInQuad).setOnComplete(() =>
        {
            LeanTween.alpha(effectHover2.rectTransform, 1f, 1.5f).setEase(LeanTweenType.easeInQuad).setOnComplete(() =>
            {
                if (this.nextStepCounter >= 30)
                {
                    effectHover2.gameObject.SetActive(false);
                    return;
                }
                else
                {
                      Step28();  
                }
            });
        });
    }
   
    private IEnumerator Step29()
    {
        animatorChest.enabled = false;
        chest.gameObject.SetActive(true);
        LeanTween.scale(chest, Vector3.one, 2f).setOnComplete(() =>
        {
            animatorChest.enabled = true;
        });
        yield return new WaitUntil(() => animatorChest.GetCurrentAnimatorStateInfo(0).normalizedTime > 1);
        LeanTween.alpha(daimonds.rectTransform, 1f, 1f);
        yield return new WaitForSeconds(2.5f);
        NextStep();
    }

    private void Step30() 
    {
        SceneManager.LoadScene("Cellar Game");
    }

    private void CameraPosAndSize(int cameraSizeVal)
    {
        LeanTween.value(cameraPos.orthographicSize, cameraSizeVal, 2f)
            .setOnUpdate((float value) =>
            {
                cameraPos.orthographicSize = value;
                effectHover.gameObject.SetActive(false);
            }).setOnComplete(() =>
            {
                StepCounter(26);
                effectHover.gameObject.SetActive(true);
            });
    }
    public void NextStep(Button btn = null)
    {
        StepCounter(nextStepCounter);
        if (btn != null)
        {
            btn.interactable = false;
        }
    }

    private void ImageAlpha(Image img,float value) 
    {
        var color = img.color;
        color.a = value;
        img.color = color;
    }
}
