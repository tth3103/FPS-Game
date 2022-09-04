using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCasting : MonoBehaviour
{
    public static bool enemyInRange;
    private float range=7;
    public static GameObject enemy;

    void Update()
    {
        Vector3 direction = transform.forward;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit, range))
        {
            if(hit.collider.tag != "Enemy")
            {
                enemyInRange = false;
            }
            if(hit.collider.tag == "Enemy")
            {
                enemyInRange = true;
                enemy = hit.transform.gameObject;
            }
        }
    }
}
