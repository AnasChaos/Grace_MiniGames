using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BoardManager : MonoBehaviour
{
    public static BoardManager instance;
    //pices that will be used for game 
    public GameObject[] candyPrefabs;    
    //tile gameobject
    public GameObject tile;     
    //size of grid
    public int xSize, ySize;
    //offset
    public float candySize = 1.0f;
    //grid
    private tiles[,] tiles;
    bool fast = true;
    [SerializeField] private TMP_Text result;
    int score;
    bool loop = true;

    float startX;
    float startY;
    Vector2 offset;
    GameObject candyPrefab;
    public GameObject panel;
    bool canmove = true;
    Soundmanager Sou;
    public GameObject chelfprefeb;
    int currentclef;

    //start-----------------------------------
    void Start()
    {
        Sou = Soundmanager.instance;
        //Find the starting positions for the board generation.
        startX = transform.position.x;
        startY = transform.position.y;
        //create a instange--------------------------
        instance = GetComponent<BoardManager>();     
        //calculating offset of tile
        offset = candyPrefabs[0].GetComponent<SpriteRenderer>().bounds.size;
        //creating board-------------------------
        CreateBoard(offset.x, offset.y);     
    }

    //creating a grid sponing randome tiles-------------------- 
    private void CreateBoard(float xOffset, float yOffset)
    {


        //create tile grid---------------------------------------
        tiles = new tiles[xSize, ySize];

        List<string> previousRowTileTypes = new List<string>();
        string previousBelow = "";
        string newTileType= "ss";
        for (int x = 0; x < xSize; x++)
        {

            for (int y = 0; y < ySize; y++)
            {
                //spone random notes---------------------------------------------
                do{ 

                    candyPrefab = candyPrefabs[Random.Range(0, candyPrefabs.Length)];
                    newTileType = candyPrefab.name;
                    if(previousBelow != newTileType)
                    {
                        if (x == 0 || previousRowTileTypes[y] != newTileType)
                        {

                            //Debug.Log(x == 0 || previousRowTileTypes[y] != newTileType);
                            if (newTileType == "clef")
                            {
                                if (currentclef != 2)
                                {
                                    currentclef += 1;
                                    loop = false;
                                }
                            }
                            else
                            {
                                loop = false;
                            }

                        }
                    }


                } while (loop);
                //---------------------------------------------------------------------------
                loop = true;
                //Debug.Log(previousBelow == newTileType && (x == 0 || previousRowTileTypes[y] == newTileType));

                if(x == 0) 
                {
                    previousRowTileTypes.Add(newTileType);
                }
                else
                {
                    //Debug.Log((newTileType, previousRowTileTypes[y]));
                    previousRowTileTypes[y] = newTileType;
                }

                previousBelow = newTileType;

                GameObject newTile = Instantiate(candyPrefab, new Vector3(startX + (xOffset * x), startY + (yOffset * y), 0), Quaternion.identity);
                tiles candy = newTile.GetComponent<tiles>();
                candy.Initialize(this, x, y);
                tiles[x, y] = candy;
                newTile.transform.parent = transform;

            }
        }
    }

    int dragX = -1;
    int dragY = -1;

    public void Drag(tiles tile)
    {

        dragX = tile.x;
        dragY = tile.y;
        //Debug.Log(dragX);
        //Debug.Log(dragY);
    }
    public void Drop(tiles tile)
    {
        //Debug.Log(tile.x);
        //Debug.Log(tile.y);
        if (!canmove)
        {
            return;
        }
        if (dragX == -1 || dragY == -1)
            return;

        List<tiles> adjacentTiles1 = FindAdjacentTiles(dragX, dragY);

        foreach (tiles tt in adjacentTiles1)
        {
            if (tt == tile)
            {
                SwapTiles(dragX, dragY, tile.x, tile.y);
            }
        }

        dragX = -1;
        dragY = -1;
    }


    //moves tiles and also checks for metchs-------------
    void SwapTiles(int x1, int y1, int x2, int y2)
    {
        canmove = false;
        fast = false;
        if (x1 == x2 && y1 == y2)
            return;


        MoveTile(x1, y1, x2, y2);
        List<tiles> TilesToCheck = CheckHorizontalMatches();
        TilesToCheck.AddRange(CheckVerticalMatches());
        if (TilesToCheck.Count > 0)
        {
            StartCoroutine(Check());
        }
        else
        {
            StartCoroutine(MoveTileWithDelay(x2, y2, x1, y1, 0.5f));

        }
    }
    IEnumerator MoveTileWithDelay(int x1, int y1, int x2, int y2, float delay)
    {
        yield return new WaitForSeconds(delay);

        MoveTile(x2, y2, x1, y1);
        canmove = true;
    }



    //checks for matchs and destroys gems 
    IEnumerator Check()
    {
        List<tiles> TilesToDestroy = CheckHorizontalMatches();
        TilesToDestroy.AddRange(CheckVerticalMatches());
        TilesToDestroy = TilesToDestroy.Distinct().ToList();
        List<tiles> combos = new List<tiles>();

        yield return new WaitForSeconds(0.5f);
        

        //combo check-------------------------------------
        foreach (tiles de in TilesToDestroy)
        {
            List<tiles> adjacentTiles1 = FindAdjacentTiles(de.x, de.y);

            foreach (tiles tt in adjacentTiles1)
            {
                if (tt.type == "clef")
                {
                    currentclef -= 1;
                    for (int x = 0; x < xSize; x++)
                    {
                        Debug.Log(x.ToString()+ tt.y.ToString());
                        if (tiles[x, tt.y] != null)
                        {
                            combos.Add(tiles[x, tt.y]);
                        }
                    }
                }
            }
        }
        Debug.Log("loop ended");

        TilesToDestroy.AddRange(combos);
        TilesToDestroy = TilesToDestroy.Distinct().ToList();

        bool sw = TilesToDestroy.Count == 0;

        foreach (tiles t in TilesToDestroy)
        {
            score += 50;
            LeanTween.scale(t.gameObject, new Vector3(0.5F, 0.5F, 0.5F), 0.2F).setOnComplete(() => Destroy(t.gameObject) );

            if(tiles[t.x, t.y].type == "whole")
            {
                Sou.Play("whole");
            }
            else if (tiles[t.x, t.y].type == "half")
            {
                Sou.Play("half");
            }
            else if (tiles[t.x, t.y].type == "qua")
            {
                Sou.Play("quarter");
            }


            tiles[t.x, t.y] = null;
            //Debug.Log((t.x, t.y));
            //Destroy(t.gameObject);

            //InstantiateTile(t.x, t.y);
        }
        //if ()
        //{
          // your code here
        //}

        if (!sw)
        {
            result.text = score.ToString();
            gravity();
        }
        else
        {
            canmove = true;
        }
        if(score >= 10000)
        {
            panel.SetActive(true);
        }
    }


    //checks form left to right for match and return a list start from bottom 
    void gravity()
    {
        bool done = true;
        while (done)
        {
            done = false;
            for (int j = 0; j < ySize; j++)
            {
                for (int i = 0; i < xSize; i++)
                {

                    if(Fall(i, j))
                    {
                        done = true;
                    }

                }
            }
        }


        StartCoroutine(spone());


    }

    IEnumerator spone()
    {
        yield return new WaitForSeconds(0.3f);
        for (int j = 0; j < ySize; j++)
        {
            for (int i = 0; i < xSize; i++)
            {
                //Debug.Log((i, j));
                if (tiles[i, j] == null)
                {
                    InstantiateTile(i, j);
                }
            }
        }
        StartCoroutine(Check());
    }




    //moves tiles to fall
    bool Fall(int x, int y)
    {
        if (x < 0 || y <= 0 || x >= xSize || y >= ySize * 2) // <- SizeY * 2
        {
            return false;
        }

        if (tiles[x, y] == null)
        {
            //Debug.Log((x, y));
            return false;
        }

        if (tiles[x, y - 1] != null)
        {
            //Debug.Log((x, y));
            return false;
        }

        MoveTile(x, y, x, y - 1);
        return true;
    }



    //InstantiateTile from given location
    void InstantiateTile(int x, int y)
    {
        bool lop;
        string ty;
        
        do
        {
            lop = true; // Initialize lop to false at the beginning of each iteration
            candyPrefab = candyPrefabs[Random.Range(0, candyPrefabs.Length)];
            ty = candyPrefab.GetComponent<tiles>().type;

            // Check if the selected candy prefab is the same as the one on the adjacent tile
            if (tiles[x, y - 1].type != ty)
            {
                if (ty == "clef")
                {
                    if (currentclef != 2)
                    {
                        currentclef += 1;
                        lop = false; // If they are the same, set lop to true to repeat the loop
                    }
                }
                else
                {
                    lop = false;
                }


            }
            //Debug.Log(tiles[x, y - 1].name);

        } while (lop);

        GameObject newTile = Instantiate(candyPrefab, new Vector3(startX + (offset.x * x), 4+startY + (offset.y * y), 0), Quaternion.identity);
        tiles candy = newTile.GetComponent<tiles>();
        candy.Initialize(this, x, y);
        tiles[x, y] = candy;
        newTile.transform.parent = transform;
        LeanTween.move(tiles[x, y].gameObject, new Vector3(startX + (offset.x * x), startY + (offset.y * y), 0), 0.3f);
    }

    //checks if there is any empty space at the bottom of the tile and moves it or retuns fales 




    //this funtion moves tiles and change grid ---------------------
    void MoveTile(int x1, int y1, int x2, int y2)
    {

        //Debug.Log("this move");
        //Vector3 lo = tiles[x1, y1].transform.position;
        if (tiles[x1, y1] != null)
        {
            //Debug.Log(new Vector3(startX + (offset.x * x2), startY + (offset.y * y2), 0));
            //Debug.Log(tiles[x2, y2].transform.position);
            //tiles[x1, y1].transform.position = new Vector3(startX + (offset.x * x2), startY + (offset.y * y2), 0);
            LeanTween.move(tiles[x1, y1].gameObject, new Vector3(startX + (offset.x * x2), startY + (offset.y * y2), 0), 0.3f);

        }


        if (tiles[x2, y2] != null)
        {
            LeanTween.move(tiles[x2, y2].gameObject, new Vector3(startX + (offset.x * x1), startY + (offset.y * y1), 0), 0.3f);
            //tiles[x2, y2].transform.position = new Vector3(startX + (offset.x * x1), startY + (offset.y * y1), 0);
        }


        tiles temp = tiles[x1, y1];
        tiles[x1, y1] = tiles[x2, y2];
        tiles[x2, y2] = temp;

        if (tiles[x1, y1] != null)
        {
            tiles[x1, y1].ChangePosition(x1, y1);
        }

        if (tiles[x2, y2] != null)
        {
            tiles[x2, y2].ChangePosition(x2, y2);
        }


    }

    //check the alinement of the tiles nodes------------
    public List<tiles> FindAdjacentTiles(int x, int y)
    {
        List<tiles> adjacentTiles = new List<tiles>();

        // Check left
        if (x > 0)
            adjacentTiles.Add(tiles[x - 1, y]);

        // Check right
        if (x < xSize - 1)
            adjacentTiles.Add(tiles[x + 1, y]);

        // Check above
        if (y < ySize - 1)
            adjacentTiles.Add(tiles[x, y + 1]);

        // Check below
        if (y > 0)
            adjacentTiles.Add(tiles[x, y - 1]);

        return adjacentTiles;
    }




    //checks form left to right for match and return a list  
    List<tiles> CheckHorizontalMatches()
    {
        List<tiles> TilesToCheck = new List<tiles>();
        List<tiles> TilesToReturn = new List<tiles>();
        string Type = "";

        for (int j = 0; j < ySize; j++)
        {
            for (int i = 0; i < xSize; i++)
            {
                if (tiles[i, j].type != Type)
                {
                    if (TilesToCheck.Count >= 3)
                    {
                        TilesToReturn.AddRange(TilesToCheck);
                    }
                    TilesToCheck.Clear();
                }
                Type = tiles[i, j].type;
                TilesToCheck.Add(tiles[i, j]);
            }

            if (TilesToCheck.Count >= 3)
            {
                TilesToReturn.AddRange(TilesToCheck);
            }
            TilesToCheck.Clear();
        }
        return TilesToReturn;
    }




    //checks form down to up for match and return a list  
    List<tiles> CheckVerticalMatches()
    {
        List<tiles> TilesToCheck = new List<tiles>();
        List<tiles> TilesToReturn = new List<tiles>();
        string Type = "";

        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {
                if (tiles[i, j].type != Type)
                {
                    if (TilesToCheck.Count >= 3)
                    {
                        TilesToReturn.AddRange(TilesToCheck);
                    }
                    TilesToCheck.Clear();
                }
                Type = tiles[i, j].type;
                TilesToCheck.Add(tiles[i, j]);
            }

            if (TilesToCheck.Count >= 3)
            {
                TilesToReturn.AddRange(TilesToCheck);
            }
            TilesToCheck.Clear();
        }
        return TilesToReturn;
    }


}
