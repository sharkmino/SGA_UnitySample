using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6.0f;

    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidBody;
    int floorMask;
    float camRayLength = 100.0f;

    // Start함수와 유사하지만
    // 스크립트의 활성여부와 상관없이 호출
    // 따라서 참조 설정 등에 유리
    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody>();
    }

    // Unity가 자동으로 호출하는 함수
    // 모든 Physics Update를 유발
    // 일반적인 Update() 함수는 렌더링과(렌더링 전) 함께 실행
    // FixedUpdate()는 물리 효과와 함께 실행
    void FixedUpdate()
    {
        // Input.GetAxis        : -1 ~ 1사이의 값을 가짐
        // Input.GetAxisRaw     : -1, 0, 1의 값만 가짐 
        // 그러므로 캐릭터를 컨트롤할때 최고 속도로 서서히 가속하는 것이 아니라
        // 곧바로 최고 속도에 도달하므로 훨씬 반응성이 우수
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
        Turning();
        Animating(h, v);

        // 축은 실제로 입력
        // horizontal, vertical, jump, fire1, fire2 등은 Unity에서 기본 설정
        // Edit -> Project Setting -> Input에서 확인 가능
    }

    void Move(float h, float v)
    {
        movement.Set(h, 0.0f, v);

        // Time.deltaTime 곱하는 이유 : 초단위가 되도록 변경하기 위해서
        // 곱하지 않았다면 1/50초당 6을 이동.
        // deltaTime은 각 Update 호출의 시간간격.
        movement = movement.normalized * speed * Time.deltaTime;

        playerRigidBody.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        // 스크린에서 포인트를 잡아 그 포인트에서 앞쪽의 장면으로 Ray를 쏘는 것. 
        // 따라서 우리가 지정하려는 포인트는 마우스의 위치.
        // 그러므로 이 함수는 언제나 마우스 밑의 포인트를 찾으려 할 것.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        // 스크린의 마우스가 있고, 마우스 밑의 포인트는 플로어 쿼드를 맞히면 찾을 수 있는 포인트

        // 이 레이캐스트를 실행하면 정보를 되찾아야 하고
        // 이 레이캐스트에서 정보를 되찾으려면 레이캐스트를 실행해야함.
        RaycastHit floorHit;

        // Ray가 무언가를 맞힐 수 있도록 Ray를 쏘는 작업을 수행
        // Raycast 함수는 무언가를 맞힌 경우 참을 반환하고 그렇지 않은 경우 거짓을 반환

        // 첫번째 파라미터 : 광선 자체. 이것은 우리가 가지려는 캐스트의 위치와 방향
        // 두번째 파라미터 : 히트를 위해 out 키워드를 이용
        // 세번째 파라미터 : 레이캐스트 길이
        // 네번째 파라미터 : 이 레이캐스트가 플로어 레이어에서만 맞히기를 시도하는지 확인

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            // floorHit.point : 플로어를 맞힌 포인트
            // transform.position : 플레이어의 위치
            Vector3 playerToMouse = floorHit.point - transform.position;

            // 이것을 이용해 캐릭터에 적용해 캐릭터가 방향을 전활할 수 있도록 함.
            // 뒤로 기댄 상태에서 시작할 것은 아니므로 이 벡터의 Y 컴포넌트가 0이 되도록 해야함.
            playerToMouse.y = 0.0f;

            // 이제 벡터를 기준으로 플레이어의 회전을 설정할 수 없으므로
            // 벡터에서 쿼터니온으로 변경해야함.
            // 기본적으로 Quaternion은 회전을 저장하는 방식
            // Vector3가 있지만 이를 이용해 회전을 저장할수 없으므로 Quaternion을 이용하며 
            // newRotation이라는 Quaternion을 생성

            // LookRotation 역할 수행
            // Unity와 대부분의 3D모델링에서 캐릭터와 카메라 등은 
            // Z 축이 전진축으로 기본 설정되어 있음
            // 따라서 playerToMouse 벡터를 플레이어의 전진 벡터로 지정하려고 함.
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidBody.MoveRotation(newRotation);
        }
    }

    void Animating(float h, float v)
    {
        bool walking = h != 0.0f || v != 0.0f;
        anim.SetBool("IsWalking", walking);
    }
}
