using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    public delegate void OnActivation();
    public event OnActivation activate;
    public WeightClass weight = WeightClass.Light;
    public bool isHoldable;
    public Rigidbody rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        if(rb != null) isHoldable = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Action()
    {
        if(activate != null)
            activate();
    }
}

[Flags]
public enum WeightClass
{
    Light = 0b_0000_0001,
    Moderate = 0b_0000_0010,
    Heavy = 0b_0000_0100,
    Monkey = 0b_0000_1000,
    General = Light | Moderate
}
