using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class chestmanager : MonoBehaviour
{
    public Animator num1;
    public Animator num2;
    public Animator num3;
    public Animator num4;
    public Animator chestani;
    public int numer1;
    public int numer2;
    public int numer3;
    public int numer4;
    [SerializeField] private TMP_Text nu1;
    [SerializeField] private TMP_Text nu2;
    [SerializeField] private TMP_Text nu3;
    [SerializeField] private TMP_Text nu4;
    public GameObject panel;

    //camrera zoom---------------------
    public Camera mainCamera;
    public Transform[] fixedPoints;
    public float MinZoom = 1.0f;
    public float MaxZoom = 10.0f;
    public float ZoomSpeed = 2.0f;
    public float ZoomDuration = 1.0f;
    private Vector3 initialCameraPosition;
    private float zoomStartTime;
    private bool isZooming = false;
    public int positions;
    public int current;
    public Vector3 Offset;
    public GameObject panelclick;


    void Update()
    {
        moving();
    }





    public void changenumber1(int number)
    {

            if (number == -1)
            {
                num1.Play("down");
            }
            else
            {
                num1.Play("up");
            }
            numer1 = numer1 + number;
            if(numer1 == 10)
            {
            numer1 = 0;
            }
            else if (numer1 == -1)
            {
            numer1 = 9;
            }
            StartCoroutine(ChangeTextWithDelay(0.2f,1));

    }
    public void changenumber2(int number)
    {

            if (number == -1)
            {
                num2.Play("down");
            }
            else
            {
                num2.Play("up");
            }
            numer2 = numer2 + number;
            if (numer2 == 10)
            {
                numer2 = 0;
            }
            else if (numer2 == -1)
            {
                numer2 = 9;
            }
        StartCoroutine(ChangeTextWithDelay(0.2f, 2));

    }
    public void changenumber3(int number)
    {

            if (number == -1)
            {
                num3.Play("down");
            }
            else
            {
                num3.Play("up");
            }
            numer3 = numer3 + number;
            if (numer3 == 10)
            {
                numer3 = 0;
            }
            else if (numer3 == -1)
            {
                numer3 = 9;
            }
            StartCoroutine(ChangeTextWithDelay(0.2f, 3));

    }
    public void changenumber4(int number)
    {

            if (number == -1)
            {
                num4.Play("down");
            }
            else
            {
                num4.Play("up");
            }
            numer4 = numer4 + number;
        if (numer4 == 10)
        {
            numer4 = 0;
        }
        else if (numer4 == -1)
        {
            numer4 = 9;
        }
        StartCoroutine(ChangeTextWithDelay(0.2f, 4));
        
    }

    private IEnumerator ChangeTextWithDelay(float delay,int change)
    {
        yield return new WaitForSeconds(delay);
        if(change == 1)
        {
            nu1.text = numer1.ToString();
        }
        else if (change == 2)
        {
            nu2.text = numer2.ToString();
        }
        else if (change == 3)
        {
            nu3.text = numer3.ToString();
        }
        else if (change == 4)
        {
            nu4.text = numer4.ToString();
        }



    }

    public void hh()
    {
        if (numer1 == 2 && numer2 == 1 && numer3 == 4 && numer4 == 2)
        {
            panel.SetActive(true);
            controlzoom(0);
            Invoke("opens", 1);

        }
    }
    public void opens()
    {
        chestani.Play("open");
    }


    public void controlzoom(int positionss)
    {
        Debug.Log(positionss);
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
        if (positions == 1)
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
                panelclick.SetActive(false);
                initialCameraPosition = mainCamera.transform.position;
            }
        }
        else if(positions == 0)
        {

            if (mainCamera.orthographicSize != MaxZoom)
            {
                float progress = (Time.time - zoomStartTime) / ZoomDuration;
                float newSize = Mathf.Lerp(MinZoom, MaxZoom, progress);
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

        }

    }


    
}
