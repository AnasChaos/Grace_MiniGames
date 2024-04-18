using Newtonsoft.Json;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene3Handler : MonoBehaviour
{
    [SerializeField] Steps steps;
    [SerializeField] int nextStepCounter = 31;
    [SerializeField] Camera cameraPos;
    [SerializeField] GameObject inventory;

    [Header("Step 31")]
    [SerializeField] Image effectHover;

    [Header("Step 32")]
    [SerializeField] Image emptyJar;
    [SerializeField] Image filledJar;
    [SerializeField] Image emptyInventoryJar;
    [SerializeField] Image filledInventoryJar;

    [Header("Step 34")]
    [SerializeField] Image effectHover2;

    [Header("Step 36")]
    [SerializeField] Image knifeInventory;
    [SerializeField] Image knife;
    [SerializeField] Image food;
    [SerializeField] Image foodInventory;
    
    [Header("Step 37")]
    [SerializeField] Image effectHover3;


    private void Start()
    {
        cameraPos.orthographicSize = 40f;
        string steps = Resources.Load<TextAsset>("Dialouges").ToString();
        Debug.Log(steps);
        this.steps = JsonConvert.DeserializeObject<Steps>(steps);
        CameraPosAndSize(80);
        StepCounter(31);
        ImageAlpha(effectHover, 0);
        effectHover.gameObject.SetActive(false);
        emptyJar.transform.localScale = Vector3.zero;
        ImageAlpha(filledJar, 0);
        ImageAlpha(filledInventoryJar, 0);
        ImageAlpha(effectHover2,0);
        effectHover2.gameObject.SetActive(false);
        knifeInventory.gameObject.SetActive(false);
        food.transform.localScale = Vector3.zero;
        knife.transform.localScale = Vector3.zero;
        ImageAlpha(foodInventory, 0);
        ImageAlpha(effectHover3, 0);
        effectHover3.gameObject.SetActive(false);

    }

    private void StepCounter(int stepCounter)
    {
        Debug.Log($"Step counter: {stepCounter}");
        nextStepCounter++;
        switch (stepCounter)
        {
            case 31:
                Step31();
                break;
            case 32:
                Step32();
                break;
            case 33:
                Step33();
                break;
            case 34:
                Step34();
                break;
            case 35:
                Step35();
                break;
            case 36:
                Step36();
                break;
            case 37:
                Step37();
                break;
            case 38:
                Step38();
                break;
            default:
                break;

        }
    }

    private void Step31()
    {   
        effectHover.gameObject.SetActive(true);
        LeanTween.alpha(effectHover.rectTransform, 0, 1.5f).setEase(LeanTweenType.easeInQuad).setOnComplete(() =>
        {
            LeanTween.alpha(effectHover.rectTransform, 1f, 1.5f).setEase(LeanTweenType.easeInQuad).setOnComplete(() =>
            {
                if (this.nextStepCounter >= 33)
                {
                    effectHover.gameObject.SetActive(false);
                    return;
                }
                else
                {
                    Step31();
                }
                Debug.Log("Debug");
            });
        });

    }

    private void Step32()
    {
        CameraPosAndSize(40, true, 900, 55f);
    }

    private void Step33()
    {
        LeanTween.moveY(inventory.gameObject, 10, 2f).setDelay(1).setOnComplete(() =>
        {
            LeanTween.moveLocalY(emptyJar.gameObject, 0, 1f);
            LeanTween.scale(emptyJar.gameObject, new Vector3(2,2,2), 1f).setOnComplete(() =>
            {
                LeanTween.alpha(filledJar.rectTransform, 1f, 2f).setDelay(1).setOnComplete(() =>
                {
                    emptyJar.gameObject.SetActive(false);
                    LeanTween.moveLocalY(filledJar.gameObject, -300, 1f);
                    LeanTween.scale(filledJar.gameObject, Vector2.zero, 1f).setOnComplete(() =>
                    {
                        LeanTween.alpha(filledInventoryJar.rectTransform, 1f, 2f).setDelay(1).setOnComplete(() =>
                        {
                            LeanTween.moveY(inventory.gameObject, -50, 5f).setDelay(1).setOnComplete(() =>
                            {
                                CameraPosAndSize(80, true, 855, 91);
                            });

                        });

                    });

                });

            });

        });


    }

    private void Step34()
    {
        effectHover2.gameObject.SetActive(true);
        LeanTween.alpha(effectHover2.rectTransform, 0, 1.5f).setEase(LeanTweenType.easeInQuad).setOnComplete(() =>
        {
            LeanTween.alpha(effectHover2.rectTransform, 1f, 1.5f).setEase(LeanTweenType.easeOutQuad).setOnComplete(() =>
            {
                if (this.nextStepCounter >= 36)
                {
                    effectHover2.gameObject.SetActive(false);
                    return;
                }
                else
                {
                    Step34();
                }
                Debug.Log("Debug");
            });
        });

    }

    private void Step35()
    {
        CameraPosAndSize(40, true, 810, 61);
    }
    private void Step36()
    {   
        effectHover2.gameObject.SetActive(false);
        knifeInventory.gameObject.SetActive(true);
        LeanTween.moveY(inventory.gameObject, 20, 1.5f).setDelay(1).setOnComplete(() =>
        {
            LeanTween.moveLocalY(knife.gameObject, 20, 1f).setDelay(1f);
            LeanTween.scale(knife.gameObject, new Vector2(2, 2), 1f).setDelay(1f).setOnComplete(() =>
            {
                LeanTween.rotateZ(knife.gameObject, 40, 1f).setDelay(1f).setOnComplete(() =>
                {
                    LeanTween.moveLocalY(knife.gameObject, -300, 1f).setDelay(1f);
                    LeanTween.scale(knife.gameObject, Vector2.zero, 1.5f).setDelay(1f).setOnComplete(() =>
                    {
                        LeanTween.scale(food.gameObject, new Vector2(2, 2), 1.5f).setDelay(1).setOnComplete(() =>
                        {
                            LeanTween.moveLocalY(knife.gameObject, -300, 1f).setDelay(1);
                            LeanTween.moveLocalY(food.gameObject, -300, 1f).setDelay(1);
                            LeanTween.scale(food.gameObject, Vector2.zero, 1f).setDelay(1).setOnComplete(() =>
                            {                                
                                LeanTween.alpha(foodInventory.rectTransform, 1, 1.5f).setDelay(1).setOnComplete(() =>
                                {
                                    LeanTween.moveY(inventory.gameObject, -50, 1.5f).setDelay(1).setOnComplete(() =>
                                    {
                                        CameraPosAndSize(80, true, 855, 91);
                                    });

                                });
                            });
                        });

                    });

                });
            });
        });

    }
    private void Step37() 
    {
        effectHover3.gameObject.SetActive(true);
        LeanTween.alpha(effectHover3.rectTransform, 0, 1.5f).setEase(LeanTweenType.easeInQuad).setOnComplete(() =>
        {
            LeanTween.alpha(effectHover3.rectTransform, 1f, 1.5f).setEase(LeanTweenType.easeInQuad).setOnComplete(() =>
            {
                if (this.nextStepCounter >= 39)
                {
                    effectHover3.gameObject.SetActive(false);
                    return;
                }
                else
                {
                    Step37();
                }
                Debug.Log("Debug");
            });
        });
    }
    private void Step38()
    {
        SceneManager.LoadScene("Cell Door");
    }
    public void NextStep(Button btn = null)
    {
        StepCounter(nextStepCounter);
        if (btn != null)
        {
            btn.interactable = false;
        }
    }


    private void CameraPosAndSize(int cameraSizeVal,bool isPosChange = false,float valueX = 0, float valueY = 0)
    {   //915 //55
        if (isPosChange) 
        {
            LeanTween.move(cameraPos.gameObject, new Vector2(valueX, valueY), 1).setDelay(1).setOnComplete(() =>
            {
                NextStep();
            });
        }

        LeanTween.value(cameraPos.orthographicSize, cameraSizeVal, 2f)
        .setOnUpdate((float value) =>
        {
            cameraPos.orthographicSize = value;
            effectHover.gameObject.SetActive(false);
        }).setOnComplete(() =>
        {            
            effectHover.gameObject.SetActive(!isPosChange);
        });
        
        
    }

    private void ImageAlpha(Image img, float value)
    {
        var color = img.color;
        color.a = value;
        img.color = color;
    }

}
