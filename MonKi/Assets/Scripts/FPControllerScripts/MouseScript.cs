using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScript : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody, cameraPosition;

    float xRotation = 0f;
    float yRotation = 0f;
    void Start()
    {
        //playerBody = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
        //playerBody = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cameraPosition.position;
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
        
        yRotation += mouseX;

        transform.rotation = Quaternion.Euler(xRotation, playerBody.eulerAngles.y, 0);
        playerBody.eulerAngles = new Vector3(0, yRotation, 0);
    }
}
