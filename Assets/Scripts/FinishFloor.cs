using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FinishFloor : MonoBehaviour
{
    public GameObject fadeScreen;
    private void OnTriggerEnter()
    {
        StartCoroutine(CalcResult());
    }
    IEnumerator CalcResult()
    {
        fadeScreen.SetActive(true);
        yield return new WaitForSeconds(1);
    }
}
