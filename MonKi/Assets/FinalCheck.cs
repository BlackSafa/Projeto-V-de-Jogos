using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class FinalCheck : MonoBehaviour
{
    PhotonView photonView;

    private void Start() {
        photonView = GetComponent<PhotonView>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        photonView.RPC("ChangeScene", RpcTarget.All);
    }

    [PunRPC]
    public void ChangeScene()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("FinalScene");
        }
    }
}
