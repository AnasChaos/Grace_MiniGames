using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class spiceslot : MonoBehaviour, IDropHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public notepieces piecese;
    public notepieces prev;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    [SerializeField] private Canvas canvas;
    private Vector2 initialPosition;
    Transform parentAfterDrag;
    public bool fixs;

    private void Start()
    {

        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        initialPosition = rectTransform.anchoredPosition;

        parentAfterDrag = transform.parent;
        if (piecese != null)
        {
            transform.gameObject.GetComponent<Image>().sprite = piecese.sprite;
            transform.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }

    }
    public void removes()
    {
        piecese = null;
        //transform.gameObject.GetComponent<Image>().sprite = colors.sprite;
        transform.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 0);

    }
    public void sets(notepieces pie)
    {
        piecese = pie;
        transform.gameObject.GetComponent<Image>().sprite = piecese.sprite;
        transform.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);

    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!fixs) {
            GameObject droppedObject = eventData.pointerDrag;
            if (eventData.pointerDrag != null)
            {
                spiceholder gemHolder = droppedObject.GetComponent<spiceholder>();

                if (gemHolder != null)
                {
                    piecese = eventData.pointerDrag.GetComponent<spiceholder>().pieces;
                    transform.gameObject.GetComponent<Image>().sprite = piecese.sprite;
                    transform.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                    eventData.pointerDrag.GetComponent<spiceholder>().des();
                    //Debug.Log(alphabet.letter);
                    //RaiseEvent("removed");
                }
                else
                {
                    if (eventData.pointerDrag.GetComponent<spiceslot>().fixs)
                    {
                        return;
                    }
                    prev = piecese;
                    piecese = eventData.pointerDrag.GetComponent<spiceslot>().piecese;
                    transform.gameObject.GetComponent<Image>().sprite = piecese.sprite;
                    transform.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);

                    if (prev == null)
                    {
                        eventData.pointerDrag.GetComponent<spiceslot>().removes();
                    }
                    else
                    {
                        eventData.pointerDrag.GetComponent<spiceslot>().sets(prev);
                    }

                    //RaiseEvent("removed");
                }
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!fixs)
        {
            // When dragging starts, disable raycasting on this object
            parentAfterDrag = transform.parent;
            transform.SetParent(canvas.transform);
            transform.SetAsLastSibling();
            canvasGroup.blocksRaycasts = false;
            Debug.Log("i am being draged");
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!fixs)
        {
            // Update the position of the object to follow the mouse/finger
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
            //transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!fixs)
        {
            // When dragging ends, enable raycasting on this object
            transform.SetParent(parentAfterDrag);
            canvasGroup.blocksRaycasts = true;

            rectTransform.anchoredPosition = initialPosition;
        }

    }
}
