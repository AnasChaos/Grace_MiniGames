using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scalegamemanager : MonoBehaviour
{

    int saclesclered;
    public GameObject panel;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void solvedscale()
    {
        saclesclered  +=1;
        if(saclesclered == 6)
        {
         panel.SetActive(true);
         //animator.Play("threestar");
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
