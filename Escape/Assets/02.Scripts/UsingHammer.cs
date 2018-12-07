using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsingHammer : MonoBehaviour {

    private GameObject keyGold;     // gold key object
    private Vector3 keyLocation;    // gold key 위치

    /* 초기설정 */
    private void Start()
    {
        keyGold = GameObject.FindGameObjectWithTag("key_gold");
        keyLocation = keyGold.transform.position;
    }

    /* Hammer이 object에 trigger중일 때 */
    private void OnTriggerStay(Collider other)
    {
        // trigger중인 object이 KeyTree 태그를 가지고 있고(열쇠가 존재할 수 있는 나무들에 태그 설정) F키를 눌렀을 때
        if (other.tag == "KeyTree" && Input.GetKeyDown("f"))
        {
            // gold key가 현재 나무에 있을 때
            if(other.transform.position.x == keyLocation.x && other.transform.position.z == keyLocation.z)
            {
                // gold key가 나무 밑으로 내려옴
                keyGold.transform.position = new Vector3(other.transform.position.x + 5, other.transform.position.y + 3, other.transform.position.z + 5);
            }
        }
    }
}
