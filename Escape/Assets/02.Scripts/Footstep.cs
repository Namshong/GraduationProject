using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 발걸음 소리 코드 */
public class Footstep : MonoBehaviour {
    public AudioClip foreststepSound;   // 잔디 위 걷는 소리
    public AudioClip desertstepSound;   // 모래 위 걷는 소리
    public AudioClip cavestepSound;     // 동굴 안 걷는 소리

    private void OnTriggerEnter(Collider other)
    {
        /* 캐릭터가 숲을 걷고 있을 때 소리 재생 */
        if(other.gameObject.layer == LayerMask.NameToLayer("Forest Ground"))
        {
            AudioSource.PlayClipAtPoint(foreststepSound, transform.position);
        }

        /* 캐릭터가 사막을 걷고 있을 때 소리 재생 */
        if (other.gameObject.layer == LayerMask.NameToLayer("Desert Ground"))
        {
            AudioSource.PlayClipAtPoint(desertstepSound, transform.position);
        }

        /* 캐릭터가 동굴 안을 걷고 있을 때 소리 재생 */
        if (other.gameObject.layer == LayerMask.NameToLayer("Cave Ground"))
        {
            AudioSource.PlayClipAtPoint(cavestepSound, transform.position);
        }
    }
}
