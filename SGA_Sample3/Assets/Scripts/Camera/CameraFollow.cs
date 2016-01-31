using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float smoothing = 5.0f;

    Vector3 offset;

	// Use this for initialization
	void Start () {
        // offset은 카메라에서 플레이어까지의 벡터이므로
        // 작성한 스크립트는 카메라에 적용할 스크립트
        // transform.position : 카메라의 위치
        // target.position : 플레이어의 위치
        offset = transform.position - target.position;	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // 모든 물리 효과 단계
    // Update 를 사용하게 된다면 플레이어와 다른 시간대에 움직이게 됨.
    void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset;
        // Time.deltaTime : 초당 50회를 실행되는 것을 원하지 않기 위해 사용
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
