using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GlobalHealth : MonoBehaviour
{
    public static int currentHealth;
    
    private int maxHealth=100;
    
    public GameObject HpUI;
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        HpUI.GetComponent<Text>().text = "HP: " + currentHealth;
    }
}
