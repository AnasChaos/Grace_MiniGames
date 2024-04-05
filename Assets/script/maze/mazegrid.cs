using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mazegrid : MonoBehaviour
{
    public GameObject panel;
    public bool started;
    public bool win;
    Soundmanager Sou;
    public GameObject tile;
    public int[,] mazeLayout;
    private mazepath[,] mazepath;
    float startX;
    float startY;
    public float offestme;
    public Vector2 startingpoints;
    public Vector2 winningpoint;

    // player
    public float speed = 5f;
    public Rigidbody2D rb;
    public Animator player;
    bool playermove;
    Vector2 target;
    public float threshold = 0.1f;  // Adjust this threshold as needed
    public float offsets;
    Vector2 currentlocation;

    // monitar
    public float monitarspeed = 5f;
    public Rigidbody2D monitarrb;
    public Animator monitarani;
    bool monitarmove;
    Vector2 monitartarget;
    public float monitarthreshold = 0.1f;  // Adjust this threshold as needed
    public float monitaroffsets;

    public GameObject lostpannel;

    
    private List<mazepath> pastmoves = new List<mazepath>();

    public void moveplayer()
    {
        if (playermove)
        {
            Vector2 direction = new Vector2(target.x - rb.position.x, (target.y + offsets) - rb.position.y);
            direction.Normalize(); // Normalize the vector to get a unit vector

            if (direction.x >= 0.5f)
            {
                rb.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                rb.transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            Debug.Log(Mathf.Abs(direction.x));
            rb.position = Vector2.MoveTowards(rb.position, new Vector2(target.x, target.y + offsets), speed * Time.deltaTime);

            // Check if the player has reached the target point within a threshold
            if (Vector2.Distance(rb.position, new Vector2(target.x, (target.y + offsets))) < threshold)
            {
                player.SetBool("run", false);
                playermove = false;
                // You can add additional logic here when the player reaches the target
                Debug.Log("Player reached the target");
            }
        }
    }

    public void movemonitar()
    {
        if (monitarmove)
        {
            Vector2 direction = new Vector2(monitartarget.x - monitarrb.position.x, (monitartarget.y + offsets) - monitarrb.position.y);
            direction.Normalize(); // Normalize the vector to get a unit vector

            if (direction.x >= 0.5f)
            {
                monitarrb.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                monitarrb.transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            Debug.Log(Mathf.Abs(direction.x));
            monitarrb.position = Vector2.MoveTowards(monitarrb.position, new Vector2(monitartarget.x, monitartarget.y + offsets), speed * Time.deltaTime);

            // Check if the player has reached the target point within a threshold
            if (Vector2.Distance(rb.position, new Vector2(target.x, (target.y + offsets))) < threshold)
            {
                monitarani.SetBool("run", false);
                monitarmove = false;
            }
        }
    }




    void Update()
    {
        moveplayer();
        movemonitar();
    }


    void Start()
    {
        Sou = Soundmanager.instance;
        startX = transform.position.x;
        startY = transform.position.y;
        //offset = tile.GetComponent<SpriteRenderer>().bounds.size;
        GenerateMaze();
        currentlocation = new Vector2((int)startingpoints.x, (int)startingpoints.y);
        activate((int)startingpoints.x, (int)startingpoints.y);

    }

    void GenerateMaze()
    {
        // Example maze layout
        mazeLayout = new int[,]
        {
            {0, 0, 0, 0, 0 ,0 ,0, 0 ,0 ,0 ,0 ,0 ,0},
            {0, 1, 1, 1, 0 ,1 ,1, 1 ,0 ,1 ,1 ,1 ,0},
            {0, 1, 1, 1, 0 ,1 ,1, 1 ,0 ,1 ,1 ,1 ,0},
            {0, 0, 1, 0, 0 ,1 ,1, 1 ,0 ,0 ,1 ,0 ,0},
            {0, 1, 1, 0, 0 ,0 ,1, 0 ,0 ,0 ,1 ,0 ,0},
            {0, 1, 0, 0, 0 ,0 ,1, 0 ,0 ,0 ,1 ,0 ,0},
            {0, 1, 0, 0, 1 ,0 ,1, 0 ,1 ,0 ,1 ,0 ,0},
            {1, 1, 1, 1, 1 ,1 ,1, 1 ,1 ,1 ,1 ,1 ,0},
            {0, 1, 0, 1, 1 ,0 ,1, 1 ,1 ,0 ,0 ,0 ,0},
            {0, 1, 0, 1, 0 ,0 ,1, 1 ,1 ,0 ,0 ,0 ,0},
            {0, 1, 1, 1, 0 ,0 ,1, 1 ,1 ,0 ,0 ,0 ,0},
            {0, 0, 0, 1, 0 ,0 ,0, 1 ,0 ,0 ,0 ,0 ,0},
            {0, 0, 0, 1, 1 ,1 ,1, 1 ,1 ,1 ,1 ,1 ,0},
            {0, 0, 0, 0, 0 ,0 ,0, 0 ,0 ,0 ,0 ,1 ,0},
            {0, 0, 0, 0, 0 ,0 ,0, 0 ,0 ,0 ,0 ,1 ,0},
        };
        InstantiateMazeObjects();
    }

    void InstantiateMazeObjects()
    {

        mazepath = new mazepath[mazeLayout.GetLength(0), mazeLayout.GetLength(1)];

        for (int row = 0; row < mazeLayout.GetLength(0); row++)
        {
            for (int col = 0; col < mazeLayout.GetLength(1); col++)
            {
                if (mazeLayout[row, col] == 0)
                {
                    //Instantiate wall prefab at position (row, col)wall
                    GameObject newTile = Instantiate(tile, new Vector3(startX + (offestme * row), startY + (offestme * col), 0), Quaternion.identity);
                    mazepath point = newTile.GetComponent<mazepath>();
                    point.Initialize(this, row, col,false);
                    mazepath[row, col] = point;
                    newTile.transform.parent = transform;
                    newTile.SetActive(false);
                    //newTile.transform.localScale = Vector3.one;
                }
                else if (mazeLayout[row, col] == 1)
                {
                    // Instantiate path prefab at position (row, col)
                    GameObject newTile = Instantiate(tile, new Vector3(startX + (offestme * row), startY + (offestme * col), 0), Quaternion.identity);
                    mazepath point = newTile.GetComponent<mazepath>();
                    point.Initialize(this, row, col, true);
                    mazepath[row, col] = point;
                    newTile.transform.parent = transform;
                    newTile.SetActive(false);
                    //newTile.transform.localScale = Vector3.one;
                }
            }
        }
    }

    public void activate(int x , int y)
    {

      

        // Check if the indices are within the bounds of the array
        if (IsValidIndex(x, y))
            mazepath[x, y].gameObject.SetActive(true);

        ActivateAdjacentPaths(x - 1, y);
        ActivateAdjacentPaths(x + 1, y);
        ActivateAdjacentPaths(x, y + 1);
        ActivateAdjacentPaths(x, y - 1);
        ActivateAdjacentPaths(x + 1, y - 1);
        ActivateAdjacentPaths(x + 1, y + 1);
        ActivateAdjacentPaths(x - 1, y - 1);
        ActivateAdjacentPaths(x - 1, y + 1);

    }
    // Helper method to check if the indices are valid
    private bool IsValidIndex(int x, int y)
    {
        return x >= 0 && x < mazepath.GetLength(0) && y >= 0 && y < mazepath.GetLength(1);
    }

    // Helper method to activate adjacent paths
    private void ActivateAdjacentPaths(int x, int y)
    {
        if (IsValidIndex(x, y))
            mazepath[x, y].gameObject.SetActive(true);
    }

    public void clickpoint(int x, int y)
    {
        //Debug.Log(x);
        //Debug.Log(y);
        if (FindAdjacentTiles(x, y))
        {

            pastmoves.Add(mazepath[(int)currentlocation.x, (int)currentlocation.y]);

            monitartarget = mazepath[(int)currentlocation.x, (int)currentlocation.y].transform.position;
            monitarmove = true;
            monitarani.SetBool("run", true);


            check(x,y);

            activate(x, y);
            target = mazepath[x, y].transform.position;
            playermove = true;
            player.SetBool("run", true);






            currentlocation = new Vector2(x, y);

            if (x == winningpoint.x && y == winningpoint.y)
            {
                wins();
            }

        }
    }

    public void check(int x, int y)
    {

        for (int i = 0; i < pastmoves.Count; i++)
        {

            if(pastmoves[i] == mazepath[x, y])
            {
                lost();
            }

        }

    }

    public void wins()
    {
        win = true;
        panel.SetActive(true);
    }

    public void lost()
    {
        lostpannel.SetActive(true);

    }


    //check the alinement of the tiles nodes------------
    public bool FindAdjacentTiles(int x, int y)
    {

        if ((currentlocation.x-1) == x && currentlocation.y == y) return true;

        if ((currentlocation.x +1) == x && currentlocation.y == y) return true;

        if (currentlocation.x == x && (currentlocation.y+1) == y) return true;

        if (currentlocation.x == x && (currentlocation.y- 1) == y) return true;
        return false;

    }

}
