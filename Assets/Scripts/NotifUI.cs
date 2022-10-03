using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class NotifUI : MonoBehaviour
{
    public GameObject notifDisplay;
    public static string notif;
    public static bool isTrigger = false;
    // Update is called once per frame
    void Update()
    {
        if (isTrigger == true)
        {
            StartCoroutine(DisplayNotif());
        }
        else
        {
            return;
        }
    }
    public IEnumerator DisplayNotif()
    {
        notifDisplay.GetComponent<Text>().text = notif;
        notifDisplay.SetActive(true);
        notifDisplay.GetComponent<Animator>().Play("NotifUI");
        isTrigger = false;
        yield return new WaitForSeconds(1.5f); 
        notifDisplay.SetActive(false);
    }
}
