using System.Collections.Generic;
using UnityEngine;

public class P_Finder : MonoBehaviour
{
	//�÷��̾ �߽����� �ش� �ݰ� �ȿ� �ִ� ������Ʈ�� Ž���Ѵ�.
	[SerializeField] private float checkRadius = 5.0f;
	//� Layer�� �ִ� ������Ʈ�� Ž������ ���ϴ� ���͸� ���̴�.
	[SerializeField] private LayerMask interactableLayer;
	//�������� ��� UI ĵ������ �����ϴ� ����
	[SerializeField] Canvas uiCanvas;
	//ǥ���� ������ ������
	[SerializeField] private GameObject IconPrefab;
	//�������� ���� �Ÿ���, Ž�� �ݰ� ������ �ش� �Ÿ���ŭ ������Ʈ�� ������ �־�� �������� Ȱ��ȭ �Ѵ�.
	[SerializeField] private float activationDistance = 3.0f;
	
	//� ������Ʈ�� � �������� �پ��ִ��� �����ϴ� ��ųʸ�
	//key = Ž���� ������Ʈ�� Transform, value = �ش� ������Ʈ�� �ش��ϴ� ������ GameObject
	private Dictionary<Transform, GameObject> activeIcons = new Dictionary<Transform, GameObject>();
	//���� ����� ������Ʈ�� �����ϴ� ����
	Transform closetObject = null;
	//���� ������Ʈ�� ��ȣ�ۿ� ������ Ȯ���ϴ� ���� ����(�ν����� â������ �������� �ʴ´�.)
	[HideInInspector]public bool OnInteraction = false;

	private void Start()
	{
		//OnInteracation ��������Ʈ��(��ȣ�ۿ� ���۽� �߻��Ǵ� �̺�Ʈ ����)
		//�ش� �Լ��� OnInteraction ���� true�� ����
		Delegate_Holder.OnInteraction += OnInteractionVoid;
		//OnInteractionOut ��������Ʈ��(��ȣ�ۿ� ����� �߻��Ǵ� �̺�Ʈ ����)
		//�ش� �Լ��� OnInteraction ���� false�� ����
		Delegate_Holder.OnInteractionOut += OnInteractionOut;
	}

	//��ȣ�ۿ��� Ȱ��ȭ �Ǹ� ����Ǵ� �Լ�
	void OnInteractionVoid()
	{
		//�ش� �÷��׸� true�� ���� �� �̻� �������� ����� �ʵ��� �Ѵ�.
		OnInteraction = true;
		//���� ���� ����� ������Ʈ ���� �ʱ�ȭ�Ѵ�.
		closetObject = null;
		//���� ����� �������� �����Ѵ�.
		IconInit();
	}

	//�ش� �Լ��� �ٽ� ��ȣ�ۿ� ���θ� false�� �ؼ�
	//�ٽ� collider Ž���� �����ϵ��� �Ѵ�.
	void OnInteractionOut()
	{
		//OnInteraction�� 1���Ŀ� false�� �ؼ�, FŰ�� ���������� ������ ��츦 ����
		Invoke("InteractionFalse", 1.0f);
		activeIcons.Clear();
	}

	void InteractionFalse() => OnInteraction = false;

	private void Update()
	{
		//���� ������Ʈ�� ��ȣ�ۿ��� ������ ���, �� �̻� �ݶ��̴� ������ �������� �ʰ�, 'F'Key �������� ����� �ʴ´�.
		if (OnInteraction) return;

		//�� �����Ӹ��� �÷��̾� �ֺ� checkRadius �ݰ� ���� interactiveLayer�� �ش��ϴ� ������Ʈ�� Ž�� �Ѵ�.
		//Physcis.OverlapSphere�� �� ������ ���� �ȿ� �ִ� �ݶ��̴��� ���� ã�Ƽ� ��ȯ�ϴ� �Լ���.
			//�Ű� ������ ���ʴ�� ���� �߽� ��ġ, ���� �ݰ�, Ư�� ���̾� ���� �̴�.
			//3D ���� �ݶ��̴��� �����ϸ�, isTrigger�� �ݶ��̴��� �����Ѵ�.
			//��ȯ ���� Collider��.
		Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, checkRadius, interactableLayer);

		//���� ����� ������Ʈ�� ������ ����
		closetObject = null;
		//���� ����� ������Ʈ���� �Ÿ� ���� ������ ����
		float closetDistance = Mathf.Infinity;


		//Ž���� ������Ʈ���� �ݺ��ϸ鼭 �Ʒ� �۾��� �����Ѵ�.
		foreach(Collider obj in nearbyObjects)
		{
			//Ž���� ������Ʈ�� transform ���� ����
			Transform targetTransform = obj.transform;

			//���� �÷����̿� �ش� ������Ʈ�� �Ÿ��� ����Ѵ�.
			float distance = Vector3.Distance(transform.position, targetTransform.position);

			//���� ������Ʈ�� �÷��̾�κ��� Ȱ�� �Ÿ� ���� �ְ�, �÷��̾�κ��� ���� ������
			if(distance <= activationDistance && distance < closetDistance)
			{
				//�ش� ������Ʈ�� ���� ����� ������Ʈ�� ����
				closetObject = targetTransform;
				//�ּ� �Ÿ� ���� ����
				closetDistance = activationDistance;
			}
		}

		//�÷��̾� ���� ���� ������ �����ϴ� ���(���� ����� ������)
		if(closetObject != null)
		{
			//�ش� ������Ʈ ��ġ�� �������� ǥ���Ѵ�.
			ShowIcon(closetObject);

			//FŰ�� ������
			if (Input.GetKeyDown(KeyCode.F))
			{
				//��������Ʈ�� Invoke �ϴ� �Լ� ȣ��
				Delegate_Holder.OnStartInteraction();
			}
		}

		//�������� �����ϴ� �Լ�
		IconInit();
	}


	private void IconInit()
	{
		//�ش� �����ӿ��� ����� �����۵��� �����ϴ� ����Ʈ
		List<Transform> toRemove = new List<Transform>();
		//������ ǥ�õǴ� �����۵��� ��ȸ�Ѵ�.
		foreach (var iconEntry in activeIcons)
		{
			//���� �ش� �������� ���� ���� ����� �����ܰ� �ٸ� ���
			if (iconEntry.Key != closetObject)
			{
				//�ش� ������Ʈ�� ����� ��ũ��Ʈ�� AnimationChage �Լ��� ȣ���Ѵ�.
				iconEntry.Value.GetComponent<UI_Animation_Handler>().AnimationChange("Out");
				//�ش� ������Ʈ�� ����Ʈ�� �߰��ؼ� ��ųʸ����� ������ �غ� �Ѵ�.
				toRemove.Add(iconEntry.Key);
			}
		}

		//���� ����Ʈ�� �ö� ������Ʈ�� ��ųʸ����� �����Ѵ�.
		foreach (var transformToRemove in toRemove) activeIcons.Remove(transformToRemove);
	}

	//�������� ǥ���ϴ� �Լ�
	private void ShowIcon(Transform targetTransform)
	{
		//�̹� ǥ�õ� �������� ���
		if (activeIcons.ContainsKey(targetTransform))
		{
			//�ش� �������� ��ġ�� ������Ʈ �Ѵ�.
			UpdateIconPosition(targetTransform, activeIcons[targetTransform]);
			return;
		}

		//���ο� ������ �ʿ� �� ���������� ���� �����Ѵ�.
		//Instantiate()�� ������, ������Ʈ ���� �ν��Ͻ�ȭ(����) �� �� ���� �Լ���.
			//�ش� �Լ��� �Ű� ������ ���ʴ�� (���� ������, ��ġ, ȸ������) �̴�.
			//�Ʒ� �Լ��� IconPrefab�� uiCanvas�� �ڽĿ� ��ġ���Ѽ� �����Ѵ�.
		GameObject iconInstance = Instantiate(IconPrefab, uiCanvas.transform);
		//������ �� ������ �������� ��ųʸ��� ����Ѵ�.
		activeIcons[targetTransform] = iconInstance;
		//�ش� �������� ��ġ�� ������Ʈ �Ѵ�.
		UpdateIconPosition(targetTransform, iconInstance);
	}

	//�������� ��ġ�� ������Ʈ �ϴ� �Լ�
	private void UpdateIconPosition(Transform targetTransform, GameObject Icon)
	{
		//Canvas�� React Transform�� ����ϱ� ������, �������� �ߴ� Transform�� React Transform���� ���������� �Ѵ�.
		//���� ��ǥ(3D ������Ʈ ��ġ)�� ȭ�� ��ǥ�� ��ȯ�Ѵ�.
		Vector3 screenPosition = Camera.main.WorldToScreenPoint(
			new Vector3(
				targetTransform.position.x,
				targetTransform.position.y + 1.5f,
				targetTransform.position.z
				));
		
		//�� ���� ���� UI �������� ��ġ�� ȭ�� ��ǥ�� ������Ʈ�Ѵ�.
		Icon.GetComponent<RectTransform>().position = screenPosition;
	}
}
