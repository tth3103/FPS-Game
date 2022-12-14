using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class PistolFire : MonoBehaviour
{
    public GameObject pistolFlash;
    public GameObject pistol;

    public AudioSource pistolFireSound;
    public AudioSource pistolReloadSound;
    public AudioSource pistolEmptySound;

    private bool isFiring = false;
    private bool isReloading = false;
    
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isReloading)
        {
            if(GlobalAmmo.currentAmmo < 1)
            {
                pistolEmptySound.Play();
                NotifUI.isTrigger = true;
                NotifUI.notif = "Empty ammo";
            }
            else if (!isFiring)
            {
                StartCoroutine(PistolFiring());
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (GlobalAmmo.currentMag < 1)
            {
                pistolEmptySound.Play();
                NotifUI.isTrigger = true;
                NotifUI.notif = "Insufficient mag";
            }
            else if (!isReloading)
            {
                StartCoroutine(PistolReloading());
            }
        }
    }
    private IEnumerator PistolFiring()
    {
        isFiring = true;
        pistol.GetComponent<Animator>().Play("Shooting");
        pistolFlash.SetActive(true);
        pistolFireSound.Play();

        if (PlayerCasting.enemyInRange)
        {
            if (LevelControl.currentLevel != 9)
            {
                PlayerCasting.enemy.GetComponent<EnemyAI>().TakeDamage(10);
            }
            else
            {
                PlayerCasting.enemy.GetComponent<BossAI>().TakeDamage(10);
            }
        }
        
        GlobalAmmo.currentAmmo--;
        yield return new WaitForSeconds(0.5f);
        pistolFlash.SetActive(false);
        yield return new WaitForSeconds(0.05f);
        pistol.GetComponent<Animator>().Play("New State");
        isFiring = false;
    }
    private IEnumerator PistolReloading()
    {
        isReloading = true;
        pistol.GetComponent<Animator>().Play("Reload");
        NotifUI.isTrigger = true;
        NotifUI.notif = "Reloading";
        pistolReloadSound.Play();
        GlobalAmmo.currentMag--;
        GlobalAmmo.currentAmmo = 10;
        yield return new WaitForSeconds(1.75f);
        pistol.GetComponent<Animator>().Play("New State");
        isReloading = false;
    }
}
