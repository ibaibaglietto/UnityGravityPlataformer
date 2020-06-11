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
        player = GameObject.Find("Player");
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == player.GetComponent<Collider2D>())
        {
            player.GetComponent<PlayerMovement>().canRest = true;
            player.GetComponent<PlayerMovement>().restPos = restPos;
            player.GetComponent<PlayerMovement>().benchScene = scene;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == player.GetComponent<Collider2D>())
        {
            player.GetComponent<PlayerMovement>().canRest = false;
        }
    }
}
