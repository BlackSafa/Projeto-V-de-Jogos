using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacacoF : InteracterScript
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerStarter();
        carryCapacity = WeightClass.Light | WeightClass.Moderate | WeightClass.Heavy | WeightClass.Monkey;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerUpdate();
    }
    protected override void Interact(RaycastHit hit)
    {
        InteractiveObject objScript = hit.transform.GetComponent<InteractiveObject>();
        if(objScript == null)
        {
            return;
        }

        if(objScript.isHoldable && Input.GetButtonDown("Interact"))
        {
            objScript.action();
        }
        else if(objScript.isHoldable)
        {
            switch (objScript.weight){
            case WeightClass.Light:
                Debug.Log("Agarrando objeto leve");
                objScript.rb.isKinematic = true;
                objScript.transform.position = hand.position;
                objScript.transform.parent = hand.parent;
                Physics.IgnoreCollision(hit.collider, gameObject.GetComponent<Collider>(), true);
                grabbed = objScript.rb;
                isHolding = true;
                break;
            case WeightClass.Moderate:
                Debug.Log("Agarrando objeto médio");
                objScript.rb.isKinematic = true;
                objScript.transform.position = shouder.position;
                objScript.transform.parent = shouder.parent;
                Physics.IgnoreCollision(hit.collider, gameObject.GetComponent<Collider>(), true);
                grabbed = objScript.rb;
                isHolding = true;
                break;
            case WeightClass.Monkey:
                Debug.Log("Agarrando mamaco");
                objScript.rb.isKinematic = true;
                objScript.transform.position = shouder.position;
                objScript.transform.parent = shouder.parent;
                Physics.IgnoreCollision(hit.collider, gameObject.GetComponent<Collider>(), true);
                grabbed = objScript.rb;
                isHolding = true;
                break;
            default:
                Debug.Log("Agarrando objeto pesado");
                objScript.rb.isKinematic = true;
                objScript.transform.position = shouder.position;
                objScript.transform.parent = shouder.parent;
                Physics.IgnoreCollision(hit.collider, gameObject.GetComponent<Collider>(), true);
                grabbed = objScript.rb;
                isHolding = true;
                break;
            }
        }
        else
        {
            objScript.action();
        }
    }

    protected override void Throw()
    {
        //Debug.Log("Jogou");
        switch (grabbed.gameObject.GetComponent<InteractiveObject>().weight)
        {
            case WeightClass.Light:
                Debug.Log("Arremessandfo objeto leve");
                grabbed.transform.parent = null;
                grabbed.isKinematic = false;
                grabbed.AddForce(personalCamera.transform.forward * 700);
                Physics.IgnoreCollision(grabbed.GetComponent<Collider>(), gameObject.GetComponent<Collider>(), false);
                isHolding = false;
            break;
            case WeightClass.Moderate:
                Debug.Log("Arremessandfo objeto médio");
                grabbed.transform.parent = null;
                grabbed.isKinematic = false;
                grabbed.AddForce(personalCamera.transform.forward * 500);
                Physics.IgnoreCollision(grabbed.GetComponent<Collider>(), gameObject.GetComponent<Collider>(), false);
                isHolding = false;
                break;
            case WeightClass.Monkey:
                Debug.Log("Arremessando Macaco");
                grabbed.transform.parent = null;
                grabbed.isKinematic = false;
                grabbed.AddForce(personalCamera.transform.forward * 600);
                Physics.IgnoreCollision(grabbed.GetComponent<Collider>(), gameObject.GetComponent<Collider>(), false);
                isHolding = false;
                break;
            case WeightClass.Heavy:
                Debug.Log("Pesado demais para ser lançado normalmente");
                grabbed.transform.parent = null;
                grabbed.isKinematic = false;
                grabbed.AddForce(personalCamera.transform.forward * 500);
                Physics.IgnoreCollision(grabbed.GetComponent<Collider>(), gameObject.GetComponent<Collider>(), false);
                isHolding = false;
                break;
        }
    }
}
