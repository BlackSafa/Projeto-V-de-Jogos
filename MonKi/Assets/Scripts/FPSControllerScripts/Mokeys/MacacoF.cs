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
            objScript.Action();
        }
        else if(objScript.isHoldable)
        {
            switch (objScript.weight){
            case WeightClass.Light:
                Debug.Log("Agarrando objeto leve");
                objScript.rb.isKinematic = true;
                objScript.transform.position = shouder.position;
                objScript.transform.parent = shouder.parent;
                grabbed = objScript.rb;
                isHolding = true;
                break;
            case WeightClass.Moderate:
                Debug.Log("Agarrando objeto médio");
                objScript.rb.isKinematic = true;
                objScript.transform.position = shouder.position;
                objScript.transform.parent = shouder.parent;
                grabbed = objScript.rb;
                isHolding = true;
                break;
            case WeightClass.Monkey:
                Debug.Log("Macacos são pesados demais para agarrar");
                break;
            default:
                Debug.Log("Objeto pesado demais");
                break;
            }
        }
        else
        {
            objScript.Action();
        }
    }

    protected override void Throw()
    {
        //Debug.Log("Jogou");
        switch (grabbed.gameObject.GetComponent<InteractiveObject>().weight)
        {
            case WeightClass.Light:
                grabbed.transform.parent = null;
                grabbed.isKinematic = false;
                grabbed.AddForce(personalCamera.transform.forward * 500);
                isHolding = false;
            break;
            default:
                Debug.Log("Pesado demais para ser lançado normalmente");
                grabbed.transform.parent = null;
                grabbed.isKinematic = false;
                grabbed.AddForce(personalCamera.transform.forward * 100);
                isHolding = false;
                break;
        }
    }
}
