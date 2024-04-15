using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scene2Handler : MonoBehaviour
{
    [SerializeField] Steps steps;
    [SerializeField] int nextStepCounter;
    [SerializeField] Image effectHover;
    [SerializeField] Camera cameraPos;


    private void Start()
    {
        cameraPos.orthographicSize = 40f;
        string steps = Resources.Load<TextAsset>("Dialouges").ToString();
        Debug.Log(steps);
        this.steps = JsonConvert.DeserializeObject<Steps>(steps);
        CameraPosAndSize(80);
        StepCounter(26);
    }

    private void StepCounter(int stepCounter)
    {
        switch (stepCounter)
        {
            case 26:
                Step26();
                break;
            case 27:
                break;
            case 28:
                break;
            case 29:
                break;

            default:
                break;

        }
    }

    private void Step26()
    {
        LeanTween.alpha(effectHover.rectTransform, 0.5f, 1.5f).setEase(LeanTweenType.easeInQuad).setOnComplete(() =>
        {
            if (this.nextStepCounter == 28)
            {
                
            }
            else
            {
                Step26();
            }
            Debug.Log("Debug");
        });

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

            });
    }
}
