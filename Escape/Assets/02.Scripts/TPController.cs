using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 3인칭 카메라 코드 */
public class TPController : MonoBehaviour {
    public Transform player;            // 캐릭터 위치
    public float dist = 5.0f;           // 캐릭터와의 거리
    public float height = 1.0f;         // 캐릭터와의 높이
    public float dampTrace = 20.0f;     // 카메라 이동 조절 상수

    private bool isLocked;              // 마우스 커서 잠금 여부
    private Transform tr;               // 카메라 위치
    private Vector3 center;             // 화면의 중심으로 둘 지점

	// Use this for initialization
	void Start () {
        isLocked = false;
        tr = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // 잠겨 있을 때
            if (isLocked)
            {
                Cursor.lockState = CursorLockMode.None;
                isLocked = false;
            }
            // 잠겨 있지 않을 때
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                isLocked = true;
            }
        }
    }

    // player의 이동이 완료된 후 카메라가 추적하기위해 LateUpdate 사용
    private void LateUpdate()
    {
        // 카메라가 바라보는 center을 캐릭터 위치보다 조금 높게 설정
        center = player.position + Vector3.up * 4;

        // 카메라 위치를 center보다 dist만큼 뒤로, height만큼 위로 올림
        tr.position = Vector3.Lerp(tr.position, center - (player.forward * dist) + (Vector3.up * height),
            Time.deltaTime * dampTrace);

        // 카메라가 center을 보게 함
        tr.LookAt(center);
    }
}
