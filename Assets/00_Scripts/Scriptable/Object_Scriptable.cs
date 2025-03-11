using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ITEMLIST //아이템 리스트의 개별 항목을 저장하는 클래스
{
    public Item_Scriptable Item_Data;

    //0~100 범위의 아이템 드롭 확률을 설정하는 값
    [Range(0.0f, 100.0f)]
    public float Value;
}

[CreateAssetMenu(fileName = "Object_Scriptable", menuName = "Scriptable Objects/Object_Scriptable")]
public class Object_Scriptable : ScriptableObject
{
    public Object_Type m_Type;
    public string Name;
    public int HP;

    //이 오브젝트가 드롭하는 아이템 리스트
    public List<ITEMLIST> Drop_Items = new List<ITEMLIST>();
}
