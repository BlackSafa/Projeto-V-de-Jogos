using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MacacoF : InteracterScript
{
    // Start is called before the first frame update
    public bool isPause;
    public Animator boxAnimation;
    Transform insideShouder;
    void Start()
    {
        PlayerStarter();
        if(!photonView.IsMine)
        {
            GameObject newShouder = PhotonNetwork.Instantiate("SMonkey Outside Shouder", shouder.position, shouder.rotation);
            ShouderPositioner newShouderScript= newShouder.GetComponent<ShouderPositioner>();
            insideShouder = shouder;
            shouder = newShouder.transform;
            newShouderScript.reference = insideShouder;
        }
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
        Debug.Log("here");
        yield return new WaitForSeconds(2.5f);
        Debug.Log("here 2");
        yield return null;
        Debug.Log("here 3");
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
