using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    //4 gameobjects to represent the gravity to each side
    private GameObject g0;
    private GameObject g1;
    private GameObject g2;
    private GameObject g3;
    //The gameobject of the player
    private GameObject player;
    //The gameobject of the lvl up interface
    private GameObject lvlUp;
    //The exp text
    private Text exp;
    //The health level
    private Text healthLevel;
    //The mana level
    private Text manaLevel;
    //The dealt damage level
    private Text dealtDamageLevel;
    //The dash level
    private Text dashLevel;
    //The healing level
    private Text healingLevel;
    //The damage resistance level
    private Text damageResistanceLevel;
    //The exp gaining level
    private Text expGainingLevel;
    //The total level
    private Text lvl;
    //The exp needed to lvl up
    private Text needExp;
    //The exp left after lvl up
    private Text lvlUpExp;
    //The health level minus button
    private GameObject healthLevelMinus;
    //The mana level minus button
    private GameObject manaLevelMinus;
    //The dealt damage level minus button
    private GameObject dealtDamageLevelMinus;
    //The dash level minus button
    private GameObject dashLevelMinus;
    //The healing level minus button
    private GameObject healingLevelMinus;
    //The damage resistance level minus button
    private GameObject damageResistanceLevelMinus;
    //The exp gaining level minus button
    private GameObject expGainingLevelMinus;

    private void Start()
    {
        //Find all the gameobjects and save them
        g0 = GameObject.Find("g0");
        g1 = GameObject.Find("g1");
        g2 = GameObject.Find("g2");
        g3 = GameObject.Find("g3");
        player = GameObject.Find("Player");
        lvlUp = GameObject.Find("Upgrade player");
        exp = GameObject.Find("LightNumb").GetComponent<Text>();
        healthLevel = GameObject.Find("HealthLevel").GetComponent<Text>();
        manaLevel = GameObject.Find("ManaLevel").GetComponent<Text>();
        dealtDamageLevel = GameObject.Find("DamageDealtLevel").GetComponent<Text>();
        dashLevel = GameObject.Find("DashLevel").GetComponent<Text>();
        healingLevel = GameObject.Find("HealingLevel").GetComponent<Text>();
        damageResistanceLevel = GameObject.Find("DamageResistanceLevel").GetComponent<Text>();
        expGainingLevel = GameObject.Find("ExpGainLevel").GetComponent<Text>();
        healthLevelMinus = GameObject.Find("HealthMinus");
        manaLevelMinus = GameObject.Find("ManaMinus");
        dealtDamageLevelMinus = GameObject.Find("DamageDealtMinus");
        dashLevelMinus = GameObject.Find("DashMinus");
        healingLevelMinus = GameObject.Find("HealingMinus");
        damageResistanceLevelMinus = GameObject.Find("DamageResistanceMinus");
        expGainingLevelMinus = GameObject.Find("ExpGainMinus");
        lvl = GameObject.Find("PlayerLevelNumb").GetComponent<Text>();
        needExp = GameObject.Find("UpgradeNumb").GetComponent<Text>();
        lvlUpExp = GameObject.Find("LightLeftNumb").GetComponent<Text>();
        lvlUp.SetActive(false);
    }


    void Update()
    {
        //Check that all the numbers of the lvl up interface are up to date
        if (!lvlUp.activeSelf && player.GetComponent<PlayerMovement>().sleeping)
        {
            healthLevel.text = PlayerPrefs.GetInt("healthLevel").ToString();
            manaLevel.text = PlayerPrefs.GetInt("manaLevel").ToString();
            dealtDamageLevel.text = PlayerPrefs.GetInt("dealtDamageLevel").ToString();
            dashLevel.text = PlayerPrefs.GetInt("dashLevel").ToString();
            healingLevel.text = PlayerPrefs.GetInt("healingLevel").ToString();
            damageResistanceLevel.text = PlayerPrefs.GetInt("damageResistanceLevel").ToString();
            expGainingLevel.text = PlayerPrefs.GetInt("expGainingLevel").ToString();
            lvl.text = PlayerPrefs.GetInt("lvl").ToString();
            needExp.text = PlayerPrefs.GetInt("needExp").ToString();
            lvlUpExp.text = PlayerPrefs.GetInt("exp").ToString();
            lvlUp.SetActive(true);
            healthLevelMinus.SetActive(false);
            manaLevelMinus.SetActive(false);
            dealtDamageLevelMinus.SetActive(false);
            dashLevelMinus.SetActive(false);
            healingLevelMinus.SetActive(false);
            damageResistanceLevelMinus.SetActive(false);
            expGainingLevelMinus.SetActive(false);
        }
        //Check if we are changing gravity and if so make the gameobjects active
        g0.SetActive(player.GetComponent<PlayerMovement>().changingGravity && !player.GetComponent<PlayerMovement>().rotating);
        g1.SetActive(player.GetComponent<PlayerMovement>().changingGravity && !player.GetComponent<PlayerMovement>().rotating);
        g2.SetActive(player.GetComponent<PlayerMovement>().changingGravity && !player.GetComponent<PlayerMovement>().rotating);
        g3.SetActive(player.GetComponent<PlayerMovement>().changingGravity && !player.GetComponent<PlayerMovement>().rotating);
        //Write the gravity applied to each side
        g0.GetComponent<Text>().text = player.GetComponent<PlayerMovement>().prevGravityDown.ToString("F1");
        g1.GetComponent<Text>().text = player.GetComponent<PlayerMovement>().prevGravityUp.ToString("F1");
        g2.GetComponent<Text>().text = player.GetComponent<PlayerMovement>().prevGravityLeft.ToString("F1");
        g3.GetComponent<Text>().text = player.GetComponent<PlayerMovement>().prevGravityRight.ToString("F1");
        //Show the light number on the interface
        exp.text = PlayerPrefs.GetInt("exp").ToString();
    }
    public void lvlUpPlayer(int atrib)
    {
        //Lvl up only if the player has enough light
        if(int.Parse(lvlUpExp.text) >= int.Parse(needExp.text))
        {
            if (atrib == 1)
            {
                healthLevel.text = (int.Parse(healthLevel.text) + 1).ToString();
                healthLevelMinus.SetActive(true);
            }
            else if (atrib == 2)
            {
                manaLevel.text = (int.Parse(manaLevel.text) + 1).ToString();
                manaLevelMinus.SetActive(true);
            }
            else if (atrib == 3)
            {
                dealtDamageLevel.text = (int.Parse(dealtDamageLevel.text) + 1).ToString();
                dealtDamageLevelMinus.SetActive(true);
            }
            else if (atrib == 4)
            {
                dashLevel.text = (int.Parse(dashLevel.text) + 1).ToString();
                dashLevelMinus.SetActive(true);
            }
            else if (atrib == 5)
            {
                healingLevel.text = (int.Parse(healingLevel.text) + 1).ToString();
                healingLevelMinus.SetActive(true);
            }
            else if (atrib == 6)
            {
                damageResistanceLevel.text = (int.Parse(damageResistanceLevel.text) + 1).ToString();
                damageResistanceLevelMinus.SetActive(true);
            }
            else if (atrib == 7)
            {
                expGainingLevel.text = (int.Parse(expGainingLevel.text) + 1).ToString();
                expGainingLevelMinus.SetActive(true);
            }
            lvl.text = (int.Parse(lvl.text) + 1).ToString();
            lvlUpExp.text = (int.Parse(lvlUpExp.text) - int.Parse(needExp.text)).ToString();
            needExp.text = ((int.Parse(lvl.text) * 10) * (int.Parse(lvl.text) * 3)).ToString();
        }        
    }
    public void lvlDownPlayer(int atrib)
    {
        if (atrib == 1)
        {
            healthLevel.text = (int.Parse(healthLevel.text) - 1).ToString();
            if(int.Parse(healthLevel.text) == PlayerPrefs.GetInt("healthLevel")) healthLevelMinus.SetActive(false);
        }
        else if (atrib == 2)
        {
            manaLevel.text = (int.Parse(manaLevel.text) - 1).ToString();
            if(int.Parse(manaLevel.text) == PlayerPrefs.GetInt("manaLevel")) manaLevelMinus.SetActive(false);
        }
        else if (atrib == 3)
        {
            dealtDamageLevel.text = (int.Parse(dealtDamageLevel.text) - 1).ToString();
            if(int.Parse(dealtDamageLevel.text) == PlayerPrefs.GetInt("dealtDamageLevel")) dealtDamageLevelMinus.SetActive(false);
        }
        else if (atrib == 4)
        {
            dashLevel.text = (int.Parse(dashLevel.text) - 1).ToString();
            if(int.Parse(dashLevel.text) == PlayerPrefs.GetInt("dashLevel")) dashLevelMinus.SetActive(false);
        }
        else if (atrib == 5)
        {
            healingLevel.text = (int.Parse(healingLevel.text) - 1).ToString();
            if(int.Parse(healingLevel.text) == PlayerPrefs.GetInt("healingLevel")) healingLevelMinus.SetActive(false);
        }
        else if (atrib == 6)
        {
            damageResistanceLevel.text = (int.Parse(damageResistanceLevel.text) - 1).ToString();
            if(int.Parse(damageResistanceLevel.text) == PlayerPrefs.GetInt("damageResistanceLevel")) damageResistanceLevelMinus.SetActive(false);
        }
        else if (atrib == 7)
        {
            expGainingLevel.text = (int.Parse(expGainingLevel.text) - 1).ToString();
            if (int.Parse(expGainingLevel.text) == PlayerPrefs.GetInt("expGainingLevel")) expGainingLevelMinus.SetActive(false);
        }
        lvl.text = (int.Parse(lvl.text) - 1).ToString();
        needExp.text = ((int.Parse(lvl.text) * 10) * (int.Parse(lvl.text) * 3)).ToString();
        lvlUpExp.text = (int.Parse(lvlUpExp.text) + int.Parse(needExp.text)).ToString();        
    }
    public void CloseLvlUp()
    {
        //Save all the changes of the lvl up
        PlayerPrefs.SetInt("healthLevel", int.Parse(healthLevel.text));
        PlayerPrefs.SetInt("manaLevel", int.Parse(manaLevel.text));
        PlayerPrefs.SetInt("dealtDamageLevel", int.Parse(dealtDamageLevel.text));
        PlayerPrefs.SetInt("dashLevel", int.Parse(dashLevel.text));
        PlayerPrefs.SetInt("healingLevel", int.Parse(healingLevel.text));
        PlayerPrefs.SetInt("damageResistanceLevel", int.Parse(damageResistanceLevel.text));
        PlayerPrefs.SetInt("expGainingLevel", int.Parse(expGainingLevel.text));
        PlayerPrefs.SetInt("lvl", int.Parse(lvl.text));
        PlayerPrefs.SetInt("needExp", int.Parse(needExp.text));
        PlayerPrefs.SetInt("exp", int.Parse(lvlUpExp.text));
        player.GetComponent<PlayerMovement>().sleeping = false;
        player.GetComponent<Animator>().SetBool("isResting", false);
        lvlUp.SetActive(false);
    }
}
