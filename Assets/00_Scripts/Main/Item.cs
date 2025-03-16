using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private float spreadRadius = 10.0f; //퍼지는 반경
    [SerializeField] private float arcHeight = 5.0f; // 포물선 높이
    [SerializeField] private float moveSpeed = 5.0f; //아이템 이동 속도
	[SerializeField] private GameObject GetParticle;

	//아이템이 따라갈 플레이어 객체
    Transform Player;

	//아이템의 정보를 담고 있는 스크립터블 오브젝트를 저장할 변수
	ITEM m_Item;
	//아이템이 드롭되는 갯수를 저장할 변수
	int CountValue;

	//아이템의 정보를 받는 함수
	public void Init(ITEM item)
	{
		m_Item = item;
	}

	private void Start()
	{
		Player = P_Movement.instance.transform;
		StartCoroutine(SpreadAndMoveToPlayer());
	}

	//트레일의 퍼지는 동작을 구현하는 코루틴
	IEnumerator SpreadAndMoveToPlayer()
	{
		//아이템이 spreadRadius 범위 내에서 랜덤한 방향으로 퍼지게 한다.
		//insideUnitSphere는 반지름이 1인 구 내부의 무작위 점을 반환한다.
		Vector3 spreadDirection = Random.insideUnitSphere * spreadRadius;
		//아이템이 이동할 목표 위치 설정
		Vector3 spreadPosition = transform.position + spreadDirection;

		//목표 위치의 y 좌표가 1보다 작아지지 않게 설정(spreadPos.y와 1.0f 중 큰 값으로 선정)
		spreadPosition.y = Mathf.Max(spreadPosition.y, arcHeight);

		//퍼지는 시간
		float spreadTime = 0.3f;
		float elapsedTime = 0.0f;
		//트레일 시작 위치
		Vector3 startPosition = transform.position;
		
		//목표 위치까지 Lerp를 통해 부드럽게 이동
		while(elapsedTime < spreadTime)
		{
			elapsedTime += Time.deltaTime;
			float t = elapsedTime / spreadTime;
			transform.position = Vector3.Lerp(startPosition, spreadPosition, t);
			yield return null;
		}

		//트레일 퍼지기가 완료된 후, 플레이어로 향해 이동하는 코루틴 실행
		StartCoroutine(MoveToPlayer(spreadPosition));
	}

	IEnumerator MoveToPlayer(Vector3 startPosition)
	{
		float journeyTime;
		float elapsedTime;
		Vector3 endPosition;

		while (true) //무한 루프 조심
		{
			//종료 위치는 플레이어의 위치
			endPosition = Player.position + new Vector3(0.0f, 1.0f, 0.0f);
			//이동 시간은 시작 위치에서 도착 위치까지의 거리를 이동 속도로 나눈 값(시간 = 거리 / 속도)
			journeyTime = Vector3.Distance(startPosition, endPosition) / moveSpeed;
			elapsedTime = 0.0f;

			while(elapsedTime < journeyTime)
			{
				elapsedTime += Time.deltaTime;
				float t = elapsedTime / journeyTime;

				//Mathf.Sin(Mathf.PI * t)는 t = 0에서 t = 1까지 0 -> 1 -> 0으로 변화
				//이를 이용하여 arcHeight 만큼의 높이를 추가하여 포물선 궤적을 만든다.
				//float height = Mathf.Sin(Mathf.PI * t) * arcHeight;
				//트레일 위치를 Lerp로 부드럽게 이동
				Vector3 currentPos = Vector3.Lerp(startPosition, endPosition, t);
				//해당 트레일 위치의 높이 값에 아까 구한 포물선 높이 값 적용
				//currentPos.y += height;

				//트레일 위치 재초기화
				transform.position = currentPos;
				//이동 중에도 플레이어가 움직이면 목표 위치 갱신
				endPosition = Player.position + new Vector3(0.0f, 1.0f, 0.0f);

				yield return null;
			}

			//플레이어와 0.5 이내로 가까워지면 반복문을 종료
			if (Vector3.Distance(transform.position, Player.position + new Vector3(0.0f, 1.0f, 0.0f)) < 0.5f) break;
			
			//0.5 이내로 가까워지지 않은 경우 다시 반복문 실행하기 위해 시작 위치 초기화
			startPosition = transform.position; 
		}
		//아이템 획득 시, 파티클 재생
		Instantiate(GetParticle, transform.position, Quaternion.identity);

		Navigation_Mng.Instance.PanelGet_Item(m_Item.Data);

		//인벤토리에 아이템을 삽입하고 갯수 또한 추가한다.
		ItemFlowController.GETITEM(m_Item.Data, m_Item.Count);

		Destroy(this.gameObject);
	}
}
