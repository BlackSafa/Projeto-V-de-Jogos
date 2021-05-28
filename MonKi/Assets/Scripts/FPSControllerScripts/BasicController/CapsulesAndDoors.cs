﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsulesAndDoors : MonoBehaviour
{
    // Start is called before the first frame update
    public InteractiveObject fechadura;
    private bool open;
    void Start()
    {
        fechadura.activate += DoorAnimation;
    }

    // Update is called once per frame
    
    // ReSharper disable Unity.PerformanceAnalysis
    void DoorAnimation()
    {
        if (!open)
        {
            GetComponent<Animator>().SetBool("Open", true);
            open = true;
        }
        else
        {
            GetComponent<Animator>().SetBool("Open", false);
            open = false;
        }
    }
}
