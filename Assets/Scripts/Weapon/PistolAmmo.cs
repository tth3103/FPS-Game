using UnityEngine;

public class PistolAmmo : MonoBehaviour
{
    public AudioSource pickUpAudio;
    public GameObject fakeAmmo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) { 
            if (GlobalAmmo.currentMag >= 5)
            {
                NotifUI.isTrigger = true;
                NotifUI.notif = "Max capacity reached";
                return;
            }
            else {
                NotifUI.isTrigger = true;
                NotifUI.notif = "Mag + 1";
                fakeAmmo.SetActive(false);
                pickUpAudio.Play();
                GlobalAmmo.currentMag++;
            }
        }
    }
}
