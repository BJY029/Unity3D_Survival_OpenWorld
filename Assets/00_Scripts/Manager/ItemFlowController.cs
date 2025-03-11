using UnityEngine;
using System.Collections.Generic;

//Monobehavior ��� ����(������Ʈ�� ���� �ȵ�)
public class ItemFlowController
{
	//����ϴ� ������ ����Ʈ�� ��ȯ�ϴ� �Լ�(static)
	public static List<Item_Scriptable>DROPITEMLIST(List<ITEMLIST> m_ItemList)
	{
		//��ӵǴ� �����۵��� ������ ����Ʈ
		List<Item_Scriptable>Get_Item_List = new List<Item_Scriptable>();
		//�ش� ������Ʈ�� ��� ����Ʈ�� ���鼭
		for(int i = 0; i < m_ItemList.Count; i++)
		{
			//Ȯ�� ����
			float RandomValue = Random.Range(0.0f, 100.0f);
			//���� ������ Ȯ���� ���� Ȯ�� �� ���� ũ�ų� ������
			if(RandomValue <= m_ItemList[i].Value)
			{
				//��� ������ ����Ʈ�� �ش� �������� �����Ѵ�.
				Get_Item_List.Add(m_ItemList[i].Item_Data);
			}
		}
		//��� ������ ����Ʈ ��ȯ
		return Get_Item_List;
	}
}
