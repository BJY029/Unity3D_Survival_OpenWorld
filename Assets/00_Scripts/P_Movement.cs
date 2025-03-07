using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class P_Movement : MonoBehaviour
{
    [Header("#Movement Settings")]
    public float moveSpeed = 5.0f;
    public float gravity = -9.81f;

	[Space(20f)]
	[Header("#Mouse Rotation")]
	//레이어 마스크를 선택할 수 있도록 선언(주로 캐릭터가 바닥에 닿아 있는지 체크할 때 사용)
	public LayerMask groundLayer;
	//회전 속도 정의
	public float rotationSpeed = 10.0f;


    private CharacterController controller;
	private Animator animator;
	private P_Finder Finder;
	

	private void Start()
	{
		controller = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
		Finder = GetComponent<P_Finder>();
	}

	private void Update()
	{
		//현재 상호작용 중이면,
		if (Finder.OnInteraction)
		{
			//아무 키나 눌렸을 때,
			if (Input.anyKeyDown)
			{
				//상호작용을 벗어나는 이벤트를 호출하는 델리게이트를 실행한다.
				Delegate_Holder.OnOutInteraction();
			}
			return;
		}

		Move();
		RotateTowardsMouse();
	}

	void Move()
	{
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		
		//다음과 같은 방식으로도 키값 부여 가능 하지만 사용 안함
		//if (Input.GetKeyDown(KeyCode.W))
		//{

		//}

		//카메라가 바라보는 방향(전방)
		Vector3 cameraForward = Camera.main.transform.forward;
		//카메라의 오른쪽 방향
		Vector3 cameraRight = Camera.main.transform.right;

		//수직 방향(y축) 이동 제거(이동이 평면상에서 이루어지도록 한다.)
		cameraForward.y = 0f;
		cameraRight.y = 0f;

		//정규화. 길이를 1로 조정하여 이동 속도가 균일하도록 함
		cameraForward.Normalize();
		cameraRight.Normalize();

		//카메라가 보고 있는 방향 기준으로, 입력에 맞는 방향으로 움직이는 벡터를 구한다.
		Vector3 moveDirection = cameraRight * horizontal + cameraForward * vertical;

		//카메라 방향에 따라 캐릭터가 움직이게 된다.
		controller.Move(moveDirection * moveSpeed * Time.deltaTime);

		//즉 해당 코드는 카메라 방향에 따라 이동 방향이 달라지게 되고,
		//W를 누른 상태로 카메라 방향을 변경하면, 해당 방향으로 캐릭터가 이동하게 된다.
		//해당 방식은 3인칭, 1인칭 같은 카메라 따라 움직이는 이동시스템을 구현하기에 적합

		//.magnitude : 벡터의 길이(크기)로 현재 이동 방향의 강도
		float currentSpeed = moveDirection.magnitude * moveSpeed;
		animator.SetFloat("a_Speed", currentSpeed);
	}

	void RotateTowardsMouse()
	{
		//현재 마우스 위치(Input.mousePosition)를 기준으로 화면에서 Ray를 생성한다.
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		//Ray가 groundLayer에 닿았는지 체크
		//해당되는 ray의 충돌 정보를 hit에 저장하며, 해당 ray는 무한 거리까지 검사한다.
		//그리고 LayerMask를 사용하여 groundLayer에만 광선이 반응하도록 설정한다.
		if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
		{
			//마우스가 가리킨 바닥의 좌표를 저장한다.
			Vector3 targetPosition = hit.point;

			//현재 캐릭터 위치에서 마우스 위치까지의 방향 벡터를 저장한다.
			Vector3 direction = (targetPosition - transform.position).normalized;
			//y축을 0으로 설정하여 캐릭터가 수평면에서만 회전하도록 설정
			direction.y = 0f;

			//만약 방향 벡터가 0이 아니라면(마우스가 현재 바라보는 위치와 같지 않으면)
			if (direction != Vector3.zero)
			{
				//마우스 방향을 바라보는 회전 값(Quaternion) 생성
				Quaternion targetRotation = Quaternion.LookRotation(direction);
				//현재 회전 값에서 목표 회전값으로 부드럽게 회전한다.
				transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
			}
		}
	}
}
