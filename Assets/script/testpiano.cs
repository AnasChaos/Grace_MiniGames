using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testpiano : MonoBehaviour
{
    public Button[] whitekeys;
    public Button[] blackkeys;
    public List<Octivebuttons> oc = new List<Octivebuttons>();

    [System.Serializable]
    public class Octivebuttons
    {
        public int octives;
        public string value;
        public Button bu;
        public int number;
    }




    void Start()
    {
        InitializePianoKeys();
    }

    void InitializePianoKeys()
    {
        bool loop = true;
        string[] noteNames = {"C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
        int nu = 0;
        int wh = 0;
        int bl = 0;
        // Create 88 piano keys
        for (int octave = 0; octave < 8; octave++) // Assuming a standard piano with 7 octaves
        {
            foreach (string note in noteNames)
            {
                if (octave == 0 && !(note == "A" || note == "A#" || note == "B"))
                {
                    continue; // Skip notes other than A, A#, and B for octave 0
                }
                if (loop)
                {
                    nu += 1;
                    string fullNoteName = note + octave.ToString();
                    bool hasSharp = fullNoteName.Contains("#");
                    Debug.Log(fullNoteName);

                    if (!hasSharp)
                    {
                        Octivebuttons button1 = new Octivebuttons();
                        button1.octives = octave;
                        button1.value = fullNoteName;
                        button1.bu = whitekeys[wh];
                        button1.number = nu;
                        oc.Add(button1);
                        wh +=1;
                    }
                    else
                    {
                        Octivebuttons button1 = new Octivebuttons();
                        button1.octives = octave;
                        button1.value = fullNoteName;
                        button1.bu = blackkeys[bl];
                        button1.number = nu;
                        oc.Add(button1);
                        bl += 1;
                    }
                }
            }

            // Reset the loop variable for the next octave
            loop = true;
        }
        // You now have a list of piano keys in the pianoKeys variable
    }
}
