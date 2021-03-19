using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacacoA : InteracterScript
{
    // Start is called before the first frame update
    public float jumpSpeedAccelerationx = new float();
    public float jumpSpeedAccelerationz = new float();
    
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
        if (!isGrounded && Input.GetAxis("Vertical") >=0 || momentum.z < 4 || momentum.x < 4 || momentum.z > -4 || momentum.x > -4)
        {
            jumpSpeedAccelerationz = Input.GetAxis("Vertical") * 3f;
            jumpSpeedAccelerationx = Input.GetAxis("Horizontal") * 3f;
            momentum += transform.forward * (jumpSpeedAccelerationz * Time.deltaTime) + transform.right * (jumpSpeedAccelerationx *Time.deltaTime);
        }
        else if (!isGrounded && Input.GetAxis("Vertical") < 0 || momentum.z > -4 || momentum.x > -4 || momentum.z < 4 || momentum.x < 4)
        {
            jumpSpeedAccelerationz = Input.GetAxis("Vertical") * 22.5f;
            jumpSpeedAccelerationx = Input.GetAxis("Horizontal") * 3f;
            momentum += transform.forward * (jumpSpeedAccelerationz * Time.deltaTime) + transform.right * (jumpSpeedAccelerationx *Time.deltaTime);
        }
        else if( momentum.z > -4 || momentum.x > -4 || momentum.z < 4 || momentum.x < 4)
        {
            jumpSpeedAccelerationx = Input.GetAxis("Horizontal") * 3f;
            momentum += transform.forward * (jumpSpeedAccelerationz * Time.deltaTime) + transform.right * (jumpSpeedAccelerationx *Time.deltaTime);
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
