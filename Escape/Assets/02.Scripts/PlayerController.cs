using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float inputH;                   // 수평 input(왼쪽, 오른쪽)
    private float inputV;                   // 수직 input(앞, 뒤)
    private bool isRunning;                 // 달리는 중인지 여부
    private bool isJumping;                 // 점프중인지 여부
    private GameObject firstPersonCam;      // 1인칭 카메라
    private GameObject thirdPersonCam;      // 3인칭 카메라
    private bool isToggling;                // 카메라 시점 어떤건지 여부
    private float animSpeed = 1.5f;         // 애니메이션 재생 속도
    private float groundDistance = 2.0f;    // 땅까지 거리
    private bool isGrounded = true;         // 땅에 닿아있는지 여부

    public float speed = 5.0f;              // 걷는 속도
    public float runningSpeed = 15.0f;      // 달리는 속도
    public float rotateSpeed = 2.0f;        // 회전 속도
    public float jumpPower = 1.0f;          // 점프하는 힘
    public Animator animator;
    public Rigidbody rbody;                

    void Start()
    {
        animator = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
        isRunning = false;
        rbody.useGravity = true;

        firstPersonCam = GameObject.FindWithTag("FirstPersonCamera");
        Debug.Log(firstPersonCam.name);
        thirdPersonCam = GameObject.FindWithTag("ThirdPersonCamera");
        Debug.Log(thirdPersonCam.name);
        isToggling = true;
    }

    // Update is called once per frame
    void Update()
    {
        inputH = Input.GetAxis("Horizontal");       // A,D키 눌렀을 때 받아오는 -1~1값
        inputV = Input.GetAxis("Vertical");         // W,S키 눌렀을 때 받아오는 -1~1값

        // 1~4 눌렀을 때 애니메이션 동작
        if (Input.GetKeyDown("1"))
        {
            animator.Play("WAIT01", -1, 0f);
        }

        if (Input.GetKeyDown("2"))
        {
            animator.Play("WAIT02", -1, 0f);
        }

        if (Input.GetKeyDown("3"))
        {
            animator.Play("WAIT03", -1, 0f);
        }

        if (Input.GetKeyDown("4"))
        {
            animator.Play("WAIT04", -1, 0f);
        }

        // 왼쪽 shift누르고 있을 경우 달리기
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }

        // space키 누를때 점프
        if (Input.GetKey(KeyCode.Space))
        {
            isJumping = true;
        }

        animator.SetFloat("inputH", inputH);
        animator.SetFloat("inputV", inputV);
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", isJumping);

        // c키를 누를 경우 1인칭, 3인칭 전환
        if (Input.GetKeyDown("c"))
        {
            if (isToggling == false)
            {
                firstPersonCam.SetActive(true);
                thirdPersonCam.SetActive(false);
            }
            else
            {
                thirdPersonCam.SetActive(true);
                firstPersonCam.SetActive(false);
            }
            isToggling = !isToggling;
        }

    }

    void FixedUpdate()
    {
        // 애니메이션 스피드 변경
        animator.speed = animSpeed;

        CamToggle();
        Running();
        Jumping();

        RaycastHit hit;
        Ray ray = new Ray(transform.position, -Vector3.up);

        // raycast 이용하여 땅까지 거리 측정
        if(Physics.Raycast(ray, out hit))
        {
            Debug.Log("땅까지 거리 : " + hit.distance);
            // 땅에 있음
            if(hit.distance - 0.05f <= groundDistance)
            {
                isGrounded = true;
            }
            // 점프 중임
            else
            {
                isGrounded = false;
            }
        }
    }

    /* 카메라 전환 */
    void CamToggle()
    {
        //1인칭일 때
        if (isToggling == true)
        {
            transform.Translate(Vector3.right * inputH * 0.4f, Space.Self);
        }
        // 3인칭일 때
        else
        {
            transform.Rotate(0, inputH * rotateSpeed, 0);
        }
    }

    /* 달리기 */
    void Running()
    {
        Vector3 movement = new Vector3(inputH, 0.0f, inputV);

        // 걷고 있을때
        if (!isRunning)
        {
            transform.Translate(movement * speed * Time.fixedDeltaTime, Space.Self);
        }
        // 달릴때
        else
        {
            transform.Translate(movement * runningSpeed * Time.fixedDeltaTime, Space.Self);
            isRunning = false;
        }
    }

    /* 점프 */
    void Jumping()
    {
        if (!isJumping) return;

        if (isGrounded)
        {
            rbody.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
        }
        isJumping = false;
    }
}