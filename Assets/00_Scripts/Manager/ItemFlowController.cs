using UnityEngine;
using System.Collections.Generic;

//Monobehavior 상속 안함(오브젝트에 적용 안됨)
public class ItemFlowController
{
	//드롭하는 아이템 리스트를 반환하는 함수(static)
	public static List<Item_Scriptable>DROPITEMLIST(List<ITEMLIST> m_ItemList)
	{
		//드롭되는 아이템들을 저장할 리스트
		List<Item_Scriptable>Get_Item_List = new List<Item_Scriptable>();
		//해당 오브젝트의 드롭 리스트를 돌면서
		for(int i = 0; i < m_ItemList.Count; i++)
		{
			//확률 생성
			float RandomValue = Random.Range(0.0f, 100.0f);
			//만약 지정된 확률이 현재 확률 값 보다 크거나 같으면
			if(RandomValue <= m_ItemList[i].Value)
			{
				//드롭 아이템 리스트에 해당 아이템을 삽입한다.
				Get_Item_List.Add(m_ItemList[i].Item_Data);
			}
		}
		//드롭 아이템 리스트 반환
		return Get_Item_List;
	}
}
