using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InteracterScript : PlayerMovementScript
{
    Camera cam;
    public WeightClass carryCapacity = WeightClass.Light
    , weigthClass = WeightClass.Monkey;
    public Rigidbody grabbed;
    [SerializeField]
    public Transform shouder, hand;
    protected Ray camRay;
    public bool isHolding = false;
 
    public Transform spawn;

    [SerializeField]
    float grabReach = 3.2f;
    public void PlayerStarter()
    {
        if(!photonView.IsMine)
        {
            this.enabled = false;
            personalCamera.gameObject.SetActive(false);
        }
        StartMovement();
        //print(carryCapacity);
        //carryCapacity = WeightClass.Light | WeightClass.Moderate;
        //print(carryCapacity);
        cam = personalCamera.GetComponent<Camera>();
        shouder.position = shouder.localPosition;
        shouder.parent = cam.transform;
        hand.localPosition = shouder.localPosition;
    }


    public void PlayerUpdate()
    {
        MoveUpdate();
        Vector3 screenCenter = new Vector3(Screen.width/2, Screen.height/2, 0);
        RaycastHit hit;
        camRay = cam.ScreenPointToRay(screenCenter);
        Debug.DrawLine(camRay.origin, (camRay.direction * grabReach) + cameraPosition.position, Color.blue);
        if(Input.GetKeyDown(KeyCode.F))
        {
            GetComponentInChildren<Macacosvoice>().MamacoFalaFPD();
        }
        if(Physics.Raycast(camRay, out hit, grabReach, LayerMask.GetMask("Objects")))
        {
            if(Input.GetButtonUp("Grab") && !isHolding)
            {
                Grab(hit);
            }
            if(Input.GetButtonDown("Interact"))
            {
                Interact(hit);
            }
        }

        else if(isHolding)
        {
            if(isHolding && Input.GetButtonUp("Grab"))
            {
                if((carryCapacity & WeightClass.Heavy) != WeightClass.Heavy)
                {
                    Drop();
                }
                else
                {
                    AnimationCaller();
                    StartCoroutine("AnimationRetake");
                }
            }
            /*else if(isHolding && Input.GetButtonDown("Interact"))
            {
                Throw();
            }*/
        }
        else
        {
            return;
        }
    }

    protected virtual void Grab(RaycastHit hit)
    {
        InteractiveObject objScript = hit.transform.GetComponent<InteractiveObject>();
        if(objScript == null)
        {
            return;
        }
        else if(objScript.isHoldable && (carryCapacity & objScript.weight) == objScript.weight)
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
            default:
                Debug.Log("Objeto pesado demais");
                break;
            }
        }

        AnimationCaller();
        StartCoroutine("AnimationRetake");
    }

    protected virtual void Interact(RaycastHit hit)
    {
        InteractiveObject objScript = hit.transform.GetComponent<InteractiveObject>();
        if (objScript == null)
        {
            return;
        }
        else
        {
            objScript.Action(this);
        }
    }

    public void Drop()
    {
        //Debug.Log("Soltou");
        grabbed.GetComponent<InteractiveObject>().photonView.RPC("GettingDropped", Photon.Pun.RpcTarget.All, photonView.ViewID, true, 0.0f);
        //AnimationRetake();
    }

    protected virtual void Throw()
    {
        //Debug.Log("Jogou");
        switch (grabbed.gameObject.GetComponent<InteractiveObject>().weight)
        {
            case WeightClass.Light:
                grabbed.GetComponent<InteractiveObject>().photonView.RPC("GettingDropped", Photon.Pun.RpcTarget.All, photonView.ViewID, false, 500.0f);
            break;
            default:
                Debug.Log("Pesado demais para ser lançado normalmente");
                grabbed.GetComponent<InteractiveObject>().photonView.RPC("GettingDropped", Photon.Pun.RpcTarget.All, photonView.ViewID, false, 100.0f);
                break;
        }
    }

    [PunRPC]
    public void Respawn ()
    {
        transform.position = spawn.position;
        transform.eulerAngles = spawn.eulerAngles;
    }

    [PunRPC]
    public void ChangeSpawn (float[] pos, float[] rot)
    {
        spawn.position = new Vector3(pos[0], pos[1], pos[2]);
        spawn.eulerAngles = new Vector3(rot[0], rot[1], rot[2]);
    }
}