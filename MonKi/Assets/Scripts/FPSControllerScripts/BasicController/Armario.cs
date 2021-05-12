using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class Armario : InteractiveObject
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
        abrirArmario();
    }

    void abrirArmario()
    {
        if (!open)
        {
            Debug.Log("Abriu");
        }
      
    }
}
