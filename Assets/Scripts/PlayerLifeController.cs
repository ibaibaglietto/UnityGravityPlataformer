using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLifeController : MonoBehaviour
{
    //The player
    private GameObject player;
    //The healthbar
    private GameObject healthBar;
    //The current health of the player
    public float health;
    //The maximum health the player can have
    public float maxHealth;
    //The animator of the player
    public Animator playerAnimator;
    //The mana bar
    private GameObject manaBar;

    
    void Awake()
    {
        //We initialize the health depending on the players level
        maxHealth = Mathf.Sqrt(2000 * PlayerPrefs.GetInt("healthLevel")) + 55;
        //We find all the gameobjects
        player = GameObject.Find("Player");
        healthBar = GameObject.Find("Playerhealth");
        manaBar = GameObject.Find("Manabar");
        //We take the health if the player has changed scene
        if (PlayerPrefs.GetInt("hasDied") == 0)
        {
            health = PlayerPrefs.GetFloat("health");
        }
        else health = maxHealth;
    }

    void Update()
    {
        //We update the health every frame
        healthBar.GetComponent<Image>().fillAmount = health / maxHealth;
    }


    void FixedUpdate()
    {
        //If the player isnt dead 
        if (health > 0.0f)
        {
            //If the player is healing we add health and substract mana
            if (health < maxHealth && player.GetComponent<PlayerMovement>().hasMana && player.GetComponent<PlayerMovement>().healing)
            {
                manaBar.GetComponent<ManaController>().mana -= (Mathf.Sqrt(2 * PlayerPrefs.GetInt("healingLevel")) + 1.1f) / 50f;
                health += (Mathf.Sqrt(2 * PlayerPrefs.GetInt("healingLevel")) + 1.1f) / 50f;
            }
            //If the players health is full we stop healing
            else if (health >= maxHealth && player.GetComponent<PlayerMovement>().healing) player.GetComponent<PlayerMovement>().healing = false;
            //If the player is sleeping we heal her
            if (health < maxHealth && player.GetComponent<PlayerMovement>().sleeping) health += 0.2f;
            if (health > maxHealth) health = maxHealth;
        }
        //If the player is dead we change the gravity to the ground
        else if(!playerAnimator.GetBool("isDead"))
        {
            player.GetComponent<PlayerMovement>().changeGravity(false, 1.0f, 0.0f, 0.0f, 0.0f);
            playerAnimator.SetBool("isDead", true);
            PlayerPrefs.SetInt("hasDied", 2);
        }
    }

    //function to receive damage
    public void receiveDamage(float damage)
    {
        health -= damage * ((100 - ((Mathf.Sqrt(150 * PlayerPrefs.GetInt("damageResistanceLevel"))) - 12.247f)) * 0.01f);
    }

}
