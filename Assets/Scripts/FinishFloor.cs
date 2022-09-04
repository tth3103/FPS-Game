using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FinishFloor : MonoBehaviour
{
    public GameObject fadeScreen;
    private void OnTriggerEnter()
    {
        StartCoroutine(FinishLevel());
    }
    IEnumerator FinishLevel()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        fadeScreen.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("FinishScene");
    }
}
