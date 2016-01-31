using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;


    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        // GetComponentInChildren : 모든 자식 게임 오브젝트를 검토(첫번재 파티클 시스템을 찾아 반환)
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();

        currentHealth = startingHealth;
    }


    void Update ()
    {
        if(isSinking)
        {
            // 물리 효과와의 동조를 상실할 염려 없이 Translate를 이용할 수 잇음.
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        if(isDead)
            return;

        enemyAudio.Play ();

        currentHealth -= amount;
            
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        if(currentHealth <= 0)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;

        anim.SetTrigger ("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play ();
    }


    public void StartSinking ()
    {
        // SetActive = false가 보이면 그것은 게임 오브젝트, 전체 게임오브젝트를 끈다는것.
        // [.enable = false] 이것은 전체 게임 오브젝트가 아니라 해당 게임 오브젝트의
        // 이 컴포넌트 하나만 끈다는 것을 의미.
        GetComponent <NavMeshAgent> ().enabled = false;

        // Rigidbody를 kinematic으로 설정하는 것은
        // 씬에서 콜라이더를 움직일 때 Unity가 모든 스태틱 지오메트리를 다시 계산할 것이기 때문.
        // Unity가 보기에, '아, 레벨이 변경되었구나, 그러니까 이걸 다시 살펴봐야겠어.'라고 생각하기 때문
        // 하지만 kinematic 리지드바디로 설정하고 이 오브젝트를 변환하면 Unity는 그것을 무시할 것.
        GetComponent <Rigidbody> ().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;

        // 2초 후에 파괴
        Destroy (gameObject, 2f);
    }
}
