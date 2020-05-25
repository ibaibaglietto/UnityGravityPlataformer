using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyScript : MonoBehaviour
{

    //The gameobject of the player
    private GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        //Save the gameobject of the player
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(player.transform.position.x - transform.transform.position.x) < 0.05f) Destroy(gameObject);
    }
}
