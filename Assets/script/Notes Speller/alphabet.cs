using UnityEngine;

[CreateAssetMenu(fileName = "New Alphabet Letter", menuName = "Alphabet Letter")]
public class alphabet : ScriptableObject
{
    public string letter;  // The letter itself (e.g., "A", "B", "C")
    public Sprite sprite;  // The sprite for the alphabet letter
}
