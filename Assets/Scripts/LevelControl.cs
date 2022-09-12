using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelControl : MonoBehaviour
{
    public static int  currentLevel;
    void Start()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
