using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    public bool isHoldable;
    public Rigidbody rb;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        if(rb) isHoldable = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Action()
    {
        Debug.Log("Agindo");
        GameObject novo = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        novo.transform.position = new Vector3(0,10,0);
    }
}
