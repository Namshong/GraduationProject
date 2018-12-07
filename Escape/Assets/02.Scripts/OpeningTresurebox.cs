using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 보물상자 여는 코드 */
public class OpeningTresurebox : MonoBehaviour {

    public Animator anim;           // 보물상자 열리는 애니메이션

    // 보물상자가 있을 위치 배열
    private Vector3[] treasureLoc = new[] { new Vector3(2768f, 30f, 246f), new Vector3(2617f,30f, 710f), new Vector3(3487f,30f, 1491.67f) };
    private bool isActive = false;      // 보물상자 활성화 여부
    private Transform currentPos;       // 보물상자 현재 위치

	/* 초기화 구성 - 보물상자 위치 선정과 그 위치에 따른 나무 표지판 회전 */
	void Start () {
        anim.SetBool("isActive", false);

        /* 보물상자 위치를 랜덤으로 선택하여 설정 */
        currentPos = GetComponent<Transform>();
        int choice = Random.Range(0, 3);
        currentPos.position = treasureLoc[choice];

        /* 보물상자의 위치에 따라 표지판 회전 */
        GameObject desertWSObj = GameObject.FindGameObjectWithTag("DesertWoodSign");
        Vector3 desertWS = desertWSObj.transform.position;
        float radians = Mathf.Atan2(currentPos.position.z - desertWS.z, currentPos.position.x - desertWS.x);
        float angle = radians * (180 / Mathf.PI);
        desertWSObj.transform.Rotate(Vector3.up * angle * (-1));
    }

    /* 보물상자와 trigger있을 때 */
    private void OnTriggerStay(Collider other)
    {
        // trigger중인 대상이 캐릭터이고 F키 눌렀을 대
        if (other.tag == "Player" && Input.GetKeyDown("f"))
        {
            // 보물상자 활성화
            isActive = true;

            // 보물상자 열리는 애니매이션 실행
            anim.SetBool("isActive", isActive);
        }
    }
}
