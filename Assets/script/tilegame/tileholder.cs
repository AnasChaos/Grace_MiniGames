using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class tileholder : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    [SerializeField] private Canvas canvas;
    private Vector2 initialPosition;

    public tileobjects tileobjects;


    private void Start()
    {

        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        // Set the image sprite
        transform.gameObject.GetComponent<Image>().sprite = tileobjects.sprite;
        initialPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("i am being draged");
        // When dragging starts, disable raycasting on this object
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Update the position of the object to follow the mouse/finger
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // When dragging ends, enable raycasting on this object
        canvasGroup.blocksRaycasts = true;
        rectTransform.anchoredPosition = initialPosition;
    }
}
