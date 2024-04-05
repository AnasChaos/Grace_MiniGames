using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conductor : MonoBehaviour
{

    //Song beats per minute
    public float songBpm;
    //The number of seconds for each song beat
    public float secPerBeat;

    //Current song position, in seconds
    public float songPosition;

    //Current song position, in beats
    public float songPositionInBeats;

    //How many seconds have passed since the song started
    public float dspSongTime;

    //an AudioSource attached to this GameObject that will play the music.
    public AudioSource musicSource;

    //The offset to the first beat of the song in seconds
    public float firstBeatOffset;

    [System.Serializable]
    public class Nodes
    {
        public float beat;
        public float position;
        public typeofnotes notes;
    }

    public enum typeofnotes
    {
        full_note,
        half_note,
        quater_note
    }

    public List<Nodes> nodes;
    public GameObject nodeprefeb;
    public GameObject sponer;
    public int index = 0;

    public static conductor instance;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

    }








    // Start is called before the first frame update
    void Start()
    {
        //Load the AudioSource attached to the Conductor GameObject
        musicSource = GetComponent<AudioSource>();

        //Calculate the number of seconds in each beat
        secPerBeat = 60f / songBpm;

        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;

        //Start the music
        musicSource.Play();
    }

    void Update()
    {
        //determine how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);

        //determine how many beats since the song started
        songPositionInBeats = songPosition / secPerBeat;
        if (nodes.Count > index)
        {
            if (nodes[index].beat <= songPositionInBeats)
            {
                spone(nodes[index]);
                index += 1;
            }
        }
    }

    void spone(Nodes vv)
    {
        Debug.Log("spone");
        Instantiate(nodeprefeb, sponer.transform.position, Quaternion.identity);
    }


}
    