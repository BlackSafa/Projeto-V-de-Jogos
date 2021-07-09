using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ShouderPositioner : MonoBehaviourPunCallbacks
{
    public Transform reference;

    private void Start() {
        gameObject.AddComponent<PhotonView>();
        gameObject.AddComponent<PhotonTransformView>();
        if(!photonView.IsMine)
        {
            this.enabled = false;
        }
    }
    void Update()
    {
        transform.position = reference.position;
        transform.rotation = reference.rotation;
    }
}
