using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "pieces", menuName = "notepieces")]
public class notepieces : ScriptableObject
{
    public string nodes;  // The letter itself (e.g., "A", "B", "C")
    public Sprite sprite;
    public float number;
   
}
