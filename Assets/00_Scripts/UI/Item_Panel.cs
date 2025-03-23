using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//IPointerEnterHandler : ���콺 Ŀ���� UI ��ҿ� ���� �� ȣ��Ǵ� �������̽�
//IPointerExitHandler : ���콺 Ŀ���� UI ��ҿ� ���� �� ȣ��Ǵ� �������̽�
public class Item_Panel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //���콺�� �ش� �гο� �ö���� �� ����Ǵ� �������̽� �Լ�
	public void OnPointerEnter(PointerEventData eventData)
	{
        //�� ������Ʈ�� Ŀ���� �ö� ���, init�� ȣ����� �ʾƼ� parentPanel�� null ������
        //���� �� ������Ʈ�� Ŀ���� �ö� ��� �׳� ó������ �ʵ��� �Ѵ�.
        if (parentPanel == null) return;
        //INVENTORY�� �ִ� SetItemClickAnimation() �Լ��� ȣ��
        parentPanel.SetItemClickAnimation(this);
	}

    //���콺�� �г� ������ ������ ����Ǵ� �������̽� �Լ�
	public void OnPointerExit(PointerEventData eventData)
	{
		//�� ������Ʈ�� Ŀ���� �ö� ���, init�� ȣ����� �ʾƼ� parentPanel�� null ������
		//���� �� ������Ʈ�� Ŀ���� �ö� ��� �׳� ó������ �ʵ��� �Ѵ�.
		if (parentPanel == null) return;
		//���� ������ �̹��� ������Ʈ�� �����ִ� ���
		if (parentPanel.ItemClickTap.activeSelf == true)
        {
            //�ش� ������Ʈ�� ��Ȱ��ȭ ��Ų��.
            parentPanel.ItemClickTap.SetActive(false);
        }
	}


	//�� ������ �гο� ���� �ϴ� ������
	public ITEM m_item ;
    public GameObject m_ITEMPANEL;
    public Image Rarity;
    public Image Item_Icon;
    public TextMeshProUGUI ItemCountText;
    public TextMeshProUGUI ItemWeightText;

    //INVENTORY ����
    public INVENTORY parentPanel;

    public void Init(ITEM item, INVENTORY inventory)
    {
        m_item = item;
        //parentPanel �ʱ�ȭ
        parentPanel = inventory;
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
            //�������� ���� ���� �����Ѵ�.(0��° ��ġ�� ����, �Ҽ��� ���ڸ��� ����)
            ItemWeightText.text = string.Format("{0:0.0}",ItemFlowController.WeightItem(m_item.Data.itemId));
		}
        //������
        else
        {
            //�⺻ ��������Ʈ�� ä���.
            Rarity.sprite = Asset_Manager.Get_Atlas("DefaultSquare");
        }
	}
}
