using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Rigidbody rb;
    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

	// 프레임을 렌더링하기 전에 호출
    // 여기에 대부분의 게임 코드를 적을것.
    void Update()
    {

    }

    // 물리 효과 계산 수행하기 직전에 호출되며 물리효과 코드를 적을 곳.
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);

    }
}
