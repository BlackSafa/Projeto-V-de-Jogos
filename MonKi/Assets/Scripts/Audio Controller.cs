using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AudioMixer aMixer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     public void VolumeMaster(Slider slider)
    {
        switch (slider.value)
        {
            case 0:
               // aMixer.SetFloat("Mu")
                break;
            case 1:

                break;
            case 2:

                break;
            case 3:

                break;
            case 4:

                break;
            case 5:

                break;
        }
    }
    public void VolumeMusic(Slider slider)
    {
        switch (slider.value)
        {
            case 0:

                break;
            case 1:

                break;
            case 2:

                break;
            case 3:

                break;
            case 4:

                break;
            case 5:

                break;
        }
    }
    public void VolumeSFX(Slider slider)
    {
        switch (slider.value)
        {
            case 0:

                break;
            case 1:

                break;
            case 2:

                break;
            case 3:

                break;
            case 4:

                break;
            case 5:

                break;
        }
    }
}
