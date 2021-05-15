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
                objScript.photonView.RPC("GettingGrabbed", Photon.Pun.RpcTarget.All, gameObject, true);
                break;
            case WeightClass.Moderate:
                Debug.Log("Agarrando objeto médio");
                objScript.photonView.RPC("GettingGrabbed", Photon.Pun.RpcTarget.All, gameObject, false);
                break;
            case WeightClass.Monkey:
                Debug.Log("Agarrando mamaco");
                objScript.photonView.RPC("GettingGrabbed", Photon.Pun.RpcTarget.All, gameObject, false);
                break;
            default:
                Debug.Log("Agarrando objeto pesado");
                objScript.photonView.RPC("GettingGrabbed", Photon.Pun.RpcTarget.All, gameObject, false);
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
                Debug.Log("Arremessandfo objeto leve");
                grabbed.GetComponent<InteracterScript>().photonView.RPC("GettingDropped", Photon.Pun.RpcTarget.All, gameObject, false, 700);
            break;
            case WeightClass.Moderate:
                Debug.Log("Arremessandfo objeto médio");
                grabbed.GetComponent<InteracterScript>().photonView.RPC("GettingDropped", Photon.Pun.RpcTarget.All, gameObject, false, 500);
                break;
            case WeightClass.Monkey:
                Debug.Log("Arremessando Macaco");
                grabbed.GetComponent<InteracterScript>().photonView.RPC("GettingDropped", Photon.Pun.RpcTarget.All, gameObject, false, 600);
                break;
            case WeightClass.Heavy:
                Debug.Log("Pesado demais para ser lançado normalmente");
                grabbed.GetComponent<InteracterScript>().photonView.RPC("GettingDropped", Photon.Pun.RpcTarget.All, gameObject, false, 500);
                break;
        }
    }
}
