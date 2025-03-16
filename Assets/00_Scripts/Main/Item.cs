using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private float spreadRadius = 10.0f; //������ �ݰ�
    [SerializeField] private float arcHeight = 5.0f; // ������ ����
    [SerializeField] private float moveSpeed = 5.0f; //������ �̵� �ӵ�
	[SerializeField] private GameObject GetParticle;

	//�������� ���� �÷��̾� ��ü
    Transform Player;

	//�������� ������ ��� �ִ� ��ũ���ͺ� ������Ʈ�� ������ ����
	ITEM m_Item;
	//�������� ��ӵǴ� ������ ������ ����
	int CountValue;

	//�������� ������ �޴� �Լ�
	public void Init(ITEM item)
	{
		m_Item = item;
	}

	private void Start()
	{
		Player = P_Movement.instance.transform;
		StartCoroutine(SpreadAndMoveToPlayer());
	}

	//Ʈ������ ������ ������ �����ϴ� �ڷ�ƾ
	IEnumerator SpreadAndMoveToPlayer()
	{
		//�������� spreadRadius ���� ������ ������ �������� ������ �Ѵ�.
		//insideUnitSphere�� �������� 1�� �� ������ ������ ���� ��ȯ�Ѵ�.
		Vector3 spreadDirection = Random.insideUnitSphere * spreadRadius;
		//�������� �̵��� ��ǥ ��ġ ����
		Vector3 spreadPosition = transform.position + spreadDirection;

		//��ǥ ��ġ�� y ��ǥ�� 1���� �۾����� �ʰ� ����(spreadPos.y�� 1.0f �� ū ������ ����)
		spreadPosition.y = Mathf.Max(spreadPosition.y, arcHeight);

		//������ �ð�
		float spreadTime = 0.3f;
		float elapsedTime = 0.0f;
		//Ʈ���� ���� ��ġ
		Vector3 startPosition = transform.position;
		
		//��ǥ ��ġ���� Lerp�� ���� �ε巴�� �̵�
		while(elapsedTime < spreadTime)
		{
			elapsedTime += Time.deltaTime;
			float t = elapsedTime / spreadTime;
			transform.position = Vector3.Lerp(startPosition, spreadPosition, t);
			yield return null;
		}

		//Ʈ���� �����Ⱑ �Ϸ�� ��, �÷��̾�� ���� �̵��ϴ� �ڷ�ƾ ����
		StartCoroutine(MoveToPlayer(spreadPosition));
	}

	IEnumerator MoveToPlayer(Vector3 startPosition)
	{
		float journeyTime;
		float elapsedTime;
		Vector3 endPosition;

		while (true) //���� ���� ����
		{
			//���� ��ġ�� �÷��̾��� ��ġ
			endPosition = Player.position + new Vector3(0.0f, 1.0f, 0.0f);
			//�̵� �ð��� ���� ��ġ���� ���� ��ġ������ �Ÿ��� �̵� �ӵ��� ���� ��(�ð� = �Ÿ� / �ӵ�)
			journeyTime = Vector3.Distance(startPosition, endPosition) / moveSpeed;
			elapsedTime = 0.0f;

			while(elapsedTime < journeyTime)
			{
				elapsedTime += Time.deltaTime;
				float t = elapsedTime / journeyTime;

				//Mathf.Sin(Mathf.PI * t)�� t = 0���� t = 1���� 0 -> 1 -> 0���� ��ȭ
				//�̸� �̿��Ͽ� arcHeight ��ŭ�� ���̸� �߰��Ͽ� ������ ������ �����.
				//float height = Mathf.Sin(Mathf.PI * t) * arcHeight;
				//Ʈ���� ��ġ�� Lerp�� �ε巴�� �̵�
				Vector3 currentPos = Vector3.Lerp(startPosition, endPosition, t);
				//�ش� Ʈ���� ��ġ�� ���� ���� �Ʊ� ���� ������ ���� �� ����
				//currentPos.y += height;

				//Ʈ���� ��ġ ���ʱ�ȭ
				transform.position = currentPos;
				//�̵� �߿��� �÷��̾ �����̸� ��ǥ ��ġ ����
				endPosition = Player.position + new Vector3(0.0f, 1.0f, 0.0f);

				yield return null;
			}

			//�÷��̾�� 0.5 �̳��� ��������� �ݺ����� ����
			if (Vector3.Distance(transform.position, Player.position + new Vector3(0.0f, 1.0f, 0.0f)) < 0.5f) break;
			
			//0.5 �̳��� ��������� ���� ��� �ٽ� �ݺ��� �����ϱ� ���� ���� ��ġ �ʱ�ȭ
			startPosition = transform.position; 
		}
		//������ ȹ�� ��, ��ƼŬ ���
		Instantiate(GetParticle, transform.position, Quaternion.identity);

		Navigation_Mng.Instance.PanelGet_Item(m_Item.Data);

		//�κ��丮�� �������� �����ϰ� ���� ���� �߰��Ѵ�.
		ItemFlowController.GETITEM(m_Item.Data, m_Item.Count);

		Destroy(this.gameObject);
	}
}
