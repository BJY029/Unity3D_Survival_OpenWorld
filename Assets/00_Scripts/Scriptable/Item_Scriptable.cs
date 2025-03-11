using UnityEngine;

[CreateAssetMenu(fileName = "Item_Scriptable", menuName = "Scriptable Objects/Item_Scriptable")]
public class Item_Scriptable : ScriptableObject
{
    public int itemId;
    public string ItemName;
    public string Description;
    public Item_Type Type;
    public Rarity rarity;
}
