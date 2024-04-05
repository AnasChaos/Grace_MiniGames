using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class tunnelmanager : MonoBehaviour 
{

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
    Soundmanager Sou;
    public int tune;
    int currenttune;

    // sponers
    public GameObject nodesponner;
    public tunnelletters[] nodes;
    public GameObject nodesprefeb;
    public GameObject trigger;

    public GameObject groundprefeb;
    public Transform startingpoint;
    Vector3 lastpoint;
    int currentnode;
    public int nodevariance;
    public int variance;



    public static tunnelmanager instance;



    public delegate void MyEventDelegate(bool status);

    public event MyEventDelegate MyEvent;

    public void RaiseEvent(bool status)
    {

        MyEvent?.Invoke(status);
    }





    private void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        lastpoint = startingpoint.position;
        beatInterval = 60.0f / bpm;
        Sou = Soundmanager.instance;
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component


    }
    void FixedUpdate()
    {




        if (currentnode <= nodes.Length)
        {
            spones();
        }

        if (started && !win) 
        {
            ticker();

            playercontroller();
            //background movement ---------------------------------------------------------------------
            backgroundrendere.material.mainTextureOffset += new Vector2(backgroundspeed * Time.deltaTime, 0f);
            //========================================================================================
        }
        if (win)
        {
            animators.SetBool("jump", false);
            rb.velocity = new Vector2(1, -1) * 10;
            GameObject ground = Instantiate(groundprefeb, lastpoint , Quaternion.identity);
            lastpoint = ground.transform.Find("endposition").position;
            Instantiate(groundprefeb, lastpoint, Quaternion.identity);
        }
    }



    public void ticker()
    {
        // Check if it's time for the next beat
        if (Time.time >= nextBeatTime)
        {
            //spones();
            currenttune += 1;
            if (currenttune != tune)
            {
                //Sou.Play("tick");
            }
            else
            {
                currenttune = 0;
                //Sou.Play("tock");
            }
            // Print your text or perform any action here
            Debug.Log("Text printed at " + Time.time);


            // Update the next beat time
            nextBeatTime += beatInterval;
        }
    }

    public void spones()
    {
        if (currentnode < nodes.Length) {
            GameObject nodepre = Instantiate(nodesprefeb, lastpoint + new Vector3(0, nodesponner.transform.position.y+1.57f, 0), Quaternion.identity);
            nodepre.transform.GetComponent<tunnelslot>().tunnelletters = nodes[currentnode];
            nodepre.transform.SetParent(nodesponner.transform, true);
            GameObject ground = Instantiate(groundprefeb, lastpoint+new Vector3(nodes[currentnode].space* variance, 0,0), Quaternion.identity);
            lastpoint = ground.transform.Find("endposition").position;
            currentnode += 1;
        }
        else if(currentnode == nodes.Length)
        {
            GameObject tri = Instantiate(trigger, lastpoint + new Vector3(-5, 0, 0), trigger.transform.rotation);
            GameObject ground = Instantiate(groundprefeb, lastpoint , Quaternion.identity);
            lastpoint = ground.transform.Find("endposition").position;
            Debug.Log("trigger sponed");
            currentnode += 1;
        }
    }

    //player controller---------------------------
    public void playercontroller()
    {
        //player jumping ----------------------------------------------------------------------------
        if (isClicking && isGroundeds())
        {
            animators.SetBool("jump", true);
            rb.velocity = new Vector2(rb.velocity.x, jumppower);

        }
        if (!isClicking && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (!isClicking && isGroundeds())
        {
            //Debug.Log("grounded");
            animators.SetBool("jump", false);
        }
        if (rb.position.y <= -10f)
        {
            int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneBuildIndex);
        }
        

    }



    //check if button was clicked 
    public void OnPointerDown()
    {
        Debug.Log("called");
        clickStartTime = Time.time;
        audioSource.time = 0.0f;
        audioSource.Play();
        if (!started) 
        {
            started = true;
            RaiseEvent(started);
            nextBeatTime = Time.deltaTime+beatInterval;
            animators.Play("run 0");
        }
        else
        {
            isClicking = true;
        }

    }

    //check if button was letten go  
    public void OnPointerUp()
    {
        isClicking = false;
        audioSource.Stop();
    }


    // used to check if player is no ground 
    bool isGroundeds()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

    public void Win()
    {
        //animators.Play("stand");
        win = true;
        pannel.SetActive(true);
        RaiseEvent(false);
    }




}
