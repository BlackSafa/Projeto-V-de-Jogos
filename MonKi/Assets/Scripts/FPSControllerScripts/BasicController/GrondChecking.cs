using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GrondChecking : MonoBehaviour
{
    public PlayerMovementScript script;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 8 || other.gameObject.layer == 9 || other.tag == "Player")
        {
            script.isGrounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == 8 || other.gameObject.layer == 9 || other.tag == "Player")
        {
            script.isGrounded = false;
        }
    }
}