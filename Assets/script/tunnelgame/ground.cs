using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ground : MonoBehaviour
{
    public float moveSpeed = 1.0f;     // Adjust this to control the movement speed
    public float decelerationFactor = 1.0f;
    public float destroyPositionX = -20.0f; // Adjust this to control the position at which the object is destroyed
    public float limitx = 1.0f;
    public bool started;
    bool end;
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = moveSpeed / 60;
        StartCoroutine(LateStart(0.1f));
    }
    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (tunnelmanager.instance.started == true)
        {
            started = true;
        }
        tunnelmanager.instance.MyEvent += lishen;
    }

    public void lishen(bool start)
    {
        if(started && !start)
        {
            end = true;
        }
        started = start;
    }

    // Update is called once per frame

    void LateUpdate()
    {
        //Debug.Log(transform.position.x);
        if (started)
        {
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
        if (!started && end)
        {

            if (moveSpeed > 0)
            {
                // Calculate the new speed with a deceleration factor
                moveSpeed -= decelerationFactor * Time.deltaTime;
                Vector3 newPosition = transform.position;
                newPosition.x -= moveSpeed * Time.deltaTime;
                transform.position = newPosition;
            }
            
        }
    }
}
