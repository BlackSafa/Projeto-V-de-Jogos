using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ShouderPositioner : MonoBehaviourPunCallbacks
{
    public Transform reference;
    void Update()
    {
        transform.position = reference.position;
        transform.rotation = reference.rotation;
    }
}
