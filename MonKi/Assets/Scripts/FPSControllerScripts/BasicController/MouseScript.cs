using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MouseScript : MonoBehaviourPunCallbacks
{
    public float mouseSensitivityX = 100f;
    public Transform cameraPosition, personalCamera;
    public Transform Mental;

    float xRotation = 0f;
    float yRotation = 0f;

    protected void StartCamera()
    {
        personalCamera = new GameObject().transform;
        Camera camconfig = personalCamera.gameObject.AddComponent<Camera>();
        camconfig.clearFlags = CameraClearFlags.SolidColor;
        camconfig.backgroundColor = Color.black;
        personalCamera.gameObject.AddComponent<AudioListener>();
        Mental = new GameObject().transform;
        Mental.SetParent(personalCamera);
        Mental.position += new Vector3(0, 0, 10);
        
        
        if(cameraPosition == null)
        {
            for(int c = 0; c < transform.childCount; c++)
            {
                Transform tested = transform.GetChild(c);
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
    protected void OperateCamera()
    {
        //Debug.Log(Mental.position);
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
