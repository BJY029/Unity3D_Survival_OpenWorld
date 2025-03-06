using UnityEngine;

public class Cam_OrbitCamera : MonoBehaviour
{
    //ĳ������ ��ġ(ī�޶� �ٶ� ���)
    public Transform target;
    [SerializeField] private float distance = 5.0f; //ĳ���Ϳ� ī�޶� ������ �Ÿ�
    [SerializeField] private float xSpeed = 120.0f; //�¿� ȸ�� �ӵ�
    [SerializeField] private float ySpeed = 80.0f; //���� ȸ�� �ӵ�

    //�� �Ʒ� �� ���� ����
    [SerializeField] private float yMinLimit = -10f;
    [SerializeField] private float yMaxLimit = 80f;

    //���� �¿� ���� ����
    private float x = 0.0f;
    private float y = 0.0f;

	private void Start()
	{
        //�ʱ� ���� ����(ĳ���� �ڿ��� ����)
        Vector3 angles = transform.eulerAngles;
        x = angles.x;
        y = angles.y;

        //���콺 Ŀ�� �����
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
	}

	private void LateUpdate()
	{
        if (target)
        {
            //x�� y�� ���� ��ȭ��, �������� ���� ���콺 ���� ���� ����
            x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
            y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;

            //�ش� y ���� ���� �ּ�, �ִ��� Clamp�� ����
            y = Mathf.Clamp(y, yMinLimit, yMaxLimit);

            //ī�޶��� ȸ�� ���� ���
            Quaternion rotation = Quaternion.Euler(y, x, 0);
            //ī�޶��� ��ġ ���(������ �°� ȸ���Ͽ�, �ش� �������� -distance��ŭ ������ ��ġ + ���� ĳ���� ��ġ)
            Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

            //����, ��ġ ����
            transform.rotation = rotation;
            transform.position = position;
        }
	}
}
