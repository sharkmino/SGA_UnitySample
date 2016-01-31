using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public float restartDelay = 5.0f;


    Animator anim;
    float restartTimer;


    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("GameOver");
            restartTimer += Time.deltaTime;
            if(restartTimer >= restartDelay)
            {
                //Application.LoadLevel("Level01");
                
                // 같은 씬을 리로드하고 있다면, 그곳에 있는 LoadLevel 이라는
                // 바로가기를 이용하면 됨.
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }
}
