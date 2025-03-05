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



	private void Update()
	{
		//�� �����Ӹ��� �÷��̾� �ֺ� checkRadius �ݰ� ���� interactiveLayer�� �ش��ϴ� ������Ʈ�� Ž�� �Ѵ�.
		//Physcis.OverlapSphere�� �� ������ ���� �ȿ� �ִ� �ݶ��̴��� ���� ã�Ƽ� ��ȯ�ϴ� �Լ���.
			//�Ű� ������ ���ʴ�� ���� �߽� ��ġ, ���� �ݰ�, Ư�� ���̾� ���� �̴�.
			//3D ���� �ݶ��̴��� �����ϸ�, isTrigger�� �ݶ��̴��� �����Ѵ�.
			//��ȯ ���� Collider��.
		Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, checkRadius, interactableLayer);

		//�̹� �����ӿ��� ���� Ž���� ������Ʈ���� ������ �ӽø��
		//HashSet�� �ߺ��� ������� �ʴ� �÷�������, �Ȱ��� �����Ͱ� �� �� ���� �� ����.
			//HashTable�� ����� ������, �� �����Ϳ� ���� �ؽ� �Լ��� ����ؼ� �ؽ� ���� Űó�� ����� ������ ����
			//���� �浹�� ü�̴� ������� �浹 �ذ�(O(1))
		HashSet<Transform> currentObjects = new HashSet<Transform>();


		//Ž���� ������Ʈ���� �ݺ��ϸ鼭 �Ʒ� �۾��� �����Ѵ�.
		foreach(Collider obj in nearbyObjects)
		{
			//Ž���� ������Ʈ�� transform ���� ����
			Transform targetTransform = obj.transform;

			//���� �÷����̿� �ش� ������Ʈ�� �Ÿ��� ����Ѵ�.
			float distance = Vector3.Distance(transform.position, targetTransform.position);

			//���� ������Ʈ�� �÷��̾�κ��� Ȱ�� �Ÿ� ���� ������
			if(distance <= activationDistance)
			{
				//�������� ǥ���Ѵ�.
				ShowIcon(targetTransform);
				//currentObject HashSet�� ����Ѵ�.
				currentObjects.Add(targetTransform);
			}
		}

		//�ش� �����ӿ��� ����� �������� �����ϴ� ����Ʈ
		List<Transform> toRemove = new List<Transform>();
		//������ ǥ�õǴ� �����ܵ��� ��ȸ�Ѵ�.
		foreach(var iconEntry in activeIcons)
		{
			//���� ������ ǥ�õǴ� �������� �̹� �����ӿ� ����� ���
			if (!currentObjects.Contains(iconEntry.Key))
			{
				//�ش� ������Ʈ�� ����� ��ũ��Ʈ�� AnimationChage �Լ��� ȣ���Ѵ�.
				iconEntry.Value.GetComponent<UI_Animation_Handler>().AnimationChange("Out");
				//�ش� ������Ʈ�� ��ųʸ����� ���� �� �غ� �Ѵ�.
				toRemove.Add(iconEntry.Key);
			}
		}

		//���� ����Ʈ�� �ö� �������� ��ųʸ����� �����Ѵ�.
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
