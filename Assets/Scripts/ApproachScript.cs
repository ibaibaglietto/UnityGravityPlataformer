using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachScript : MonoBehaviour
{
    //the player
    private GameObject player;
    //The musicSource
    private AudioSource musicSource;

    //We find the player and the music source
    void Start()
    {
        player = GameObject.Find("Player");
        musicSource = GameObject.Find("MusicSource").GetComponent<AudioSource>();
    }

    //When the player enters the collider she will start walking to the right
    public void OnTriggerEnter2D(Collider2D collision)
    {
        musicSource.Stop();
        if (player.GetComponent<PlayerMovement>().changingGravity) player.GetComponent<PlayerMovement>().changingGravity = false;
        if (player.GetComponent<PlayerMovement>().healing) player.GetComponent<PlayerMovement>().healing = false;
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        player.GetComponent<PlayerMovement>().changeGravity(true, 0.3f, 0.0f, 0.0f, 0.0f);
        if (!player.GetComponent<PlayerMovement>().m_FacingRight) player.GetComponent<PlayerMovement>().Flip();
        player.GetComponent<PlayerMovement>().approach = true;
    }
}
