using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceCave : MonoBehaviour {

    private Vector3 entrancePos;

    public Material skybox;
    public float intensity = 0.1f;
    GameObject mainLight;
    GameObject caveLight;
    GameObject player;

    private void Start()
    {
        mainLight = GameObject.FindGameObjectWithTag("DirectionalLight");
        caveLight = GameObject.FindGameObjectWithTag("CaveLight");
        caveLight.SetActive(false);
    }
    // Use this for initialization
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            entrancePos = GameObject.FindGameObjectWithTag("CaveEntrance").transform.position;
            player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = entrancePos;
            RenderSettings.skybox = skybox;
            mainLight.SetActive(false);
            caveLight.SetActive(true);
        }
    }
}
