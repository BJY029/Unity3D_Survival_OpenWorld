using UnityEngine;

public class Interaction_Hit : M_Object
{
    public override void Interaction()
    {
        //P_Movement의 AnimationChage 함수 호출,
        //인자는 스크립트 오브젝트 m_Data의 해당 오브젝트 Type를 문자열로 넘긴다.
        P_Movement.instance.AnimationChange(m_Data.m_Type.ToString());
        base.Interaction();
    }
}
