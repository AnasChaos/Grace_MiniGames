using UnityEngine;

[CreateAssetMenu(fileName = "New gems", menuName = "gems")]
public class gems : ScriptableObject
{

    public string gem;  // The letter itself (e.g., "A", "B", "C")
    public Sprite sprite;  // The sprite for the alphabet letter
}
