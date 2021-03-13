using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScript : MonoBehaviour
{
    public float mouseSensitivityX = 100f;
    public Transform cameraPosition;
    Transform personalCamera;

    float xRotation = 0f;
    float yRotation = 0f;

    void Start()
    {
        personalCamera = new GameObject().transform;
        personalCamera.gameObject.AddComponent<Camera>();
        personalCamera.gameObject.AddComponent<AudioListener>();
        if(cameraPosition == null)
        {
            for(int c = 0; c < transform.childCount; c++)
            {
                Transform tested = transform.GetChild(0);
                if (tested.tag == "CameraHead") 
                {
                    cameraPosition = tested;
                    break;
                }
            }
        }
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    public void OperateCamera()
    {
        personalCamera.position = cameraPosition.position;
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityX * Time.deltaTime;
        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
        
        yRotation += mouseX;

        personalCamera.rotation = Quaternion.Euler(xRotation, transform.eulerAngles.y, 0);
        transform.eulerAngles = new Vector3(0, yRotation, 0);
    }
}
