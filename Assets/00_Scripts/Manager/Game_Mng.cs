using UnityEngine;

public class Game_Mng : MonoBehaviour
{
    public static Game_Mng Instance;

	private void Awake()
	{
		if(Instance == null) Instance = this;
	}
}
