using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class P_Movement : MonoBehaviour
{
    [Header("#Movement Settings")]
    public float moveSpeed = 5.0f;
    public float gravity = -9.81f;

	[Space(20f)]
	[Header("#Mouse Rotation")]
	//���̾� ����ũ�� ������ �� �ֵ��� ����(�ַ� ĳ���Ͱ� �ٴڿ� ��� �ִ��� üũ�� �� ���)
	public LayerMask groundLayer;
	//ȸ�� �ӵ� ����
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
		//���� ��ȣ�ۿ� ���̸�,
		if (Finder.OnInteraction)
		{
			//�ƹ� Ű�� ������ ��,
			if (Input.anyKeyDown)
			{
				//��ȣ�ۿ��� ����� �̺�Ʈ�� ȣ���ϴ� ��������Ʈ�� �����Ѵ�.
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
		
		//������ ���� ������ε� Ű�� �ο� ���� ������ ��� ����
		//if (Input.GetKeyDown(KeyCode.W))
		//{

		//}

		//ī�޶� �ٶ󺸴� ����(����)
		Vector3 cameraForward = Camera.main.transform.forward;
		//ī�޶��� ������ ����
		Vector3 cameraRight = Camera.main.transform.right;

		//���� ����(y��) �̵� ����(�̵��� ���󿡼� �̷�������� �Ѵ�.)
		cameraForward.y = 0f;
		cameraRight.y = 0f;

		//����ȭ. ���̸� 1�� �����Ͽ� �̵� �ӵ��� �����ϵ��� ��
		cameraForward.Normalize();
		cameraRight.Normalize();

		//ī�޶� ���� �ִ� ���� ��������, �Է¿� �´� �������� �����̴� ���͸� ���Ѵ�.
		Vector3 moveDirection = cameraRight * horizontal + cameraForward * vertical;

		//ī�޶� ���⿡ ���� ĳ���Ͱ� �����̰� �ȴ�.
		controller.Move(moveDirection * moveSpeed * Time.deltaTime);

		//�� �ش� �ڵ�� ī�޶� ���⿡ ���� �̵� ������ �޶����� �ǰ�,
		//W�� ���� ���·� ī�޶� ������ �����ϸ�, �ش� �������� ĳ���Ͱ� �̵��ϰ� �ȴ�.
		//�ش� ����� 3��Ī, 1��Ī ���� ī�޶� ���� �����̴� �̵��ý����� �����ϱ⿡ ����

		//.magnitude : ������ ����(ũ��)�� ���� �̵� ������ ����
		float currentSpeed = moveDirection.magnitude * moveSpeed;
		animator.SetFloat("a_Speed", currentSpeed);
	}

	void RotateTowardsMouse()
	{
		//���� ���콺 ��ġ(Input.mousePosition)�� �������� ȭ�鿡�� Ray�� �����Ѵ�.
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		//Ray�� groundLayer�� ��Ҵ��� üũ
		//�ش�Ǵ� ray�� �浹 ������ hit�� �����ϸ�, �ش� ray�� ���� �Ÿ����� �˻��Ѵ�.
		//�׸��� LayerMask�� ����Ͽ� groundLayer���� ������ �����ϵ��� �����Ѵ�.
		if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
		{
			//���콺�� ����Ų �ٴ��� ��ǥ�� �����Ѵ�.
			Vector3 targetPosition = hit.point;

			//���� ĳ���� ��ġ���� ���콺 ��ġ������ ���� ���͸� �����Ѵ�.
			Vector3 direction = (targetPosition - transform.position).normalized;
			//y���� 0���� �����Ͽ� ĳ���Ͱ� ����鿡���� ȸ���ϵ��� ����
			direction.y = 0f;

			//���� ���� ���Ͱ� 0�� �ƴ϶��(���콺�� ���� �ٶ󺸴� ��ġ�� ���� ������)
			if (direction != Vector3.zero)
			{
				//���콺 ������ �ٶ󺸴� ȸ�� ��(Quaternion) ����
				Quaternion targetRotation = Quaternion.LookRotation(direction);
				//���� ȸ�� ������ ��ǥ ȸ�������� �ε巴�� ȸ���Ѵ�.
				transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
			}
		}
	}
}
