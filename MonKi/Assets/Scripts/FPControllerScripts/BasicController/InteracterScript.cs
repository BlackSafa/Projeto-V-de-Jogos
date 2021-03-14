using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteracterScript : PlayerMovementScript
{
    Camera cam;
    
    public GameObject grabbed;

    public bool isHolding = false;

    [SerializeField]
    float grabReach = 2;
    public void PlayerStarter()
    {
        StartMovement();
        cam = personalCamera.GetComponent<Camera>();
    }

    public void PlayerUpdate()
    {
        MoveUpdate();
        Vector3 screenCenter = new Vector3(Screen.width/2, Screen.height/2,0);
        RaycastHit hit;
        Vector3 ponto = cameraPosition.position;
        Debug.Log(ponto);
        Ray point = new Ray(cameraPosition.transform.position, cam.transform.forward);
        Physics.Raycast(point, out hit, grabReach);
        Debug.DrawLine(point.origin, (point.direction * grabReach) + ponto, Color.blue);
        if(Input.GetButtonUp("Grab") || Input.GetButtonDown("Interact") && !isHolding)
        {
            Interact(hit);
        }
        if(isHolding && Input.GetButtonUp("Grab"))
        {
            Drop();
        }
        else if(isHolding && Input.GetButtonDown("Interact"))
        {
            Throw();
        }
    }


    void Interact(RaycastHit hit)
    {
        if(hit.transform.GetComponent<InteractiveObject>().isHoldable && Input.GetButtonDown("Grab"))
        {}
        else if(hit.transform.GetComponent<InteractiveObject>().isHoldable)
        {}
        else
        {}
    }

    void Drop()
    {}

    void Throw()
    {}
}
