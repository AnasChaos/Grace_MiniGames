using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class points : MonoBehaviour
{

    public int x, y;
    mapmanager manager;
    public string type;
    public bool four;

    public void Initialize(mapmanager game, int tileX, int tileY)
    {
        manager = game;
        x = tileX;
        y = tileY;
     
    }
    void OnMouseDown()
    {
        Debug.Log("clicked");
        manager.clickpoint(x,y);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
