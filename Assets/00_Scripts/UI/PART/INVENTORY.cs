using System.Collections.Generic;
using UnityEngine;

public class INVENTORY : UIPART
{
    //�������� ǥ�õ� �гο� �޸� ��ũ��Ʈ
    public Item_Panel Item_Panel;
    //�ش� UI�� ������ ��ġ
    public Transform Content;

    //������ UI�� ������ ����Ʈ
    List<Item_Panel> items = new List<Item_Panel>();
    //�ִ� ������ ĭ�� 50���� ����
    int ItemMaximimValue = 50;

    //���۰� ���ÿ� �ʱ�ȭ
	private void Start()
	{
		Init();
	}

    //�������� ���� ���� ��, �ش� ���� ������ �κ��丮 â�� ������ �� �ֽ�ȭ �ϱ� ����
    //OnEnable()������ �κ��丮 ���� �Լ� ȣ��
	private void OnEnable()
	{
		SetInventory();
	}

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


        int value = 0;
        //���� ������ ������ ������ŭ
        foreach(var item in ItemFlowController.Item_Pairs)
        {
            //�ش�Ǵ� ������ �г��� Init �Լ��� �����Ų��.(Item_Panel�� Init �Լ� ȣ��)
            items[value].Init(item.Value);
            value++;
        }
        //�κ��丮 ����
        SetInventory();
	}


    public void SetInventory()
    {
        //�ش�Ǵ� ������ ������ SetItem �Լ� ȣ��
        for(int i = 0; i < items.Count; i++)
        {
			items[i].SetItem();

        }
    }
}
