using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ShouderAnimationController : MonoBehaviourPunCallbacks
{
    public Animator shouder, original;

    // Update is called once per frame
    void Update()
    {
        if(photonView.IsMine)
        {
            if(original.GetFloat("Move") > 0.1f)
                shouder.SetBool("Walking", true);
            else
                shouder.SetBool("Walking", false);

            if(original.GetBool("Lift"))
                shouder.SetBool("Lifting", true);
            else
                shouder.SetBool("Lifting", false);

            if(original.GetBool("Place"))
                shouder.SetBool("Placing", true);
            else
                shouder.SetBool("Placing", false);
        }
    }
}
