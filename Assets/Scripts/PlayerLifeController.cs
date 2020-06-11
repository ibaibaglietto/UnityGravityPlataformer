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

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = Mathf.Sqrt(2000 * PlayerPrefs.GetInt("healthLevel")) + 55;
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
            health -= (player.GetComponent<PlayerMovement>().gravityDamage + player.GetComponent<PlayerMovement>().trapDamage + player.GetComponent<PlayerMovement>().enemyDamage) * ((100 - ((Mathf.Sqrt(150 * PlayerPrefs.GetInt("damageResistanceLevel"))) - 12.247f)) * 0.01f);
            player.GetComponent<PlayerMovement>().trapDamage = 0.0f;
            player.GetComponent<PlayerMovement>().enemyDamage = 0.0f;
            if (health < maxHealth && player.GetComponent<PlayerMovement>().hasMana && player.GetComponent<PlayerMovement>().healing)
            {
                manaBar.GetComponent<ManaController>().mana -= (Mathf.Sqrt(2 * PlayerPrefs.GetInt("healingLevel")) + 1.1f)/50f;
                health += (Mathf.Sqrt(2 * PlayerPrefs.GetInt("healingLevel")) + 1.1f) / 50f;
            }
            else if (health < maxHealth && player.GetComponent<PlayerMovement>().sleeping) health += 0.2f;
            if (health > maxHealth) health = maxHealth;
        }
        else if(!playerAnimator.GetBool("isDead"))
        {
            player.GetComponent<PlayerMovement>().changeGravity(false, 1.0f, 0.0f, 0.0f, 0.0f);
            playerAnimator.SetBool("isDead", true);
            PlayerPrefs.SetInt("hasDied", 2);
            PlayerPrefs.SetFloat("diedx", player.transform.position.x);
            PlayerPrefs.SetFloat("diedy", player.transform.position.y - 0.257f);
            PlayerPrefs.SetInt("diedscene", player.GetComponent<PlayerMovement>().scene);
        }
    }
}
