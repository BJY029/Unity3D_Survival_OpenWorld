using UnityEngine;

public class P_Handler : MonoBehaviour
{
	//현재 상호작용 중인 오브젝트를 저장(static로 설정해서 전역접근 가능)
	//순서
		//'F'Key가 눌리면, P_Finder 에서 closetObject.GetComponent<M_Object>().Interaction()가 실행됨
		//Interaaction()에서 현재 상호작용중인 객체를 m_Object에 저장
	public static M_Object m_Object = null;

	[SerializeField] private GameObject HitParticle;


	//*****강의에서는 Hit이라고 함수가 정의됨*****
	public void Attack()
	{
		m_Object.HP -= 10;

		//파티클 생성 위치 정의
		Vector3 pos = new Vector3(
			m_Object.transform.position.x + Random.Range(-0.5f, 0.5f),
			m_Object.transform.position.y + 1.5f,
			m_Object.transform.position.z + Random.Range(-0.5f, 0.5f)
			);
		//파티클 프리팹 생성
		Instantiate(HitParticle, pos, Quaternion.identity);
		m_Object.OnHit();
	}
}
