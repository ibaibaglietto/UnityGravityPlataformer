using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestScript : MonoBehaviour
{
    //The player
    private GameObject player;
    //The position the player rests
    public float restPos;
    //The scene the bench is
    public int scene;


    void Start()
    {
        //We find the player
        player = GameObject.Find("Player");
    }

    //If the player enters the collider she can rest
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == player.GetComponent<Collider2D>())
        {
            player.GetComponent<PlayerMovement>().canRest = true;
            player.GetComponent<PlayerMovement>().restPos = restPos;
            player.GetComponent<PlayerMovement>().benchScene = scene;
        }
    }

    //If the player exits the collider she cant rest
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == player.GetComponent<Collider2D>())
        {
            player.GetComponent<PlayerMovement>().canRest = false;
        }
    }
}
