using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteracterScript : PlayerMovementScript
{
    Camera cam;
    public WeightClass carryCapacity = WeightClass.Light | WeightClass.Moderate
    , weigthClass = WeightClass.Monkey;

    public Rigidbody grabbed;
    [SerializeField]
    protected Transform shouder, hand;
    protected Ray camRay;

    public bool isHolding = false;

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
        print(carryCapacity);
        carryCapacity = WeightClass.Light | WeightClass.Moderate;
        print(carryCapacity);
        cam = personalCamera.GetComponent<Camera>();
        shouder.position = shouder.localPosition;
        shouder.parent = cam.transform;
        hand.localPosition = shouder.localPosition;
    }


    public void PlayerUpdate()
    {
        MoveUpdate();
        Vector3 screenCenter = new Vector3(Screen.width/2, Screen.height/2,0);
        RaycastHit hit;
        camRay = cam.ScreenPointToRay(screenCenter);
        Debug.DrawLine(camRay.origin, (camRay.direction * grabReach) + cameraPosition.position, Color.blue);
        if(Physics.Raycast(camRay, out hit, grabReach, LayerMask.GetMask("Objects")) && !isHolding)
        {
            if(Input.GetButtonUp("Grab") || Input.GetButtonDown("Interact"))
            {
                Interact(hit);
            }
        }

        else if(isHolding)
        {
            if(isHolding && Input.GetButtonUp("Grab"))
            {
                Drop();
            }
            else if(isHolding && Input.GetButtonDown("Interact"))
            {
                Throw();
            }
        }
        else
        {
            return;
        }
    }


    protected virtual void Interact(RaycastHit hit)
    {
        InteractiveObject objScript = hit.transform.GetComponent<InteractiveObject>();
        if(objScript == null)
        {
            return;
        }

        if(objScript.isHoldable && Input.GetButtonDown("Interact"))
        {
            objScript.Action();
        }
        else if(objScript.isHoldable)
        {
            switch (objScript.weight){
            case WeightClass.Light:
                Debug.Log("Agarrando objeto leve");
                objScript.rb.isKinematic = true;
                objScript.transform.position = hand.position;
                objScript.transform.parent = hand.parent;
                Physics.IgnoreCollision(hit.collider, gameObject.GetComponent<Collider>(), true);
                grabbed = objScript.rb;
                isHolding = true;
                break;
            case WeightClass.Moderate:
                Debug.Log("Agarrando objeto médio");
                objScript.rb.isKinematic = true;
                objScript.transform.position = shouder.position;
                objScript.transform.parent = shouder.parent;
                Physics.IgnoreCollision(hit.collider, gameObject.GetComponent<Collider>(), true);
                grabbed = objScript.rb;
                isHolding = true;
                break;
            case WeightClass.Monkey:
                Debug.Log("Macacos são pesados demais para agarrar");
                break;
            default:
                Debug.Log("Objeto pesado demais");
                break;
            }
        }
        else
        {
            objScript.Action();
        }
    }

    protected void Drop()
    {
        //Debug.Log("Soltou");
        grabbed.transform.parent = null;
        grabbed.isKinematic = false;
        Physics.IgnoreCollision(grabbed.GetComponent<Collider>(), gameObject.GetComponent<Collider>(), false);
        isHolding = false;
    }

    protected virtual void Throw()
    {
        //Debug.Log("Jogou");
        switch (grabbed.gameObject.GetComponent<InteractiveObject>().weight)
        {
            case WeightClass.Light:
                grabbed.transform.parent = null;
                grabbed.isKinematic = false;
                grabbed.AddForce(personalCamera.transform.forward * 500);
                Physics.IgnoreCollision(grabbed.GetComponent<Collider>(), gameObject.GetComponent<Collider>(), false);
                isHolding = false;
            break;
            default:
                Debug.Log("Pesado demais para ser lançado normalmente");
                grabbed.transform.parent = null;
                grabbed.isKinematic = false;
                grabbed.AddForce(personalCamera.transform.forward * 100);
                Physics.IgnoreCollision(grabbed.GetComponent<Collider>(), gameObject.GetComponent<Collider>(), false);
                isHolding = false;
                break;
        }
    }

    void CallThrowAnimation()
    {}
}