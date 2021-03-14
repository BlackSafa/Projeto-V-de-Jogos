using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    public bool isHoldable;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Action()
    {
        Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere), new Vector3(0,10,0), Quaternion.identity);
    }
}
