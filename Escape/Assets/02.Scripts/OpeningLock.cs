using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 산장에 있는 키패드 코드 */
[RequireComponent(typeof(AudioSource))]
public class OpeningLock : MonoBehaviour {

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

    public string secretNumber = "302454";      // 설정되있는 비밀번호
    public TextMesh text;                       // 스크린에 뜨는 번호
    public Animator anim;                       // 산장 문 열리는 애니메이션

    private AudioSource source = null;          // 클릭 소리
    private string screenNumber = "";           // 스크린에 적혀있는 번호
    private bool isLocked = true;               // 잠겨있는지 여부
    private bool isActive = false;              // 활성화 여부
    private int maxString = 6;                  // 비밀번호 길이
    private Transform doorTr;                   // 문의 Transform

    void Start()
    {
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        doorTr = GetComponent<Transform>();
    }

    /* 문앞에서 trigger시 */
    private void OnTriggerStay(Collider other)
    {
        // 캐릭터가 문과 trigger중이고 F키를 눌렀을 때
        if (other.tag == "Player" && Input.GetKeyDown("f"))
        {
            isActive = true;    // 키패드 활성화
        }
    }

    /* 문과 trigger 종료시 */
    private void OnTriggerExit(Collider other)
    {
        isActive = false;       // 키패드 비활성화
    }

    /* 키패드 번호 눌렀을 경우 */
    void Clicked(GameObject obj)
    {
        obj.GetComponent<Renderer>().material.color = Color.cyan;
        source.Play();
        StartCoroutine(ReturnColor(obj, 0.1f));
        Debug.Log("button clicked");
    }
    
    // Update is called once per frame
    void Update () {

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
                screenNumber = screenNumber.Substring(0,(screenNumber.Length - 1));
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
        if(screenNumber.Length > maxString)
        {
            screenNumber = "";
        }

        // 입력한 텍스트 화면에 띄움
        text.text = screenNumber;

        // 문이 활성화 되어있고 열렸을 때
        if(isActive == true && isLocked == false)
        {
            // 문열리는 애니메이션 실행
            anim.SetBool("isLocked", isLocked);

            // 열리고 문을 회전시킴
            Invoke("DoorRotation", 4);
        }
    }

    // 문 회전하는 함수
    void DoorRotation()
    {
        // 문을 이동시킴
        doorTr.position = new Vector3(10.78f, 3.42f, 7.17f);
        doorTr.rotation = Quaternion.Euler(0, -90, -90);

        // 문 비활성화 시킴
        isActive = false;
    }

    IEnumerator ReturnColor(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.GetComponent<Renderer>().material.color = Color.white;
    }
}
