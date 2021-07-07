using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MacacoF : InteracterScript
{
    // Start is called before the first frame update
    bool isPause;
    public Animator boxAnimation;
    void Start()
    {
        PlayerStarter();
        //carryCapacity = WeightClass.Light | WeightClass.Moderate | WeightClass.Heavy;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPause)
        {
            PlayerUpdate();
        }
    }
    protected override void Grab(RaycastHit hit)
    {
        InteractiveObject objScript = hit.transform.GetComponent<InteractiveObject>();
        if(objScript == null)
        {
            return;
        }
        else
        {
            switch (objScript.weight){
            case WeightClass.Light:
                Debug.Log("Agarrando objeto leve");
                objScript.photonView.RPC("GettingGrabbed", Photon.Pun.RpcTarget.All, photonView.ViewID, true);
                break;
            case WeightClass.Moderate:
                Debug.Log("Agarrando objeto médio");
                objScript.photonView.RPC("GettingGrabbed", Photon.Pun.RpcTarget.All, photonView.ViewID, false);
                break;
            case WeightClass.Monkey:
                Debug.Log("Agarrando mamaco");
                objScript.photonView.RPC("GettingGrabbed", Photon.Pun.RpcTarget.All, photonView.ViewID, false);
                break;
            default:
                Debug.Log("Agarrando objeto pesado");
                objScript.photonView.RPC("GettingGrabbed", Photon.Pun.RpcTarget.All, photonView.ViewID, false);
                break;
            }
        }

        AnimationCaller();
        StartCoroutine("AnimationRetake");
    }

    protected override void Throw()
    {
        //Debug.Log("Jogou");
        switch (grabbed.gameObject.GetComponent<InteractiveObject>().weight)
        {
            case WeightClass.Light:
                Debug.Log("Arremessandfo objeto leve");
                grabbed.GetComponent<InteractiveObject>().photonView.RPC("GettingDropped", Photon.Pun.RpcTarget.All, photonView.ViewID, false, 700.0f);
            break;
            case WeightClass.Moderate:
                Debug.Log("Arremessandfo objeto médio");
                grabbed.GetComponent<InteractiveObject>().photonView.RPC("GettingDropped", Photon.Pun.RpcTarget.All, photonView.ViewID, false, 500.0f);
                break;
            case WeightClass.Monkey:
                Debug.Log("Arremessando Macaco");
                grabbed.GetComponent<InteractiveObject>().photonView.RPC("GettingDropped", Photon.Pun.RpcTarget.All, photonView.ViewID, false, 600.0f);
                break;
            case WeightClass.Heavy:
                Debug.Log("Pesado demais para ser lançado normalmente");
                grabbed.GetComponent<InteractiveObject>().photonView.RPC("GettingDropped", Photon.Pun.RpcTarget.All, photonView.ViewID, false, 500.0f);
                break;
        }
    }

    public override void AnimationCaller()
    {
        if(animator.GetBool("Carrying"))
        {
            animator.SetBool("Place", true);
        }
        else
        {
            animator.SetBool("Lift", true);
            Debug.Log("Entrou animação");
        }
        base.AnimationCaller();
    }

    public override IEnumerator AnimationRetake()
    {
        for(int i = 0; i <= 60; i++)
        {
            yield return null;
        }
        yield return null;
        movementPause = false;
        if(animator.GetBool("Lift"))
        {
            animator.SetBool("Lift",false);
            animator.SetBool("Carrying", true);
            Debug.Log("Finalizou animação");
        }
        else
        {
            animator.SetBool("Place", false);
            animator.SetBool("Carrying", false);
            Drop();
        }
    }
}
