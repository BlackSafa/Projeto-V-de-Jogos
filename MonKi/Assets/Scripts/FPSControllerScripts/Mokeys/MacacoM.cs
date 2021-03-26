using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Drop();
        }
        else if (inmind)
        {
            _minded = null;
            inmind = false;
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
            inmind = true;
            _minded = hit.transform.gameObject;
        }
    }
    public void MoveObjectInMind()
    {

        if (inmind)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            float z = Input.mouseScrollDelta.y;
            Vector3 moveinair;
            Vector3 actualposition = _minded.transform.position;
            Vector3 macacoM;
            macacoM = transform.position;
            moveinair.x = x;
            moveinair.y = y;
            moveinair.z = z;
            actualposition += moveinair;
            actualposition -= macacoM;
            actualposition.x = Mathf.Clamp(actualposition.x, -10, 10);
            actualposition.y = Mathf.Clamp(actualposition.y, -10, 10);
            actualposition.z = Mathf.Clamp(actualposition.z, -10, 10);
            actualposition += macacoM;
            _minded.transform.position += actualposition;

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
