using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Holder : MonoBehaviour
{
	public static Canvas_Holder instance = null;

    [SerializeField] private GameObject Board;
	[SerializeField] private GameObject InventoryPanel;
	//ü�¹��� ���� �κа� ��� �κ��� �����´�.
	public Image BoardHpFill, BoardHpWhiteFill;
	Coroutine F_Courtine;

	public void StopAllCoroutine() => StopAllCoroutines();

	private void Start()
	{
		//OnInteraction �����Ը�Ʈ(��ȣ�ۿ� ����)�� GetBoard �̺�Ʈ �߰�
		Delegate_Holder.OnInteraction += GetBoard;
		//OnInteractionOut ��������Ʈ(��ȣ�ۿ� ����)�� BoardOut �̺�Ʈ �߰�
		Delegate_Holder.OnInteractionOut += BoardOut;
	}

	private void Awake()
	{
		if(instance == null) instance = this;
	}


	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Tab))
		{
			//�ش� ������Ʈ�� SetActive ���� true�� false, false�� true�� ó���� �ȴ�.
			//���ǿ����� �ش� ��ư�� 'I'�� ����
			InventoryPanel.SetActive(!InventoryPanel.activeSelf);
		}
	}

	public void GetBoard()
    {
        Board.SetActive(true);
    }

	//ü�� �ٰ� ������� �ִϸ��̼� ����
	public void BoardOut() => Board.GetComponent<UI_Animation_Handler>().AnimationChange("Out");


	public void BoardFill(float hp, float MaxHp)
	{
		//���� ���� ü���� ���� �ֽ�ȭ �Ѵ�.
		BoardHpFill.fillAmount = hp / MaxHp;
		//������ ���� ���̴� �ڷ�ƾ�� �ִٸ� ����(�ߺ� ���� ����)
		if(F_Courtine != null)
		{
			StopCoroutine(F_Courtine);
		}
		//õõ�� �پ��� ��� ü�¹ٸ� �����ϴ� �ڷ�ƾ ����
		F_Courtine = StartCoroutine(FillCoroutine());
	}

	//��� ü�¹ٸ� õõ�� ������Ʈ �ϴ� �ڷ�ƾ
	IEnumerator FillCoroutine()
	{
		//��� �κ��� ü�� ���� ���� �κ� ü�� ������ Ŭ ������
		while (BoardHpWhiteFill.fillAmount > BoardHpFill.fillAmount)
		{
			//Lerp�� �ε巴�� ü�� ����(���� ������ �ӵ����� 2�� ���� �ӵ��� ����)
			BoardHpWhiteFill.fillAmount = Mathf.Lerp(BoardHpWhiteFill.fillAmount,
				BoardHpFill.fillAmount, Time.deltaTime * 2.0f);

			yield return null;
		}
	}
}
