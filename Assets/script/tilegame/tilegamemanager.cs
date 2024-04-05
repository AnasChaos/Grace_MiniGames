using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tilegamemanager : MonoBehaviour
{

    public bool started;
    public bool win;
    Soundmanager Sou;
    public tilegridmanager[] TM;
    public GameObject pannel;



    public void chec()
    {
        for (int x = 0; x < TM.Length; x++)
        {
            if (!TM[x].completed)
            {
                return;
            }
            
        }
        pannel.SetActive(true);


    }



}
