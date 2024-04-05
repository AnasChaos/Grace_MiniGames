using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tunnelslot : MonoBehaviour
{
    public tunnelletters tunnelletters;
    public float moveSpeed = 1.0f;     // Adjust this to control the movement speed
    public float destroyPositionX = -20.0f; // Adjust this to control the position at which the object is destroyed
    public float limitx = 1.0f;
    public float BPMS;
    public bool started;
    // Start is called before the first frame update
    void Start()
    {
        transform.gameObject.GetComponent<SpriteRenderer>().sprite = tunnelletters.sprite;
        moveSpeed = BPMS / 60;
        if (tunnelmanager.instance.started == true)
        {
            started = true;
        }
        tunnelmanager.instance.MyEvent += lishen;
    }
    public void lishen(bool start)
    {
        started = start;
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            //Debug.Log(transform.position.x);
            if (transform.position.x >= limitx)
            {
                // Move the object to the left.
                Vector3 newPosition = transform.position;
                newPosition.x -= moveSpeed * Time.deltaTime;
                transform.position = newPosition;
                //
                // Check if the object's X position is beyond the destroy position.
                if (transform.position.x <= destroyPositionX)
                {
                    // Destroy the object when it goes off-screen to the left.
                    Destroy(gameObject);
                }
            }
        }
    }
}







