using UnityEngine;

public class Canvas_Holder : MonoBehaviour
{
    [SerializeField] private GameObject Board;

	private void Start()
	{
		//OnInteraction �����Ը�Ʈ(��ȣ�ۿ� ����)�� GetBoard �̺�Ʈ �߰�
		Delegate_Holder.OnInteraction += GetBoard;
		//OnInteractionOut ��������Ʈ(��ȣ�ۿ� ����)�� BoardOut �̺�Ʈ �߰�
		Delegate_Holder.OnInteractionOut += BoardOut;
	}

	public void GetBoard()
    {
        Board.SetActive(true);
    }

	//ü�� �ٰ� ������� �ִϸ��̼� ����
	public void BoardOut() => Board.GetComponent<UI_Animation_Handler>().AnimationChange("Out");
}
