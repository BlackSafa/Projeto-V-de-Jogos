using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAnimControl : MonoBehaviour
{
    public PlayerMovementScript script;

    public void EndAnim()
    {
        script.movementPause = false;
    }

    public void WhatEnd(int eventNum)
    {
        int playing = 0;
        Debug.Log("Entrou no evento");
        Debug.Log(playing);
        if(script.animator.GetBool("Lift"))
        {
            playing = 2;
        }

        else if(script.animator.GetBool("Place"))
        {
            playing = 1;
        }
        Debug.Log(playing);
        if(eventNum == playing)
        {
            Debug.Log("Finalizou o evento");
            EndAnim();
        }
    }
}
