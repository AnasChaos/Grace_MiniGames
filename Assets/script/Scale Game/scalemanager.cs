using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class scalemanager : MonoBehaviour
{
    public scaleslot slotq;
    notesscale noteweight;
    float currentweight;
    public GameObject panel;
    bool havy;
    bool medium;
    bool normal;
    private Animator animator;
    string cstate;
    string currentstate;
    public scalesoltmanager sm;
    public GameObject win;
    public scalegamemanager manager;
    Soundmanager Sou;

    private void Start()
    {
        animator = GetComponent<Animator>();
        noteweight = slotq.scale;
        animator.Play("heavy");
        cstate = "hevy";
        Sou = Soundmanager.instance;
    }


    public void checkweight(List<notesscale> scaless)
    {
        currentweight = 0f;
        foreach (var note in scaless)
        {
            currentweight += note.weight;
        }
        
        if(noteweight.weight == currentweight)
        {
            if(cstate == "medium")
            {
                changestate("mediumtonormal");
            }
            else if (cstate == "light")
            {
                changestate("lighttonormal");
            }
            else if (cstate == "hevy")
            {
                changestate("mediumtonormal");
            }
            Sou.Play("correct");
            win.GetComponent<Image>().color = new Color32(0, 255, 0, 40);
            Debug.Log("sloved");
            manager.solvedscale();
            sm.solveds();

        }
        else
        {
            if ((currentweight/noteweight.weight)<0.5)
            {
                if(cstate != "hevy")
                {
                    changestate("mediumtohavy");
                    cstate = "hevy";
                }
            }
            else if ((currentweight / noteweight.weight) > 0.5 && (currentweight / noteweight.weight) < 1)
            {
                if (cstate != "medium")
                {
                    changestate("havy to midium");
                    cstate = "medium";
                }

            }
            else if ((currentweight / noteweight.weight)>1)
            {
                if (cstate != "light")
                {
                    changestate("normaltolight");
                    cstate = "light";
                }
            }
            Debug.Log((currentweight / noteweight.weight));

            Debug.Log("wrong weight");
            Debug.Log(currentweight);
            Debug.Log(noteweight.weight);
        }

    }

    void changestate(string state)
    {
        if (currentstate == state) return;

        animator.Play(state);
        currentstate = state;

    }


}
