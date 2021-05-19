using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Painel : InteractiveObject
{
    // Start is called before the first frame update
    public int allKeys;
    public bool hasKey;
    void Start()
    {
        allKeys = 3;
        isHoldable = false;
    }

    void Update()
    {
    }

    public override void Action()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            aaa();
        }
    }

    void aaa()
    {
        if (allKeys >= 3)
        {
            Action();
        }
    }

    void addkeys()
    {
        if (hasKey)
        {
            allKeys++;
        }
    }
    // Update is called once per frame
        
}
