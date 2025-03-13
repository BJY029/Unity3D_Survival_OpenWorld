using System.Collections;
using UnityEngine;

public class Interaction_Hit : M_Object
{
    float shakeAmount = 5.0f;
    float shakeDuration = 0.5f;

    private Quaternion originalRotation;

	private void Start()
	{
		originalRotation = transform.rotation;
		HP = m_Data.HP;
	}

	public override void Interaction()
    {
        //P_Movement의 AnimationChage 함수 호출,
        //인자는 스크립트 오브젝트 m_Data의 해당 오브젝트 Type를 문자열로 넘긴다.
        P_Movement.instance.AnimationChange(m_Data.m_Type.ToString());
        base.Interaction();
    }

	public override void OnHit()
	{
		base.OnHit();
		//ShakeTree 함수 호출
		//(인자로 공격 방향을 불러오기 위해서, 오브젝트 위치에서 플레이어 위치를 뺀 방향 벡터 값을 넘긴다.)
		ShakeTree(transform.position - P_Movement.instance.transform.position);

		if(HP <= 0) //오브젝트의 체력이 0보다 같거나 작으면
		{
			//해당 오브젝트로부터 드롭되는 아이템들을 받아온다.
			var items = ItemFlowController.DROPITEMLIST(m_Data.Drop_Items);

			//드롭된 아이템만큼의 트레일 프리펩을 생성시킨다.
			for(int i = 0; i < items.Count; i++)
			{
				var go = Instantiate(Item_Prefab, transform.position, Quaternion.identity);
				//드롭될 아이템 리스트를 Item 스크립트의 Init 함수로 전달한다.
				go.Init(items[i]);
			}
		}
	}

	private void ShakeTree(Vector3 attackDirection)
	{
		//넘겨받은 방향 벡터를 정규화해서 크기를 1로 만든다.(방향 값만 필요)
		Vector3 oppositeDirection = attackDirection.normalized;

		//transform.rotation vs. transform.eulerAngles
		//transform.rotation : 회전 값을 Quaternion 으로 가져온다.
		//transform.eulerAngles : 회전 값을 Vector3로 가져온다.

		//공격이 들어온 방향의 반대 방향으로 나무가 움직이는 것이 자연스럽기 때문에,
		//공격 방향의 Z값을 X축 회전에 적용하고, 공격 방향의 X값을 Z축 회전에 적용한다.
		//이때 X축 회전은 오브젝트의 상하 회전을 담당, Z축 회전은 좌우 회전을 담당한다.
		Quaternion targetRotation = Quaternion.Euler(
			originalRotation.eulerAngles.x + oppositeDirection.z * shakeAmount,
			originalRotation.eulerAngles.y,
			originalRotation.eulerAngles.z - oppositeDirection.x * shakeAmount
			);

		//기존 실행 중이던 코루틴들을 멈추고
		StopAllCoroutines();
		//흔들림 애니메이션을 시작할 코루틴을 활성화
		StartCoroutine(ShakeAnimation(targetRotation));	
	}


	//해당 코루틴은 객체를 목표 회전값 까지 서서히 흔들었다가 원래 상태로 돌아오게 만드는 코루틴
	private IEnumerator ShakeAnimation(Quaternion targetRotation)
	{
		float elapsedTime = 0.0f;

		//흔들림 시간의 절반을 목표 회전치 값 만큼 회전하는데 사용
		while (elapsedTime < shakeDuration / 2) 
		{
			//slerp를 통해 originalRotation 값에서 targetRotation 값으로 
			// elapsedTime / (shakeDuration / 2) 비율만큼 부드럽게 보간
			transform.rotation = Quaternion.Slerp(originalRotation, targetRotation, elapsedTime / (shakeDuration / 2));
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		elapsedTime = 0.0f;
		//나머지 흔들림 시간의 절반을 본래 회전 값으로 복귀하는 데 사용
		while(elapsedTime < shakeDuration / 2)
		{
			//slerp를 통해 targetRotation 값에서 originalRotation 값으로 
			// elapsedTime / (shakeDuration / 2) 비율만큼 부드럽게 보간
			transform.rotation = Quaternion.Slerp(targetRotation, originalRotation, elapsedTime / (shakeDuration / 2));
			elapsedTime += Time.deltaTime;
			yield return null;
		}
	}
}
