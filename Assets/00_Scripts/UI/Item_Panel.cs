using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item_Panel : MonoBehaviour
{
    //�� ������ �гο� ���� �ϴ� ������
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

    //�ش� �г��� �����ϴ� �Լ�
    public void SetItem()
    {
        //�ش�Ǵ� �������� ����Ÿ ���ο� ���� Ȱ��ȭ ���� ����
        m_ITEMPANEL.gameObject.SetActive(m_item.Data == null ? false : true);
        //���� �ش�Ǵ� ������ �����ϴ� ���
		if (m_item.Data != null)
		{
            //���� ��������Ʈ���� �˸°� ä���.
			Rarity.sprite = Asset_Manager.Get_Atlas(m_item.Data.rarity.ToString());
			Item_Icon.sprite = Asset_Manager.Get_Atlas(m_item.Data.itemId.ToString());
			ItemCountText.text = m_item.Count.ToString();
		}
        //������
        else
        {
            //�⺻ ��������Ʈ�� ä���.
            Rarity.sprite = Asset_Manager.Get_Atlas("DefaultSquare");
        }
	}
}
