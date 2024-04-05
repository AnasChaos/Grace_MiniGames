using UnityEngine;

[CreateAssetMenu(fileName = "new color", menuName = "master colors")]
public class mastercolors : ScriptableObject
{
    public string color;  // The letter itself (e.g., "A", "B", "C")
    public Sprite sprite;  // The sprite for the alphabet letter
    public bool corrects;
}
