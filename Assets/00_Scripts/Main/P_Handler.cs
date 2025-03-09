using UnityEngine;

public class P_Handler : MonoBehaviour
{
	//���� ��ȣ�ۿ� ���� ������Ʈ�� ����(static�� �����ؼ� �������� ����)
	//����
		//'F'Key�� ������, P_Finder ���� closetObject.GetComponent<M_Object>().Interaction()�� �����
		//Interaaction()���� ���� ��ȣ�ۿ����� ��ü�� m_Object�� ����
	public static M_Object m_Object = null;

	[SerializeField] private GameObject HitParticle;


	//*****���ǿ����� Hit�̶�� �Լ��� ���ǵ�*****
	public void Attack()
	{
		m_Object.HP -= 10;

		//��ƼŬ ���� ��ġ ����
		Vector3 pos = new Vector3(
			m_Object.transform.position.x + Random.Range(-0.5f, 0.5f),
			m_Object.transform.position.y + 1.5f,
			m_Object.transform.position.z + Random.Range(-0.5f, 0.5f)
			);
		//��ƼŬ ������ ����
		Instantiate(HitParticle, pos, Quaternion.identity);
		m_Object.OnHit();
	}
}
