using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "letters", menuName = "tunnelletters")]
public class tunnelletters : ScriptableObject
{
    public string nodes;  // The letter itself (e.g., "A", "B", "C")
    public Sprite sprite;
    public float space;

}
