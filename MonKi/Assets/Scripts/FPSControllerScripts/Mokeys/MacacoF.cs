﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacacoF : InteracterScript
{
    // Start is called before the first frame update
    bool isPause;
    void Start()
    {
        PlayerStarter();
        //carryCapacity = WeightClass.Light | WeightClass.Moderate | WeightClass.Heavy;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPause)
        {
            PlayerUpdate();
        }

        if(grabbed != null)
        {
            animator.SetBool("Carrying", true);
        }
        else
        {
            animator.SetBool("Carrying", false);
        }
    }
    protected override void Grab(RaycastHit hit)
    {
        InteractiveObject objScript = hit.transform.GetComponent<InteractiveObject>();
        if(objScript == null)
        {
            return;
        }
        else
        {
            switch (objScript.weight){
            case WeightClass.Light:
                Debug.Log("Agarrando objeto leve");
                objScript.photonView.RPC("GettingGrabbed", Photon.Pun.RpcTarget.All, photonView.ViewID, true);
                break;
            case WeightClass.Moderate:
                Debug.Log("Agarrando objeto médio");
                objScript.photonView.RPC("GettingGrabbed", Photon.Pun.RpcTarget.All, photonView.ViewID, false);
                break;
            case WeightClass.Monkey:
                Debug.Log("Agarrando mamaco");
                objScript.photonView.RPC("GettingGrabbed", Photon.Pun.RpcTarget.All, photonView.ViewID, false);
                break;
            default:
                Debug.Log("Agarrando objeto pesado");
                objScript.photonView.RPC("GettingGrabbed", Photon.Pun.RpcTarget.All, photonView.ViewID, false);
                break;
            }
        }

        AnimationCaller();
    }

    protected override void Throw()
    {
        //Debug.Log("Jogou");
        switch (grabbed.gameObject.GetComponent<InteractiveObject>().weight)
        {
            case WeightClass.Light:
                Debug.Log("Arremessandfo objeto leve");
                grabbed.GetComponent<InteractiveObject>().photonView.RPC("GettingDropped", Photon.Pun.RpcTarget.All, photonView.ViewID, false, 700.0f);
            break;
            case WeightClass.Moderate:
                Debug.Log("Arremessandfo objeto médio");
                grabbed.GetComponent<InteractiveObject>().photonView.RPC("GettingDropped", Photon.Pun.RpcTarget.All, photonView.ViewID, false, 500.0f);
                break;
            case WeightClass.Monkey:
                Debug.Log("Arremessando Macaco");
                grabbed.GetComponent<InteractiveObject>().photonView.RPC("GettingDropped", Photon.Pun.RpcTarget.All, photonView.ViewID, false, 600.0f);
                break;
            case WeightClass.Heavy:
                Debug.Log("Pesado demais para ser lançado normalmente");
                grabbed.GetComponent<InteractiveObject>().photonView.RPC("GettingDropped", Photon.Pun.RpcTarget.All, photonView.ViewID, false, 500.0f);
                break;
        }
    }

    public override void AnimationCaller()
    {
        base.AnimationCaller();
        animator.SetBool("Lift", true);
    }

    public override void AnimationRetake()
    {
        base.AnimationRetake();
        animator.SetBool("Lift",false);

    }
}
