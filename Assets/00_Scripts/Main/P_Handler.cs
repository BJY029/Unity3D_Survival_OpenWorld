using UnityEngine;

public class P_Handler : MonoBehaviour
{
	//���� ��ȣ�ۿ� ���� ������Ʈ�� ����(static�� �����ؼ� �������� ����)
	//����
		//'F'Key�� ������, P_Finder ���� closetObject.GetComponent<M_Object>().Interaction()�� �����
		//Interaaction()���� ���� ��ȣ�ۿ����� ��ü�� m_Object�� ����
	public static M_Object m_Object = null;

	//*****���ǿ����� Hit�̶�� �Լ��� ���ǵ�*****
	public void Attack()
	{
		m_Object.HP -= 10;
		m_Object.HP_init();
	}
}
