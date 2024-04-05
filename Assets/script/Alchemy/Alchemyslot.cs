using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Alchemyslot : MonoBehaviour, IDropHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Beaker bb = new Beaker();
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    [SerializeField] private Canvas canvas;
    private Vector2 initialPosition;
    Transform parentAfterDrag;
    public Alchemymanager mm;
    public SpriteRenderer water;
    public GameObject point;
    public Animator ban;
    Beaker olds = new Beaker();
    Beaker olds2 = new Beaker();
    public Canvas ca;
    public Canvas ca2;
    public LineRenderer line;
    public GameObject st;

    public IEnumerator changes(GameObject pp, Beaker old, Beaker ce)
    {
        LeanTween.move(this.gameObject, pp.transform.position, 0.5f);
        yield return new WaitForSeconds(0.5f);
        float t1 = ((float)old.bottel / (float)old.max);
        float t2 = ((float)ce.bottel / (float)ce.max);


        ban.Play("transfer");

        line.SetPosition(0, st.transform.position);
        line.SetPosition(1, st.transform.position - Vector3.up * 3.45f);
        line.startColor = new Color(255,255,255,255);
        line.endColor = new Color(255, 255, 255, 255);
        line.enabled = true;




        // Use LeanTween.value to interpolate a value over time
        float from = Mathf.Lerp(-1.2f, 1.2f, t1);
        float to = Mathf.Lerp(-1.2f, 1.2f, t2);

        Debug.Log(from);
        Debug.Log(to);
        Debug.Log(t2);
        Debug.Log((ce.bottel));
        Debug.Log((ce.max));
        // Use LeanTween.value to interpolate a value over time
        LeanTween.value(gameObject, from, to, 1.5f).setOnUpdate((float value) =>
        {
            // Convert the interpolated value to the desired range
            float convertedValue = Mathf.Lerp(-1.2f, 1.2f, value);
            water.material.SetFloat("_fill", value);
        });



        //LeanTween.scaleY(water, 0.5f, 2);
        yield return new WaitForSeconds(1.7f);

        line.enabled = false;
        rectTransform.anchoredPosition = initialPosition;

    }

    public IEnumerator changevolume(Beaker old, Beaker ce)
    {

        yield return new WaitForSeconds(0.2f);
        float t1 = ((float)old.bottel / (float)old.max);
        float t2 = ((float)ce.bottel / (float)ce.max);
        float from = Mathf.Lerp(-1.2f, 1.2f, t1);
        float to = Mathf.Lerp(-1.2f, 1.2f, t2);

        //Debug.Log(from);
        //Debug.Log(to);

        // Use LeanTween.value to interpolate a value over time
        LeanTween.value(gameObject, from, to, 1.5f).setOnUpdate((float value) =>
        {
            // Convert the interpolated value to the desired range
            float convertedValue = Mathf.Lerp(-1.2f, 1.2f, value);

            // Update a property based on the converted value
            water.material.SetFloat("_fill", value);
        });
    }

    // Start is called before the first frame update
    private void Start()
    {

        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        initialPosition = rectTransform.anchoredPosition;

        parentAfterDrag = transform.parent;

    }


    public void OnDrop(PointerEventData eventData)
    {
        Beaker dropbreker;
        GameObject droppedObject = eventData.pointerDrag;
        if (eventData.pointerDrag != null)
        {
            Alchemyslot gemHolder = droppedObject.GetComponent<Alchemyslot>();
            olds.bottel = bb.bottel;
            olds.max = bb.max;

            if (gemHolder != null)
            {
                dropbreker = eventData.pointerDrag.GetComponent<Alchemyslot>().bb;
                //StartCoroutine(gemHolder.changes(point));
                mm.transfer(dropbreker, bb);
                StartCoroutine(changevolume(olds, bb));
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
        //ca.sortingOrder = 4;
        water.sortingOrder = 5;
        ca2.sortingOrder = 6;
        //transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // When dragging ends, enable raycasting on this object
        transform.SetParent(parentAfterDrag);
        canvasGroup.blocksRaycasts = true;

        rectTransform.anchoredPosition = initialPosition;
        water.sortingOrder = 2;
        //ca.sortingOrder = 1;
        water.sortingOrder = 5;
        ca2.sortingOrder = 3;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {

        }

    }
}
