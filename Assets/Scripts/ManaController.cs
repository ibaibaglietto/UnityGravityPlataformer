using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaController : MonoBehaviour
{
    //The player
    private GameObject player;
    //The mana bar
    private GameObject manaBar;
    //The actual mana the player has
    public float mana;
    //The maximum mana the player can have
    public float maxMana;

    //We initialize the mana and find everything
    void Awake()
    {           
        maxMana = Mathf.Sqrt(2000 * PlayerPrefs.GetInt("manaLevel")) + 5;
        player = GameObject.Find("Player");
        manaBar = GameObject.Find("Playermana");
        //We check if the player has changed scene or if it is spawning in a bench
        if (PlayerPrefs.GetInt("hasDied") == 0)
        {
            mana = PlayerPrefs.GetFloat("mana");
        }
        else if (PlayerPrefs.GetInt("hasDied") > 0 && PlayerPrefs.GetInt("lastDialogue") >= 7) mana = maxMana;
        else mana = 0.0f;

    }

    //We check the mana bar every frame
    void Update()
    {
        manaBar.GetComponent<Image>().fillAmount = mana / maxMana;
    }

    void FixedUpdate()
    {
        //If the player has mana it continues spending normally
        if (mana > 0.0f)
        {
            mana -= player.GetComponent<PlayerMovement>().spendingMana;
            player.GetComponent<PlayerMovement>().hasMana = true;
        }
        //If the player losses all her mana we stop healing and put the gravity to the ground
        else if(player.GetComponent<PlayerMovement>().hasMana && mana <= 0.0f)
        {
            mana = 0.0f;
            player.GetComponent<PlayerMovement>().healing = false;
            player.GetComponent<PlayerMovement>().changeGravity(false, 1.0f,0.0f,0.0f,0.0f);           
        }
        //We check if the player has full mana or not
        if (mana > maxMana) mana = maxMana;
        if (mana > (maxMana-0.2f)) player.GetComponent<PlayerMovement>().fullMana = true;
        else player.GetComponent<PlayerMovement>().fullMana = false;
    }
}
