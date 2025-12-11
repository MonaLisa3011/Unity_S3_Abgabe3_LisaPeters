using UnityEngine;

[CreateAssetMenu(fileName = "SO_Item", menuName = "Scriptable Objects/SO_Item")]
public class SO_Item : ScriptableObject
{
    public string ItemID;
    public string itemName;
    public string description;

    [Space]
    public Sprite itemSprite;
    public Sprite highlightSprite;

    [Header("Player Stats")]
    [Tooltip("This bonus speed gives the Players Movement Speed")]
    public float bonusSpeed;
    public int moreHealth;
}
