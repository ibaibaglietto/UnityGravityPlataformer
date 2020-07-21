using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFloorScript : MonoBehaviour
{


    void Update()
    {
        
    }

    public void StopSound()
    {
        gameObject.GetComponent<AudioSource>().Stop();
    }
}
