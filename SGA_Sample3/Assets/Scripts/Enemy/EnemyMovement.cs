using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    NavMeshAgent nav;


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent <NavMeshAgent> ();
    }


    // FixedUpdate가 아닌 Update 함수를 사용하는데 유의
    // 이것은 Nav Mesh Agent이므로 물리 효과를 시간에 따라 맞추지 않음.
    void Update ()
    {
        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            // nav는 Nav Mesh Agent가 되고 우리는
            // '이봐. 저기가 내가 가려는 곳이야'
            // '난 플레이어 쪽으로 가길 원해'
            // 라고 명령하게 되는것.
            // 이 방식이 좋은 점은 적들이 서로 부딪히지 않고
            // 서로 간섭하지 않으며 느릿느릿한 아기 팔과 레고를 이용해
            // 돌아다니고 씬에서 설정한 섬뜩한 분위기를 연출하며
            // 매정하고 정확하게 공격한다는 점.
            nav.SetDestination (player.position);
        }
        else
        {
            nav.enabled = false;
        }
    }
}
