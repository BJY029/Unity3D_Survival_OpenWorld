 using UnityEngine;

public class Cam_Movement : MonoBehaviour
{
    //�÷��̾��� ��ġ��
    [SerializeField] private Transform player;

    //�÷��̾� ��ġ����, ī�޶��� ��ġ�� ������ ��
    [SerializeField] private float PosX;
    [SerializeField] private float PosY;
    [SerializeField] private float PosZ;

    //Lerp �ӵ�
    [SerializeField] private float m_Speed = 2.0f;

	private void Update()
	{
        Move();
	}

    //ī�޶� �������� �����ϴ� �Լ�
    void Move()
    {
        //���� ��ũ��Ʈ�� ����� ������Ʈ(ī�޶�)�� ��ġ�� �����Ѵ�.
        transform.position = Vector3.Lerp(transform.position, new Vector3(
            //�÷��̾� ��ġ�κ��� ī�޶��� ��ġ�� ����
            player.transform.position.x + PosX,
            player.transform.position.y + PosY,
            player.transform.position.z + PosZ
            ), Time.deltaTime * m_Speed);
    }
}
