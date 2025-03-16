using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Holder : MonoBehaviour
{
	public static Canvas_Holder instance = null;

    [SerializeField] private GameObject Board;
	[SerializeField] private Transform UI_PART_PARENT;
	//ü�¹��� ���� �κа� ��� �κ��� �����´�.
	public Image BoardHpFill, BoardHpWhiteFill;
	Coroutine F_Courtine;

	public void StopAllCoroutine() => StopAllCoroutines();



	private void Awake()
	{
		if(instance == null) instance = this;
	}

	//UI��ҵ��� �����ϴ� ��ųʸ�
	private Dictionary<string, UIPART> uiPart = new Dictionary<string, UIPART>();

	//�ش�Ǵ� UI�� �����ϸ�, UIPART�� Open() �Լ��� ���� �ش� UI Ȱ��ȭ
	public void OpenUI(string uiName)
	{
		if (uiPart.ContainsKey(uiName))
		{
			uiPart[uiName].Open();
		}
		else Debug.LogWarning($"UI {uiName} not found.");
	}

	//�ش�Ǵ� UI�� �����ϸ�, UIPART�� Close() �Լ��� ���� �ش� UI ��Ȱ��ȭ
	public void CloseUI(string uiName)
	{
		if (uiPart.ContainsKey(uiName))
		{
			uiPart[uiName].Close();
		}
	}

	//��� UI ��Ȱ��ȭ
	public void CloseAllUI()
	{
		foreach (var part in uiPart.Values)
		{
			part.Close();
		}
	}

	private void Start()
	{
		//�ش�Ǵ� ������Ʈ �ڽĵ��� �迭�� �޾ƿ´�.(true ������ ��Ȱ��ȭ �� ������Ʈ�� ã�� ����)
		UIPART[] parts = UI_PART_PARENT.GetComponentsInChildren<UIPART>(true);
		//�ϳ��� ��ųʸ��� �����Ѵ�.
		foreach(var part in parts)
		{
			uiPart.Add(part.name, part);
		}
		Debug.Log(uiPart.Keys);

		//OnInteraction �����Ը�Ʈ(��ȣ�ۿ� ����)�� GetBoard �̺�Ʈ �߰�
		Delegate_Holder.OnInteraction += GetBoard;
		//OnInteractionOut ��������Ʈ(��ȣ�ۿ� ����)�� BoardOut �̺�Ʈ �߰�
		Delegate_Holder.OnInteractionOut += BoardOut;
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Tab))
		{
			//���ǿ����� �ش� ��ư�� 'I'�� ����
			//INVENTORY�� �ش�Ǵ� uiPart�� �Ѱų� ����.
			uiPart["INVENTORY"].Toggle();
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
