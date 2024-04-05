using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spicemanager : MonoBehaviour
{

    bool started;
    bool win;
    public GameObject pannel;
    public spiceslot[] slotes;
    public notepieces[] answers;

    public void checks()
    {
        for (int i = 0; i < slotes.Length; i++)
        {
            Debug.Log(i);
            if(slotes[i].piecese != answers[i])
            {
                return;
            }
        }
        wins();


    }

    public void wins()
    {
        win = true;
        pannel.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
