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

    // Start is called before the first frame update
    void Start()
    {
        maxMana = Mathf.Sqrt(2000 * PlayerPrefs.GetInt("manaLevel")) + 5;
        player = GameObject.Find("Player");
        manaBar = GameObject.Find("Playermana");
        mana = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        manaBar.GetComponent<Image>().fillAmount = mana / maxMana;
    }

    void FixedUpdate()
    {
        if (mana > 0.0f)
        {
            mana -= player.GetComponent<PlayerMovement>().spendingMana;
            player.GetComponent<PlayerMovement>().hasMana = true;
        }
        else if(player.GetComponent<PlayerMovement>().hasMana && mana <= 0.0f)
        {
            mana = 0.0f;
            player.GetComponent<PlayerMovement>().healing = false;
            player.GetComponent<PlayerMovement>().normalGravity();           
        }
        if (mana < maxMana && player.GetComponent<PlayerMovement>().sleeping) mana += 0.2f;
        if (mana > maxMana) mana = maxMana;
        if (mana > (maxMana-0.2f)) player.GetComponent<PlayerMovement>().fullMana = true;
        else player.GetComponent<PlayerMovement>().fullMana = false;
    }
}
