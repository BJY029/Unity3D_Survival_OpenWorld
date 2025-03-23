using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class INVENTORY : UIPART
{
    //아이템이 표시될 패널에 달린 스크립트
    public Item_Panel Item_Panel;
    //해당 UI가 생성될 위치
    public Transform Content;

    //총 무게 값의 슬라이더 부분
    public Image WeightFill;
    //총 무게 값의 텍스트 부분
    public TextMeshProUGUI WeightText;

    //아이템 UI를 저장할 리스트
    List<Item_Panel> items = new List<Item_Panel>();
    
    //아이템 정보들을 저장할 딕셔너리
    Dictionary<int, ITEM> Inventory_Items = new Dictionary<int, ITEM>();

    //최대 아이템 칸을 50개로 설정
    int ItemMaximimValue = 50;

    //아이템 선택을 알려주는 이미지 오브젝트
    public GameObject ItemClickTap;

    //시작과 동시에 초기화
    private void Start()
	{
		Init();
        //델리게이트에 SetItemList 함수와 SetInventory 함수 추가
        //이로써 아이템을 획득하게 될 때 마다, 해당 함수들이 차례로 실행된다.
		ItemFlowController.OnItemGet += SetItemList;
		ItemFlowController.OnItemGet += SetInventory;
	}

    //델리게이트 설정으로 필요없어짐
	//private void OnEnable()
	//{
	//	SetInventory();
 //       SetItemList();
	//}

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

        //각 패널에 아이템을 채워넣는다.
        SetItemList();

		//인벤토리 설정
		SetInventory();
	}

    public void SetItemList()
    {
		int value = 0;
		//현재 보유한 아이템 갯수만큼
		foreach (var item in ItemFlowController.Item_Pairs)
		{
            //만약 현재 보유한 아이템 딕셔너리에, 해당되는 아이템이 없는 경우
            //그리고, 해당 아이템의 parentPanel이 아직 null상태인 경우,
            //즉 아이템이 비어서 Item_Panel의 Init() 함수가 수행되지 않은 경우
            if (Inventory_Items.ContainsKey(item.Value.Data.itemId) == false
                && items[value].parentPanel == null)
            {
                //해당되는 아이템 패널의 Init 함수를 실행시킨다.(Item_Panel의 Init 함수 호출)
                items[value].Init(item.Value, this);
                //그리고 해당 아이템을 딕셔너리에 추가시킨다.
                Inventory_Items.Add(item.Value.Data.itemId, item.Value);
            }
			value++;
		}
	}


    public void SetInventory()
    {
        //해당되는 아이템 패털의 SetItem 함수 호출
        for(int i = 0; i < items.Count; i++)
        {
			items[i].SetItem();
        }

        //현재 무게에서, 플레이어의 기본 무게 값을 나눠서 0~1 사이의 값을 통해 슬라이더 설정
        WeightFill.fillAmount = ItemFlowController.Weight() / ItemFlowController.Player_Weight;
        //무게 값을 담당하는 텍스트 또한 해당 값으로 변경시켜준다.
        WeightText.text = string.Format("{0:0.0}/{1:0.0})", ItemFlowController.Weight(), ItemFlowController.Player_Weight);
    }

    //아이템 선택 이미지를 설정하는 함수
    public void SetItemClickAnimation(Item_Panel panel)
	{
        //해당 이미지 오브젝트를 활성화 시키고,
        ItemClickTap.gameObject.SetActive(true);
        //해당 요소를 활성화 된 panel의 자식으로 위치시킨 다음,
        ItemClickTap.transform.SetParent(panel.transform);
        //해당 위치에서의 localPosition을 0으로 초기화 시킨다.
        ItemClickTap.transform.localPosition = Vector2.zero;
	}
}