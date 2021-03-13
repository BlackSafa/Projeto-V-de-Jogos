using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacacoA : GrabberScript
{
    // Start is called before the first frame update
    void Start()
    {
        
        StartMovement();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerUpdate();
      
    }

    public override void Movement()
    {
        groundSpeed = 30;
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * groundSpeed * Time.deltaTime);
    }

    public override void Jump()
    {
        jumpHeight = 9f;
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * -gravityAcceleration);
        controller.Move(velocity * Time.deltaTime);
    }
}
