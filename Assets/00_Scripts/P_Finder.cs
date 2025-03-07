using System.Collections.Generic;
using UnityEngine;

public class P_Finder : MonoBehaviour
{
	//플레이어를 중심으로 해당 반경 안에 있는 오브젝트를 탐색한다.
	[SerializeField] private float checkRadius = 5.0f;
	//어떤 Layer에 있는 오브젝트만 탐색할지 정하는 필터링 값이다.
	[SerializeField] private LayerMask interactableLayer;
	//아이콘을 띄울 UI 캔버스를 지정하는 변수
	[SerializeField] Canvas uiCanvas;
	//표시할 아이콘 프리팹
	[SerializeField] private GameObject IconPrefab;
	//아이콘을 띄우는 거리로, 탐지 반경 내에서 해당 거리만큼 오브젝트가 가까이 있어야 아이콘을 활성화 한다.
	[SerializeField] private float activationDistance = 3.0f;
	
	//어떤 오브젝트에 어떤 아이콘이 붙어있는지 관리하는 딕셔너리
	//key = 탐지된 오브젝트의 Transform, value = 해당 오브젝트에 해당하는 아이콘 GameObject
	private Dictionary<Transform, GameObject> activeIcons = new Dictionary<Transform, GameObject>();
	//가장 가까운 오브젝트를 저장하는 변수
	Transform closetObject = null;
	//현재 오브젝트와 상호작용 중인지 확인하는 불형 변수(인스펙터 창에서는 보여주지 않는다.)
	[HideInInspector]public bool OnInteraction = false;

	private void Start()
	{
		//OnInteracation 델리게이트에(상호작용 시작시 발생되는 이벤트 모음)
		//해당 함수는 OnInteraction 값을 true로 설정
		Delegate_Holder.OnInteraction += OnInteractionVoid;
		//OnInteractionOut 델리게이트에(상호작용 종료시 발생되는 이벤트 모음)
		//해당 함수는 OnInteraction 값을 false로 설정
		Delegate_Holder.OnInteractionOut += OnInteractionOut;
	}

	//상호작용이 활성화 되면 실행되는 함수
	void OnInteractionVoid()
	{
		//해당 플래그를 true로 만들어서 더 이상 아이콘을 띄우지 않도록 한다.
		OnInteraction = true;
		//현재 가장 가까운 오브젝트 또한 초기화한다.
		closetObject = null;
		//현재 띄워진 아이콘을 삭제한다.
		IconInit();
	}

	//해당 함수는 다시 상호작용 여부를 false로 해서
	//다시 collider 탐색을 시작하도록 한다.
	void OnInteractionOut()
	{
		//OnInteraction을 1초후에 false로 해서, F키가 연속적으로 눌리는 경우를 방지
		Invoke("InteractionFalse", 1.0f);
		activeIcons.Clear();
	}

	void InteractionFalse() => OnInteraction = false;

	private void Update()
	{
		//만약 오브젝트와 상호작용을 진행한 경우, 더 이상 콜라이더 추적을 진행하지 않고, 'F'Key 아이콘을 띄우지 않는다.
		if (OnInteraction) return;

		//매 프레임마다 플레이어 주변 checkRadius 반경 내의 interactiveLayer에 해당하는 오브젝트를 탐지 한다.
		//Physcis.OverlapSphere는 구 형태의 범위 안에 있는 콜라이더를 전부 찾아서 반환하는 함수다.
			//매개 변수는 차례대로 구의 중심 위치, 구의 반경, 특정 레이어 필터 이다.
			//3D 물리 콜라이더만 감지하며, isTrigger인 콜라이더도 감지한다.
			//반환 값은 Collider다.
		Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, checkRadius, interactableLayer);

		//가장 가까운 오브젝트를 저장할 변수
		closetObject = null;
		//가장 가까운 오브젝트와의 거리 값을 저장할 변수
		float closetDistance = Mathf.Infinity;


		//탐지된 오브젝트마다 반복하면서 아래 작업을 수행한다.
		foreach(Collider obj in nearbyObjects)
		{
			//탐지된 오브젝트의 transform 값을 저장
			Transform targetTransform = obj.transform;

			//현재 플레어이와 해당 오브젝트의 거리를 계산한다.
			float distance = Vector3.Distance(transform.position, targetTransform.position);

			//만약 오브젝트가 플레이어로부터 활성 거리 내에 있고, 플레이어로부터 가장 가까우면
			if(distance <= activationDistance && distance < closetDistance)
			{
				//해당 오브젝트를 가장 가까운 오브젝트로 선정
				closetObject = targetTransform;
				//최소 거리 값도 수장
				closetDistance = activationDistance;
			}
		}

		//플레이어 범위 내에 나무가 존재하는 경우(가장 가까운 나무만)
		if(closetObject != null)
		{
			//해당 오브젝트 위치에 아이콘을 표시한다.
			ShowIcon(closetObject);

			//F키가 눌리면
			if (Input.GetKeyDown(KeyCode.F))
			{
				//델리게이트를 Invoke 하는 함수 호출
				Delegate_Holder.OnStartInteraction();
			}
		}

		//아이콘을 삭제하는 함수
		IconInit();
	}


	private void IconInit()
	{
		//해당 프레임에서 사라진 아이템들을 저장하는 리스트
		List<Transform> toRemove = new List<Transform>();
		//기존에 표시되던 아이템들을 순회한다.
		foreach (var iconEntry in activeIcons)
		{
			//만약 해당 아이콘이 현재 가장 가까운 아이콘과 다른 경우
			if (iconEntry.Key != closetObject)
			{
				//해당 오브젝트에 적용된 스크립트의 AnimationChage 함수를 호출한다.
				iconEntry.Value.GetComponent<UI_Animation_Handler>().AnimationChange("Out");
				//해당 오브젝트를 리스트에 추가해서 딕셔너리에서 제거할 준비를 한다.
				toRemove.Add(iconEntry.Key);
			}
		}

		//삭제 리스트에 올라간 오브젝트를 딕셔너리에서 제거한다.
		foreach (var transformToRemove in toRemove) activeIcons.Remove(transformToRemove);
	}

	//아이콘을 표시하는 함수
	private void ShowIcon(Transform targetTransform)
	{
		//이미 표시된 아이콘의 경우
		if (activeIcons.ContainsKey(targetTransform))
		{
			//해당 아이콘의 위치만 업데이트 한다.
			UpdateIconPosition(targetTransform, activeIcons[targetTransform]);
			return;
		}

		//새로운 아이콘 필요 시 프리팹으로 새로 생성한다.
		//Instantiate()는 프리팹, 오브젝트 등을 인스턴스화(복제) 할 때 쓰는 함수다.
			//해당 함수의 매개 변수는 차례대로 (원본 프리팹, 위치, 회전각도) 이다.
			//아래 함수는 IconPrefab를 uiCanvas의 자식에 위치시켜서 복제한다.
		GameObject iconInstance = Instantiate(IconPrefab, uiCanvas.transform);
		//생성된 새 프리펩 아이콘을 딕셔너리에 등록한다.
		activeIcons[targetTransform] = iconInstance;
		//해당 아이콘의 위치를 업데이트 한다.
		UpdateIconPosition(targetTransform, iconInstance);
	}

	//아이콘의 위치를 업데이트 하는 함수
	private void UpdateIconPosition(Transform targetTransform, GameObject Icon)
	{
		//Canvas는 React Transform을 사용하기 때문에, 아이콘이 뜨는 Transform을 React Transform으로 변경시켜줘야 한다.
		//월드 좌표(3D 오브젝트 위치)를 화면 좌표로 변환한다.
		Vector3 screenPosition = Camera.main.WorldToScreenPoint(
			new Vector3(
				targetTransform.position.x,
				targetTransform.position.y + 1.5f,
				targetTransform.position.z
				));
		
		//그 다음 실제 UI 아이콘의 위치를 화면 좌표로 업데이트한다.
		Icon.GetComponent<RectTransform>().position = screenPosition;
	}
}
