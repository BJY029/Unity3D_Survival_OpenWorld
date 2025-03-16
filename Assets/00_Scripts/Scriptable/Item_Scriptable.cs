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


//�ش� Ŭ������ ��ũ���ͺ� ������Ʈ�� �ƴϸ�, 
//������ ����� ��, Ư�� �÷��̾��� �κ��丮���� �����Ǵ� �����͸� �����ϴ� ���� ����
[System.Serializable]
public class ITEM
{
    public Item_Scriptable Data;
    public int Count;
}
