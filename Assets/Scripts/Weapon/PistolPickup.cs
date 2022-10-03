using System.Collections;
using UnityEngine;
public class PistolPickup : MonoBehaviour
{
    public AudioSource pickUpAudio;
    public GameObject playerPistol;
    public GameObject fakeWeapon;
    public GameObject crossHair;

    private void OnTriggerEnter()
    {
        playerPistol.SetActive(true);
        NotifUI.notif = "Weapon picked up";
        NotifUI.isTrigger = true;
        fakeWeapon.SetActive(false);
        crossHair.SetActive(true);
        pickUpAudio.Play();
        
    }
}
