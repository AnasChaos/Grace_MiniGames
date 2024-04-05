 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nodescontrol : MonoBehaviour
{
    float BPMS;

    // Start is called before the first frame update
    void Start()
    {
        BPMS = conductor.instance.songBpm/60;
        Debug.Log(BPMS);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, -BPMS* Time.deltaTime, 0);
    }
}
