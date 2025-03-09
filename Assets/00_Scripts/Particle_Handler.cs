using UnityEngine;

public class Particle_Handler : MonoBehaviour
{
    //�̱������� �����ؼ� �ش� ��ũ��Ʈ ���� ���� ����
    public static Particle_Handler instance = null;
    //��ƼŬ ��ü �޾ƿ���
    ParticleSystem myParticleSystem;

	private void Awake()
	{
		if(instance == null) instance = this;
	}

	private void Start()
	{
		myParticleSystem = GetComponent<ParticleSystem>();
	}

    //��ƼŬ�� �����ϴ� �Լ�(mesh�� �޾ƿ´�.)
	public void OnParticle(MeshRenderer mesh)
    {
        UpdateParticleMesh(mesh);
        myParticleSystem.Play();
    }

    //��ƼŬ�� Mesh �� ���� ������ ����ϴ� �Լ�
    private void UpdateParticleMesh(MeshRenderer meshRenderer)
    {
        //�ش� ��ƼŬ ���� ��ġ�� mesh ���� �޾ƿ� ������Ʈ�� ��ġ�� �̵���Ų��.
        transform.position = meshRenderer.transform.position;
        //��ƼŬ shape�� mesh�� �޾ƿ� mesh�� �����Ѵ�.
        //�ش� ������ ��ƼŬ�� ������Ʈ ũ�� ��ŭ �����Ǵ� ������ ����
        var shape = myParticleSystem.shape;
        shape.meshRenderer = meshRenderer;
    }
}
