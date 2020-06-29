using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthController : MonoBehaviour
{
    //The boss
    private GameObject king;
    //The healthbar
    private GameObject healthBar;
    //The background of the healthbar
    private GameObject backBar;
    //The current health of the player
    private float health;
    //The maximum health the player can have
    public float maxHealth;
    //The player
    private GameObject player;
    //A boolean to see if the fight has started
    public bool fighting;
    //The animator off the boss fight arena
    private Animator bossArena;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 1500.0f;
        king = GameObject.Find("King");
        healthBar = GameObject.Find("Bosshealth");
        backBar = GameObject.Find("BossHealthBar");
        health = maxHealth;
        player = GameObject.Find("Player");
        backBar.GetComponent<Image>().enabled = false;
        healthBar.GetComponent<Image>().enabled = false;
        fighting = false;
        if (player.GetComponent<PlayerMovement>().scene == 3) bossArena = GameObject.Find("Boss platform").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.GetComponent<Image>().fillAmount = health / maxHealth;
        if(fighting && !king.GetComponent<Animator>().GetBool("IsDead") && !player.GetComponent<PlayerMovement>().talking)
        {
            backBar.GetComponent<Image>().enabled = true;
            healthBar.GetComponent<Image>().enabled = true;
            king.GetComponent<KingScript>().fighting = true;
        }
    }

    void FixedUpdate()
    {
        if (fighting)
        {
            if (health > 0.0f)
            {
                health -= king.GetComponent<KingScript>().damage;
                king.GetComponent<KingScript>().damage = 0f;
                if (health < 750.0f && !king.GetComponent<KingScript>().fase2) king.GetComponent<Animator>().SetBool("EnterFase2", true);
            }
            else
            {
                king.GetComponent<Animator>().SetBool("IsDead", true);
                bossArena.SetBool("Close", false);
                backBar.GetComponent<Image>().enabled = false;
                healthBar.GetComponent<Image>().enabled = false;
            }
        }        
    }
}
