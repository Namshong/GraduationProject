using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsingHammer : MonoBehaviour {

    private GameObject keyGold;
    private Vector3 keyLocation;

    private void Start()
    {
        keyGold = GameObject.FindGameObjectWithTag("key_gold");
        keyLocation = keyGold.transform.position;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "KeyTree" && Input.GetKeyDown("f"))
        {
            if(other.transform.position.x == keyLocation.x && other.transform.position.z == keyLocation.z)
            {
                keyGold.transform.position = new Vector3(other.transform.position.x + 5, other.transform.position.y + 3, other.transform.position.z + 5);
            }
        }
    }
}
