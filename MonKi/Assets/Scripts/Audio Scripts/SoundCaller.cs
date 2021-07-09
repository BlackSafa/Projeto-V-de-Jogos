using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCaller : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Macaco")
        {
            other.transform.GetComponentInParent<AudioSource>().Play();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Box")
        {
            other.gameObject.GetComponent<AudioSource>().Play();
        }
    }
}
