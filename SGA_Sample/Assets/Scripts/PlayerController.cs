using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Rigidbody rb;
    public Text countText;
    public Text winText;

    public float speed;
    private int count;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
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

    // 게임 오브젝트가 트리거 콜라이더를 처음 접촉하면
    // Unity로부터 호출
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            ++count;
            SetCountText(); 
        }

    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 14)
            winText.text = "You Win!";
    }
}
