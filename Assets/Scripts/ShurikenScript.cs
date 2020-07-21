using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenScript : MonoBehaviour
{
    //The gameobject of the player
    private GameObject player;


    void Start()
    {
        //Save the gameobject of the player
        player = GameObject.Find("Player");
    }

    void Update()
    {
        //Destroy the shuriken if it's so far away from the player
        if (Mathf.Abs(player.transform.position.x - transform.transform.position.x) + Mathf.Abs(player.transform.position.y - transform.transform.position.y) > 25.0f) Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Button")
        {
            collision.transform.parent.gameObject.GetComponent<AudioSource>().Play();
            collision.GetComponent<AudioSource>().Play();
            collision.transform.parent.gameObject.GetComponent<Animator>().SetTrigger("Open");
            if (PlayerPrefs.GetInt("trap") == 0)
            {
                if (collision.transform.parent.gameObject.transform.position.x == -81.48338f) PlayerPrefs.SetInt("trap",1);
            }            
        }
        else if (collision.tag == "Wall") Destroy(gameObject);
    }
}
