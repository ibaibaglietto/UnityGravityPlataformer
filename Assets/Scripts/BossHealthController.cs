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

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 1000.0f;
        king = GameObject.Find("King");
        healthBar = GameObject.Find("Bosshealth");
        backBar = GameObject.Find("BossHealthBar");
        health = maxHealth;
        player = GameObject.Find("Player");
        backBar.GetComponent<Image>().enabled = false;
        healthBar.GetComponent<Image>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.GetComponent<Image>().fillAmount = health / maxHealth;
        if(player.transform.position.x > -10.0f && player.transform.position.x < 16.5f && !king.GetComponent<Animator>().GetBool("IsDead"))
        {
            backBar.GetComponent<Image>().enabled = true;
            healthBar.GetComponent<Image>().enabled = true;
        }
    }

    void FixedUpdate()
    {
        if (health > 0.0f)
        {
            health -= king.GetComponent<KingScript>().damage;
            king.GetComponent<KingScript>().damage = 0f;
            if (health < 500.0f && !king.GetComponent<KingScript>().fase2) king.GetComponent<Animator>().SetBool("EnterFase2", true);
        }
        else
        {
            king.GetComponent<Animator>().SetBool("IsDead", true);
            backBar.GetComponent<Image>().enabled = false;
            healthBar.GetComponent<Image>().enabled = false;
        }
    }
}
