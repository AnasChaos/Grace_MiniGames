using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzelmanager : MonoBehaviour
{
    public bool started;
    public bool win;
    [SerializeField] private GameObject panel;
    public List<puzzelslot> PS;
    // Start is called before the first frame update

    public void starteds()
    {
        started = true;
    }

    public void check()
    {
        bool allcorrect = true;

        for (int y = 0; y < PS.Count; y++)
        {
            if (PS[y].set == false)
            {
                allcorrect = false;
                break;  // Break the loop early if any set is false
            }
        }

        if (allcorrect)
        {
            wins();  // Call wins method outside the loop
        }
    }
    public void wins()
    {
        panel.SetActive(true);
    }

}
