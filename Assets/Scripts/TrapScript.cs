using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    //The player
    private GameObject player;
    //The healthbar
    private GameObject healthBar;


    void Start()
    {
        //Find the player
        player = GameObject.Find("Player");
        //Find the healthbar
        healthBar = GameObject.Find("Healthbar");
    }

    //If the player enters the collider and she hasn't taken damage in the previous 2 seconds she will take damage
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other == player.GetComponent<Collider2D>() && ((Time.fixedTime - player.GetComponent<PlayerMovement>().lastDamage) > 2.0f))
        {
            player.GetComponent<Animator>().SetBool("takeDamage", true);
            player.GetComponent<PlayerMovement>().takingDamage = true;
            player.GetComponent<PlayerMovement>().lastDamage = Time.fixedTime;
            healthBar.GetComponent<PlayerLifeController>().receiveDamage(20.0f);
        }
    }
}
