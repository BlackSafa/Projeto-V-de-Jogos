using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class MacacoM : InteracterScript
{
    public bool inmind = false;
    public GameObject _minded;
    private RaycastHit levitation;
    private float mindlenght = 10f;
    
    void Start()
    {
        PlayerStarter();
    }
    
    void Update()
    {
        PlayerUpdate();
        
        Debug.DrawLine(camRay.origin, (camRay.direction * mindlenght) + cameraPosition.position, Color.red);
        if (Physics.Raycast(camRay, out levitation, mindlenght, LayerMask.GetMask("Objects")) && !isHolding && !inmind)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                GetObjectInMind(levitation);
            }
        }
        else if (isHolding)
        {
            if(isHolding && Input.GetKeyDown(KeyCode.Tab))
            {
                Drop();
            }
        }
        else if (inmind)
        {
            if(inmind && Input.GetKeyDown(KeyCode.Tab))
            {
                _minded.GetComponent<Rigidbody>().useGravity = true;
                _minded = null;
                inmind = false;
            }
            else
            {
                MoveObjectInMind();
            }
        }

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

    public void GetObjectInMind(RaycastHit hit)
    {
        InteractiveObject objScript = hit.transform.GetComponent<InteractiveObject>();
        if (objScript.isHoldable)
        {
            if ((carryCapacity & objScript.weight) == objScript.weight)
            {
                Debug.Log("Pegando objeto");
                inmind = true;
                _minded = hit.transform.gameObject;
                _minded.GetComponent<Rigidbody>().useGravity = false;
            }
        }
    }
    public void MoveObjectInMind()
    {
        if (inmind)
        {
            Debug.Log("Movendo o objeto");
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            float z = Input.mouseScrollDelta.y;
            
            x = Mathf.Clamp(x, -10, 10);
            y = Mathf.Clamp(y, -10, 10);
            z = Mathf.Clamp(z, -10, 10);
            _minded.transform.Translate(x * Time.deltaTime,y * Time.deltaTime,z * Time.deltaTime * 5);

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
