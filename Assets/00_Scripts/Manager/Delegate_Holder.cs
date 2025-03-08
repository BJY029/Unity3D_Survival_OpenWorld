using UnityEngine;

//델리게이트 선언
public delegate void Interaction();
public class Delegate_Holder : MonoBehaviour
{
	//델리게이트를 이벤트와 활용
	public static event Interaction OnInteraction;
	public static event Interaction OnInteractionOut;

	//함수 정의(람다), 해당 함수는 OnInteraction 델리게이트가 null이 아니면 Invoke 한다.
	public static void OnStartInteraction() => OnInteraction?.Invoke();
	//함수 정의(람다), 해당 함수는 OnInteractionOut 델리게이트가 null이 아니면 Invoke 한다.
	public static void OnOutInteraction() => OnInteractionOut?.Invoke();
}
