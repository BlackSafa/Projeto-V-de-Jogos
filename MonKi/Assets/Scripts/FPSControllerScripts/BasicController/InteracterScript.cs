using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteracterScript : PlayerMovementScript
{
    Camera cam;
    
    public Rigidbody grabbed;
    [SerializeField]
    Transform shouder;
    Ray camRay;

    public bool isHolding = false;

    [SerializeField]
    float grabReach = 2;
    public void PlayerStarter()
    {
        StartMovement();
        cam = personalCamera.GetComponent<Camera>();
        shouder.position = shouder.localPosition;
        shouder.parent = cam.transform;
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


    void Interact(RaycastHit hit)
    {
        InteractiveObject objScript = hit.transform.GetComponent<InteractiveObject>();
        if(objScript == null)
        {
            //Debug.Log("Não Interativo");
            return;
        }

        if(objScript.isHoldable && Input.GetButtonDown("Interact"))
        {
            //Debug.Log("Interagiu ao invés de pegar");
            objScript.Action();
        }
        else if(objScript.isHoldable)
        {
            //Debug.Log("Pegou");
            objScript.rb.isKinematic = true;
            objScript.transform.position = shouder.position;
            objScript.transform.parent = shouder.parent;
            grabbed = objScript.rb;
            isHolding = true;
        }
        else
        {
            //Debug.Log("Interagiu");
            objScript.Action();
        }
    }

    void Drop()
    {
        //Debug.Log("Soltou");
        grabbed.transform.parent = null;
        grabbed.isKinematic = false;
        isHolding = false;
    }

    void Throw()
    {
        //Debug.Log("Jogou");
        grabbed.transform.parent = null;
        grabbed.isKinematic = false;
        grabbed.AddForce(camRay.direction * 500);
        isHolding = false;
    }
}