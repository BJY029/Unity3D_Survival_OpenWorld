using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Nav_Item : MonoBehaviour
{
    //�ش� UI�� ��ҵ��� ������ ������
    [SerializeField] private Image Rarity_Image;
    [SerializeField] private Image Item_Icon_Image;
    [SerializeField] private TextMeshProUGUI Item_Name_Text;

    //UI ��ҵ��� �������� �����͸� ���� �ʱ�ȭ�����ش�.
    public void Init(Item_Scriptable m_Data)
    {
        //Atlas ���� �̹����� �ҷ��´�.
        Rarity_Image.sprite = Asset_Manager.Get_Atlas(m_Data.rarity.ToString());
        Item_Icon_Image.sprite = Asset_Manager.Get_Atlas(m_Data.itemId.ToString());
        Item_Name_Text.text = m_Data.ItemName;
    }

}
