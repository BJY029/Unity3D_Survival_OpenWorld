using UnityEngine;

public class M_Object : MonoBehaviour
{
    public Object_Scriptable m_Data;
    public bool GetInteraction = false;

    public int HP;

    public virtual void Interaction()
    {
        //���� ������Ʈ�� P_Handler�� m_Object�� ����
        P_Handler.m_Object = this;
        //�ش�Ǵ� ������Ʈ�� ���� ü���� �޾ƿ´�.
        HP = m_Data.HP;
        GetInteraction = true;
    }

    //HP ü�� �ٸ� �ʱ�ȭ �����ִ� �Լ�(�ٸ� ������ Override ����)
	public virtual void  HP_init()
    {
        //HP�� 0���� �۰ų� ��������
        if(HP <= 0)
        {
            //HP�� 0���� �ʱ�ȭ�ϰ�
            HP = 0;
            //�ش� ������Ʈ�� �ı��� ����
            Destroy(this.gameObject);
            //��ȣ�ۿ� ���� ��������Ʈ�� ȣ���Ѵ�.
			Delegate_Holder.OnOutInteraction();
			return;
        }

        //���ڷ� ���� ü�°� �ִ� ü���� �ѱ�
        Canvas_Holder.instance.BoardFill(HP, m_Data.HP);
    }
}
