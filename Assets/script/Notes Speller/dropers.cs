using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class dropers : MonoBehaviour, IDropHandler
{

    public alphabet alphabet;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        if (eventData.pointerDrag != null)
        {
            slots slots = droppedObject.GetComponent<slots>();

            if (slots != null)
            {
                eventData.pointerDrag.GetComponent<slots>().remove();


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
