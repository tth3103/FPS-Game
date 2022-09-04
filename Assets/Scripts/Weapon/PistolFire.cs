using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            PlayerCasting.enemy.GetComponent<EnemyAI>().TakeDamage(10);
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
        pistolReloadSound.Play();
        GlobalAmmo.currentMag--;
        GlobalAmmo.currentAmmo = 10;
        yield return new WaitForSeconds(1.75f);
        pistol.GetComponent<Animator>().Play("New State");
        isReloading = false;
    }
}
