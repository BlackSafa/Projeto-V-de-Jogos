using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsulesAndDoors : InteractiveObject
{
    // Start is called before the first frame update
    private bool open;
    void Start()
    {
        isHoldable = false;
    }

    // Update is called once per frame
    public override void Action()
    {
    
            DoorAnimation();
       
    }
    
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
