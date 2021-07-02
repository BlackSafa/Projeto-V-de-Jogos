using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PlayerMovementScript : MouseScript
{
    public CharacterController controller;
    public float groundSpeed = 3.5f;
    public float gravityAcceleration = 10f;
    public Vector3 momentum;
    public Transform groundChecker;
    public float groundCheckerLength = 0.2f;
    public float jumpHeight = 2.2f;
    public Vector3 velocity;
    public bool isGrounded = false;
    public bool movementPause = false;
    public bool jumper = true;
    public LayerMask groundLayer;
    public Animator animator;

    public void StartMovement() {
        StartCamera();
        Changes();
        groundLayer = LayerMask.GetMask("Ground");
        groundLayer += LayerMask.GetMask("Objects");
        controller = GetComponent<CharacterController>();
        if(groundChecker == null)
        {
            groundChecker = new GameObject().transform;
            groundChecker.SetParent(transform);
            groundChecker.localPosition = new Vector3(0,0-transform.localScale.y, 0);
        }
    }
    public virtual void MoveUpdate()
    {
        if(!movementPause)
        {
            if (isGrounded)
            {
                momentum = (Movement() / 2);
                animator.SetBool("Jump", false);
            }
            else
            {
                animator.SetBool("Jump", true);
            }

            /*else if(Input.GetAxis("Vertical") >= 0)
            {
                Vector3 movejump = ((transform.forward * Input.GetAxis("Vertical")) * 20);
                momentum = movejump;
            }
            else if (Input.GetAxis("Vertical") < 0 && momentum.z >= 0)
            {
                Vector3 movejump = ((transform.forward * Input.GetAxis("Vertical")) * 5);
                momentum = movejump;
            }
            else if (Input.GetAxis("Vertical") < 0 && momentum.z <= 0)
            {
                Vector3 movejump = (transform.forward * 0);
                momentum = movejump;
            }*/

            if (Input.GetButtonDown("Jump") && isGrounded && jumper) Jump();
            Gravity();
            OperateCamera();
        }
    }

    virtual public Vector3 Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        Vector3 move = transform.right * x + transform.forward * z;
        animator.SetFloat("Move", move.magnitude);
        controller.Move(move * groundSpeed * Time.deltaTime);
        return (move * (groundSpeed / 1.5f));
    }

    virtual public void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * -gravityAcceleration);
        controller.Move(velocity * Time.deltaTime);
    }

    private void Gravity()
    {
        isGrounded = Physics.Raycast(groundChecker.position, -groundChecker.up, groundCheckerLength, groundLayer);
        Debug.DrawLine(groundChecker.position, groundChecker.position - new Vector3(0,groundCheckerLength,0), Color.red);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -gravityAcceleration -(-1);
        }
        velocity.y += -gravityAcceleration * Time.deltaTime;
        if (!isGrounded)
        {
            //float speedjump = Input.GetAxis("Vertical");
            controller.Move((velocity + momentum)  * Time.deltaTime);
        }
            
        else
            controller.Move(velocity * Time.deltaTime);
    }
    
    public virtual void Changes()
    {
        
    }

    public virtual void AnimationCaller()
    {
        movementPause = true;
    }

    public virtual void AnimationRetake()
    {
        movementPause = false;
    }
}
