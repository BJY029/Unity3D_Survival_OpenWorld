using UnityEngine;

public class Navigation_Mng : MonoBehaviour
{
	//�̱������� ����
    public static Navigation_Mng Instance;

	private void Awake()
	{
		if(Instance == null) Instance = this;
	}

	//Navgation�� ��ġ�� �����´�.
	[SerializeField] private Transform Content;
	//Navgation �ȿ� ������ ������ UI�� ������ ����
	Nav_Item P_Item;

	private void Start()
	{
		//�ش� UI�� GetComponetInChilderen�� ���� ã�´�.
		P_Item = GetComponentInChildren<Nav_Item>();
		//�ش� UI�� ��Ȱ��ȭ ��Ų��.
		P_Item.gameObject.SetActive(false);
	}

	//������ UI�� �����ϰ� �ʱ�ȭ �ϴ� �Լ�
	public void PanelGet_Item(Item_Scriptable data)
	{
		//P_Item���� �����Ѵ�.
		var go = Instantiate(P_Item, Content);
		//������ ������Ʈ�� Ȱ��ȭ
		go.gameObject.SetActive(true);
		//�� �����ۿ� ���Ե� ��ũ���ͺ� �����ͷ� UI ������� �ʱ�ȭ��Ų��.
		go.Init(data);
	}
}
