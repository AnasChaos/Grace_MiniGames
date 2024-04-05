using UnityEngine;


public class testcodes : MonoBehaviour
{



    public double fatherfrequency = 440.0;
    public double maxfrequency = 440.0;
    public double frequency = 440.0;


    private double increment; // the amount of music moves per frame generated depending on frequency 
    private double phase; // current location in music frame 
    private double sampling_frequency = 48000.0; //defalt of unity 

    public float gain; //volume or amplitude 
    [Range(0,1)]
    public float maxvolume = 0.1f; //volume 
    public float mediumvolume = 0.1f; //volume 
    public float decayRate = 0.02f;
    public float accendRate = 0.02f;
    public bool clicked;
    public bool fullyaccend;
    private void Start()
    {
        frequency = fatherfrequency;
        sampling_frequency = AudioSettings.outputSampleRate;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            frequency = fatherfrequency;
            //gain = maxvolume;
            clicked = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            clicked = false;
            fullyaccend = false;
        }
    }

    //this funtion is run every 47.5 times diff frames rate
    void OnAudioFilterRead(float[] data, int channels)
    {
        fre();
        // update increment in case frequency has changed
        increment = frequency * 2.0 * Mathf.PI / sampling_frequency;

        for (var i = 0; i < data.Length; i = i + channels)
        {
            phase = phase + increment;
            data[i] = (float)(gain * Mathf.Sin((float)phase));

            if(channels == 2)
            {
                data[i + 1] = data[i];
            }
            if (phase > (Mathf.PI * 2))
            {
                phase = 0;
            }

        }
    }


    void fre()
    {
        if (!clicked && gain > 0)
        {
            gain -= decayRate;
            frequency -= decayRate * 100;
        }
        else if(!clicked && gain <= 0)
        {
            frequency = fatherfrequency;
        }
        if (clicked && gain < maxvolume && !fullyaccend)
        {
            gain += accendRate;
            frequency += decayRate * 100;
        }
        else if (clicked && gain >= maxvolume)
        {
            fullyaccend = true;
        }
        if (gain > mediumvolume && fullyaccend)
        {
            gain -= decayRate;
        }
    }


}
