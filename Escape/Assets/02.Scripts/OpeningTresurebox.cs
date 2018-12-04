using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningTresurebox : MonoBehaviour {

    public Animator anim;

    private Vector3[] treasureLoc = new[] { new Vector3(2768f, 30f, 246f), new Vector3(2617f,30f, 710f), new Vector3(3487f,30f, 1491.67f) };
    private bool isActive = false;
    private Transform currentPos;

	// Use this for initialization
	void Start () {
        anim.SetBool("isActive", false);

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

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKeyDown("f"))
        {
            isActive = true;
            anim.SetBool("isActive", isActive);
        }
    }
}
