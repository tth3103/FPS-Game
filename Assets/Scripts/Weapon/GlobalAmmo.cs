using UnityEngine;
using UnityEngine.UI;
public class GlobalAmmo : MonoBehaviour
{
    public GameObject AmmoUI;
    public GameObject MagUI;

    public static int currentAmmo;
    public static int currentMag;

    private void Start()
    {
        currentAmmo = 0;
        currentMag = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (currentAmmo < 1) currentAmmo = 0;
        AmmoUI.GetComponent<Text>().text = "Ammo: " + currentAmmo;
        if (currentMag < 1) currentMag = 0;
        MagUI.GetComponent<Text>().text = "Mag: " + currentMag;
    }
}
