using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ITEMLIST //������ ����Ʈ�� ���� �׸��� �����ϴ� Ŭ����
{
    public Item_Scriptable Item_Data;

    //0~100 ������ ������ ��� Ȯ���� �����ϴ� ��
    [Range(0.0f, 100.0f)]
    public float Value;
}

[CreateAssetMenu(fileName = "Object_Scriptable", menuName = "Scriptable Objects/Object_Scriptable")]
public class Object_Scriptable : ScriptableObject
{
    public Object_Type m_Type;
    public string Name;
    public int HP;

    //�� ������Ʈ�� ����ϴ� ������ ����Ʈ
    public List<ITEMLIST> Drop_Items = new List<ITEMLIST>();
}
