using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacacoA : InteracterScript
{
    // Start is called before the first frame update
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
        groundSpeed = 30;
        jumpHeight = 6.5f;
    }
}
