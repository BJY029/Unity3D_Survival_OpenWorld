using UnityEngine;

public class Cam_OrbitCamera : MonoBehaviour
{
    //캐릭터의 위치(카메라가 바라볼 대상)
    public Transform target;
    [SerializeField] private float distance = 5.0f; //캐릭터와 카메라 사이의 거리
    [SerializeField] private float xSpeed = 120.0f; //좌우 회전 속도
    [SerializeField] private float ySpeed = 80.0f; //상하 회전 속도

    //각 아래 위 각도 제한
    [SerializeField] private float yMinLimit = -10f;
    [SerializeField] private float yMaxLimit = 80f;

    //현재 좌우 상하 각도
    private float x = 0.0f;
    private float y = 0.0f;

	private void Start()
	{
        //초기 각도 설정(캐릭터 뒤에서 시작)
        Vector3 angles = transform.eulerAngles;
        x = angles.x;
        y = angles.y;

        //마우스 커서 숨기기
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
	}

	private void LateUpdate()
	{
        if (target)
        {
            //x와 y의 각도 변화량, 곱해지는 값은 마우스 감도 조절 역할
            x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
            y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;

            //해당 y 값에 대한 최소, 최댓값을 Clamp로 제한
            y = Mathf.Clamp(y, yMinLimit, yMaxLimit);

            //카메라의 회전 정보 계산
            Quaternion rotation = Quaternion.Euler(y, x, 0);
            //카메라의 위치 계산(각도에 맞게 회전하여, 해당 각도에서 -distance만큼 떨어진 위치 + 현재 캐릭터 위치)
            Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

            //각도, 위치 설정
            transform.rotation = rotation;
            transform.position = position;
        }
	}
}
