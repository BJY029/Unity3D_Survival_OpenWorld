using UnityEngine;

public class Interaction_Hit : M_Object
{
    public override void Interaction()
    {
        //P_Movement�� AnimationChage �Լ� ȣ��,
        //���ڴ� ��ũ��Ʈ ������Ʈ m_Data�� �ش� ������Ʈ Type�� ���ڿ��� �ѱ��.
        P_Movement.instance.AnimationChange(m_Data.m_Type.ToString());
        base.Interaction();
    }
}
