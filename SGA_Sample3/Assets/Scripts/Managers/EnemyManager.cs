using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;


    void Start ()
    {
        // 기본적으로 이것은 반복되는 어떤 것을 위해 타이머를 가질
        // 필요가 없음을 의미
        // 첫번째 파라미터 : Spawn 함수 호출
        // 두번째 파라미터 : 실행전 대기시간
        // 세번째 파라미터 : 반복전 대기시간  

        // 3초에 한번씩 Spawn 함수 호출
        InvokeRepeating ("Spawn", spawnTime, spawnTime);
    }


    void Spawn ()
    {
        if(playerHealth.currentHealth <= 0f)
        {
            return;
        }

        int spawnPointIndex = Random.Range (0, spawnPoints.Length);

        /// <summary>
        /// 첫번째 파라미터 : 대상
        /// 두번째 파라미터 : 위치 
        /// 세번째 파라미터 : 회전
        /// </summary>
        Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
