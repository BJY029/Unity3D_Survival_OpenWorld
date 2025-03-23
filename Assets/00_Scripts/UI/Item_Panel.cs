using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//IPointerEnterHandler : 마우스 커서가 UI 요소에 들어올 때 호출되는 인터페이스
//IPointerExitHandler : 마우스 커서가 UI 요소에 나갈 때 호출되는 인터페이스
public class Item_Panel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //마우스가 해당 패널에 올라왔을 때 실행되는 인터페이스 함수
	public void OnPointerEnter(PointerEventData eventData)
	{
        //빈 오브젝트에 커서가 올라간 경우, init이 호출되지 않아서 parentPanel이 null 상태임
        //따라서 빈 오브젝트에 커서가 올라간 경우 그냥 처리되지 않도록 한다.
        if (parentPanel == null) return;
        //INVENTORY에 있는 SetItemClickAnimation() 함수를 호출
        parentPanel.SetItemClickAnimation(this);
	}

    //마우스가 패널 밖으로 나가면 실행되는 인터페이스 함수
	public void OnPointerExit(PointerEventData eventData)
	{
		//빈 오브젝트에 커서가 올라간 경우, init이 호출되지 않아서 parentPanel이 null 상태임
		//따라서 빈 오브젝트에 커서가 올라간 경우 그냥 처리되지 않도록 한다.
		if (parentPanel == null) return;
		//현재 강조용 이미지 오브젝트가 켜져있는 경우
		if (parentPanel.ItemClickTap.activeSelf == true)
        {
            //해당 오브젝트를 비활성화 시킨다.
            parentPanel.ItemClickTap.SetActive(false);
        }
	}


	//각 아이템 패널에 들어가야 하는 정보들
	public ITEM m_item ;
    public GameObject m_ITEMPANEL;
    public Image Rarity;
    public Image Item_Icon;
    public TextMeshProUGUI ItemCountText;
    public TextMeshProUGUI ItemWeightText;

    //INVENTORY 선언
    public INVENTORY parentPanel;

    public void Init(ITEM item, INVENTORY inventory)
    {
        m_item = item;
        //parentPanel 초기화
        parentPanel = inventory;
    }

    //해당 패널을 설정하는 함수
    public void SetItem()
    {
        //해당되는 아이템의 데이타 여부에 따라 활성화 여부 결정
        m_ITEMPANEL.gameObject.SetActive(m_item.Data == null ? false : true);
        //만약 해당되는 정보가 존재하는 경우
		if (m_item.Data != null)
		{
            //각종 스프라이트들을 알맞게 채운다.
			Rarity.sprite = Asset_Manager.Get_Atlas(m_item.Data.rarity.ToString());
			Item_Icon.sprite = Asset_Manager.Get_Atlas(m_item.Data.itemId.ToString());
			ItemCountText.text = m_item.Count.ToString();
            //아이템의 무게 값을 설정한다.(0번째 위치의 값을, 소수점 한자리로 포맷)
            ItemWeightText.text = string.Format("{0:0.0}",ItemFlowController.WeightItem(m_item.Data.itemId));
		}
        //없으면
        else
        {
            //기본 스프라이트로 채운다.
            Rarity.sprite = Asset_Manager.Get_Atlas("DefaultSquare");
        }
	}
}
