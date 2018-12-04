using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitFromGame : MonoBehaviour {

    private int keyInsertNum = 0;
    private int finalKeyNum = 3;

    private EquipmentManager equipManager;
	// Use this for initialization
	void Start () {
        equipManager = EquipmentManager.instance;
	}

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("key_gold") && Input.GetKeyDown("f"))
        {
            other.transform.parent = this.transform;
            other.transform.position = this.transform.position + new Vector3(2.95f,-0.83f,-0.91f);
            other.transform.Rotate(0, 0, -66);
            keyInsertNum++;
            equipManager.UsedItem(1);
            Debug.Log("goldKey inserted.");
        }

        if (other.CompareTag("key_silver") && Input.GetKeyDown("f"))
        {
            other.transform.parent = this.transform;
            other.transform.position = this.transform.position + new Vector3(-2.06f, 0.82f,-2.12f);
            other.transform.Rotate(6, -19, 106);
            keyInsertNum++;
            equipManager.UsedItem(1);
            Debug.Log("silverKey inserted.");
        }

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
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
