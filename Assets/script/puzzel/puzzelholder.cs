using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class puzzelholder : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    [SerializeField] private Canvas canvas;
    private Vector2 initialPosition;

    public notepieces pieces;
    private Vector2 originalSize;
    public float scaleFactor;


    //event system------------------------------------
    public delegate void MyEventDelegate(puzzelholder puzzelholders);

    public event MyEventDelegate MyEvent;

    public void RaiseEvent()
    {

        MyEvent?.Invoke(this);
    }
    //---------------------------




    private void Start()
    {

        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        // Set the image sprite
        transform.gameObject.GetComponent<Image>().sprite = pieces.sprite;
        initialPosition = rectTransform.anchoredPosition;
        // Store the original size of the image
        originalSize = rectTransform.sizeDelta;
    }

    public void des()
    {
        Destroy(this.gameObject);
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
        rectTransform.sizeDelta = originalSize * scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // When dragging ends, enable raycasting on this object
        RaiseEvent();
        canvasGroup.blocksRaycasts = true;
        rectTransform.anchoredPosition = initialPosition;
        rectTransform.sizeDelta = originalSize;

    }
}
