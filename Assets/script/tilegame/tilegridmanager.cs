using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class tilegridmanager : MonoBehaviour
{

    public GameObject[] tiles;
    public tileslot[] tileslots;
    public GameObject holder;
    public bool completed;
    public tileobjects[] TONodes;
    public tilegamemanager TT;


    public void Start()
    {
        for (int x = 0; x < tileslots.Length; x++)
        {
            tileslots[x].Initialize(this, x);
        }
        randomplacenodes();
    }

    public void randomplacenodes()
    {
        int randomindex = Random.Range(0, TONodes.Length);
        int randomindex2 = Random.Range(0, TONodes.Length);
        Droped(TONodes[randomindex], tileslots[randomindex2]);

    }



    public void check()
    {
        for (int x = 0; x < tileslots.Length; x++)
        {
            if (tileslots[x].free)
            {
                return;
            }
        }
        completed = true;
        TT.chec();
    }

    public void Droped(tileobjects tileobjects, tileslot tileslot )
    {

        float spacereq;
        if (tileslot.free == true)
        {
            spacereq = tileobjects.number;
            List<tileslot> availableSlots = SpaceAvailable(spacereq, tileslot.x);
            if (availableSlots.Count == spacereq)
            {
                GameObject tt = Instantiate(tiles[tileobjects.index], CalculateCenter(availableSlots), Quaternion.identity);
                tt.transform.SetParent(holder.transform);
                tt.transform.localScale = Vector3.one;
                check();
            }
        }
    }

    Vector3 CalculateCenter(List<tileslot> slots)
    {
        float totalX = 0f;
        foreach (tileslot slot in slots)
        {
            totalX += slot.gameObject.transform.position.x;
        }

        return new Vector3(totalX / slots.Count, slots[0].gameObject.transform.position.y, 0f);
    }


    public List<tileslot> SpaceAvailable(float spaceRequired, int startRow)
    {
        int space = 0;
        List<tileslot> slots = new List<tileslot>();



        if (spaceRequired > tileslots.Length)
        {
            return slots;
        }

        // Check to the right
        for (int x = startRow; x < tileslots.Length; x++)
        {
            if (tileslots[x].free == true)
            {
                slots.Add(tileslots[x]);
                space++;
                if (space == spaceRequired)
                {
                    for (int i = 0; i < slots.Count; i++)
                    {
                        slots[i].free = false;
                    }

                        return slots;
                }
            }
            else
            {
                // If any slot to the right is not free, stop checking
                break;
            }
        }


        // Check to the left
        for (int x = startRow - 1; x >= 0; x--)
        {
            if (tileslots[x].free == true)
            {
                slots.Add(tileslots[x]);
                space++;
                if (space == spaceRequired)
                {
                    for (int i = 0; i < slots.Count; i++)
                    {
                        slots[i].free = false;
                    }
                    return slots;
                }
            }
            else
            {
                // If any slot to the left is not free, stop checking
                break;
            }
        }

        // If both checks fail, there is not enough space
        return slots;
    }

    


    /*    public bool SpaceAvailable(float spaceRequired, int startRow)
        {
            int space = 0;

            Debug.Log(spaceRequired);
            Debug.Log(startRow);

            if (spaceRequired > tileslots.Length)
            {
                return false;
            }

            for (int x = 0; x < spaceRequired; x++)
            {
                if (tileslots[x + startRow].free == true)
                {
                    space++;
                    if (space == spaceRequired)
                    {
                        return true;
                    }
                }
                else
                {
                    // If any slot is not free, continue checking the next slots
                    break;
                }
            }

            // If the loop completes, all required slots are free
            return false;
        }*/

}
