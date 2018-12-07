using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 1인칭 카메라 코드 */
public class FPController : MonoBehaviour {

    Vector2 mouseLook;      // 카메라가 움직이는 정도를 담는 벡터
    Vector2 smoothVector;   // 커서가 움직이는 정도를 담는 벡터

    public float sesitivity = 5.0f;     // 마우스 민감도
    public float smoothing = 3.0f;      // 마우스 부드러운 정도

    private bool isLocked;  // 마우스 커서 잠금 여부

    GameObject player;
	
	void Start () {
        player = this.transform.parent.gameObject;
        Cursor.lockState = CursorLockMode.Locked;
        isLocked = true;
	}
	
	// Update is called once per frame
	void Update () {

        if (isLocked)
        {
            // 커서 움직이는 정도
            Vector2 mousedelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            mousedelta = Vector2.Scale(mousedelta, new Vector2(sesitivity, sesitivity));

            // Lerp : 선형보간을 이용하여 smoothing정도에 따라 마우스 움직임의 크기를 조절
            smoothVector.x = Mathf.Lerp(smoothVector.x, mousedelta.x, 1f / smoothing);
            smoothVector.y = Mathf.Lerp(smoothVector.y, mousedelta.y, 1f / smoothing);

            // 마우스커서의 위치에 smoothVector 축적
            mouseLook += smoothVector;

            /* 위아래로 90도 이상 볼 수 없게 제한 */
            if(Mathf.Abs(mouseLook.y) > 90)
            {
                mouseLook.y *= 90 / Mathf.Abs(mouseLook.y);
            }

            /* mouseLook을 직접 카메라와 캐릭터에 적용 */
            transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
            player.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, player.transform.up);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            // 마우스 커서가 잠겨 있을 때
            if (isLocked)
            {
                Cursor.lockState = CursorLockMode.None;
                isLocked = false;
            }
            // 마우스 커서가 잠겨 있지 않을 때
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                isLocked = true;
            }
        }
    }
}
