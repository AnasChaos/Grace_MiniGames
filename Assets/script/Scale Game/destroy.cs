using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class destroy : MonoBehaviour, IDropHandler
{
    scaleslot scale;
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("some one droped this");
        //Debug.Log("drop");
        if (eventData.pointerDrag != null)
        {
            try
            {
                eventData.pointerDrag.GetComponent<scaleslot>().de();
            }
            catch
            {
                Debug.Log("no scaleslot");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
