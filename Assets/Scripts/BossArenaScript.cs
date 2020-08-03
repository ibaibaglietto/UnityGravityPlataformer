using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArenaScript : MonoBehaviour
{
    //Funtion to start the sound of the arena movement
    public void startSound()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }
    //Funtion to stop the sound of the arena movement
    public void stopSound()
    {
        gameObject.GetComponent<AudioSource>().Stop();
    }

}
