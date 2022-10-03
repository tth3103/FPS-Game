using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public AudioSource clickSound;
    AudioSource bgm;

    public Button[] lvlButtons;
    public GameObject[] lockSprites;

    public void Awake()
    {
        bgm = GetComponent<AudioSource>();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        bgm.Play();
        int levelAt = PlayerPrefs.GetInt("PlayerAt", 4);
        for (int i = 0; i < lvlButtons.Length; i++)
        {
            if (i + 4 > levelAt)
            {
                lvlButtons[i].interactable = false;
                lockSprites[i].SetActive(true);
            }

        }
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
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        bgm.Stop();
        clickSound.Play();
        SceneManager.LoadScene("Floor4");
    }
    public void Level_5()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        bgm.Stop();
        clickSound.Play();
        SceneManager.LoadScene("Floor5");
    }
    public void Level_6()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        bgm.Stop();
        clickSound.Play();
        SceneManager.LoadScene("Floor6");
    }
    public void ReturnToMainMenu()
    {
        clickSound.Play();
        SceneManager.LoadScene("MainMenu");
    }
    public void ResetProgress()
    {
        clickSound.Play();
        PlayerPrefs.SetInt("PlayerAt", 4);
        SceneManager.LoadScene("LevelSelectScene");
    }
}
