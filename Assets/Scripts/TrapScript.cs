using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    //The player
    private GameObject player;
    //The healthbar
    private GameObject healthBar;
    //The x where the dieExp will appear
    public float dieExpx;
    //The y where the dieExp will appear
    public float dieExpy;



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
        if(other == player.GetComponent<Collider2D>() && (Time.fixedTime - player.GetComponent<PlayerMovement>().lastDamage) > 2.0f)
        {
            player.GetComponent<Animator>().SetBool("takeDamage", true);
            player.GetComponent<PlayerMovement>().takingDamage = true;
            player.GetComponent<PlayerMovement>().lastDamage = Time.fixedTime;
            healthBar.GetComponent<PlayerLifeController>().receiveDamage(20.0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == player.GetComponent<Collider2D>())
        {
            player.GetComponent<PlayerMovement>().trap = true;
            PlayerPrefs.SetFloat("diedx", dieExpx);
            PlayerPrefs.SetFloat("diedy", dieExpy);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == player.GetComponent<Collider2D>())
        {
            player.GetComponent<PlayerMovement>().trap = false;
        }
    }
}
