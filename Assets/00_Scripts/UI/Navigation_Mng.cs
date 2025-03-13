using UnityEngine;

public class Navigation_Mng : MonoBehaviour
{
	//싱글톤으로 설정
    public static Navigation_Mng Instance;

	private void Awake()
	{
		if(Instance == null) Instance = this;
	}

	//Navgation의 위치를 가져온다.
	[SerializeField] private Transform Content;
	//Navgation 안에 생성될 아이템 UI를 저장할 변수
	Nav_Item P_Item;

	private void Start()
	{
		//해당 UI를 GetComponetInChilderen을 통해 찾는다.
		P_Item = GetComponentInChildren<Nav_Item>();
		//해당 UI를 비활성화 시킨다.
		P_Item.gameObject.SetActive(false);
	}

	//아이템 UI를 생성하고 초기화 하는 함수
	public void PanelGet_Item(Item_Scriptable data)
	{
		//P_Item으로 생성한다.
		var go = Instantiate(P_Item, Content);
		//생성된 오브젝트를 활성화
		go.gameObject.SetActive(true);
		//각 아이템에 포함된 스크립터블 데이터로 UI 내용들을 초기화시킨다.
		go.Init(data);
	}
}
