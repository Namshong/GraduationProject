using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/* 사막에 있는 엘레베이터 코드 */
[RequireComponent(typeof(AudioSource))]
public class ElevatorControll : MonoBehaviour {

    public GameObject button1;                  // 버튼 0 ~ 9, ok, cancel
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

    public string secretNumber = "";            // 설정되있는 비밀번호
    public TextMesh text;                       // 스크린에 뜨는 번호

    public GameObject handle;                   // 손잡이 object
    public GameObject elevator;                 // 엘레베이터 object

    private AudioSource source = null;          // 클릭 소리
    private string screenNumber = "";           // 스크린에 적혀있는 번호
    private bool isLocked = true;               // 잠겨있는지 여부
    private bool isActive = false;              // 활성화 여부
    private int maxString = 4;                  // 비밀번호 길이

    private float rotSpeed = 50.0f;             // 손잡이 회전 속도
    private float moveSpeed = 10.0f;            // 엘레베이터 이동 속도
    private int moveDirection = 0;              // 엘레베이터 이동 방향

    /* 초기화 - 키패드 비밀번호 설정(오늘 날짜 + 전시회 날짜) */
    void Start()
    {
        source = GetComponent<AudioSource>();
        int currentDay = Convert.ToInt32(System.DateTime.Now.ToString("MMdd"));
        int exhibitionDay = 1122;

        secretNumber = (currentDay + exhibitionDay).ToString();
        Debug.Log("비밀번호: " + secretNumber);
    }

    /* 키패드 앞에서 trigger시 */
    private void OnTriggerStay(Collider other)
    {
        // 캐릭터가 키패드 앞에서 trigger중이고 F키를 눌렀을 때
        if (other.tag == "Player" && Input.GetKeyDown("f"))
        {
            isActive = true;    // 키패드 활성화
        }
    }

    /* 키패드와 trigger 종료시 */
    private void OnTriggerExit(Collider other)
    {
        // 키패드 open 되어있으면
        if(isLocked != false)
        {
            isActive = false;   // 키패드 비활성화
        }
    }

    /* 키패드 번호 눌렀을 경우 */
    void Clicked(GameObject obj)
    {
        obj.GetComponent<Renderer>().material.color = Color.cyan;
        source.Play();
        StartCoroutine(ReturnColor(obj, 0.1f));
        Debug.Log("button clicked");
    }

    void Update()
    {
        // 키패드 활성화 되있을 경우
        if (isActive)
        {   
            // 번호 0 ~ 9까지 눌렀을 경우
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
            
            // backspace와 enter 눌렀을 경우
            else if (Input.GetKeyDown("backspace"))
            {
                screenNumber = screenNumber.Substring(0, (screenNumber.Length - 1));
                cancel.GetComponent<Renderer>().material.color = Color.red;
                source.Play();
                StartCoroutine(ReturnColor(cancel, 0.1f));
                Debug.Log("cancel");
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("enter");
                ok.GetComponent<Renderer>().material.color = Color.green;
                source.Play();
                StartCoroutine(ReturnColor(ok, 0.1f));

                // 비밀번호 = 스크린번호일때 성공
                if (screenNumber == secretNumber)
                {
                    isLocked = false;
                    Debug.Log("sucess");
                }
                // 오답일 경우 실패
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
        // 입력한 숫자가 비밀번호 길이보다 길때 초기화
        if (screenNumber.Length > maxString)
        {
            screenNumber = "";
        }

        // 입력한 텍스트 화면에 띄움
        text.text = screenNumber;

        // 엘레베이터 목표 지점(위, 아래)
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

            // 엘레베이터가 젤 위층, 아래층 도달했을 경우 방향 바꿔줌
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

    /* 엘레베이터 방향 변경 */
    void ChangeDirection()
    {
        if(moveDirection == 0)
        {
            moveDirection = 1;
        }
        moveDirection = -moveDirection;

        elevator.transform.position = Vector3.MoveTowards(elevator.transform.position, elevator.transform.position + new Vector3(0f, moveDirection, 0f), moveSpeed*Time.deltaTime);
    }

    /* delay초만큼 이따가 버튼 색깔 다시 원상복귀 */
    IEnumerator ReturnColor(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.GetComponent<Renderer>().material.color = Color.white;
    }
}
