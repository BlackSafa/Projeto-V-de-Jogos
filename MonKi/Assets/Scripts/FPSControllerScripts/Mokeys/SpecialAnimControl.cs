using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAnimControl : MonoBehaviour
{
    public InteracterScript script;
    public Animator animator;

    public void EndAnimGrab()
    {
        script.movementPause = false;
        animator.SetBool("Grab",false);
    }

    public void WhatEnd()
    {
        script.movementPause = false;
        Debug.Log("Entrou no evento");
        if(animator.GetBool("Lift"))
        {
            animator.SetBool("Lift",false);
            animator.SetBool("Carrying", true);
            Debug.Log("Finalizou animação");
        }

        else if(animator.GetBool("Place"))
        {
            animator.SetBool("Place", false);
            animator.SetBool("Carrying", false);
            script.Drop();
        }
    }

}
