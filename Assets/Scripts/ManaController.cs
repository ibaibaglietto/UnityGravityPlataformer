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
    void Awake()
    {
        //We initialize all the playerprefs on the awake
        //The mana level
        if (!PlayerPrefs.HasKey("manaLevel")) PlayerPrefs.SetInt("manaLevel", 1);
        //The dealt damage level
        if (!PlayerPrefs.HasKey("dealtDamageLevel")) PlayerPrefs.SetInt("dealtDamageLevel", 1);
        //The stamina level
        if (!PlayerPrefs.HasKey("staminaLevel")) PlayerPrefs.SetInt("staminaLevel", 1);
        //The healing level
        if (!PlayerPrefs.HasKey("healingLevel")) PlayerPrefs.SetInt("healingLevel", 1);
        //The damage resistance level
        if (!PlayerPrefs.HasKey("damageResistanceLevel")) PlayerPrefs.SetInt("damageResistanceLevel", 1);
        //The exp gaining level
        if (!PlayerPrefs.HasKey("expGainingLevel")) PlayerPrefs.SetInt("expGainingLevel", 1);
        //The total lvl of the player
        if (!PlayerPrefs.HasKey("lvl")) PlayerPrefs.SetInt("lvl", 1);
        //The needed exp to lvl up
        if (!PlayerPrefs.HasKey("needExp")) PlayerPrefs.SetInt("needExp", 30);
        //The exp 
        if (!PlayerPrefs.HasKey("exp")) PlayerPrefs.SetInt("exp", 0);
        //A float to save the current mana
        if (!PlayerPrefs.HasKey("mana")) PlayerPrefs.SetFloat("mana", 0.0f);
        //An int to see if the exp tutorial has been shown
        if (!PlayerPrefs.HasKey("expTutorial")) PlayerPrefs.SetInt("expTutorial", 0);
        //A float to save the x of the respawn point
        if (!PlayerPrefs.HasKey("respawnx")) PlayerPrefs.SetFloat("respawnx", -49.826f);
        //A float to save the y of the respawn point
        if (!PlayerPrefs.HasKey("respawny")) PlayerPrefs.SetFloat("respawny", -3.367f);
        //A float to save the side the player is facing on the respawn point. 0-> left, 1-> right
        if (!PlayerPrefs.HasKey("respawnface")) PlayerPrefs.SetInt("respawnface", 1);
        //An int to save the scene of the respawn point
        if (!PlayerPrefs.HasKey("respawnscene")) PlayerPrefs.SetInt("respawnscene", 0);
        //An int to save the number of the las dialogue
        if (!PlayerPrefs.HasKey("lastDialogue")) PlayerPrefs.SetInt("lastDialogue", 0);
        //0 -> scene change, 1 -> to respawn when loading the game, 2 -> Player died
        if (!PlayerPrefs.HasKey("hasDied")) PlayerPrefs.SetInt("hasDied", 1);
        //A float to save the x of the spawn point (when we move from one scene to another without dieing)
        if (!PlayerPrefs.HasKey("spawnx")) PlayerPrefs.SetFloat("spawnx", -49.76f);
        //A float to save the y of the spawn point (when we move from one scene to another without dieing)
        if (!PlayerPrefs.HasKey("spawny")) PlayerPrefs.SetFloat("spawny", -3.367f);
        //A float to save the side the player is facing on the spawn point. 0-> left, 1-> right
        if (!PlayerPrefs.HasKey("spawnface")) PlayerPrefs.SetInt("spawnface", 0);
        //A float to see if the player has fallen into the trap of level 1-2
        if (!PlayerPrefs.HasKey("trap")) PlayerPrefs.SetInt("trap", 0);
        //An int to save the exp the player has when resting
        if (!PlayerPrefs.HasKey("restExp")) PlayerPrefs.SetInt("restExp", 0);
        //A float to save the x where the player died
        if (!PlayerPrefs.HasKey("diedx")) PlayerPrefs.SetFloat("diedx", 0);
        //A float to save the y where the player died
        if (!PlayerPrefs.HasKey("diedy")) PlayerPrefs.SetFloat("diedy", 0);
        //An int to save the exp lost when dieing
        if (!PlayerPrefs.HasKey("diedexp")) PlayerPrefs.SetInt("diedexp", 0);
        //An int to save the scene the player died
        if (!PlayerPrefs.HasKey("diedscene")) PlayerPrefs.SetInt("diedscene", 0);
        //An int to save if the die tutorial has been seen
        if (!PlayerPrefs.HasKey("dieTutorial")) PlayerPrefs.SetInt("dieTutorial", 0);        
        maxMana = Mathf.Sqrt(2000 * PlayerPrefs.GetInt("manaLevel")) + 5;
        player = GameObject.Find("Player");
        manaBar = GameObject.Find("Playermana");
        if (PlayerPrefs.GetInt("hasDied") == 0)
        {
            mana = PlayerPrefs.GetFloat("mana");
        }
        else if (PlayerPrefs.GetInt("hasDied") > 0 && PlayerPrefs.GetInt("lastDialogue") >= 7) mana = maxMana;
        else mana = 0.0f;

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
            player.GetComponent<PlayerMovement>().changeGravity(false, 1.0f,0.0f,0.0f,0.0f);           
        }
        if (mana > maxMana) mana = maxMana;
        if (mana > (maxMana-0.2f)) player.GetComponent<PlayerMovement>().fullMana = true;
        else player.GetComponent<PlayerMovement>().fullMana = false;
    }
}
