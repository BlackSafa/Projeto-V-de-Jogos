using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabberScript : PlayerMovementScript
{
    Camera cam;
    
    public GameObject grabbed;

    public bool isHolding = false;

    [SerializeField]
    float grabReach = 2;
    void Start()
    {
        cam = gameObject.GetComponentInChildren<Camera>();
    }

    void Update()
    {
        Vector3 screenCenter = new Vector3(Screen.width/2, Screen.height/2,0);
        RaycastHit hit;
        Physics.Raycast(cam.ScreenPointToRay(screenCenter), out hit, grabReach);
        if(Input.GetButtonUp("Interact") && !isHolding)
        {
            Grab(hit);
        }
        if(isHolding && Input.GetButtonUp("Interact"))
        {
            Drop();
        }
        else if(isHolding && Input.GetButtonDown("Fire1"))
        {
            Throw();
        }
    }


    public virtual void Grab(RaycastHit hit)
    {}

    void Drop()
    {}

    void Throw()
    {}
}
