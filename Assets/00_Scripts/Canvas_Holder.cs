using UnityEngine;

public class Canvas_Holder : MonoBehaviour
{
    [SerializeField] private GameObject Board;

	private void Start()
	{
		//OnInteraction 델리게리트(상호작용 시작)에 GetBoard 이벤트 추가
		Delegate_Holder.OnInteraction += GetBoard;
		//OnInteractionOut 델리게이트(상호작용 종료)에 BoardOut 이벤트 추가
		Delegate_Holder.OnInteractionOut += BoardOut;
	}

	public void GetBoard()
    {
        Board.SetActive(true);
    }

	//체력 바가 사라지는 애니메이션 수행
	public void BoardOut() => Board.GetComponent<UI_Animation_Handler>().AnimationChange("Out");
}
