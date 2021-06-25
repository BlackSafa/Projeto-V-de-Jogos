using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ArmarioPorta : InteractiveObject
{
    Armario dad;
    private void Start() 
    {
        //photonView = gameObject.GetComponent<PhotonView>();
        dad = transform.GetComponentInParent<Armario>();

    }

    public override void Action(InteracterScript caller)
    {
        dad.Action(caller);
    }
}
