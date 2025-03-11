using UnityEngine;
using UnityEngine.U2D;

//해당 스크립트를 오브젝트에 넣지 않기 때문에, Monobehavior를 상속하지 않는다.
public class Asset_Manager
{
	//Resources 폴더에서 Sprite Atlas 로드
	public static SpriteAtlas atlas = Resources.Load<SpriteAtlas>("Atlas");

	//특정 이름의 스프라이트를 아틀라으에서 가져오는 함수
	public static Sprite Get_Atlas(string temp)
	{
		return atlas.GetSprite(temp);
	}
}
