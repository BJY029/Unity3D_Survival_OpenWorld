using UnityEngine;
using System.Collections.Generic;

//�������� ������ Ȱ��ȭ �Ǵ� ��������Ʈ
public delegate void OnItemGet();

//Monobehavior ��� ����(������Ʈ�� ���� �ȵ�)
public class ItemFlowController
{
	//��������Ʈ ����
	public static event OnItemGet OnItemGet;


	//���� ���ϰ� �ִ� ������ �������� �����ϴ� ��ųʸ�
	public static Dictionary<int, ITEM> Item_Pairs = new Dictionary<int, ITEM>();
	//�÷��̾��� �⺻ ���� �� ����
	public static float Player_Weight = 2500.0f;

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
			Debug.Log("new item added : " + item.Data.name);
		}
		//�ش� ��������Ʈ�� null�� �ƴ϶��, �����Ų��.
		OnItemGet?.Invoke();
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

	//������ ���Ը� �����ϴ� �Լ�, �ش� �������� ���� id�� �޾ƿ´�.
	public static float WeightItem(int key)
	{
		//�ش�Ǵ� �������� ���� �κ��丮�� �����ϸ�
		if (HaveItem(key))
		{
			//�ش� �������� ���԰��� �޾ƿͼ�, ���� �ش� ������ ������ ���Ѵ�.
			ITEM item = Item_Pairs[key];
			float value = item.Data.Weight * item.Count;
			//���� ���� ��ȯ�Ѵ�.
			return value;
		}
		return -1.0f;
	}

	//��ü ���Ը� ����ϴ� �Լ�
	public static float Weight()
	{
		float weight = 0.0f;
		foreach(var item in Item_Pairs)
		{
			//��� �������� ���Ը� ����ؼ�
			weight += WeightItem(item.Key);
		}
		//��ü ���� ���� ��ȯ�Ѵ�.
		return weight;
	}
}
