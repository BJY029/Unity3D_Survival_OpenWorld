using UnityEngine;
using System.Collections.Generic;

//아이템을 얻을때 활성화 되는 델리게이트
public delegate void OnItemGet();

//Monobehavior 상속 안함(오브젝트에 적용 안됨)
public class ItemFlowController
{
	//델리게이트 선언
	public static event OnItemGet OnItemGet;


	//현재 지니고 있는 아이템 정보들을 관리하는 딕셔너리
	public static Dictionary<int, ITEM> Item_Pairs = new Dictionary<int, ITEM>();
	//플레이어의 기본 무게 값 설정
	public static float Player_Weight = 2500.0f;

	//드롭하는 아이템 리스트를 반환하는 함수(static)
	public static List<ITEM>DROPITEMLIST(List<ITEMLIST> m_ItemList)
	{
		//드롭되는 아이템들을 저장할 리스트
		List<ITEM>Get_Item_List = new List<ITEM>();
		//해당 오브젝트의 드롭 리스트를 돌면서
		for(int i = 0; i < m_ItemList.Count; i++)
		{
			//확률 생성
			float RandomValue = Random.Range(0.0f, 100.0f);
			//만약 지정된 확률이 현재 확률 값 보다 크거나 같으면
			if(RandomValue <= m_ItemList[i].Value)
			{
				//드롭될 아이템 갯수를 지정된 max 범위 중 랜덤으로 설정
				int value = Random.Range(1, m_ItemList[i].Maximum);

				//ITEM 객체 생성
				ITEM item = new ITEM();
				//ITEM 객체 수정
				item.Data = m_ItemList[i].Item_Data;
				item.Count = value;
				//드롭 아이템 리스트에 해당 아이템을 삽입한다.
				Get_Item_List.Add(item);
			}
		}
		//드롭 아이템 리스트 반환
		return Get_Item_List;
	}

	//아이템을 얻을 때 호출될 함수(해당 아이템의 데이터와, 갯수 값이 인자)
	public static void GETITEM(Item_Scriptable scriptableData, int value)
	{
		//ITEM 객체 선언 및 저장
		ITEM item = new ITEM();
		item.Data = scriptableData;
		item.Count = value;

		//해당 아이템의 itemId를 불러온다.
		int ID = item.Data.itemId;
		//얻은 아이템이 현재 아이템에 이미 있는 경우
		if (HaveItem(ID))
		{
			//해당 딕셔너리에 저장된 ITEM 객체의 Count 값을 얻은 값 만큼 증가시킨다.
			Item_Pairs[ID].Count += value;
		}
		else
		{
			//얻은 아이템이 현재 아이템에 없는 경우
			//해당 아이템을 딕셔너리에 새로 추가시킨다.
			Item_Pairs.Add(ID, item);
			Debug.Log("new item added : " + item.Data.name);
		}
		//해당 델리게이트가 null이 아니라면, 실행시킨다.
		OnItemGet?.Invoke();
	}

	//얻은 아이템이 현재 아이템에 있는지 없는지 확인하는 함수
	public static bool HaveItem(int value)
	{
		if (Item_Pairs.ContainsKey(value))
		{
			return true;
		}
		return false;
	}

	//아이템 무게를 설정하는 함수, 해당 아이템의 고유 id를 받아온다.
	public static float WeightItem(int key)
	{
		//해당되는 아이템이 현재 인벤토리에 존재하면
		if (HaveItem(key))
		{
			//해당 아이템의 무게값을 받아와서, 현재 해당 아이템 갯수와 곱한다.
			ITEM item = Item_Pairs[key];
			float value = item.Data.Weight * item.Count;
			//최종 값을 반환한다.
			return value;
		}
		return -1.0f;
	}

	//전체 무게를 계산하는 함수
	public static float Weight()
	{
		float weight = 0.0f;
		foreach(var item in Item_Pairs)
		{
			//모든 아이템의 무게를 계산해서
			weight += WeightItem(item.Key);
		}
		//전체 무게 값을 반환한다.
		return weight;
	}
}
