 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerazoom : MonoBehaviour
{
    public float zoomSpeed = 2.0f;
    public Transform[] fixedPoints;
    public float minzoom;
    public float maxzoom;
    private Camera mainCamera;
    private Vector3 initialCameraPosition;
    public float zoomDuration = 1.0f;
    private float zoomStartTime;
    private bool isZooming = false;
    public int positions;
    private bool Zoomingout = false;
    private bool Zoom = false;

    private void Start()
    {
        mainCamera = Camera.main;
        initialCameraPosition = mainCamera.transform.position;
    }

    private void Update()
    {


        zooming();




    }

    public void controlzoom(int positionss)
    {
        if (Zoom == false)
        {
            Debug.Log("click");
            if (isZooming == false)
            {
                zoomStartTime = Time.time;
                isZooming = true;
                positions = positionss;
            }
            else
            {
                zoomStartTime = Time.time;
                positions = 0;
                isZooming = false;
                Zoomingout = true;

            }
        }

    }


    private void zooming()
    {

        if (isZooming)
        {
            if (positions != 0)
            {
                float progress = (Time.time - zoomStartTime) / zoomDuration;
                float newSize = Mathf.Lerp(maxzoom, minzoom, progress); // Reverse min and max for zooming in.
                newSize = Mathf.Clamp(newSize, minzoom, maxzoom);
                mainCamera.orthographicSize = newSize;

                mainCamera.transform.position = Vector3.Lerp(initialCameraPosition, fixedPoints[positions].position, progress);

                if (progress < 1.0f)
                {
                    Zoom = true;
                }
                else
                {
                    Zoom = false;
                }

                if (progress >= 1.0f)
                {
                    //isZooming = false;
                    initialCameraPosition = mainCamera.transform.position;
                }
            }
            else
            {
                float progress = (Time.time - zoomStartTime) / zoomDuration;
                float newSize = Mathf.Lerp( 7, 5, progress); // Reverse min and max for zooming in.
                newSize = Mathf.Clamp(newSize, 5, 7);
                mainCamera.orthographicSize = newSize;

                mainCamera.transform.position = Vector3.Lerp(initialCameraPosition, fixedPoints[positions].position, progress);

                if (progress < 1.0f)
                {
                    Zoom = true;
                }
                else
                {
                    Zoom = false;
                }



                if (progress >= 1.0f)
                {
                    isZooming = false;
                    initialCameraPosition = mainCamera.transform.position;
                }
            }
        }
   
        else if (Zoomingout == true)
        {
            float progress = (Time.time - zoomStartTime) / zoomDuration;
            float newSize = Mathf.Lerp(minzoom, maxzoom, progress); // Reverse min and max for zooming in.
            newSize = Mathf.Clamp(newSize, minzoom, maxzoom);
            mainCamera.orthographicSize = newSize;

            mainCamera.transform.position = Vector3.Lerp(initialCameraPosition, fixedPoints[positions].position, progress);

            if (progress < 1.0f)
            {
                Zoom = true;
            }
            else
            {
                Zoom = false;
            }



            if (progress >= 1.0f)
            {
                isZooming = false;
                initialCameraPosition = mainCamera.transform.position;
            }
        }


    }




}

