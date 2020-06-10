using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("trap") == 3) gameObject.GetComponent<Animator>().SetBool("Opened", true);
        else gameObject.GetComponent<Animator>().SetBool("Opened", false);
    }

}
