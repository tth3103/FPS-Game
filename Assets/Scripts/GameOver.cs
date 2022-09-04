using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    public GameObject confirmBox;
    private void Update()
    {
        StartCoroutine(Waiting());
    }
    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(2);
        confirmBox.SetActive(true);
    }
    public void Continue() 
    {
        SceneManager.LoadScene("Scene1");
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
}
