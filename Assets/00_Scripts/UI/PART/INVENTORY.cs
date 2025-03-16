using System.Collections.Generic;
using UnityEngine;

public class INVENTORY : UIPART
{
    //아이템이 표시될 패널에 달린 스크립트
    public Item_Panel Item_Panel;
    //해당 UI가 생성될 위치
    public Transform Content;

    //아이템 UI를 저장할 리스트
    List<Item_Panel> items = new List<Item_Panel>();
    //최대 아이템 칸을 50개로 설정
    int ItemMaximimValue = 50;

    //시작과 동시에 초기화
	private void Start()
	{
		Init();
	}

    //아이템이 새로 들어온 후, 해당 변경 사항을 인벤토리 창이 켜졌을 때 최신화 하기 위해
    //OnEnable()에서도 인벤토리 설정 함수 호출
	private void OnEnable()
	{
		SetInventory();
	}

    //아이템 리스트를 초기화 하는 함수
	public void Init()
    {
        //만약 현재 보유한 아이템이 50개 이상이 되면
        if(ItemFlowController.Item_Pairs.Count >= ItemMaximimValue)
            //최대 아이템 갯수를 최신화한다.
            ItemMaximimValue = ItemFlowController.Item_Pairs.Count;

        //50개의 패널을 생성한다.
        for(int i = 0;i < ItemMaximimValue; i++)
        {
            //아이템 패널을 해당 위치에 생성한다.(50개 생성)
            var go = Instantiate(Item_Panel, Content);
            //생성된 패널들을 활성화 시키고
            go.gameObject.SetActive(true);
            //이를 리스트에 저장한다.
            items.Add(go);
        }


        int value = 0;
        //현재 보유한 아이템 갯수만큼
        foreach(var item in ItemFlowController.Item_Pairs)
        {
            //해당되는 아이템 패널의 Init 함수를 실행시킨다.(Item_Panel의 Init 함수 호출)
            items[value].Init(item.Value);
            value++;
        }
        //인벤토리 설정
        SetInventory();
	}


    public void SetInventory()
    {
        //해당되는 아이템 패털의 SetItem 함수 호출
        for(int i = 0; i < items.Count; i++)
        {
			items[i].SetItem();

        }
    }
}
