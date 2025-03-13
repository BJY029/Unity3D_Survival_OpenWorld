using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Nav_Item : MonoBehaviour
{
    //해당 UI의 요소들을 저장할 변수들
    [SerializeField] private Image Rarity_Image;
    [SerializeField] private Image Item_Icon_Image;
    [SerializeField] private TextMeshProUGUI Item_Name_Text;

    //UI 요소들을 아이템의 데이터를 통해 초기화시켜준다.
    public void Init(Item_Scriptable m_Data)
    {
        //Atlas 내의 이미지를 불러온다.
        Rarity_Image.sprite = Asset_Manager.Get_Atlas(m_Data.rarity.ToString());
        Item_Icon_Image.sprite = Asset_Manager.Get_Atlas(m_Data.itemId.ToString());
        Item_Name_Text.text = m_Data.ItemName;
    }

}
