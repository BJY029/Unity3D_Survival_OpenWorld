using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item_Panel : MonoBehaviour
{
    //각 아이템 패널에 들어가야 하는 정보들
    public ITEM m_item ;
    public GameObject m_ITEMPANEL;
    public Image Rarity;
    public Image Item_Icon;
    public TextMeshProUGUI ItemCountText;
    public TextMeshProUGUI ItemWeightText;

    public void Init(ITEM item)
    {
        m_item = item;
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
		}
        //없으면
        else
        {
            //기본 스프라이트로 채운다.
            Rarity.sprite = Asset_Manager.Get_Atlas("DefaultSquare");
        }
	}
}
