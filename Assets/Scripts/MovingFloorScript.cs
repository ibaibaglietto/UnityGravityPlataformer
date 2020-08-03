using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFloorScript : MonoBehaviour
{
    //The time the sound started 
    private float soundTime;

    void Start()
    {
        //We initialize the sound time
        soundTime = 0.0f;
    }

    void Update()
    {
        //If the sound has started and was more than 1 second ago we stop it
        if ((Time.fixedTime - soundTime) > 1.0f && soundTime != 0.0f)
        {
            gameObject.GetComponent<AudioSource>().Stop();
            soundTime = 0.0f;
        }
    }

    //Function to start the sound
    public void StopSound()
    {
        if (PlayerPrefs.GetInt("trap") != 3)
        {
            gameObject.GetComponent<AudioSource>().Play();
            soundTime = Time.fixedTime;
        }
    }
}
