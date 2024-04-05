using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class slots : MonoBehaviour,IDropHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public alphabet alphabet;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    [SerializeField] private Canvas canvas;
    private Vector2 initialPosition;
    Transform parentAfterDrag;
    Sprite basic;
    public void remove()
    {
        alphabet = null;
        transform.gameObject.GetComponent<Image>().sprite = basic;
        transform.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(canvas.transform);
        transform.SetAsLastSibling();
        canvasGroup.blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (alphabet != null)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        transform.SetParent(parentAfterDrag);
        rectTransform.anchoredPosition = initialPosition;

    }


    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        holder gemHolder = droppedObject.GetComponent<holder>();

        if (gemHolder != null)
        {
        alphabet = eventData.pointerDrag.GetComponent<holder>().alphabet;
        transform.gameObject.GetComponent<Image>().sprite = alphabet.sprite;
        transform.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        basic = transform.gameObject.GetComponent<Image>().sprite;
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        parentAfterDrag = transform.parent;
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        initialPosition = rectTransform.anchoredPosition;
    }





}
