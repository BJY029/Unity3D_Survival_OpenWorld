using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Holder : MonoBehaviour
{
	public static Canvas_Holder instance = null;

    [SerializeField] private GameObject Board;
	[SerializeField] private Transform UI_PART_PARENT;
	//체력바의 빨간 부분과 흰색 부분을 가져온다.
	public Image BoardHpFill, BoardHpWhiteFill;
	Coroutine F_Courtine;

	public void StopAllCoroutine() => StopAllCoroutines();



	private void Awake()
	{
		if(instance == null) instance = this;
	}

	//UI요소들을 저장하는 딕셔너리
	private Dictionary<string, UIPART> uiPart = new Dictionary<string, UIPART>();

	//해당되는 UI가 존재하면, UIPART의 Open() 함수를 통해 해당 UI 활성화
	public void OpenUI(string uiName)
	{
		if (uiPart.ContainsKey(uiName))
		{
			uiPart[uiName].Open();
		}
		else Debug.LogWarning($"UI {uiName} not found.");
	}

	//해당되는 UI가 존재하면, UIPART의 Close() 함수를 통해 해당 UI 비활성화
	public void CloseUI(string uiName)
	{
		if (uiPart.ContainsKey(uiName))
		{
			uiPart[uiName].Close();
		}
	}

	//모든 UI 비활성화
	public void CloseAllUI()
	{
		foreach (var part in uiPart.Values)
		{
			part.Close();
		}
	}

	private void Start()
	{
		//해당되는 오브젝트 자식들을 배열로 받아온다.(true 설정은 비활성화 된 오브젝트도 찾는 설정)
		UIPART[] parts = UI_PART_PARENT.GetComponentsInChildren<UIPART>(true);
		//하나씩 딕셔너리에 삽입한다.
		foreach(var part in parts)
		{
			uiPart.Add(part.name, part);
		}
		Debug.Log(uiPart.Keys);

		//OnInteraction 델리게리트(상호작용 시작)에 GetBoard 이벤트 추가
		Delegate_Holder.OnInteraction += GetBoard;
		//OnInteractionOut 델리게이트(상호작용 종료)에 BoardOut 이벤트 추가
		Delegate_Holder.OnInteractionOut += BoardOut;
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Tab))
		{
			//강의에서는 해당 버튼을 'I'로 설정
			//INVENTORY에 해당되는 uiPart를 켜거나 끈다.
			uiPart["INVENTORY"].Toggle();
		}
	}

	public void GetBoard()
    {
        Board.SetActive(true);
    }

	//체력 바가 사라지는 애니메이션 수행
	public void BoardOut() => Board.GetComponent<UI_Animation_Handler>().AnimationChange("Out");


	public void BoardFill(float hp, float MaxHp)
	{
		//빨간 색의 체력을 먼저 최신화 한다.
		BoardHpFill.fillAmount = hp / MaxHp;
		//이전에 실행 중이던 코루틴이 있다면 정지(중복 실행 방지)
		if(F_Courtine != null)
		{
			StopCoroutine(F_Courtine);
		}
		//천천히 줄어드는 흰색 체력바를 갱신하는 코루틴 시작
		F_Courtine = StartCoroutine(FillCoroutine());
	}

	//흰색 체력바를 천천히 업데이트 하는 코루틴
	IEnumerator FillCoroutine()
	{
		//흰색 부분의 체력 값이 빨간 부분 체력 값보다 클 때까지
		while (BoardHpWhiteFill.fillAmount > BoardHpFill.fillAmount)
		{
			//Lerp로 부드럽게 체력 감소(기존 프레임 속도보다 2배 빠른 속도로 감소)
			BoardHpWhiteFill.fillAmount = Mathf.Lerp(BoardHpWhiteFill.fillAmount,
				BoardHpFill.fillAmount, Time.deltaTime * 2.0f);

			yield return null;
		}
	}
}
