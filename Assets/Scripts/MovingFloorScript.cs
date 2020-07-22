using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFloorScript : MonoBehaviour
{
    private float soundTime;

    void Start()
    {
        soundTime = 0.0f;
    }

    void Update()
    {
        if ((Time.fixedTime - soundTime) > 1.0f && soundTime != 0.0f)
        {
            gameObject.GetComponent<AudioSource>().Stop();
            soundTime = 0.0f;
        }
    }

    public void StopSound()
    {
        if (PlayerPrefs.GetInt("trap") != 3)
        {
            gameObject.GetComponent<AudioSource>().Play();
            soundTime = Time.fixedTime;
        }
    }
}
