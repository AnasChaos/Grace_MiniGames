using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class bridge4 : MonoBehaviour
{
    private Camera mainCamera;
    public Transform[] fixedPoints;
    public float MinZoom = 1.0f;
    public float MaxZoom = 10.0f;
    public float ZoomSpeed = 2.0f;
    public float ZoomDuration = 1.0f;
    private Vector3 initialCameraPosition;
    private float zoomStartTime;
    private bool isZooming = false;
    public int positions;
    public GameObject panel;
    [SerializeField] private TMP_Text question;
    public Vector3 Offset;
    public Soundmanager Sou;
    bool GAstart;

    // player
    public float speed = 5f;
    public Rigidbody2D rb;
    public Animator player;
    bool playermove;
    Vector2 target;
    public float threshold = 0.1f;  // Adjust this threshold as needed

    //Octive Lists------------------

    private List<Octivebuttons> currentoc;
    private Octivebuttons OCpicked;
    private int current;
    public Animator animator;
    float time;
    public float intervial;

    //new------------------
    int currentkey = -1;
    public Button[] whitekeys;
    public Button[] blackkeys;
    public List<Octivebuttons> oc = new List<Octivebuttons>();
    bool wins;
    int randomindex;
    bool loops;
    int previous;
    public int repeat;
    public int currentrepeat;



    //Octive class------------------
    [System.Serializable]
    public class Octivebuttons
    {
        public int octives;
        public string value;
        public Button bu;
        public int number;
    }


    public void gamestarts()
    {
        GAstart = true;
        time = 4;
    }

    void PlayAnimation()
    {
        // Trigger the animation
        animator.Play("cloudmove");
    }

    IEnumerator win()
    {

        yield return new WaitForSeconds(1.5f);
        wins = true;
        panel.SetActive(true);
    }

    public void moveplayer()
    {
        if (playermove)
        {

            rb.position = Vector2.MoveTowards(rb.position, new Vector2(target.x, rb.position.y), speed * Time.deltaTime);

            // Check if the player has reached the target point within a threshold
            if (Vector2.Distance(rb.position, new Vector2(target.x, rb.position.y)) < threshold)
            {
                //player.SetBool("run", false);
                playermove = false;
                // You can add additional logic here when the player reaches the target
                Debug.Log("Player reached the target");


            }
        }
    }







    /// <summary>
    /// picks random number
    /// </summary>
    /// <returns></returns>
    public string RandompicksOC()
    {
        Debug.Log(currentoc.Count);

        if (currentoc.Count == 3)
        {
            randomindex = Random.Range(1, 3);
            if (previous >= 2)
            {
                randomindex = 1;
            }

            OCpicked = currentoc[randomindex + previous - 1];
            previous = randomindex;
            return OCpicked.value;

        }
        else if (currentoc.Count == 12)
        {
            do
            {
                loops = false;
                randomindex = Random.Range(1, (((currentoc.Count) / 2)) + 1);//(1,6)range generage from 1 to 6 
                if (OCpicked != null)
                {
                    if (randomindex == previous)
                    {
                        loops = true;
                    }
                }
            }
            while (loops);
            Debug.Log(randomindex);
            OCpicked = currentoc[randomindex + previous - 1];
            previous = randomindex;
            return OCpicked.value;


        }
        else if (currentoc.Count == 1)
        {
            randomindex = 1;
            OCpicked = currentoc[randomindex + previous - 1];
            previous = randomindex;
            return OCpicked.value;

        }

        return OCpicked.value;

    }


    /// <summary>
    /// update current octive list
    /// </summary>

    IEnumerator NextOC()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < oc.Count; i++)
        {
            oc[i].bu.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        currentoc = null;
        currentoc = new List<Octivebuttons>();
        foreach (Octivebuttons octaveButton in oc)
        {
            if (octaveButton.octives == current)
            {
                currentoc.Add(octaveButton);
            }
        }

        question.text = RandompicksOC();
        yield return new WaitForSeconds(1f);
        //controlzoom(current);

    }

    /// <summary>
    /// checks the answers
    /// </summary>
    /// <param name="key"></param>

    public void CheckOC(string key, int but)
    {
        Debug.Log(key);
        Sou.Play(key);


        if (OCpicked.value == key)
        {
            playermove = true;
            //player.SetBool("run", true);
            target = oc[but].bu.transform.position;
            time = 4;
            oc[but].bu.GetComponent<Image>().color = new Color32(0, 255, 0, 255);
            if (current < 8)
            {
                currentrepeat += 1;
                if (currentrepeat > repeat)
                {
                    currentrepeat = 0;
                    previous = 0;
                    current += 1;
                }
            }
            else
            {
                StartCoroutine(win());
                return;
            }
            StartCoroutine(NextOC());




        }
        else
        {
            oc[but].bu.GetComponent<Image>().color = new Color32(255, 0, 0, 200);
        }

    }


    /// <summary>
    /// used to setup the scean
    /// </summary>
    public void setups()
    {

        currentoc = new List<Octivebuttons>();

        foreach (Octivebuttons octaveButton in oc)
        {
            if (octaveButton.octives == current)
            {
                currentoc.Add(octaveButton);
            }
        }

        for (int i = 0; i < oc.Count; i++)
        {
            int buttonIndex = i;
            string optionText = oc[i].value;
            oc[i].bu.onClick.RemoveAllListeners(); // Remove previous listeners to avoid duplicates.
            oc[i].bu.onClick.AddListener(() => CheckOC(optionText, buttonIndex)); // Use the local variable.

        }


    }


    // Start is called before the first frame update
    void Start()
    {
        InitializePianoKeys();
        Sou = Soundmanager.instance;
        setups();


        mainCamera = Camera.main;
        initialCameraPosition = mainCamera.transform.position;

        question.text = RandompicksOC();


    }



    // Update is called once per frame
    void Update()
    {
        if (Sou == null)
        {
            Sou = Soundmanager.instance;
        }

        if (GAstart && !wins)
        {
            moving();
            moveplayer();

            time += Time.deltaTime;
            if (time > intervial)
            {
                time = 0;
                PlayAnimation();
            }
        }





    }

    public void controlzoom(int positionss)
    {
        if (!isZooming)
        {
            zoomStartTime = Time.time;
            isZooming = true;
            positions = positionss;
        }
    }

    private void moving()
    {
        Vector3 targetPosition = fixedPoints[positions].position + Offset;
        if (positions == 0)
        {
            float progress = (Time.time - zoomStartTime) / ZoomDuration;
            float newSize = Mathf.Lerp(MaxZoom, MinZoom, progress);
            newSize = Mathf.Clamp(newSize, MinZoom, MaxZoom);
            mainCamera.orthographicSize = newSize;

            mainCamera.transform.position = Vector3.Lerp(initialCameraPosition, targetPosition, progress);

            if (progress < 1.0f)
            {
                isZooming = true;
            }
            else
            {
                isZooming = false;
            }

            if (progress >= 1.0f)
            {
                initialCameraPosition = mainCamera.transform.position;
            }
        }
        else
        {
            float progress = (Time.time - zoomStartTime) / ZoomDuration;
            float newSize = Mathf.Lerp(MaxZoom, MinZoom, progress);
            newSize = Mathf.Clamp(newSize, MinZoom, MaxZoom);
            //mainCamera.orthographicSize = newSize;

            mainCamera.transform.position = Vector3.Lerp(initialCameraPosition, targetPosition, progress);

            if (progress < 1.0f)
            {
                isZooming = true;
            }
            else
            {
                isZooming = false;
            }

            if (progress >= 1.0f)
            {
                initialCameraPosition = mainCamera.transform.position;
            }
        }





    }



    void InitializePianoKeys()
    {
        bool loop = true;
        string[] noteNames = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
        int nu = 0;
        int wh = 0;
        int bl = 0;
        // Create 88 piano keys
        for (int octave = 0; octave < 9; octave++) // Assuming a standard piano with 7 octaves
        {
            foreach (string note in noteNames)
            {
                if (octave == 0 && !(note == "A" || note == "A#" || note == "B"))
                {
                    continue; // Skip notes other than A, A#, and B for octave 0
                }
                if (octave == 8 && !(note == "C"))
                {
                    continue; // Skip notes other than A, A#, and B for octave 0
                }
                if (loop)
                {
                    nu += 1;
                    string fullNoteName = note + octave.ToString();
                    bool hasSharp = fullNoteName.Contains("#");
                    //Debug.Log(fullNoteName);

                    if (!hasSharp)
                    {
                        Octivebuttons button1 = new Octivebuttons();
                        button1.octives = octave;
                        button1.value = fullNoteName;
                        button1.bu = whitekeys[wh];
                        button1.number = nu;
                        oc.Add(button1);
                        wh += 1;
                    }
                    else
                    {
                        Octivebuttons button1 = new Octivebuttons();
                        button1.octives = octave;
                        button1.value = fullNoteName;
                        button1.bu = blackkeys[bl];
                        button1.number = nu;
                        oc.Add(button1);
                        bl += 1;
                    }
                }
            }

            // Reset the loop variable for the next octave
            loop = true;
        }
        // You now have a list of piano keys in the pianoKeys variable
    }
}
