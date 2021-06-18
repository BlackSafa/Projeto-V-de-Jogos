using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Armario : InteractiveObject
{
    // Start is called before the first frame update
    private bool open;
    private Animator animator;
    void Start()
    {
        photonView = gameObject.GetComponent<PhotonView>();
        animator = GetComponent<Animator>();
        isHoldable = false;
        open = false;
    }

    // Update is called once per frame
    public override void Action(InteracterScript caller)
    {
        photonView.RPC("abrirArmario",RpcTarget.All);
    }

    [PunRPC]
    public void abrirArmario()
    {
        if (!open)
        {
            Debug.Log("Abriu");
            open = true;
            //GetComponent<Animator>().SetBool("Open", open);
        }
        else
        {
            Debug.Log("Fechou");
            open = false;
            //GetComponent<Animator>().SetBool("Open", open);
        }
      
    }
}
