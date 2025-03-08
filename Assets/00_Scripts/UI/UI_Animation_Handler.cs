using UnityEngine;


public class UI_Animation_Handler : MonoBehaviour
{
    Animator animator;

	private void Start()
	{
		animator = GetComponent<Animator>();
	}

	//Animator�� �Ķ���͸� �����ϴ� �Լ�
	public void AnimationChange(string temp)
	{
		animator.SetTrigger(temp);
	}


	public void Destroy_Object() => Destroy(gameObject);
	//��ũ��Ʈ�� ����� ������Ʈ�� �ı��ϴ� �Լ�(����)
	public void Deactive() => gameObject.SetActive(false);
}
