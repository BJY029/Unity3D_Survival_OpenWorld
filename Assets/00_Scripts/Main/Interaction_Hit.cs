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
        //P_Movement�� AnimationChage �Լ� ȣ��,
        //���ڴ� ��ũ��Ʈ ������Ʈ m_Data�� �ش� ������Ʈ Type�� ���ڿ��� �ѱ��.
        P_Movement.instance.AnimationChange(m_Data.m_Type.ToString());
        base.Interaction();
    }

	public override void OnHit()
	{
		base.OnHit();
		//ShakeTree �Լ� ȣ��
		//(���ڷ� ���� ������ �ҷ����� ���ؼ�, ������Ʈ ��ġ���� �÷��̾� ��ġ�� �� ���� ���� ���� �ѱ��.)
		ShakeTree(transform.position - P_Movement.instance.transform.position);

		if(HP <= 0) //������Ʈ�� ü���� 0���� ���ų� ������
		{
			//�ش� ������Ʈ�κ��� ��ӵǴ� �����۵��� �޾ƿ´�.
			var items = ItemFlowController.DROPITEMLIST(m_Data.Drop_Items);

			//��ӵ� �����۸�ŭ�� Ʈ���� �������� ������Ų��.
			for(int i = 0; i < items.Count; i++)
			{
				var go = Instantiate(Item_Prefab, transform.position, Quaternion.identity);
				//��ӵ� ������ ����Ʈ�� Item ��ũ��Ʈ�� Init �Լ��� �����Ѵ�.
				go.Init(items[i]);
			}
		}
	}

	private void ShakeTree(Vector3 attackDirection)
	{
		//�Ѱܹ��� ���� ���͸� ����ȭ�ؼ� ũ�⸦ 1�� �����.(���� ���� �ʿ�)
		Vector3 oppositeDirection = attackDirection.normalized;

		//transform.rotation vs. transform.eulerAngles
		//transform.rotation : ȸ�� ���� Quaternion ���� �����´�.
		//transform.eulerAngles : ȸ�� ���� Vector3�� �����´�.

		//������ ���� ������ �ݴ� �������� ������ �����̴� ���� �ڿ������� ������,
		//���� ������ Z���� X�� ȸ���� �����ϰ�, ���� ������ X���� Z�� ȸ���� �����Ѵ�.
		//�̶� X�� ȸ���� ������Ʈ�� ���� ȸ���� ���, Z�� ȸ���� �¿� ȸ���� ����Ѵ�.
		Quaternion targetRotation = Quaternion.Euler(
			originalRotation.eulerAngles.x + oppositeDirection.z * shakeAmount,
			originalRotation.eulerAngles.y,
			originalRotation.eulerAngles.z - oppositeDirection.x * shakeAmount
			);

		//���� ���� ���̴� �ڷ�ƾ���� ���߰�
		StopAllCoroutines();
		//��鸲 �ִϸ��̼��� ������ �ڷ�ƾ�� Ȱ��ȭ
		StartCoroutine(ShakeAnimation(targetRotation));	
	}


	//�ش� �ڷ�ƾ�� ��ü�� ��ǥ ȸ���� ���� ������ �����ٰ� ���� ���·� ���ƿ��� ����� �ڷ�ƾ
	private IEnumerator ShakeAnimation(Quaternion targetRotation)
	{
		float elapsedTime = 0.0f;

		//��鸲 �ð��� ������ ��ǥ ȸ��ġ �� ��ŭ ȸ���ϴµ� ���
		while (elapsedTime < shakeDuration / 2) 
		{
			//slerp�� ���� originalRotation ������ targetRotation ������ 
			// elapsedTime / (shakeDuration / 2) ������ŭ �ε巴�� ����
			transform.rotation = Quaternion.Slerp(originalRotation, targetRotation, elapsedTime / (shakeDuration / 2));
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		elapsedTime = 0.0f;
		//������ ��鸲 �ð��� ������ ���� ȸ�� ������ �����ϴ� �� ���
		while(elapsedTime < shakeDuration / 2)
		{
			//slerp�� ���� targetRotation ������ originalRotation ������ 
			// elapsedTime / (shakeDuration / 2) ������ŭ �ε巴�� ����
			transform.rotation = Quaternion.Slerp(targetRotation, originalRotation, elapsedTime / (shakeDuration / 2));
			elapsedTime += Time.deltaTime;
			yield return null;
		}
	}
}
