using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MouseScript
{


    CharacterController controller;
    public float groundSpeed = 15f;
    public float gravityAcceleration = 10f;

    public Transform groundChecker;
    public float groundCheckerLength = 0.4f;
    public float jumpHeight = 3f;
    Vector3 velocity;
    public bool isGrounded = false;

    public LayerMask groundLayer;

    private void Start() {
        StartCamera();
        controller = GetComponent<CharacterController>();
        if(groundChecker == null)
        {
            groundChecker = new GameObject().transform;
            groundChecker.SetParent(transform);
            groundChecker.localPosition = new Vector3(0,-transform.localScale.y, 0);
        }
    }
    void Update()
    {
        Movement();
        if (Input.GetButtonDown("Jump") && isGrounded) Jump();
        Gravity();
        OperateCamera();
    }

    private void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * groundSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * -gravityAcceleration);
        controller.Move(velocity * Time.deltaTime);
    }

    private void Gravity()
    {
        isGrounded = Physics.Raycast(groundChecker.position, -groundChecker.up, groundCheckerLength, groundLayer);
        Debug.DrawLine(groundChecker.position, groundChecker.position - new Vector3(0,groundCheckerLength,0), Color.red);

        if (isGrounded && velocity.y < 0) {velocity.y = -gravityAcceleration -(-1);}
        velocity.y += -gravityAcceleration * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
