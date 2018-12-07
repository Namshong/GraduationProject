using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 게임 종료하는 script, exit module이란 gameobject에서 관리  */
public class ExitFromGame : MonoBehaviour {

    private int keyInsertNum = 0;       // 삽입된 열쇠 갯수
    private int finalKeyNum = 3;        // 최종 열쇠 갯수

    private EquipmentManager equipManager;      // EquipManager : 사용한 아이템은 inventory에서 삭제해야하므로 필요
	
	void Start () {
        equipManager = EquipmentManager.instance;
	}

    private void OnTriggerStay(Collider other)
    {
        /* trigger 중인 아이템이 gold key이고 f키를 눌렀을 경우 */
        if(other.CompareTag("key_gold") && Input.GetKeyDown("f"))
        {
            // 아이템을 exit module의 자식으로 둠
            other.transform.parent = this.transform;

            // 아이템을 exit module에 적절히 위치시킴
            other.transform.position = this.transform.position + new Vector3(2.95f,-0.83f,-0.91f);
            other.transform.Rotate(0, 0, -66);

            // 삽입된 열쇠 갯수 늘림
            keyInsertNum++;

            // equipManager을 통해 사용한 아이템 인벤토리에서 삭제
            equipManager.UsedItem(1);
            Debug.Log("goldKey inserted.");
        }


        /* trigger 중인 아이템이 silver key이고 f키를 눌렀을 경우 */
        if (other.CompareTag("key_silver") && Input.GetKeyDown("f"))
        {
            other.transform.parent = this.transform;
            other.transform.position = this.transform.position + new Vector3(-2.06f, 0.82f,-2.12f);
            other.transform.Rotate(6, -19, 106);
            keyInsertNum++;
            equipManager.UsedItem(1);
            Debug.Log("silverKey inserted.");
        }

        /* trigger 중인 아이템이 purple key이고 f키를 눌렀을 경우 */
        if (other.CompareTag("key_purple") && Input.GetKeyDown("f"))
        {
            other.transform.parent = this.transform;
            other.transform.position = this.transform.position + new Vector3(-1.14f,0f,2.79f);
            other.transform.Rotate(176,-90,-64);
            keyInsertNum++;
            equipManager.UsedItem(1);
            Debug.Log("purpleKey inserted.");
        }
    }

    private void Update()
    {
        /* 모든 열쇠 다 삽입했을 경우 */
        if(keyInsertNum == finalKeyNum)
        {
            Debug.Log("Congratulations! you've won the game.");
            // 축하한다는 글귀 화면에 띄우기

            // 프로그램 종료
            Quit();
        }
    }

    private void Quit()
    {
        // 유니티 에디터일 경우 플레이 모드를 정지
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;

        // 그 외의 경우 프로그램 종료
        #else
            Application.Quit();
        #endif
    }
}
