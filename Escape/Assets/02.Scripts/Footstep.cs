using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep : MonoBehaviour {
    public AudioClip foreststepSound;
    public AudioClip desertstepSound;
    public AudioClip cavestepSound;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Forest Ground"))
        {
            AudioSource.PlayClipAtPoint(foreststepSound, transform.position);
        }

        if(other.gameObject.layer == LayerMask.NameToLayer("Desert Ground"))
        {
            AudioSource.PlayClipAtPoint(desertstepSound, transform.position);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Cave Ground"))
        {
            AudioSource.PlayClipAtPoint(cavestepSound, transform.position);
        }
    }
}
