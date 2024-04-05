using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tiles : MonoBehaviour
{

    public int x, y;
    BoardManager manager;
    public string type;
    public bool four;


    public void Initialize(BoardManager game, int tileX, int tileY)
    {
        manager = game;
        x = tileX;
        y = tileY;
    }

    void OnMouseDown()
    {
        manager.Drag(this);
        //print(string.Format("Clicked on tile at ({0}, {1})", x, y));
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0))
        {
            manager.Drop(this);
            //print(string.Format("Mouse up on tile at ({0}, {1})", x, y));
        }
    }

    public void ChangePosition(int X, int Y)
    {
        x = X;
        y = Y;

        name = string.Format("({0}, {1})", x, y);

    }




}
