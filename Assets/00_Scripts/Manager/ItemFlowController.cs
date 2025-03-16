using UnityEngine;
using System.Collections.Generic;

//Monobehavior ��� ����(������Ʈ�� ���� �ȵ�)
public class ItemFlowController
{
	//���� ���ϰ� �ִ� ������ �������� �����ϴ� ��ųʸ�
	public static Dictionary<int, ITEM> Item_Pairs = new Dictionary<int, ITEM>();

	//����ϴ� ������ ����Ʈ�� ��ȯ�ϴ� �Լ�(static)
	public static List<ITEM>DROPITEMLIST(List<ITEMLIST> m_ItemList)
	{
		//��ӵǴ� �����۵��� ������ ����Ʈ
		List<ITEM>Get_Item_List = new List<ITEM>();
		//�ش� ������Ʈ�� ��� ����Ʈ�� ���鼭
		for(int i = 0; i < m_ItemList.Count; i++)
		{
			//Ȯ�� ����
			float RandomValue = Random.Range(0.0f, 100.0f);
			//���� ������ Ȯ���� ���� Ȯ�� �� ���� ũ�ų� ������
			if(RandomValue <= m_ItemList[i].Value)
			{
				//��ӵ� ������ ������ ������ max ���� �� �������� ����
				int value = Random.Range(1, m_ItemList[i].Maximum);

				//ITEM ��ü ����
				ITEM item = new ITEM();
				//ITEM ��ü ����
				item.Data = m_ItemList[i].Item_Data;
				item.Count = value;
				//��� ������ ����Ʈ�� �ش� �������� �����Ѵ�.
				Get_Item_List.Add(item);
			}
		}
		//��� ������ ����Ʈ ��ȯ
		return Get_Item_List;
	}

	//�������� ���� �� ȣ��� �Լ�(�ش� �������� �����Ϳ�, ���� ���� ����)
	public static void GETITEM(Item_Scriptable scriptableData, int value)
	{
		//ITEM ��ü ���� �� ����
		ITEM item = new ITEM();
		item.Data = scriptableData;
		item.Count = value;

		//�ش� �������� itemId�� �ҷ��´�.
		int ID = item.Data.itemId;
		//���� �������� ���� �����ۿ� �̹� �ִ� ���
		if (HaveItem(ID))
		{
			//�ش� ��ųʸ��� ����� ITEM ��ü�� Count ���� ���� �� ��ŭ ������Ų��.
			Item_Pairs[ID].Count += value;
		}
		else
		{
			//���� �������� ���� �����ۿ� ���� ���
			//�ش� �������� ��ųʸ��� ���� �߰���Ų��.
			Item_Pairs.Add(ID, item);
		}
	}

	//���� �������� ���� �����ۿ� �ִ��� ������ Ȯ���ϴ� �Լ�
	public static bool HaveItem(int value)
	{
		if (Item_Pairs.ContainsKey(value))
		{
			return true;
		}
		return false;
	}
}
