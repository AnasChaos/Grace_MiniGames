using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class boxmanager : MonoBehaviour
{

    public bool started;
    public bool win;
    public tunnelletters[] answers;
    public tunnelletters[] options;
    public boxholders[] BH;
    int current;
    int rounds;
    public int maxrounds;
    public List<tunnelletters> currenttl = new List<tunnelletters>();
    [SerializeField] private GameObject panel;

    public void setups()
    {
        int ran;
        currenttl = null;
        currenttl = options.ToList<tunnelletters>();

        for (int i = 0; i < BH.Length; i++)
        {
            ran = Random.Range(0, currenttl.Count);
            BH[i].set(currenttl[ran]);
            currenttl.RemoveAt(ran);
        }
    }

    public void checks()
    {
        bool allcorrect = true;
        for (int i = 0; i < BH.Length; i++)
        {
       
          if(BH[i].keys != answers[i])
            {
                allcorrect = false;
                Debug.Log(i);
                Debug.Log(BH[i].keys);
            }
        }

        if (allcorrect == true)
        {
            panel.SetActive(true);
        }


    }


    // Start is called before the first frame update
    void Start()
    {
        setups();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
