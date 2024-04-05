using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class gemslot : MonoBehaviour, IDropHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    //reffferences-------------------------
    public gems gem;
    Sprite basic;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    [SerializeField] private Canvas canvas;
    private Vector2 initialPosition;
    Transform parentAfterDrag;
    public bool droped = false; 

    //events systems-------------------------
    public delegate void MyEventDelegate(string message);
    public event MyEventDelegate gemdrop;
    public void RaiseEvent(string message)
    {
        gemdrop?.Invoke(message);
    }


    //funtions----------------------------------------
    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(canvas.transform);
        transform.SetAsLastSibling();
        canvasGroup.blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (gem != null)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        transform.SetParent(parentAfterDrag);
        rectTransform.anchoredPosition = initialPosition;
        if (droped)
        {
            droped = false;
            RaiseEvent("dropped");
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        if (eventData.pointerDrag != null)
        {
            gemholder gemHolder = droppedObject.GetComponent<gemholder>();

            if (gemHolder != null)
            {
                gem = eventData.pointerDrag.GetComponent<gemholder>().gem;
                transform.gameObject.GetComponent<Image>().sprite = gem.sprite;
                transform.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                //Debug.Log(alphabet.letter);
                RaiseEvent("removed");
            }
            else
            {
                gem = eventData.pointerDrag.GetComponent<gemslot>().gem;
                transform.gameObject.GetComponent<Image>().sprite = gem.sprite;
                transform.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                RaiseEvent("removed");
            }
        }
    }





    void Start()
    {
        parentAfterDrag = transform.parent;
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        initialPosition = rectTransform.anchoredPosition;

        basic = transform.gameObject.GetComponent<Image>().sprite;
    }

    public void right()
    {
        transform.gameObject.GetComponent<Image>().color = new Color32(0, 255, 0, 255);
    }
    public void wrong()
    {
        transform.gameObject.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
    }


    public void removegem()
    {
        gem = null;
        if (basic != null)
        {
            transform.gameObject.GetComponent<Image>().sprite = basic;
            transform.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 50);
        }
    }



}
