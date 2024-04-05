using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class tileslot : MonoBehaviour, IDropHandler
{
    public int x;
    public bool free;
    public tileobjects tileobjects;
    tilegridmanager TM;

    public void Initialize(tilegridmanager game, int tileX)
    {
        x = tileX;
        TM = game;
    }


    public void OnDrop(PointerEventData eventData)
    {

            GameObject droppedObject = eventData.pointerDrag;
            if (eventData.pointerDrag != null)
            {
                tileholder tileholder = droppedObject.GetComponent<tileholder>();

                if (tileholder != null)
                {
                tileobjects = eventData.pointerDrag.GetComponent<tileholder>().tileobjects;
                TM.Droped(tileobjects, this);



                //transform.gameObject.GetComponent<Image>().sprite = notepieces.sprite;
                  //  transform.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                }

            }
        
    }





}
