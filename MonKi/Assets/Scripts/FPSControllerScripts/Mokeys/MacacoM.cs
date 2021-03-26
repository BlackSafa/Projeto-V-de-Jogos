using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacacoM : InteracterScript
{
    public bool inmind = false;
    public GameObject _minded;
    
    void Start()
    {
        PlayerStarter();
    }
    
    void Update()
    {
        PlayerUpdate();
    }
   

    public override Vector3 Movement()
    {
        Vector3 move = new Vector3();
        if (!inmind)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
        
           move = transform.right * x + transform.forward * z;

            controller.Move(move * groundSpeed * Time.deltaTime);
            return (move * (groundSpeed / 1.5f));
        }
        else
        {
            return (move);
        }
        
    }

    public override void Jump()
    {
        if (!inmind)
        {
            base.Jump();
        }
    }

    public void GetObjectInMind()
    {
        if (!isHolding)
        {
            
        }
        else
        {
            Drop();
        }
    }
    public void MoveObjectInMind()
    {

        if (inmind)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            float z = Input.mouseScrollDelta.y;
            Vector3 moveinair;
            moveinair.x = x;
            moveinair.y = y;
            moveinair.z = z;
            _minded.transform.position += moveinair;

        }
    }

    protected override void Interact(RaycastHit hit)
    {
        if (!inmind)
        {
            base.Interact(hit);
        }
       
    }
}
