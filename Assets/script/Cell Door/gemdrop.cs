using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class gemdrop : MonoBehaviour, IDropHandler
{

    public gems gem;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        if (eventData.pointerDrag != null)
        {
            gemslot gemslots = droppedObject.GetComponent<gemslot>();

            if (gemslots != null)
            {
                eventData.pointerDrag.GetComponent<gemslot>().droped = true;

            }
            else
            {

            }
        }
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
