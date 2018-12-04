using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitCave : MonoBehaviour {

    private Vector3 exitPos;
    private GameObject player;

    public Material skybox;

    GameObject mainLight;
    GameObject caveLight;

    void Start () {
        mainLight = GameObject.FindGameObjectWithTag("DirectionalLight");
        caveLight = GameObject.FindGameObjectWithTag("CaveLight");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            exitPos = GameObject.FindGameObjectWithTag("CaveExit").transform.position;
            player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = exitPos;
            RenderSettings.skybox = skybox;
            mainLight.SetActive(true);
            caveLight.SetActive(false);
        }
    }
}
