using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class INVENTORY : UIPART
{
    //�������� ǥ�õ� �гο� �޸� ��ũ��Ʈ
    public Item_Panel Item_Panel;
    //�ش� UI�� ������ ��ġ
    public Transform Content;

    //�� ���� ���� �����̴� �κ�
    public Image WeightFill;
    //�� ���� ���� �ؽ�Ʈ �κ�
    public TextMeshProUGUI WeightText;

    //������ UI�� ������ ����Ʈ
    List<Item_Panel> items = new List<Item_Panel>();
    
    //������ �������� ������ ��ųʸ�
    Dictionary<int, ITEM> Inventory_Items = new Dictionary<int, ITEM>();

    //�ִ� ������ ĭ�� 50���� ����
    int ItemMaximimValue = 50;

    //������ ������ �˷��ִ� �̹��� ������Ʈ
    public GameObject ItemClickTap;

    //���۰� ���ÿ� �ʱ�ȭ
    private void Start()
	{
		Init();
        //��������Ʈ�� SetItemList �Լ��� SetInventory �Լ� �߰�
        //�̷ν� �������� ȹ���ϰ� �� �� ����, �ش� �Լ����� ���ʷ� ����ȴ�.
		ItemFlowController.OnItemGet += SetItemList;
		ItemFlowController.OnItemGet += SetInventory;
	}

    //��������Ʈ �������� �ʿ������
	//private void OnEnable()
	//{
	//	SetInventory();
 //       SetItemList();
	//}

    //������ ����Ʈ�� �ʱ�ȭ �ϴ� �Լ�
	public void Init()
    {
        //���� ���� ������ �������� 50�� �̻��� �Ǹ�
        if(ItemFlowController.Item_Pairs.Count >= ItemMaximimValue)
            //�ִ� ������ ������ �ֽ�ȭ�Ѵ�.
            ItemMaximimValue = ItemFlowController.Item_Pairs.Count;

        //50���� �г��� �����Ѵ�.
        for(int i = 0;i < ItemMaximimValue; i++)
        {
            //������ �г��� �ش� ��ġ�� �����Ѵ�.(50�� ����)
            var go = Instantiate(Item_Panel, Content);
            //������ �гε��� Ȱ��ȭ ��Ű��
            go.gameObject.SetActive(true);
            //�̸� ����Ʈ�� �����Ѵ�.
            items.Add(go);
        }

        //�� �гο� �������� ä���ִ´�.
        SetItemList();

		//�κ��丮 ����
		SetInventory();
	}

    public void SetItemList()
    {
		int value = 0;
		//���� ������ ������ ������ŭ
		foreach (var item in ItemFlowController.Item_Pairs)
		{
            //���� ���� ������ ������ ��ųʸ���, �ش�Ǵ� �������� ���� ���
            //�׸���, �ش� �������� parentPanel�� ���� null������ ���,
            //�� �������� �� Item_Panel�� Init() �Լ��� ������� ���� ���
            if (Inventory_Items.ContainsKey(item.Value.Data.itemId) == false
                && items[value].parentPanel == null)
            {
                //�ش�Ǵ� ������ �г��� Init �Լ��� �����Ų��.(Item_Panel�� Init �Լ� ȣ��)
                items[value].Init(item.Value, this);
                //�׸��� �ش� �������� ��ųʸ��� �߰���Ų��.
                Inventory_Items.Add(item.Value.Data.itemId, item.Value);
            }
			value++;
		}
	}


    public void SetInventory()
    {
        //�ش�Ǵ� ������ ������ SetItem �Լ� ȣ��
        for(int i = 0; i < items.Count; i++)
        {
			items[i].SetItem();
        }

        //���� ���Կ���, �÷��̾��� �⺻ ���� ���� ������ 0~1 ������ ���� ���� �����̴� ����
        WeightFill.fillAmount = ItemFlowController.Weight() / ItemFlowController.Player_Weight;
        //���� ���� ����ϴ� �ؽ�Ʈ ���� �ش� ������ ��������ش�.
        WeightText.text = string.Format("{0:0.0}/{1:0.0})", ItemFlowController.Weight(), ItemFlowController.Player_Weight);
    }

    //������ ���� �̹����� �����ϴ� �Լ�
    public void SetItemClickAnimation(Item_Panel panel)
	{
        //�ش� �̹��� ������Ʈ�� Ȱ��ȭ ��Ű��,
        ItemClickTap.gameObject.SetActive(true);
        //�ش� ��Ҹ� Ȱ��ȭ �� panel�� �ڽ����� ��ġ��Ų ����,
        ItemClickTap.transform.SetParent(panel.transform);
        //�ش� ��ġ������ localPosition�� 0���� �ʱ�ȭ ��Ų��.
        ItemClickTap.transform.localPosition = Vector2.zero;
	}
}