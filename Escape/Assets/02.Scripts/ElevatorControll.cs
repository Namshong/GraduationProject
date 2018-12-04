using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ElevatorControll : MonoBehaviour {

    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject button5;
    public GameObject button6;
    public GameObject button7;
    public GameObject button8;
    public GameObject button9;
    public GameObject button0;
    public GameObject cancel;
    public GameObject ok;
    public string secretNumber = "";
    public AudioClip clickSound;
    public TextMesh text;
    public GameObject handle;
    public GameObject elevator;

    private AudioSource source = null;
    private string screenNumber = "";
    private bool isLocked = true;
    private bool isActive = false;
    private int maxString = 4;
    private float myTime = 0.0f;
    private float nextColor = 10.0f;
    private float rotSpeed = 50.0f;
    private float moveSpeed = 10.0f;
    private int moveDirection = 0;
     
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKeyDown("f"))
        {
            isActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // open 되어있으면
        if(isLocked != false)
        {
            isActive = false;
        }
    }

    void Clicked(GameObject obj)
    {
        obj.GetComponent<Renderer>().material.color = Color.cyan;
        source.Play();
        if (myTime > nextColor)
        {
            obj.GetComponent<Renderer>().material.color = Color.white;
            Debug.Log("button clicked");
        }
    }

    void Start()
    {
        source = GetComponent<AudioSource>();
        int currentDay = Convert.ToInt32(System.DateTime.Now.ToString("MMdd"));
        int exhibitionDay = 1122;

        secretNumber = (currentDay + exhibitionDay).ToString();
        Debug.Log("비밀번호: " + secretNumber);
    }

    void Update()
    {
        myTime = myTime + Time.deltaTime;

        if (isActive)
        {
            if (Input.GetKeyDown("1"))
            {
                screenNumber = screenNumber + "1";
                Clicked(button1);
            }
            else if (Input.GetKeyDown("2"))
            {
                screenNumber = screenNumber + "2";
                Clicked(button2);
            }
            else if (Input.GetKeyDown("3"))
            {
                screenNumber = screenNumber + "3";
                Clicked(button3);
            }
            else if (Input.GetKeyDown("4"))
            {
                screenNumber = screenNumber + "4";
                Clicked(button4);
            }
            else if (Input.GetKeyDown("5"))
            {
                screenNumber = screenNumber + "5";
                Clicked(button5);
            }
            else if (Input.GetKeyDown("6"))
            {
                screenNumber = screenNumber + "6";
                Clicked(button6);
            }
            else if (Input.GetKeyDown("7"))
            {
                screenNumber = screenNumber + "7";
                Clicked(button7);
            }
            else if (Input.GetKeyDown("8"))
            {
                screenNumber = screenNumber + "8";
                Clicked(button8);
            }
            else if (Input.GetKeyDown("9"))
            {
                screenNumber = screenNumber + "9";
                Clicked(button9);
            }
            else if (Input.GetKeyDown("0"))
            {
                screenNumber = screenNumber + "0";
                Clicked(button0);
            }
            else if (Input.GetKeyDown("backspace"))
            {
                screenNumber.Remove(screenNumber.Length - 1, 1);
                cancel.GetComponent<Renderer>().material.color = Color.red;
                source.PlayOneShot(clickSound, 0.9f);
                cancel.GetComponent<Renderer>().material.color = Color.white;
                Debug.Log("cancel");
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("enter");
                ok.GetComponent<Renderer>().material.color = Color.green;
                source.PlayOneShot(clickSound, 0.9f);
                ok.GetComponent<Renderer>().material.color = Color.white;

                if (screenNumber == secretNumber)
                {
                    isLocked = false;
                    Debug.Log("sucess");
                }
                else
                {
                    screenNumber = "";
                    Debug.Log("failed");
                }
            }
        }

    }

    private void FixedUpdate()
    {
        if (screenNumber.Length > maxString)
        {
            screenNumber = "";
        }
        text.text = screenNumber;

        Vector3 targetPositionDown = new Vector3(3300.7f,-357f,1178.8f);
        Vector3 targetPositionUp = new Vector3(3300.7f, 25f, 1178.8f);

        // open 됬을 때
        if (isActive == true && isLocked == false)
        {
            // handle 회전
            if (handle.transform.localEulerAngles.z >= 330 || handle.transform.localEulerAngles.z <=30)
            {
                handle.transform.Rotate(Vector3.forward * Time.deltaTime * rotSpeed);
                Debug.Log(handle.transform.localEulerAngles.z);
            }

            if (elevator.transform.position.y == 25 || elevator.transform.position.y == -357) Invoke("ChangeDirection", 2);

            // elevator 이동
            // 위로 올라감
            if (moveDirection == 1 && elevator.transform.position.y < 25)
            {
                elevator.transform.position = Vector3.MoveTowards(elevator.transform.position, targetPositionUp, moveSpeed * Time.deltaTime);
            }

            // 아래로 내려감
            if (moveDirection == -1 && elevator.transform.position.y > -357 )
            {
                elevator.transform.position = Vector3.MoveTowards(elevator.transform.position, targetPositionDown, moveSpeed * Time.deltaTime);
            }
        }
    }

    void ChangeDirection()
    {
        if(moveDirection == 0)
        {
            moveDirection = 1;
        }
        moveDirection = -moveDirection;

        elevator.transform.position = Vector3.MoveTowards(elevator.transform.position, elevator.transform.position + new Vector3(0f, moveDirection, 0f), moveSpeed*Time.deltaTime);
    }
}
