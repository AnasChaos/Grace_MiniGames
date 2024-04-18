using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransistion : MonoBehaviour
{
    public float transitionTime = 1f;

    public void Start()
    {
        StartCoroutine(Transition(SceneManager.GetActiveScene().name));
    }

    public void NextScene(string sceneName) 
    {
        StartCoroutine(Transition(sceneName));
    }
    IEnumerator Transition(string sceneName)
    {
        if (SceneManager.GetActiveScene().name.Equals(sceneName)) 
        {
            LeanTween.alphaCanvas(GetComponent<CanvasGroup>(), 1f, transitionTime / 2f);
            this.gameObject.GetComponent<Image>().enabled = true;
            yield return new WaitForSeconds(transitionTime / 2f);
            yield return new WaitForEndOfFrame();
            LeanTween.alphaCanvas(GetComponent<CanvasGroup>(), 0f, transitionTime / 2f);
        }
        else
        {
            LeanTween.alphaCanvas(GetComponent<CanvasGroup>(), 1f, transitionTime / 2f);
            yield return new WaitUntil(() => GetComponent<CanvasGroup>().alpha == 1);
            SceneManager.LoadScene(sceneName);
        }
       
    }
}
