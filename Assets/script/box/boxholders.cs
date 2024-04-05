using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class boxholders : MonoBehaviour, IDropHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public tunnelletters keys;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    [SerializeField] private Canvas canvas;
    private Vector2 initialPosition;
    Transform parentAfterDrag;








    private void Start()
    {

        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        initialPosition = rectTransform.anchoredPosition;

        parentAfterDrag = transform.parent;

    }
    public void set(tunnelletters ee )
    {
        keys = ee;
        transform.gameObject.GetComponent<Image>().sprite = keys.sprite;
        transform.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);

    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        if (eventData.pointerDrag != null)
        {
            boxholders gemHolder = droppedObject.GetComponent<boxholders>();
            tunnelletters TEMPS;
            if (gemHolder != null)
            {
                TEMPS = keys; 
                keys = eventData.pointerDrag.GetComponent<boxholders>().keys;
                transform.gameObject.GetComponent<Image>().sprite = keys.sprite;
                transform.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                eventData.pointerDrag.GetComponent<boxholders>().set(TEMPS);
                //Debug.Log(alphabet.letter);
                //RaiseEvent("removed");
            }
            else
            {


            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // When dragging starts, disable raycasting on this object
        parentAfterDrag = transform.parent;
        transform.SetParent(canvas.transform);
        transform.SetAsLastSibling();
        canvasGroup.blocksRaycasts = false;
        Debug.Log("i am being draged");
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Update the position of the object to follow the mouse/finger
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        //transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // When dragging ends, enable raycasting on this object
        transform.SetParent(parentAfterDrag);
        canvasGroup.blocksRaycasts = true;

        rectTransform.anchoredPosition = initialPosition;

    }
}
