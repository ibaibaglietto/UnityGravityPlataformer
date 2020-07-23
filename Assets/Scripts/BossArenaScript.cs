using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArenaScript : MonoBehaviour
{
    public void startSound()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }
    public void stopSound()
    {
        gameObject.GetComponent<AudioSource>().Stop();
    }

}
