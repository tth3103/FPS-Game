using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        HpUI.GetComponent<Text>().text = "HP: " + currentHealth;
    }
}
