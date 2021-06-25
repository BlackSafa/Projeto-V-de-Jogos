using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsulesAndDoors : MonoBehaviour
{
    // Start is called before the first frame update
    public InteractiveObject fechadura;
    public GenericCheckPointScript trocaSala;
    [SerializeField]
    private bool open;
    void Start()
    {
        if(fechadura != null)
            fechadura.activate += DoorAnimation;
        if(trocaSala != null)
            trocaSala.closeDoor += DoorAnimation;

        if(open)
        {
            open = false;
            DoorAnimation();
        }
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
