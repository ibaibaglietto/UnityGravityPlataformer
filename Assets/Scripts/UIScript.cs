using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

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
    //The next maximum health
    private Text healthLevelNext;
    //The next maximum mana
    private Text manaLevelNext;
    //The next dealt damage
    private Text dealtDamageLevelNext;
    //The next dash cooldown
    private Text dashLevelNext;
    //The next healing per second
    private Text healingLevelNext;
    //The next damage resistance
    private Text damageResistanceLevelNext;
    //The next exp multiplier
    private Text expGainingLevelNext;
    //The healthbar
    private GameObject healthBar;
    //The manabar
    private GameObject manaBar;
    //The light of the player
    private Light2D playerLight;

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
        healthLevelNext = GameObject.Find("HealthDescrpitionNumb").GetComponent<Text>();
        manaLevelNext = GameObject.Find("ManaDescrpitionNumb").GetComponent<Text>();
        dealtDamageLevelNext = GameObject.Find("DamageDealtDescrpitionNumb").GetComponent<Text>();
        dashLevelNext = GameObject.Find("DashDescrpitionNumb").GetComponent<Text>();
        healingLevelNext = GameObject.Find("HealingDescrpitionNumb").GetComponent<Text>();
        damageResistanceLevelNext = GameObject.Find("DamageResistanceDescrpitionNumb").GetComponent<Text>();
        expGainingLevelNext = GameObject.Find("ExpGainDescrpitionNumb").GetComponent<Text>();
        healthBar = GameObject.Find("Healthbar");
        manaBar = GameObject.Find("Manabar");
        playerLight = player.transform.GetChild(1).gameObject.GetComponent<Light2D>();
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
            healthLevelNext.text = (Mathf.Sqrt(2000 * int.Parse(healthLevel.text)) + 55).ToString("F0");
            manaLevelNext.text = (Mathf.Sqrt(2000 * int.Parse(manaLevel.text)) + 5).ToString("F0");
            dealtDamageLevelNext.text = (Mathf.Sqrt(100 * int.Parse(dealtDamageLevel.text)) + 10).ToString("F0");
            dashLevelNext.text = (1 / Mathf.Sqrt(int.Parse(dashLevel.text))).ToString("F2");
            healingLevelNext.text = (Mathf.Sqrt(2 * int.Parse(healingLevel.text)) + 1.1f).ToString("F1");
            damageResistanceLevelNext.text = (Mathf.Sqrt(150 * int.Parse(damageResistanceLevel.text)) - 12.247f).ToString("F0");
            expGainingLevelNext.text = (1.0f + (int.Parse(expGainingLevel.text)-1.0f)*0.1f).ToString("F1");
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
        if (PlayerPrefs.GetInt("needExp") > PlayerPrefs.GetInt("exp")) playerLight.pointLightOuterRadius = (float)PlayerPrefs.GetInt("exp") / (float)PlayerPrefs.GetInt("needExp") * 3.0f;
        else playerLight.pointLightOuterRadius = 3.0f;
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
                healthLevelNext.text = (Mathf.Sqrt(2000 * int.Parse(healthLevel.text)) + 55).ToString("F0");
            }
            else if (atrib == 2)
            {
                manaLevel.text = (int.Parse(manaLevel.text) + 1).ToString();
                manaLevelMinus.SetActive(true);
                manaLevelNext.text = (Mathf.Sqrt(2000 * int.Parse(manaLevel.text)) + 5).ToString("F0");
            }
            else if (atrib == 3)
            {
                dealtDamageLevel.text = (int.Parse(dealtDamageLevel.text) + 1).ToString();
                dealtDamageLevelMinus.SetActive(true);
                dealtDamageLevelNext.text = (Mathf.Sqrt(100 * int.Parse(dealtDamageLevel.text)) + 10).ToString("F0");
            }
            else if (atrib == 4)
            {
                dashLevel.text = (int.Parse(dashLevel.text) + 1).ToString();
                dashLevelMinus.SetActive(true);
                dashLevelNext.text = (1 / Mathf.Sqrt(int.Parse(dashLevel.text))).ToString("F2");
            }
            else if (atrib == 5)
            {
                healingLevel.text = (int.Parse(healingLevel.text) + 1).ToString();
                healingLevelMinus.SetActive(true);
                healingLevelNext.text = (Mathf.Sqrt(2 * int.Parse(healingLevel.text)) + 1.1f).ToString("F1");
            }
            else if (atrib == 6)
            {
                damageResistanceLevel.text = (int.Parse(damageResistanceLevel.text) + 1).ToString();
                damageResistanceLevelMinus.SetActive(true);
                damageResistanceLevelNext.text = (Mathf.Sqrt(150 * int.Parse(damageResistanceLevel.text)) - 12.247f).ToString("F0");
            }
            else if (atrib == 7)
            {
                expGainingLevel.text = (int.Parse(expGainingLevel.text) + 1).ToString();
                expGainingLevelMinus.SetActive(true);
                expGainingLevelNext.text = (1.0f + (int.Parse(expGainingLevel.text) - 1.0f) * 0.1f).ToString("F1");
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
            healthLevelNext.text = (Mathf.Sqrt(2000 * int.Parse(healthLevel.text)) + 55).ToString("F0");
            if (int.Parse(healthLevel.text) == PlayerPrefs.GetInt("healthLevel")) healthLevelMinus.SetActive(false);
        }
        else if (atrib == 2)
        {
            manaLevel.text = (int.Parse(manaLevel.text) - 1).ToString();
            manaLevelNext.text = (Mathf.Sqrt(2000 * int.Parse(manaLevel.text)) + 5).ToString("F0");
            if (int.Parse(manaLevel.text) == PlayerPrefs.GetInt("manaLevel")) manaLevelMinus.SetActive(false);
        }
        else if (atrib == 3)
        {
            dealtDamageLevel.text = (int.Parse(dealtDamageLevel.text) - 1).ToString();
            dealtDamageLevelNext.text = (Mathf.Sqrt(100 * int.Parse(dealtDamageLevel.text)) + 10).ToString("F0");
            if (int.Parse(dealtDamageLevel.text) == PlayerPrefs.GetInt("dealtDamageLevel")) dealtDamageLevelMinus.SetActive(false);
        }
        else if (atrib == 4)
        {
            dashLevel.text = (int.Parse(dashLevel.text) - 1).ToString();
            dashLevelNext.text = (1 / Mathf.Sqrt(int.Parse(dashLevel.text))).ToString("F2");
            if (int.Parse(dashLevel.text) == PlayerPrefs.GetInt("dashLevel")) dashLevelMinus.SetActive(false);
        }
        else if (atrib == 5)
        {
            healingLevel.text = (int.Parse(healingLevel.text) - 1).ToString();
            healingLevelNext.text = (Mathf.Sqrt(2 * int.Parse(healingLevel.text)) + 1.1f).ToString("F1");
            if (int.Parse(healingLevel.text) == PlayerPrefs.GetInt("healingLevel")) healingLevelMinus.SetActive(false);
        }
        else if (atrib == 6)
        {
            damageResistanceLevel.text = (int.Parse(damageResistanceLevel.text) - 1).ToString();
            damageResistanceLevelNext.text = (Mathf.Sqrt(150 * int.Parse(damageResistanceLevel.text))-12.247f).ToString("F0");
            if (int.Parse(damageResistanceLevel.text) == PlayerPrefs.GetInt("damageResistanceLevel")) damageResistanceLevelMinus.SetActive(false);
        }
        else if (atrib == 7)
        {
            expGainingLevel.text = (int.Parse(expGainingLevel.text) - 1).ToString();
            expGainingLevelNext.text = (1.0f + (int.Parse(expGainingLevel.text) - 1.0f) * 0.1f).ToString("F1");
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
        healthBar.transform.GetComponent<RectTransform>().anchorMax = new Vector2((Mathf.Sqrt(2000 * int.Parse(healthLevel.text)) + 55)*0.002f+0.034f, 0.9644875f);
        healthBar.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        healthBar.GetComponent<PlayerLifeController>().maxHealth = Mathf.Sqrt(2000 * PlayerPrefs.GetInt("healthLevel")) + 55;
        healthBar.GetComponent<PlayerLifeController>().health = healthBar.GetComponent<PlayerLifeController>().maxHealth;
        manaBar.transform.GetComponent<RectTransform>().anchorMax = new Vector2((Mathf.Sqrt(2000 * int.Parse(manaLevel.text)) + 5) * 0.002f + 0.034f, 0.9196156f);
        manaBar.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        manaBar.GetComponent<ManaController>().maxMana = Mathf.Sqrt(2000 * PlayerPrefs.GetInt("manaLevel")) + 5;
        manaBar.GetComponent<ManaController>().mana = manaBar.GetComponent<ManaController>().maxMana;
        player.GetComponent<Animator>().SetBool("isResting", false);
        lvlUp.SetActive(false);
    }
}
