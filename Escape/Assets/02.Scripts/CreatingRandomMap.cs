using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatingRandomMap : MonoBehaviour {
    private Vector3 desertLocation;     // 사막맵 위치
    private Vector3 forestLocation;     // 숲맵 위치
    private Vector3 caveLocation;       // 동굴맵 위치

    private GameObject[] keyTreeObjects;    // GoldKey가 위치할 수 있는 나무를 저장하는 배열

    List<int> arr = new List<int> { -2000, 0, 2000 };   // 사막맵, 숲맵, 동굴맵이 위치할 x값 리스트

    float newX1, newX2, newX3;

	// Use this for initialization
	void Start () {

        /* 랜덤으로 맵 섞기 */
        // 첫번째 요소 랜덤 뽑기
        int choice = Random.Range(0, 3);

        newX1 = arr[choice];
        arr.RemoveAt(choice);
        choice = Random.Range(0, 2);
        newX2 = arr[choice];
        newX3 = arr[1 - choice];

        print(newX1);
        print(newX2);
        print(newX3);

        forestLocation.Set(newX1, 0, 0);    
        desertLocation.Set(newX2, 0, 0);    
        caveLocation.Set(newX3, 0, 0);      

        // 숲맵 위치 설정
        GameObject.FindGameObjectWithTag("ForestMap").transform.position = forestLocation;
        // 사막맵 위치 설정
        GameObject.FindGameObjectWithTag("DesertMap").transform.position = desertLocation;
        // 동굴맵 위치 설정
        GameObject.FindGameObjectWithTag("CaveMap").transform.position = caveLocation;
        

        /* ForestMap에서 최종키 랜덤한 나무에 숨기기 */
        //KeyTree 태그를 가진 게임오브젝트들을 찾아 배열에 저장
        keyTreeObjects = GameObject.FindGameObjectsWithTag("KeyTree");
        choice = Random.Range(0, 3);
        Vector3 keyTreeLocation = keyTreeObjects[choice].transform.position;

        print(keyTreeLocation);
        // GoldKey를 찾고 나무의 위치보다 높게 위치시킴 (Vector3.up은 높이를 의미)
        GameObject.FindGameObjectWithTag("key_gold").transform.position = keyTreeLocation + Vector3.up*16;


        /* 키의 위치에 따라 표지판 회전 */
        // 나무 표지판 찾아 변수에 저장
        GameObject forestWSObj = GameObject.FindGameObjectWithTag("ForestWoodSign");
        Vector3 forestWS = forestWSObj.transform.position;

        // 아크탄젠트 사용하여 얼마만큼 회전해야하는지 각도 계산
        float radians = Mathf.Atan2(keyTreeLocation.z - forestWS.z, keyTreeLocation.x - forestWS.x);
        float angle = radians * (180 / Mathf.PI);

        // 나무 표지판 회전시킴
        forestWSObj.transform.Rotate(Vector3.up * angle * (-1));
    }
}
