using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;
    public List<Sprite> characters = new List<Sprite>();

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();

                if (instance == null)
                {
                    GameObject uIManager = new GameObject(typeof(UIManager).Name);
                    instance = uIManager.AddComponent<UIManager>();
                }
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PanelSwtiching(GameObject currentScreen, GameObject nextScreen = null, float duration = 0, Action onComplete = null)
    {
        if (nextScreen != null)
        {
            nextScreen.transform.localScale = Vector3.zero;
        }
        LeanTween.scale(currentScreen, Vector3.zero, duration)
               .setOnComplete(() =>
               {
                   if (nextScreen != null)
                   {
                       LeanTween.scale(nextScreen, Vector3.one, duration).setOnComplete(() =>
                       {
                           onComplete?.Invoke();
                       });
                   }
               });

    }

    public void ShowPopup(GameObject gO,bool isShow = true,Action callBack = null) 
    {   
        if(isShow) 
        {
            gO.transform.localScale = Vector3.zero;
            LeanTween.scale(gO, Vector3.one, 1f).setEaseOutExpo();
        }
        else 
        {
            LeanTween.scale(gO, Vector3.zero, 0.5f).setEaseInExpo().setOnComplete(() => 
            {
                callBack.Invoke();
            });
        }

    }

    public Sprite GetSelectedCharacter() 
    {
        return PlayerPrefs.GetString(Global.selectedCharaterKey) == "Castian" ? characters[0] : characters[1];
    }
    public string GetUserName()
    {
        return PlayerPrefs.GetString("userName");
    }
}
