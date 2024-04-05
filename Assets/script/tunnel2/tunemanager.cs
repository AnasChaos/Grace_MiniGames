using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class tunemanager : MonoBehaviour
{
    public static tunemanager instance;

    public bool started;
    public bool win;
    private float clickStartTime;
    private bool isClicking = false;
    private AudioSource audioSource;
    public float backgroundspeed;
    public Renderer backgroundrendere;
    public GameObject pannel;


    public Rigidbody2D rb;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public Animator animators;
    public float jumppower;


    public float bpm = 120.0f; // Beats per minute
    private float beatInterval; // Time interval between beats
    private float nextBeatTime; // Time when the next beat should occur
    public float margin;

    Soundmanager Sou;
    public int tune;
    int currenttune =3;
    int totaltune;

    //new
    tunnelletters currentnode;
    float buttonHoldDuration;
    public float decayRate = 0.02f;
    public Image myImage;
    int currentround;
    public GameObject[] rounds;
    public Animator ani;

    //event system------------------------------------
    public delegate void MyEventDelegate(string status);

    public event MyEventDelegate MyEvent;

    public void RaiseEvent(string status)
    {

        MyEvent?.Invoke(status);
    }
    //---------------------------





    // Start is called before the first frame update
    void Start()
    {

        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        beatInterval = 60.0f / bpm;
        Sou = Soundmanager.instance;
        audioSource = GetComponent<AudioSource>();


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.R))
        {
            int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneBuildIndex);
        }

        if (started && !win) 
        {
            ticker();
            count();
            soundstops();
        }

    }

    public void soundstops()
    {
        if(isClicking == false && audioSource.volume>0)
        {
            audioSource.volume -= decayRate;
        }
    }

    //check if button was clicked 
    public void OnPointerDown()
    {
        Debug.Log("called");
        clickStartTime = Time.time;
        //audioSource.time = 0.0f;

        if (!started && !win)
        {
            nextBeatTime = Time.time + beatInterval;
            started = true;
            //RaiseEvent("start");
            Debug.Log(nextBeatTime);

        }
        else
        {
            //audioSource.volume = 1;
            //audioSource.Play();
            isClicking = true;
            RaiseEvent("pressed");
            if(currentnode != null)
            {
                if (currentnode.space == 1)
                {
                    Sou.Play("quarter");
                }
                if (currentnode.space == 2)
                {
                    Sou.Play("half");
                }
                if (currentnode.space == 4)
                {
                    Sou.Play("whole");
                }
            }
            else
            {
                RaiseEvent("wrong");
                Sou.Play("wrong");
            }


        }

    }

    //check if button was letten go  
    public void OnPointerUp()
    {
        isClicking = false;
        //audioSource.Stop();

        if (currentnode != null && started)
        {
            //Debug.Log(currentnode.space);
            Debug.Log(buttonHoldDuration);
            if (currentnode.space == 1 )
            {
                Debug.Log(((beatInterval - margin) / 4));
                Debug.Log(((beatInterval + margin) / 4));
                //if (buttonHoldDuration >= ((beatInterval-margin)/4) && buttonHoldDuration <= ((beatInterval + margin)/4))
                //{
                    Debug.Log("correct");
                    //Sou.Play("quarter");
                    //RaiseEvent("pause");
                    return;
                //}
            }
            if (currentnode.space == 2)
            {   
                if (buttonHoldDuration >= ((beatInterval - margin) / 2) && buttonHoldDuration <= ((beatInterval + margin) / 2))
                {
                    Debug.Log("correct");
                    //Sou.Play("half");
                    //RaiseEvent("pause");
                    return;
                }
            }
            if (currentnode.space == 4)
            {
                if (buttonHoldDuration >= (beatInterval - margin) && buttonHoldDuration <= (beatInterval + margin))
                {
                    Debug.Log("correct");
                    //Sou.Play("whole");
                    //RaiseEvent("pause");
                    return;
                }
            }
            if (currentnode.space != buttonHoldDuration)
            {
                Sou.Stopall();
                Debug.Log("wrong");
                Sou.Play("wrong");
                RaiseEvent("wrong");
                return;
            }

        }
    }

    public void count()
    {
        if (isClicking)
        {
            buttonHoldDuration = Time.time - clickStartTime;
            //Debug.Log($"Button held for {buttonHoldDuration:F2} seconds");

        }

    }

    public void selects(tunnelletters TL)
    {
        currentnode = TL;
    }

    public void ticker()
    {


        if (totaltune == 4)
        {
           RaiseEvent("start");
        }
        myImage.color = myImage.color = Color.Lerp(myImage.color, Color.black, Time.deltaTime*2);
        if (Time.time >= nextBeatTime)
        {
            //Debug.Log(nextBeatTime);
            //Debug.Log(tune);
            //Debug.Log(currenttune);
            //Debug.Log(totaltune);

            //spones();
            currenttune += 1;
            totaltune += 1;
            if (currenttune != tune)
            {
                Sou.Play("tock");
                myImage.color = Color.white;
            }
            else
            {
                currenttune = 0;
                Sou.Play("tick");
                myImage.color = Color.white;
            }
            // Print your text or perform any action here
            //Debug.Log("Text printed at " + Time.time);


            // Update the next beat time
            nextBeatTime += beatInterval;
        }
    }


    public void Win()
    {
        //animators.Play("stand");
        win = true;
        pannel.SetActive(true);
    }
    public void next()
    {
        started = false;
        ani.Play("tu" + (1+currentround).ToString());
        //animators.Play("stand");
        if ((currentround+1) == rounds.Length) 
        {
            Win();
            return;
        }
        rounds[currentround].SetActive(false);
        currentround += 1;

        rounds[currentround].SetActive(true);
        totaltune = 0;
        currenttune = 3;
        currentnode = null;
    }
    public void resets()
    {
        started = false;
        totaltune = 0;
        currenttune = 3;
        currentnode = null;
    }


}
