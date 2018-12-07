using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 동굴 입구 들어가는 코드 */
public class EntranceCave : MonoBehaviour {

    public Material skybox;             // 동굴 내부를 구성하는 skybox

    private Vector3 entrancePos;        // 동굴 내부에 있는 입구 위치
    GameObject mainLight;               // 동굴 외부 빛
    GameObject caveLight;               // 동굴 내부 빛
    GameObject player;                  // 캐릭터

    /* 초기 설정 */
    private void Start()
    {
        mainLight = GameObject.FindGameObjectWithTag("DirectionalLight");
        caveLight = GameObject.FindGameObjectWithTag("CaveLight");
        caveLight.SetActive(false);
    }
   
    /* 동굴 입구에 도달했을 때 */
    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 collider이 캐릭터일때
        if(other.tag == "Player")
        {
            // 동굴 내부 위치 받고 캐릭터 위치를 동굴 내부로 이동시킴
            entrancePos = GameObject.FindGameObjectWithTag("CaveEntrance").transform.position;
            player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = entrancePos;

            // 동굴 내부구성하는 skybox로 변경
            RenderSettings.skybox = skybox;

            // 동굴내부이므로 빛을 어둡게 함
            mainLight.SetActive(false);
            caveLight.SetActive(true);
        }
    }
}
