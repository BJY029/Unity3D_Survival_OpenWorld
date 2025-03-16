using UnityEngine;

[CreateAssetMenu(fileName = "Item_Scriptable", menuName = "Scriptable Objects/Item_Scriptable")]
public class Item_Scriptable : ScriptableObject
{
    public int itemId;
    public string ItemName;
    public string Description;
    public Item_Type Type;
    public Rarity rarity;
    public float Weight;
}


//해당 클래스는 스크립터블 오브젝트가 아니며, 
//게임이 실행된 후, 특정 플레이어의 인벤토리에서 변동되는 데이터를 저장하는 역할 수행
[System.Serializable]
public class ITEM
{
    public Item_Scriptable Data;
    public int Count;
}
