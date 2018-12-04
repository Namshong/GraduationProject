using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class OpeningLock : MonoBehaviour {

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
    public string secretNumber = "302454";
    public AudioClip clickSound;
    public TextMesh text;
    public Animator anim;

    private AudioSource source = null;
    private string screenNumber = "";
    private bool isLocked = true;
    private bool isActive = false;
    private int maxString = 6;
    private Transform tr;
    private float myTime = 0.0f;
    private float nextColor = 10.0f;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKeyDown("f"))
        {
            isActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isActive = false;
    }

    void Clicked(GameObject obj)
    {
        obj.GetComponent<Renderer>().material.color = Color.cyan;
        source.Play();
        if(myTime > nextColor)
        {
            obj.GetComponent<Renderer>().material.color = Color.white;
            Debug.Log("button clicked");
        }
    }
    
    //IEnumerator Clicked(GameObject obj)
    //{
    //    obj.GetComponent<Renderer>().material.color = Color.cyan;
    //    source.Play();
    //    yield return new  WaitForSeconds(0.8f);
    //    obj.GetComponent<Renderer>().material.color = Color.white;
    //    Debug.Log("button clicked");
    //}

    //void BackToColor(GameObject obj)
    //{
    //    obj.GetComponent<Renderer>().material.color = Color.white;
    //    Debug.Log("button clicked");
    //}

    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        tr = GetComponent<Transform>();
	}

    // Update is called once per frame
    void Update () {
        myTime = myTime + Time.deltaTime;

        if (isActive)
        {
            if (Input.GetKeyDown("1"))
            {
                screenNumber = screenNumber + "1";
                //button1.GetComponent<Renderer>().material.color = Color.cyan;
                //if (myTime > nextColor)
                //{
                //    button1.GetComponent<Renderer>().material.color = Color.white;
                //    Debug.Log("button clicked");
                //    nextColor = nextColor - myTime;
                //    myTime = 0.0f;
                //}
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
        if(screenNumber.Length > maxString)
        {
            screenNumber = "";
        }
        text.text = screenNumber;

        // open 됬을 때
        if(isActive == true && isLocked == false)
        {
            anim.SetBool("isLocked", isLocked);
            Invoke("DoorRotation", 4);
        }
    }

    void DoorRotation()
    {
        tr.position = new Vector3(10.78f, 3.42f, 7.17f);
        tr.rotation = Quaternion.Euler(0, -90, -90);
        isActive = false;
    }
}
