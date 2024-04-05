using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class scaleslot : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public notesscale scale;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    [SerializeField] private Canvas canvas;
    private Vector2 initialPosition;
    public RectTransform box;
    Transform parentAfterDrag;
    scalesoltmanager mm;
    public bool solved;

    private void Start()
    {

        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GameObject.Find("Canvas1").GetComponent<Canvas>();
        initialPosition = rectTransform.anchoredPosition;

        parentAfterDrag = transform.parent;
        mm = parentAfterDrag.GetComponent<scalesoltmanager>();

        if (scale != null)
        {
            transform.GetComponent<Image>().sprite = scale.sprite;
        }
    }

    public void de()
    {
        if (!solved)
        {
            mm.delet(scale);
            Destroy(gameObject);
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
