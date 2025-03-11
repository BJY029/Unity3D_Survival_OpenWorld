using UnityEngine;
using UnityEngine.U2D;

//�ش� ��ũ��Ʈ�� ������Ʈ�� ���� �ʱ� ������, Monobehavior�� ������� �ʴ´�.
public class Asset_Manager
{
	//Resources �������� Sprite Atlas �ε�
	public static SpriteAtlas atlas = Resources.Load<SpriteAtlas>("Atlas");

	//Ư�� �̸��� ��������Ʈ�� ��Ʋ�������� �������� �Լ�
	public static Sprite Get_Atlas(string temp)
	{
		return atlas.GetSprite(temp);
	}
}
