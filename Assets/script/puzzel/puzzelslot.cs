using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class puzzelslot : MonoBehaviour
{


    public notepieces pieces;
    public bool set = false;
    public List<puzzelholder> PH;
    public float mindistance;
    public puzzelmanager mm;

    private void Start()
    {
        foreach (puzzelholder ph in PH)
        {
            ph.MyEvent += OnDrops;
        }
    }

    private void OnDrops(puzzelholder pp)
    {

        float currentdistance = Vector2.Distance(pp.transform.position, transform.position);
        Debug.Log(currentdistance);
        if (currentdistance <= mindistance)
        {
            if (pp.pieces.number == pieces.number)
            {
                Debug.Log("droppedhere1");
                set = true;
                transform.GetComponent<Image>().sprite = pieces.sprite;
                transform.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                pp.des();
                mm.check();
            }
        }
    }


}
