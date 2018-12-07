using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 동굴밖 나가는 코드 */
public class ExitCave : MonoBehaviour {

    private Vector3 exitPos;        // 동굴 밖에 있는 출구 위치
    private GameObject player;      // 캐릭터

    public Material skybox;         // 동굴 밖을 구현하고 있는 skybox

    GameObject mainLight;           // 동굴 밖을 구성하는 빛
    GameObject caveLight;           // 동굴 내부를 구성하는 빛

    void Start () {
        mainLight = GameObject.FindGameObjectWithTag("DirectionalLight");
        caveLight = GameObject.FindGameObjectWithTag("CaveLight");
    }

    /* 동굴 내에 있는 출구에 다가갔을 때 */
    private void OnTriggerEnter(Collider other)
    {
        // 다가간 object이 캐릭터였을 경우
        if (other.tag == "Player")
        {
            // 출구 위치를 받고 player을 출구 위치로 이동시킴 
            exitPos = GameObject.FindGameObjectWithTag("CaveExit").transform.position;
            player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = exitPos;

            // 동굴 밖을 구성하고 있는 skybox로 변경
            RenderSettings.skybox = skybox;

            // 동굴 바깥처럼 환한 빛으로 교체
            mainLight.SetActive(true);
            caveLight.SetActive(false);
        }
    }
}
