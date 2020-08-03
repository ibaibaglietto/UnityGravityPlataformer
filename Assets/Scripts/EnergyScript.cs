using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyScript : MonoBehaviour
{

    //The gameobject of the player
    private GameObject player;


    
    void Start()
    {
        //Save the gameobject of the player
        player = GameObject.Find("Player");
    }

    // We destroy the energy when it comes too close to the player
    void Update()
    {
        if (Mathf.Abs(player.transform.position.x - transform.transform.position.x) < 0.05f) Destroy(gameObject);
    }
}
