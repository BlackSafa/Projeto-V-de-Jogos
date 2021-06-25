using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class FinalCheck : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        ChangeScene();
    }

    [PunRPC]
    public void ChangeScene()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Level 1");
        }
    }
}
