using UnityEngine;

public class Particle_Handler : MonoBehaviour
{
    //싱글턴으로 설정해서 해당 스크립트 전역 접근 설정
    public static Particle_Handler instance = null;
    //파티클 객체 받아오기
    ParticleSystem myParticleSystem;

	private void Awake()
	{
		if(instance == null) instance = this;
	}

	private void Start()
	{
		myParticleSystem = GetComponent<ParticleSystem>();
	}

    //파티클을 실행하는 함수(mesh를 받아온다.)
	public void OnParticle(MeshRenderer mesh)
    {
        UpdateParticleMesh(mesh);
        myParticleSystem.Play();
    }

    //파티클의 Mesh 및 각종 설정을 담당하는 함수
    private void UpdateParticleMesh(MeshRenderer meshRenderer)
    {
        //해당 파티클 생성 위치를 mesh 값을 받아온 오브젝트의 위치로 이동시킨다.
        transform.position = meshRenderer.transform.position;
        //파티클 shape의 mesh를 받아온 mesh로 변경한다.
        //해당 과정은 파티클이 오브젝트 크기 만큼 생성되는 역할을 수행
        var shape = myParticleSystem.shape;
        shape.meshRenderer = meshRenderer;
    }
}
