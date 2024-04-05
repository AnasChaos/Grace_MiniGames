using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Alchemymanager : MonoBehaviour
{

    bool start;
    bool wins;

    public Beaker[] bb;
    public int winamount;
    public GameObject panel;
    Beaker old = new Beaker();
    Beaker old2 = new Beaker();
    public Alchemyslot[] slots;
    [SerializeField] private TMP_Text[] texts;

    // Start is called before the first frame update
    void Start()
    {

        pudatetext();
        setup();

    }

    public void setup()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].bb = bb[i];
        }

    }


    public void transfer(Beaker b1, Beaker b2)
    {
        int total;
        int leftover;
        

        old.bottel = b2.bottel;
        old.max = b2.max;
        old2.bottel = b1.bottel;
        old2.max = b1.max;

        Debug.Log(old.bottel);
        total = b1.bottel + b2.bottel;
        if (total > b2.max)
        {

            b2.bottel = b2.max;
            //StartCoroutine(b2.breakers.AddComponent<Alchemyslot>().changevolume(old, b2));
            //StartCoroutine(b2.breakers.AddComponent<Alchemyslot>().changevolume(old, b2));

            leftover = total - b2.max;
            b1.bottel = leftover;

        }
        else
        {
            b2.bottel = total;
            b1.bottel = 0;

        }
        StartCoroutine(slots[b1.id].changes(slots[b1.id].point, old2, b1));
        pudatetext();
        check();
    }
    public void check()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].bb.bottel == winamount)
            {
                wins = true;
                panel.SetActive(true);
            }
        }

    }
    public void pudatetext()
    {
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].text = bb[i].bottel.ToString();
        }
    }
    

    // Update is called once per frame
    void Update()
    {



    }
}
