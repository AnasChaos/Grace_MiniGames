using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lissner : MonoBehaviour
{
    public tunnelletters tunnelletters;
    public float moveSpeed = 1.0f;     // Adjust this to control the movement speed
    public float destroyPositionX = -20.0f; // Adjust this to control the position at which the object is destroyed
    public float limitx = 1.0f;
    public float BPMS;
    public bool started = false;
    public bool pause = true;
    public bool pressed = false;
    public tunemanager TM;
    public GameObject startlocation;


    // Start is called before the first frame update
    void Start()
    {

        moveSpeed = BPMS / 60;
        //TM = tunemanager.instance;
        TM.MyEvent += lish;
    }

    public void lish(string start)
    {
        if (start == "pause")
        {
            pause = true;
        }
        if (start == "start")
        {
            started = true;
            pause = true;
        }
        if (start == "wrong")
        {
            this.transform.position = startlocation.transform.position;
            pause = false;
            pressed = false;
            TM.resets();
        }
        if (start == "pressed")
        {
            pressed = true;
        }
    }

    public void Resets()
    {
        this.transform.position = startlocation.transform.position;
        pause = false;
        pressed = false;
        TM.resets();
    }

    // Update is called once per frame
    void Update()
    {
        if (started && pause)
        {

            //Debug.Log(transform.position.x);
            if (transform.position.x <= limitx)
            {
                //Debug.Log("run");
                // Move the object to the left.
                Vector3 newPosition = transform.position;
                newPosition.x += moveSpeed * Time.deltaTime;
                transform.position = newPosition;
                //
                // Check if the object's X position is beyond the destroy position.
                if (transform.position.x >= destroyPositionX)
                {

                    started = false;
                    // Destroy the object when it goes off-screen to the left.
                    //Destroy(gameObject);
                }
            }
        }
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "keys")
        {
            tunnelletters = collision.gameObject.GetComponent<tunnelslotnodes>().tunnelletters;
            //tunnelmanager.instance.Win();
            //Debug.Log("hit");
            TM.selects(tunnelletters);
            //if(pressed)
            //pause = false;
        }
        if (collision.gameObject.tag == "Finish")
        {
            TM.Win();
            pause = false;
        }
        if (collision.gameObject.tag == "next")
        {
            //TM.Win();
            pause = false;
            TM.next();
            //self.SetActive(false);
            //next.SetActive(true);
        }


    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "keys")
        {
            if (tunnelletters.nodes == "space")
            {
                if (pressed)
                {
                    pause = false;
                    Resets();
                }
               

            }
            else
            {
                if (!pressed)
                {
                    pause = false;
                    Resets();
                }
                else
                {
                    pressed = false;

                }
            }

        }


    }


}
