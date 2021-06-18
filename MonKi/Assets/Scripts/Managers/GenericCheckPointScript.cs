using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GenericCheckPointScript : MonoBehaviour
{
    public delegate void Close();
    public event Close closeDoor;
    
    private void OnTriggerExit(Collider other)
    {
        InteracterScript[] monkeys = FindObjectsOfType<InteracterScript>();
        foreach(InteracterScript m in monkeys)
        {
            float[] pos = {transform.position.x, transform.position.y, transform.position.z};
            float[] rot = {transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z};
            m.GetComponent<PhotonView>().RPC("ChangeSpawn", RpcTarget.All, pos, rot);
        }
        if(closeDoor != null)
        {
            closeDoor();
        }
    }
}
