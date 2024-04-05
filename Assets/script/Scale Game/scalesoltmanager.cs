using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class scalesoltmanager : MonoBehaviour, IDropHandler
{
    public scalemanager sm;
    notesscale scale;
    public GameObject rows;
    List<notesscale> scaless = new List<notesscale>();
    public bool solved;
    List<scaleslot> slots = new List<scaleslot>();
    public void OnDrop(PointerEventData eventData)
    {
        if (!solved)
        {
            Debug.Log("some one droped this");
            //Debug.Log("drop");
            if (eventData.pointerDrag != null)
            {
                scale = eventData.pointerDrag.GetComponent<scaleweightholder>().scales;
                GameObject uiElement = Instantiate(rows);
                uiElement.transform.SetParent(transform, false);
                uiElement.transform.GetComponent<scaleslot>().scale = scale;
                uiElement.transform.GetComponent<Image>().sprite = scale.sprite;
                scaless.Add(scale);
                slots.Add(uiElement.transform.GetComponent<scaleslot>());
                sm.checkweight(scaless);
            }
        }
    }

    public void delet(notesscale ss)
    {

            scaless.Remove(ss);
            sm.checkweight(scaless);
        
    }
    public void solveds()
    {
        solved = true;
        foreach (var note in slots)
        {
            note.solved = true;
        }
    }

}
