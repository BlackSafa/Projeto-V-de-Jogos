using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Painel : InteractiveObject
{
    // Start is called before the first frame update
    public int keyAmount, maxKeys;
    public Transform[] KeyPositions, OwnedKeys;
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        KeyPositions = new Transform[maxKeys];
        for(int i = 0; i < maxKeys; i++)
        {
            KeyPositions[i] = transform.GetChild(i);
        }
    }

    public override void Action(InteracterScript caller)
    {
        if(caller.grabbed.tag == "Key")
        {
            foreach(Transform x in OwnedKeys)
            {
                if(caller.grabbed.transform == x)
                {
                    photonView.RPC("AddKey", RpcTarget.All, caller.grabbed.GetComponent<PhotonView>().ViewID, caller.photonView.ViewID);
                    if(keyAmount >= maxKeys)
                    {
                        base.Action(caller);
                    }
                }
            }
        }

    }

    [PunRPC]
    void AddKey(int keyID, int callerID)
    {
        Transform key = PhotonView.Find(keyID).transform;
        InteractiveObject script = key.GetComponent<InteractiveObject>();
        script.photonView.RPC("GettingDropped",RpcTarget.All, callerID, true, 0.0f);
        float[] pos = new float[3], rot = new float[3];
        pos[0] = KeyPositions[keyAmount].position.x;
        pos[1] = KeyPositions[keyAmount].position.y;
        pos[2] = KeyPositions[keyAmount].position.z;
        rot[0] = KeyPositions[keyAmount].eulerAngles.x;
        rot[1] = KeyPositions[keyAmount].eulerAngles.y;
        rot[2] = KeyPositions[keyAmount].eulerAngles.z;
        script.photonView.RPC("SetPosition",RpcTarget.All, true, pos, rot, photonView.ViewID);
        keyAmount++;
    }      
}
