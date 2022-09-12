using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    AudioSource menuBGM;
    public AudioSource clickSound;
    public GameObject settingPanel;
    public void Start()
    {
        menuBGM = GetComponent<AudioSource>();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        menuBGM.Play();
    }
    public void Play()
    {
        clickSound.Play();
        SceneManager.LoadScene("LevelSelectScene");
        menuBGM.Stop();
    }
    public void Settings()
    {
        clickSound.Play();
        settingPanel.SetActive(true);
    }
    public void Exit()
    {
        clickSound.Play();
        Application.Quit();
    }
}
