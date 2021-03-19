using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacacoA : InteracterScript
{
    // Start is called before the first frame update
    public float jumpSpeedAcceleration = new float();

    void Start()
    {
        PlayerStarter();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerUpdate();
    }

    /*public override void Movement()
   {
       groundSpeed = 30;
       float x = Input.GetAxis("Horizontal");
       float z = Input.GetAxis("Vertical");
       Vector3 move = transform.right * x + transform.forward * z;
       controller.Move(move * groundSpeed * Time.deltaTime);
      
   }

   (public override void Jump()
   {
       //float speedx;
       
       jumpHeight = 6.5f;
       velocity.y = Mathf.Sqrt(jumpHeight * -2 * -gravityAcceleration);
       controller.Move(velocity * Time.deltaTime);
   }*/
    public override void Changes()
    {
        groundSpeed = 5f;
        jumpHeight = 4.8f;
    }

    public Vector3 AirControl()
    {
        Vector3 teste = Vector3.Scale(momentum, transform.forward);
        Debug.Log(teste);
        if (!isGrounded && Input.GetAxis("Vertical") >=0)
        {
            jumpSpeedAcceleration = Input.GetAxis("Vertical") * 3f;
            momentum += transform.forward * (jumpSpeedAcceleration * Time.deltaTime);
        }
        else if (!isGrounded && Input.GetAxis("Vertical") < 0 && momentum.z > -4 && momentum.x > -4 && momentum.z < 4 && momentum.x < 4)
        {
            jumpSpeedAcceleration = Input.GetAxis("Vertical") * 22.5f;
            momentum += transform.forward * (jumpSpeedAcceleration * Time.deltaTime);
        }
        
        if (!isGrounded && Input.GetAxis("Horizontal") >= 0 && momentum.x < 4 && momentum.z < 4 && momentum.z > -4 && momentum.x > -4)
        {
            jumpSpeedAcceleration = Input.GetAxis("Horizontal") * 3f;
            momentum += transform.right * (jumpSpeedAcceleration *Time.deltaTime);
        }
        else if (!isGrounded && Input.GetAxis("Horizontal") < 0 && momentum.x > 4 && momentum.z > 4 && momentum.z > -4 && momentum.x > -4)
        {
            jumpSpeedAcceleration = Input.GetAxis("Horizontal") * 3f;
            momentum += transform.right * (jumpSpeedAcceleration *Time.deltaTime);
        }
        return (momentum);
    }

    public override void MoveUpdate()
    {
        base.MoveUpdate();
        
        if (!isGrounded)
        {
            momentum = AirControl();
        }
    }
}
