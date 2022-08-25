using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PistolPickup : MonoBehaviour
{
    public AudioSource pickUpAudio;
    public GameObject playerPistol;
    public GameObject fakeWeapon;
    public GameObject crossHair;

    private void OnTriggerEnter()
    {
        playerPistol.SetActive(true);
        fakeWeapon.SetActive(false);
        crossHair.SetActive(true);
        pickUpAudio.Play();
    }
}
