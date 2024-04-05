using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slotholdermanager : MonoBehaviour
{
    public gemslot[] gemslots;

    public int currentslot;
    public string selection;
    string[] high = {"D4","E4", "F4", "G4", "A4", "B4", "C5", "D5", "E5", "F5", "G5" };
    string[] low =  {"F2","G2", "A2", "B2", "C3", "D3", "E3", "F3", "G3", "A3", "B3" };






    private void Start()
    {
        foreach(var ge in gemslots)
        {
            ge.gemdrop += check;
        }
    }
    public void destroysall()
    {
        for (int i = 0; i < gemslots.Length; i++)
        {
            gemslots[i].removegem();
            currentslot = 0;
            selection = null;
        }
    }

    public void right()
    {
        gemslots[currentslot - 1].right();
    }
    public void wrong()
    {
        Debug.Log(currentslot-1);
        gemslots[currentslot - 1].wrong();
    }

    public void droped()
    {
        gemslots[currentslot - 1].removegem();
        currentslot = 0;
        selection = null;
    }
    void check(string me)
    {

        Debug.Log(selection);
        for (int i = 0; i < gemslots.Length; i++)
        {  
            if(gemslots[i].gem!= null)
            {
                //Debug.Log("checked");
                if (currentslot == 0)
                {
                    currentslot = 1 + i;
                    selection = gemslots[i].gem.gem;
                  //  Debug.Log(currentslot);
                }
                else if ((currentslot - 1) != i )
                {
                    gemslots[currentslot - 1].removegem();
                    currentslot = 1 + i;
                    selection = gemslots[i].gem.gem;
                    //Debug.Log(currentslot);
                }
                else if((currentslot - 1) == i && selection != gemslots[i].gem.gem) 
                {
                    selection = gemslots[i].gem.gem;
                }
            }
        }
        if (me == "dropped")
        {
            droped();
        }
    }


}
