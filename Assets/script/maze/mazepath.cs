using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mazepath : MonoBehaviour
{
    public int x, y;
    mazegrid manager;
    public string type;
    public bool four;
    public bool paths;
    public Sprite wall;
    public Sprite pathimg;


    public void Initialize(mazegrid game, int tileX, int tileY, bool path)
    {
        manager = game;
        x = tileX;
        y = tileY;
        paths = path;
        if (paths)
        {
            transform.GetComponent<SpriteRenderer>().sprite = pathimg;
        }
        else
        {
            transform.GetComponent<SpriteRenderer>().sprite = wall;
        }

    }
    void OnMouseDown()
    {
        if (paths)
        {
            Debug.Log(x);
            Debug.Log(y);
            manager.clickpoint(x, y);
        }
    }


}
