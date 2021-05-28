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
        open = false;
    }

    // Update is called once per frame
    public override void Action(InteracterScript caller)
    {
        abrirArmario();
    }

    void abrirArmario()
    {
        if (!open)
        {
            Debug.Log("Abriu");
            open = true;
        }
        else
        {
            Debug.Log("Fechou");
            open = false;
        }
      
    }
}
