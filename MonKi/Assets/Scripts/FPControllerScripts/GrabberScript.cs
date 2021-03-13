using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabberScript : PlayerMovementScript
{
    Camera cam;
    
    public GameObject grabbed;

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
        Grab(hit);
    }


    public virtual void Grab(RaycastHit hit)
    {}

    void Drop()
    {}

    void Throw()
    {}
}
