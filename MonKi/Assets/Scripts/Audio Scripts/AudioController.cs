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
                aMixer.SetFloat("Master", -88);
                break;
            case 1:
                aMixer.SetFloat("Master", -40);
                break;
            case 2:
                aMixer.SetFloat("Master", -20);
                break;
            case 3:
                aMixer.SetFloat("Master", -10);
                break;
            case 4:
                aMixer.SetFloat("Master", -0);
                break;
            case 5:
                aMixer.SetFloat("Master", 10);
                break;
        }
    }
    public void VolumeMusic(Slider slider)
    {
        switch (slider.value)
        {
            case 0:
                aMixer.SetFloat("Music", -88);
                break;
            case 1:
                aMixer.SetFloat("Music", -40);
                break;
            case 2:
                aMixer.SetFloat("Music", -20);
                break;
            case 3:
                aMixer.SetFloat("Music", -10);
                break;
            case 4:
                aMixer.SetFloat("Music", -0);
                break;
            case 5:
                aMixer.SetFloat("Music", 10);
                break;
        }
    }
    public void VolumeSFX(Slider slider)
    {
        switch (slider.value)
        {
            case 0:
                aMixer.SetFloat("SFX", -88);
                break;
            case 1:
                aMixer.SetFloat("SFX", -40);
                break;
            case 2:
                aMixer.SetFloat("SFX", -20);
                break;
            case 3:
                aMixer.SetFloat("SFX", -10);
                break;
            case 4:
                aMixer.SetFloat("SFX", -0);
                break;
            case 5:
                aMixer.SetFloat("SFX", 10);
                break;
        }
    }
}
