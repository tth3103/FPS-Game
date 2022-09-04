using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void Play()
    {
        SceneManager.LoadScene("Scene1");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Settings()
    {

    }
    public void Exit()
    {
        Application.Quit();
    }
}
