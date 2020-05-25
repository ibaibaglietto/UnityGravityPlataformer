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
    private float health;
    //The maximum health the player can have
    public float maxHealth;
    //The animator of the player
    public Animator playerAnimator;
    //The mana bar
    private GameObject manaBar;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 100.0f;
        player = GameObject.Find("Player");
        healthBar = GameObject.Find("Playerhealth");
        manaBar = GameObject.Find("Manabar");
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.GetComponent<Image>().fillAmount = health / maxHealth;
    }

    void FixedUpdate()
    {
        if (health > 0.0f)
        {
            health -= player.GetComponent<PlayerMovement>().gravityDamage + player.GetComponent<PlayerMovement>().trapDamage + player.GetComponent<PlayerMovement>().enemyDamage;
            player.GetComponent<PlayerMovement>().trapDamage = 0.0f;
            player.GetComponent<PlayerMovement>().enemyDamage = 0.0f;
            if (health < 100.0f && player.GetComponent<PlayerMovement>().hasMana && player.GetComponent<PlayerMovement>().healing)
            {
                manaBar.GetComponent<ManaController>().mana -= 0.05f;
                health += 0.05f;
            }
            else if (health < 100.0f && player.GetComponent<PlayerMovement>().sleeping) health += 0.2f;
            if (health > 100.0f) health = 100.0f;
        }
        else
        {
            player.GetComponent<PlayerMovement>().gravityDown = 1.0f;
            player.GetComponent<PlayerMovement>().gravityUp = 0.0f;
            player.GetComponent<PlayerMovement>().gravityLeft = 0.0f;
            player.GetComponent<PlayerMovement>().gravityRight = 0.0f;
            player.GetComponent<PlayerMovement>().rotating = true;
            player.GetComponent<PlayerMovement>().prevVelocity = player.GetComponent<Rigidbody2D>().velocity;
            player.GetComponent<PlayerMovement>().spendingMana = 0.0f;
            playerAnimator.SetBool("isDead", true);
        }
    }
}
