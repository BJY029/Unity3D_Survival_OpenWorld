using UnityEngine;

public class M_Object : MonoBehaviour
{
    public Object_Scriptable m_Data;
    public bool GetInteraction = false;

    public int HP;

    public virtual void Interaction()
    {
        //현재 오브젝트를 P_Handler의 m_Object로 설정
        P_Handler.m_Object = this;
        GetInteraction = true;
        HP_init();
    }

    public virtual void OnHit()
    {
        HP_init();
    }

    //HP 체력 바를 초기화 시켜주는 함수(다른 곳에서 Override 가능)
	public virtual void  HP_init()
    {
        //HP가 0보다 작거나 같아지면
        if(HP <= 0)
        {
            //HP를 0으로 초기화하고
            HP = 0;

            //파티클을 생성하는 함수 호출,
            //인자로 해당 오브젝트의 첫번째 자식에 달린 meshRenderer 값을 넘겨준다.
			Particle_Handler.instance.OnParticle(transform.GetChild(0).GetComponent<MeshRenderer>());

			//해당 오브젝트를 파괴한 다음
			Destroy(this.gameObject);
            //상호작용 종료 델리게이트를 호출한다.
			Delegate_Holder.OnOutInteraction();
            
			return;
        }

        //인자로 현재 체력과 최대 체력을 넘김
        Canvas_Holder.instance.BoardFill(HP, m_Data.HP);
    }
}
