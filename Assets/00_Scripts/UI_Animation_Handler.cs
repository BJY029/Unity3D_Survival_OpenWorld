using UnityEngine;


public class UI_Animation_Handler : MonoBehaviour
{
    Animator animator;

	private void Start()
	{
		animator = GetComponent<Animator>();
	}

	//Animator의 파라미터를 설정하는 함수
	public void AnimationChange(string temp)
	{
		animator.SetTrigger(temp);
	}


	public void Destroy_Object() => Destroy(gameObject);
	//스크립트가 적용된 오브젝트를 파괴하는 함수(람다)
	public void Deactive() => gameObject.SetActive(false);
}
