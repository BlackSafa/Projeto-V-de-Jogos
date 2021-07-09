using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Macacosvoice : MonoBehaviour
{
    // Start is called before the first frame update
   
    // Update is called once per frame
    void Update()
    {
        
    }

    public void MamacoFalaFPD()
    {
        GetComponent<AudioSource>().Play();
    }
}
