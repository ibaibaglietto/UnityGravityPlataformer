using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    // We check if the player has already been trapped, and if so we open all the doors
    void Start()
    {
        if (PlayerPrefs.GetInt("trap") == 3) gameObject.GetComponent<Animator>().SetBool("Opened", true);
        else gameObject.GetComponent<Animator>().SetBool("Opened", false);
    }

}
