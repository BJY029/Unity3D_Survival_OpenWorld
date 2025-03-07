using UnityEngine;

//��������Ʈ ����
public delegate void Interaction();
public class Delegate_Holder : MonoBehaviour
{
	//��������Ʈ�� �̺�Ʈ�� Ȱ��
	public static event Interaction OnInteraction;
	public static event Interaction OnInteractionOut;

	//�Լ� ����(����), �ش� �Լ��� OnInteraction ��������Ʈ�� null�� �ƴϸ� Invoke �Ѵ�.
	public static void OnStartInteraction() => OnInteraction?.Invoke();
	//�Լ� ����(����), �ش� �Լ��� OnInteractionOut ��������Ʈ�� null�� �ƴϸ� Invoke �Ѵ�.
	public static void OnOutInteraction() => OnInteractionOut?.Invoke();
}
