using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour {
    private Transform firstCamera;  // 1인칭 카메라

    private float horizontal = 0.0f;
    private float vertical = 0.0f;

    public float moveSpeed = 10.0f; // 카메라 속도

    bool isJumping = false;
	// Use this for initialization
	void Start () {
        firstCamera = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
            isJumping = true;
    }

    void FixedUpdate()
    {
        Vector3 moveDirection = (Vector3.forward * vertical) + (Vector3.right * horizontal);
        firstCamera.Translate(moveDirection.normalized * Time.deltaTime * moveSpeed, Space.World);

        if (isJumping)
        {

        }
    }
}
