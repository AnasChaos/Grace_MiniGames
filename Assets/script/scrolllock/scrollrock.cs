using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class scrollrock : MonoBehaviour
{
    bool start;
    bool win;
    public int maxround;
    private int current;
    public Soundmanager Sou;
    public Button numkeysup;
    public Button numkeysdown;
    public Animator numb;
    public Animator ty;
    public GameObject panel;
    SignType[] signs;

    [SerializeField] private TMP_Text[] nums;

    int previousnumber = 0;
    int currentnumber = 1;
    int nextnumber = 2;
    int max = 7;

    int currentty = 1;

    public enum SignType
    {
        high,
        Natural,
        flate
    }

    public void movesign(int ee)
    {
        if ((currentty + ee) <= 2 && (currentty + ee) >= 0)
        {
            currentty = currentty + ee;
            if (ee == 1)
            {
                if (currentty == 1)
                {
                    ty.Play("downtonormal");
                }
                if (currentty == 2)
                {
                    ty.Play("upkeys");
                }
            }
            else
            {
                if (currentty == 1)
                {
                    ty.Play("uptonormal");
                }
                if (currentty == 0)
                {
                    ty.Play("down");
                }
            }
        }
    }

    public void movenum(int ee)
    {
        if (ee == 1)
        {
            numb.Play("upnum");
            previousnumber++;
            currentnumber++;
            nextnumber++;
            if (previousnumber > max)
            {
                previousnumber = 0;
            }
            if (currentnumber > max)
            {
                currentnumber = 0;
            }
            if (nextnumber > max)
            {
                nextnumber = 0;
            }
        }
        else
        {
            numb.Play("downnum");
            previousnumber--;
            currentnumber--;
            nextnumber--;
            if (previousnumber < 0)
            {
                previousnumber = max;
            }
            if (currentnumber < 0)
            {
                currentnumber = max;
            }
            if (nextnumber < 0)
            {
                nextnumber = max;
            }
        }

        StartCoroutine(normalize());
    }

    public IEnumerator normalize()
    {
        yield return new WaitForSeconds(0.3f);
        nums[0].text = previousnumber.ToString();
        nums[1].text = currentnumber.ToString();
        nums[2].text = nextnumber.ToString();
        numb.Play("normalnum");
    }

    public void Check()
    {
        if (currentnumber == 0 && signs[currentty] == SignType.Natural)
        {
            panel.SetActive(true);
            win = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Sou = Soundmanager.instance;
        signs = new SignType[] { SignType.high, SignType.Natural, SignType.flate };
    }

    // Update is called once per frame
    void Update()
    {
        // Update logic if needed
    }
}
