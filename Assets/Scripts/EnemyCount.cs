using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCount : MonoBehaviour
{
    public static int enemyDefeated;
    public int enemyInScene=0;

    private void Start()
    {
        enemyDefeated = 0;
    }
    void Update()
    {
        if (enemyDefeated > enemyInScene) enemyDefeated = enemyInScene;
    }
}
