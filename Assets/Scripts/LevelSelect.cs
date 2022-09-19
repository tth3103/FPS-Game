using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelSelect : MonoBehaviour
{
    public AudioSource clickSound;
    AudioSource bgm;
    public void Start()
    {
        bgm = GetComponent<AudioSource>();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        bgm.Play();
    }
    public void Floor_1()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        bgm.Stop();
        clickSound.Play();
        SceneManager.LoadScene("Floor1");
    }
    public void Floor_2()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        bgm.Stop();
        clickSound.Play();
        SceneManager.LoadScene("Floor2");
    }
    public void Floor_3()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        bgm.Stop();
        clickSound.Play();
        SceneManager.LoadScene("Floor3");
    }
    public void Level_4()
    {
        clickSound.Play();
        Debug.Log("No scene available");
    }
    public void ReturnToMainMenu()
    {
        clickSound.Play();
        SceneManager.LoadScene("MainMenu");
    }
}
