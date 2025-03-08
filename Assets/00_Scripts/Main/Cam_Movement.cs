 using UnityEngine;

public class Cam_Movement : MonoBehaviour
{
    //플레이어의 위치값
    [SerializeField] private Transform player;

    //플레이어 위치기준, 카메라의 위치를 지정할 값
    [SerializeField] private float PosX;
    [SerializeField] private float PosY;
    [SerializeField] private float PosZ;

    //Lerp 속도
    [SerializeField] private float m_Speed = 2.0f;

	private void Update()
	{
        Move();
	}

    //카메라 움직임을 통제하는 함수
    void Move()
    {
        //현재 스크립트가 적용된 오브젝트(카메라)의 위치를 조정한다.
        transform.position = Vector3.Lerp(transform.position, new Vector3(
            //플레이어 위치로부터 카메라의 위치를 설정
            player.transform.position.x + PosX,
            player.transform.position.y + PosY,
            player.transform.position.z + PosZ
            ), Time.deltaTime * m_Speed);
    }
}
