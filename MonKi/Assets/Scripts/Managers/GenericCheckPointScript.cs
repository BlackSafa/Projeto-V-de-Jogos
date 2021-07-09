using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GenericCheckPointScript : MonoBehaviour
{
    public delegate void Close();
    public event Close closeDoor;
    public PhotonView spawnerpw;
    
    private void OnTriggerExit(Collider other)
    {
        float[] pos = {transform.position.x, transform.position.y, transform.position.z};
        float[] rot = {transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z};
        spawnerpw.RPC("CallSpawnChange", RpcTarget.All, pos, rot);
        if(closeDoor != null && other.GetComponent<PhotonView>().IsMine)
        {
            closeDoor();
        }
    }
}
