using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject theDoor;

    private void OnTriggerEnter()
    {
        theDoor.GetComponent<Animator>().Play("DoorOpen");
    }
    private void OnTriggerExit()
    {
        theDoor.GetComponent<Animator>().Play("DoorClose");
    }
}
