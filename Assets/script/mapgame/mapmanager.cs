using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class mapmanager : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    public static mapmanager instance;
    public bool started;
    public bool win;
    public int turns;
    public int obstical;
    int currentturns;
    public int xSize, ySize;
    private points[,] points;
    public List<points> currentpoins = new List<points>();
    public List<points> obstacles = new List<points>();
    public float candySize = 1.0f;
    float startX;
    float startY;
    public Vector2 startingpoints;
    public Vector2 winningpoint;
    Vector2 offset;
    public float offestme;
    public GameObject candyPrefab;
    Soundmanager Sou;
    public GameObject ship;
    public GameObject[] storms;
    [SerializeField] private TMP_Text turnstext;

    //zooom
    public Camera mainCamera;
    public float MinZoom = 1.0f;
    public float MaxZoom = 10.0f;
    public float ZoomSpeed = 2.0f;
    public float ZoomDuration = 1.0f;
    private float zoomStartTime;
    private bool isZooming = false;
    //-------------------------------



    // Start is called before the first frame update
    void Start()
    {
        Sou = Soundmanager.instance;
        startX = transform.position.x;
        startY = transform.position.y;
        instance = GetComponent<mapmanager>();
        offset = candyPrefab.GetComponent<SpriteRenderer>().bounds.size;
        CreateBoard(offestme, offestme);

    }
    public void starteds()
    {
        started = true;
        startingpoint();
        zoomStartTime = Time.time;
        isZooming = true;
        spones();
        turnstext.text = ("Turns Left " + (turns - currentturns));
    }



    private void CreateBoard(float xOffset, float yOffset)
    {
        points = new points[xSize, ySize];
        for (int x = 0; x < xSize; x++)
        {

            for (int y = 0; y < ySize; y++)
            {

                GameObject newTile = Instantiate(candyPrefab, new Vector3(startX + (xOffset * x), startY + (yOffset * y), 0), Quaternion.identity);
                points point = newTile.GetComponent<points>();
                point.Initialize(this, x, y);
                points[x, y] = point;
                newTile.transform.parent = transform;
                newTile.SetActive(false);

            }
        }
    }

    public void startingpoint()
    {

        int x = (int)startingpoints.x;
        int y = (int)startingpoints.y;

        points[x, y].gameObject.SetActive(true);
        points[x - 1, y].gameObject.SetActive(true);
        points[x + 1, y].gameObject.SetActive(true);
        points[x, y + 1].gameObject.SetActive(true);
        points[x, y - 1].gameObject.SetActive(true);

        points[x+1, y - 1].gameObject.SetActive(true);
        points[x + 1, y + 1].gameObject.SetActive(true);
        points[x-1, y - 1].gameObject.SetActive(true);
        points[x - 1, y + 1].gameObject.SetActive(true);

    }

    public void spones()
    {
        int rands;
        int randsx;
        int randsy;
        bool looping = true;
        GameObject clouds;
        int x = (int)startingpoints.x;
        int y = (int)startingpoints.y;

        ship = Instantiate(ship, points[x, y].transform.position, Quaternion.identity);
        ship.transform.parent = transform;

        for (int e = 0; e < obstical; e++)
        {
            do
            {
                looping = false;
                randsx = Random.Range(0, xSize);
                randsy = Random.Range(0, ySize);
                rands = Random.Range(0, storms.Length);
                if (randsx == startingpoints.x && randsy == startingpoints.y)
                {
                    looping = true;
                }
            } while (looping);
            clouds = Instantiate(storms[rands], points[randsx, randsy].transform.position, Quaternion.identity);
            clouds.transform.parent = transform;
            obstacles.Add(points[randsx, randsy]);
        }

    }


    public void clickpoint(int x, int y)
    {
        Debug.Log(x);
        Debug.Log(y);
        if (currentturns < turns)
        {
            currentturns += 1;
            turnstext.text = ("Turns Left " + (turns - currentturns));
            if ((x - 1) >= 0)
            {
                points[x - 1, y].gameObject.SetActive(true);
            }
            if ((y - 1) >= 0)
            {
                points[x, y - 1].gameObject.SetActive(true);
            }
            if ((x + 2) <= xSize)
            {
                points[x + 1, y].gameObject.SetActive(true);
            }
            if ((y + 2)<= ySize)
            {
                points[x, y + 1].gameObject.SetActive(true);
            }

            points[x, y].transform.GetComponent<SpriteRenderer>().color = new Color32(0, 255, 0, 255);
        }
        currentpoins.Add(points[x, y]);
    }

    public void checks()
    {
        StartCoroutine(cheacking());
    }

    IEnumerator cheacking()
    {

        for (int y = 0; y < currentpoins.Count; y++)
        {
            GameObject currentPoint = currentpoins[y].gameObject;
            LeanTween.move(ship, currentPoint.transform.position, 1); // Each move has a delay based on its index
            yield return new WaitForSeconds(1f);
            for (int e = 0; e < obstacles.Count; e++)
            {
                if(obstacles[e] == currentpoins[y])
                {
                    resets();
                    yield break;
                }
            }
        }
        if (currentpoins[currentpoins.Count - 1] == points[(int)winningpoint.x, (int)winningpoint.y])
        {
            wins();
        }
        else
        {
            resets();
            yield break;
        }
    }

    public void resets()
    {
        ship.transform.position = points[(int)startingpoints.x, (int)startingpoints.y].transform.position;
        currentturns = 0;
        turnstext.text = ("Turns Left " + (turns - currentturns));

        for (int y = 0; y < currentpoins.Count; y++)
        {
            currentpoins[y].transform.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        }
        currentpoins = null;
        currentpoins = new List<points>();
        for (int x = 0; x < xSize; x++)
        {

            for (int y = 0; y < ySize; y++)
            {
                points[x, y].gameObject.SetActive(false);

            }
        }
        startingpoint();


    }

    public void wins()
    {
        win = true;
        panel.SetActive(true);
    }


    public void moves() 
    {
        if (isZooming)
        {
            float progress = (Time.time - zoomStartTime) / ZoomDuration;
            float newSize = Mathf.Lerp(MaxZoom, MinZoom, progress);
            newSize = Mathf.Clamp(newSize, MinZoom, MaxZoom);
            mainCamera.orthographicSize = newSize;
            if (progress > 1.0f)
            {
                isZooming = false;
            }
        }
    }




    // Update is called once per frame
    void Update()
    {
        moves();
    }
}
