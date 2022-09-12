using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FinishScene : MonoBehaviour
{
    //Text
    public GameObject hpScore;
    public GameObject enemyScore;
    public GameObject totalScore;

    //Button
    public GameObject menuButton;
    public GameObject nextFloorButton;

    //Score
    private int finalScore; 
    private void Start()
    {
        finalScore = 0;
        StartCoroutine(CalcResult());
    }
    IEnumerator CalcResult()
    {
        yield return new WaitForSeconds(1.5f);
        hpScore.GetComponent<Text>().text ="HP remaining: "+GlobalHealth.currentHealth;
        hpScore.SetActive(true);
        yield return new WaitForSeconds(1f);
        enemyScore.GetComponent<Text>().text = "Enemy defeated: " + EnemyCount.enemyDefeated;
        enemyScore.SetActive(true);
        yield return new WaitForSeconds(1f);
        finalScore = GlobalHealth.currentHealth * 50 + EnemyCount.enemyDefeated * 30;
        totalScore.GetComponent<Text>().text = "Total Score: "+finalScore;
        totalScore.SetActive(true);
        yield return new WaitForSeconds(2f);
        menuButton.SetActive(true);
        nextFloorButton.SetActive(true);
    }
    public void NextFloor()
    {
        if (LevelControl.currentLevel >= 3)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            SceneManager.LoadScene(LevelControl.currentLevel + 1);
        }
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
