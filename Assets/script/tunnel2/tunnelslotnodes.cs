using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tunnelslotnodes : MonoBehaviour
{

    public tunnelletters tunnelletters;

    // Start is called before the first frame update
    void Start()
    {
        transform.gameObject.GetComponent<SpriteRenderer>().sprite = tunnelletters.sprite;

    }


}
