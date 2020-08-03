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
    //The animator of the boss fight arena
    private Animator bossArena;
    //The music
    public AudioClip normalMusic;
    public AudioClip bossMusic;
    //The musicSource
    private AudioSource musicSource;

    //We initialize and find everything
    void Start()
    {
        maxHealth = 1500.0f;
        king = GameObject.Find("King");
        healthBar = GameObject.Find("Bosshealth");
        backBar = GameObject.Find("BossHealthBar");
        health = maxHealth;
        player = GameObject.Find("Player");
        musicSource = GameObject.Find("MusicSource").GetComponent<AudioSource>();
        backBar.GetComponent<Image>().enabled = false;
        healthBar.GetComponent<Image>().enabled = false;
        fighting = false;
        if (player.GetComponent<PlayerMovement>().scene == 3) bossArena = GameObject.Find("Boss platform").GetComponent<Animator>();
    }

    //We check if the battle has started and update the boss health bar
    void Update()
    {
        healthBar.GetComponent<Image>().fillAmount = health / maxHealth;
        if(fighting && !king.GetComponent<Animator>().GetBool("IsDead") && !player.GetComponent<PlayerMovement>().talking && !backBar.GetComponent<Image>().enabled)
        {
            musicSource.clip = bossMusic;
            musicSource.Play();
            backBar.GetComponent<Image>().enabled = true;
            healthBar.GetComponent<Image>().enabled = true;
            king.GetComponent<KingScript>().fighting = true;
        }
    }

    
    void FixedUpdate()
    {
        if (fighting)
        {
            //We check the life of the boss to deal damage and to enter 2nd fase if he has left less than 1/2 of his life
            if (health > 0.0f)
            {
                health -= king.GetComponent<KingScript>().damage;
                king.GetComponent<KingScript>().damage = 0f;
                if (health < 750.0f && !king.GetComponent<KingScript>().fase2) king.GetComponent<Animator>().SetBool("EnterFase2", true);
            }
            //When the boss dies we resume the normal music and make dissapear the health bar
            else if(backBar.GetComponent<Image>().enabled)
            {
                musicSource.clip = normalMusic;
                musicSource.Play();
                king.GetComponent<Animator>().SetBool("IsDead", true);
                bossArena.gameObject.GetComponent<AudioSource>().Play();
                bossArena.SetBool("Close", false);
                backBar.GetComponent<Image>().enabled = false;
                healthBar.GetComponent<Image>().enabled = false;
            }
        }        
    }
}
