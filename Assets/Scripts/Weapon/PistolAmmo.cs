using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolAmmo : MonoBehaviour
{
    public AudioSource pickUpAudio;
    public GameObject fakeAmmo;
    private void OnTriggerEnter()
    {
        if (GlobalAmmo.currentMag >= 5)
        {
            return;
        }
        else {
           fakeAmmo.SetActive(false);
           pickUpAudio.Play();
           GlobalAmmo.currentMag++;
        }
        
    }
}
