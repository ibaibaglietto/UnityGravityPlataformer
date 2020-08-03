using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboDamageScript : MonoBehaviour
{
    //The player
    private GameObject player;
    //The healthbar
    private GameObject healthBar;
    //THe damage
    public float damage;

    void Start()
    {
        //find the player
        player = GameObject.Find("Player");
        //Find the healthbar
        healthBar = GameObject.Find("Healthbar");
    }

    //If the player enters the collider and she hasn't taken damage in the previous 2 seconds she will take damage
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other == player.GetComponent<Collider2D>() && ((Time.fixedTime - player.GetComponent<PlayerMovement>().lastDamage) > 2.0f))
        {
            player.GetComponent<Animator>().SetBool("takeDamage", true);
            player.GetComponent<PlayerMovement>().takingDamage = true;
            player.GetComponent<PlayerMovement>().lastDamage = Time.fixedTime - 1.7f;
            healthBar.GetComponent<PlayerLifeController>().receiveDamage(damage);
        }
    }

    //function to destroy the fase2combo
    public void destroyFase2()
    {
        Destroy(gameObject);
    }

}
