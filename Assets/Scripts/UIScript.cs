using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    //The cursors
    public Texture2D cursorArrow16;
    public Texture2D cursorArrow20;
    public Texture2D cursorArrow26;
    public Texture2D cursorArrow29;
    public Texture2D cursorArrow32;
    public Texture2D cursorArrow34;
    public Texture2D cursorArrow35;
    public Texture2D cursorArrow36;
    public Texture2D cursorArrow40;
    public Texture2D cursorArrow46;
    public Texture2D cursorArrow48;
    public Texture2D cursorArrow51;
    public Texture2D cursorArrow64;
    public Texture2D cursorArrow96;
    public Texture2D cursorSword16;
    public Texture2D cursorSword20;
    public Texture2D cursorSword26;
    public Texture2D cursorSword29;
    public Texture2D cursorSword32;
    public Texture2D cursorSword34;
    public Texture2D cursorSword35;
    public Texture2D cursorSword36;
    public Texture2D cursorSword40;
    public Texture2D cursorSword46;
    public Texture2D cursorSword48;
    public Texture2D cursorSword51;
    public Texture2D cursorSword64;
    public Texture2D cursorSword96;
    //The cursors
    private Texture2D cursorArrow;
    private Texture2D cursorSword;
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
    //The stamina level
    private Text staminaLevel;
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
    //The stamina level minus button
    private GameObject staminaLevelMinus;
    //The healing level minus button
    private GameObject healingLevelMinus;
    //The damage resistance level minus button
    private GameObject damageResistanceLevelMinus;
    //The exp gaining level minus button
    private GameObject expGainingLevelMinus;
    //The settings menu
    private GameObject settingsMenu;
    //The final text
    private GameObject finalText;
    //The next maximum health
    private Text healthLevelNext;
    //The next maximum mana
    private Text manaLevelNext;
    //The next dealt damage
    private Text dealtDamageLevelNext;
    //The next max stamina
    private Text staminaLevelNext;
    //The next healing per second
    private Text healingLevelNext;
    //The next damage resistance
    private Text damageResistanceLevelNext;
    //The next exp multiplier
    private Text expGainingLevelNext;
    //The healthbar
    private GameObject healthBar;
    //The healthbar color
    private GameObject healthBarColor;
    //The manabar
    private GameObject manaBar;
    //The manabar color
    private GameObject manaBarColor;
    //The staminabar
    private GameObject staminaBar;
    //The staminabar color
    private GameObject staminaBarColor;
    //The light of the player
    private Light2D playerLight;
    //The pause menu
    private GameObject pauseMenu;
    //The Die screen
    private GameObject dieScreen;
    //The previous screen configuration
    private int prevW;
    private int prevH;
    private bool prevFS;
    private int prevFR;
    //the screen mode
    private bool fullScreen;
    //The resolution selector dropdown
    private GameObject resolution;
    //The framerate selector dropdown
    private GameObject framerate;
    //The toggle of the full screen mode
    private GameObject fullScreenToggle;
    //The confirmation window
    private GameObject confirmationMenu;
    //The time the resolution changes were made
    private float resolutionTime;
    //The return configuration text
    private Text returnText;

    private void Start()
    {
        player = GameObject.Find("Player");
        lvlUp = GameObject.Find("Upgrade player");
        exp = GameObject.Find("LightNumb").GetComponent<Text>();
        healthLevel = GameObject.Find("HealthLevel").GetComponent<Text>();
        manaLevel = GameObject.Find("ManaLevel").GetComponent<Text>();
        dealtDamageLevel = GameObject.Find("DamageDealtLevel").GetComponent<Text>();
        staminaLevel = GameObject.Find("StaminaLevel").GetComponent<Text>();
        healingLevel = GameObject.Find("HealingLevel").GetComponent<Text>();
        damageResistanceLevel = GameObject.Find("DamageResistanceLevel").GetComponent<Text>();
        expGainingLevel = GameObject.Find("ExpGainLevel").GetComponent<Text>();
        healthLevelMinus = GameObject.Find("HealthMinus");
        manaLevelMinus = GameObject.Find("ManaMinus");
        dealtDamageLevelMinus = GameObject.Find("DamageDealtMinus");
        staminaLevelMinus = GameObject.Find("StaminaMinus");
        healingLevelMinus = GameObject.Find("HealingMinus");
        damageResistanceLevelMinus = GameObject.Find("DamageResistanceMinus");
        expGainingLevelMinus = GameObject.Find("ExpGainMinus");
        lvl = GameObject.Find("PlayerLevelNumb").GetComponent<Text>();
        needExp = GameObject.Find("UpgradeNumb").GetComponent<Text>();
        lvlUpExp = GameObject.Find("LightLeftNumb").GetComponent<Text>();
        healthLevelNext = GameObject.Find("HealthDescrpitionNumb").GetComponent<Text>();
        manaLevelNext = GameObject.Find("ManaDescrpitionNumb").GetComponent<Text>();
        dealtDamageLevelNext = GameObject.Find("DamageDealtDescrpitionNumb").GetComponent<Text>();
        staminaLevelNext = GameObject.Find("StaminaDescrpitionNumb").GetComponent<Text>();
        healingLevelNext = GameObject.Find("HealingDescrpitionNumb").GetComponent<Text>();
        damageResistanceLevelNext = GameObject.Find("DamageResistanceDescrpitionNumb").GetComponent<Text>();
        expGainingLevelNext = GameObject.Find("ExpGainDescrpitionNumb").GetComponent<Text>();
        healthBar = GameObject.Find("Healthbar");
        healthBarColor = GameObject.Find("Playerhealth");
        manaBar = GameObject.Find("Manabar");
        manaBarColor = GameObject.Find("Playermana");
        staminaBar = GameObject.Find("Staminabar");
        staminaBarColor = GameObject.Find("Playerstamina");
        pauseMenu = GameObject.Find("Pause");
        dieScreen = GameObject.Find("DieScreen");
        settingsMenu = GameObject.Find("SettingsMenu");
        resolution = GameObject.Find("DropdownResolution");
        framerate = GameObject.Find("DropdownFrameRate");
        fullScreenToggle = GameObject.Find("Windowed");
        confirmationMenu = GameObject.Find("ConfirmMenu");
        returnText = GameObject.Find("ReturnText").GetComponent<Text>();
        settingsMenu.SetActive(false);
        confirmationMenu.SetActive(false);
        playerLight = player.transform.GetChild(1).gameObject.GetComponent<Light2D>();
        lvlUp.SetActive(false);
        healthBar.transform.GetComponent<RectTransform>().anchorMax = new Vector2((Mathf.Sqrt(2000 * PlayerPrefs.GetInt("healthLevel")) + 55) * 0.002f + 0.034f, 0.9644875f);
        healthBar.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        healthBarColor.transform.GetComponent<RectTransform>().anchorMax = new Vector2((Mathf.Sqrt(2000 * PlayerPrefs.GetInt("healthLevel")) + 55) * 0.002f + 0.0315f, 0.9575f);
        healthBarColor.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        if (healthBar.GetComponent<PlayerLifeController>().maxHealth != Mathf.Sqrt(2000 * PlayerPrefs.GetInt("healthLevel")) + 55)
        {
            healthBar.GetComponent<PlayerLifeController>().health += (Mathf.Sqrt(2000 * PlayerPrefs.GetInt("healthLevel")) + 55) - healthBar.GetComponent<PlayerLifeController>().maxHealth;
            healthBar.GetComponent<PlayerLifeController>().maxHealth = Mathf.Sqrt(2000 * PlayerPrefs.GetInt("healthLevel")) + 55;
        }
        manaBar.transform.GetComponent<RectTransform>().anchorMax = new Vector2((Mathf.Sqrt(2000 * PlayerPrefs.GetInt("manaLevel")) + 5) * 0.002f + 0.034f, 0.9196156f);
        manaBar.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        manaBarColor.transform.GetComponent<RectTransform>().anchorMax = new Vector2((Mathf.Sqrt(2000 * PlayerPrefs.GetInt("manaLevel")) + 5) * 0.002f + 0.0315f, 0.9115f);
        manaBarColor.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        if (manaBar.GetComponent<ManaController>().maxMana != Mathf.Sqrt(2000 * PlayerPrefs.GetInt("manaLevel")) + 5)
        {
            manaBar.GetComponent<ManaController>().mana += (Mathf.Sqrt(2000 * PlayerPrefs.GetInt("manaLevel")) + 5) - manaBar.GetComponent<ManaController>().maxMana;
            manaBar.GetComponent<ManaController>().maxMana = Mathf.Sqrt(2000 * PlayerPrefs.GetInt("manaLevel")) + 5;
        }
        staminaBar.transform.GetComponent<RectTransform>().anchorMax = new Vector2((Mathf.Sqrt(2000 * PlayerPrefs.GetInt("staminaLevel")) / 2 + 2.639f) * 0.002f + 0.034f, 0.8901318f);
        staminaBar.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        staminaBarColor.transform.GetComponent<RectTransform>().anchorMax = new Vector2((Mathf.Sqrt(2000 * PlayerPrefs.GetInt("staminaLevel")) / 2 + 2.639f) * 0.002f + 0.0315f, 0.8825f);
        staminaBarColor.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        if (staminaBar.GetComponent<StaminaController>().maxStamina != Mathf.Sqrt(2000 * PlayerPrefs.GetInt("staminaLevel")) / 2 + 2.639f)
        {
            staminaBar.GetComponent<StaminaController>().stamina += (Mathf.Sqrt(2000 * PlayerPrefs.GetInt("staminaLevel")) / 2 + 2.639f) - staminaBar.GetComponent<StaminaController>().maxStamina;
            staminaBar.GetComponent<StaminaController>().maxStamina = Mathf.Sqrt(2000 * PlayerPrefs.GetInt("staminaLevel")) / 2 + 2.639f;
        }
        finalText = GameObject.Find("Final text");
        if (PlayerPrefs.GetInt("fullScreen") == 0) fullScreen = false;
        else fullScreen = true;
        Screen.SetResolution(PlayerPrefs.GetInt("resolutionW"), PlayerPrefs.GetInt("resolutionH"), fullScreen, PlayerPrefs.GetInt("framerate"));
        if (PlayerPrefs.GetInt("resolutionW") == 640)
        {
            cursorArrow = cursorArrow16;
            cursorSword = cursorSword16;
            resolution.GetComponent<Dropdown>().value = 0;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 800)
        {
            cursorArrow = cursorArrow20;
            cursorSword = cursorSword20;
            resolution.GetComponent<Dropdown>().value = 1;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1024)
        {
            cursorArrow = cursorArrow26;
            cursorSword = cursorSword26;
            if (PlayerPrefs.GetInt("resolutionH") == 576) resolution.GetComponent<Dropdown>().value = 2;
            else resolution.GetComponent<Dropdown>().value = 3;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1152)
        {
            cursorArrow = cursorArrow29;
            cursorSword = cursorSword29;
            resolution.GetComponent<Dropdown>().value = 4;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1280)
        {
            cursorArrow = cursorArrow32;
            cursorSword = cursorSword32;
            if (PlayerPrefs.GetInt("resolutionH") == 720) resolution.GetComponent<Dropdown>().value = 5;
            else if (PlayerPrefs.GetInt("resolutionH") == 800) resolution.GetComponent<Dropdown>().value = 6;
            else resolution.GetComponent<Dropdown>().value = 7;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1366)
        {
            cursorArrow = cursorArrow34;
            cursorSword = cursorSword34;
            resolution.GetComponent<Dropdown>().value = 8;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1400)
        {
            cursorArrow = cursorArrow35;
            cursorSword = cursorSword35;
            resolution.GetComponent<Dropdown>().value = 9;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1440)
        {
            cursorArrow = cursorArrow36;
            cursorSword = cursorSword36;
            if (PlayerPrefs.GetInt("resolutionH") == 900) resolution.GetComponent<Dropdown>().value = 10;
            else resolution.GetComponent<Dropdown>().value = 11;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1600)
        {
            cursorArrow = cursorArrow40;
            cursorSword = cursorSword40;
            if (PlayerPrefs.GetInt("resolutionH") == 900) resolution.GetComponent<Dropdown>().value = 12;
            else resolution.GetComponent<Dropdown>().value = 13;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1856)
        {
            cursorArrow = cursorArrow46;
            cursorSword = cursorSword46;
            resolution.GetComponent<Dropdown>().value = 14;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1920)
        {
            cursorArrow = cursorArrow48;
            cursorSword = cursorSword48;
            if (PlayerPrefs.GetInt("resolutionH") == 1080) resolution.GetComponent<Dropdown>().value = 15;
            else if (PlayerPrefs.GetInt("resolutionH") == 1200) resolution.GetComponent<Dropdown>().value = 16;
            else resolution.GetComponent<Dropdown>().value = 17;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 2048)
        {
            cursorArrow = cursorArrow51;
            cursorSword = cursorSword51;
            resolution.GetComponent<Dropdown>().value = 18;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 2560)
        {
            cursorArrow = cursorArrow64;
            cursorSword = cursorSword64;
            if (PlayerPrefs.GetInt("resolutionH") == 1440) resolution.GetComponent<Dropdown>().value = 19;
            else resolution.GetComponent<Dropdown>().value = 20;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 3840)
        {
            cursorArrow = cursorArrow96;
            cursorSword = cursorSword96;
            resolution.GetComponent<Dropdown>().value = 21;
        }
        if (PlayerPrefs.GetInt("framerate") == 30) framerate.GetComponent<Dropdown>().value = 0;
        else if (PlayerPrefs.GetInt("framerate") == 60) framerate.GetComponent<Dropdown>().value = 1;
        else if (PlayerPrefs.GetInt("framerate") == 90) framerate.GetComponent<Dropdown>().value = 2;
        else if (PlayerPrefs.GetInt("framerate") == 120) framerate.GetComponent<Dropdown>().value = 3;
        else if (PlayerPrefs.GetInt("framerate") == 144) framerate.GetComponent<Dropdown>().value = 4;
        else if (PlayerPrefs.GetInt("framerate") == 0) framerate.GetComponent<Dropdown>().value = 5;
        fullScreenToggle.GetComponent<Toggle>().isOn = fullScreen;
        Cursor.SetCursor(cursorSword, Vector2.zero, CursorMode.ForceSoftware);
    }


    void Update()
    {
        if (player.GetComponent<PlayerMovement>().ended)
        {
            finalText.SetActive(true);
            PlayerPrefs.SetInt("lastDialogue", 15);
        }
        else finalText.SetActive(false);
        if (player.GetComponent<PlayerMovement>().paused)
        {
            pauseMenu.SetActive(true);
            Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
        }
        else if(pauseMenu.activeSelf == true)
        {
            pauseMenu.SetActive(false);
            Cursor.SetCursor(cursorSword, Vector2.zero, CursorMode.ForceSoftware);
        }
        if(dieScreen.activeSelf == true) Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
        //Check that all the numbers of the lvl up interface are up to date
        if (!lvlUp.activeSelf && player.GetComponent<PlayerMovement>().sleeping)
        {
            healthLevel.text = PlayerPrefs.GetInt("healthLevel").ToString();
            manaLevel.text = PlayerPrefs.GetInt("manaLevel").ToString();
            dealtDamageLevel.text = PlayerPrefs.GetInt("dealtDamageLevel").ToString();
            staminaLevel.text = PlayerPrefs.GetInt("staminaLevel").ToString();
            healingLevel.text = PlayerPrefs.GetInt("healingLevel").ToString();
            damageResistanceLevel.text = PlayerPrefs.GetInt("damageResistanceLevel").ToString();
            expGainingLevel.text = PlayerPrefs.GetInt("expGainingLevel").ToString();
            lvl.text = PlayerPrefs.GetInt("lvl").ToString();
            needExp.text = PlayerPrefs.GetInt("needExp").ToString();
            lvlUpExp.text = PlayerPrefs.GetInt("exp").ToString();
            healthLevelNext.text = (Mathf.Sqrt(2000 * int.Parse(healthLevel.text)) + 55).ToString("F0");
            manaLevelNext.text = (Mathf.Sqrt(2000 * int.Parse(manaLevel.text)) + 5).ToString("F0");
            dealtDamageLevelNext.text = (Mathf.Sqrt(100 * int.Parse(dealtDamageLevel.text)) + 10).ToString("F0");
            staminaLevelNext.text = (Mathf.Sqrt(2000 * int.Parse(staminaLevel.text)) / 2 + 2.639f).ToString("F0");
            healingLevelNext.text = (Mathf.Sqrt(2 * int.Parse(healingLevel.text)) + 1.1f).ToString("F1");
            damageResistanceLevelNext.text = (Mathf.Sqrt(150 * int.Parse(damageResistanceLevel.text)) - 12.247f).ToString("F0");
            expGainingLevelNext.text = (1.0f + (int.Parse(expGainingLevel.text)-1.0f)*0.1f).ToString("F1");
            lvlUp.SetActive(true);
            healthLevelMinus.SetActive(false);
            manaLevelMinus.SetActive(false);
            dealtDamageLevelMinus.SetActive(false);
            staminaLevelMinus.SetActive(false);
            healingLevelMinus.SetActive(false);
            damageResistanceLevelMinus.SetActive(false);
            expGainingLevelMinus.SetActive(false);
            Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
        }
        //Show the light number on the interface
        exp.text = PlayerPrefs.GetInt("exp").ToString();
        if (PlayerPrefs.GetInt("needExp") > PlayerPrefs.GetInt("exp")) playerLight.pointLightOuterRadius = (float)PlayerPrefs.GetInt("exp") / (float)PlayerPrefs.GetInt("needExp") * 3.0f;
        else playerLight.pointLightOuterRadius = 3.0f;
    }

    void FixedUpdate()
    {
        if (resolutionTime != 0.0f && (Time.fixedTime - resolutionTime) >= 10)
        {
            resolutionTime = 0.0f;
            ReturnResolution();
        }
        else if (resolutionTime != 0.0f) returnText.text = "Return (" + (10 - (int)(Time.fixedTime - resolutionTime)).ToString() + ")";
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
                staminaLevel.text = (int.Parse(staminaLevel.text) + 1).ToString();
                staminaLevelMinus.SetActive(true);
                staminaLevelNext.text = (Mathf.Sqrt(2000 * int.Parse(staminaLevel.text))/2 + 2.639f).ToString("F0");
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
            staminaLevel.text = (int.Parse(staminaLevel.text) - 1).ToString();
            staminaLevelNext.text = (Mathf.Sqrt(2000 * int.Parse(staminaLevel.text)) / 2 + 2.639f).ToString("F0");
            if (int.Parse(staminaLevel.text) == PlayerPrefs.GetInt("staminaLevel")) staminaLevelMinus.SetActive(false);
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
        PlayerPrefs.SetInt("staminaLevel", int.Parse(staminaLevel.text));
        PlayerPrefs.SetInt("healingLevel", int.Parse(healingLevel.text));
        PlayerPrefs.SetInt("damageResistanceLevel", int.Parse(damageResistanceLevel.text));
        PlayerPrefs.SetInt("expGainingLevel", int.Parse(expGainingLevel.text));
        PlayerPrefs.SetInt("lvl", int.Parse(lvl.text));
        PlayerPrefs.SetInt("needExp", int.Parse(needExp.text));
        PlayerPrefs.SetInt("exp", int.Parse(lvlUpExp.text));
        PlayerPrefs.SetInt("restExp", PlayerPrefs.GetInt("exp"));
        player.GetComponent<PlayerMovement>().sleeping = false;
        healthBar.transform.GetComponent<RectTransform>().anchorMax = new Vector2((Mathf.Sqrt(2000 * int.Parse(healthLevel.text)) + 55)*0.002f+0.034f, 0.9644875f);
        healthBar.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        healthBarColor.transform.GetComponent<RectTransform>().anchorMax = new Vector2((Mathf.Sqrt(2000 * int.Parse(healthLevel.text)) + 55) * 0.002f + 0.0315f, 0.9575f);
        healthBarColor.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        if (healthBar.GetComponent<PlayerLifeController>().maxHealth != Mathf.Sqrt(2000 * PlayerPrefs.GetInt("healthLevel")) + 55)
        {
            healthBar.GetComponent<PlayerLifeController>().health += (Mathf.Sqrt(2000 * PlayerPrefs.GetInt("healthLevel")) + 55) - healthBar.GetComponent<PlayerLifeController>().maxHealth;
            healthBar.GetComponent<PlayerLifeController>().maxHealth = Mathf.Sqrt(2000 * PlayerPrefs.GetInt("healthLevel")) + 55;
        }        
        manaBar.transform.GetComponent<RectTransform>().anchorMax = new Vector2((Mathf.Sqrt(2000 * int.Parse(manaLevel.text)) + 5) * 0.002f + 0.034f, 0.9196156f);
        manaBar.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        manaBarColor.transform.GetComponent<RectTransform>().anchorMax = new Vector2((Mathf.Sqrt(2000 * int.Parse(manaLevel.text)) + 5) * 0.002f + 0.0315f, 0.9115f);
        manaBarColor.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        if (manaBar.GetComponent<ManaController>().maxMana != Mathf.Sqrt(2000 * PlayerPrefs.GetInt("manaLevel")) + 5)
        {
            manaBar.GetComponent<ManaController>().mana += (Mathf.Sqrt(2000 * PlayerPrefs.GetInt("manaLevel")) + 5) - manaBar.GetComponent<ManaController>().maxMana;
            manaBar.GetComponent<ManaController>().maxMana = Mathf.Sqrt(2000 * PlayerPrefs.GetInt("manaLevel")) + 5;
        }
        staminaBar.transform.GetComponent<RectTransform>().anchorMax = new Vector2((Mathf.Sqrt(2000 * int.Parse(staminaLevel.text)) / 2 + 2.639f) * 0.002f + 0.034f, 0.8901318f);
        staminaBar.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        staminaBarColor.transform.GetComponent<RectTransform>().anchorMax = new Vector2((Mathf.Sqrt(2000 * int.Parse(staminaLevel.text)) / 2 + 2.639f) * 0.002f + 0.0315f, 0.8825f);
        staminaBarColor.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        if (staminaBar.GetComponent<StaminaController>().maxStamina != Mathf.Sqrt(2000 * int.Parse(staminaLevel.text)) / 2 + 2.639f)
        {
            staminaBar.GetComponent<StaminaController>().stamina += (Mathf.Sqrt(2000 * int.Parse(staminaLevel.text)) / 2 + 2.639f) - staminaBar.GetComponent<StaminaController>().maxStamina;
            staminaBar.GetComponent<StaminaController>().maxStamina = Mathf.Sqrt(2000 * int.Parse(staminaLevel.text)) / 2 + 2.639f;
        }
        player.GetComponent<Animator>().SetBool("isResting", false);
        lvlUp.SetActive(false);
        Cursor.SetCursor(cursorSword, Vector2.zero, CursorMode.ForceSoftware);
    }
    public void loadMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }

    public void continueLevel()
    {
        player.GetComponent<PlayerMovement>().paused = false;
        if(player.GetComponent<PlayerMovement>().changingGravity) Time.timeScale = 0.05f;
        else Time.timeScale = 1.0f;
    }

    public void CloseGame()
    {
        Debug.Log("Closing game");
        Application.Quit();        
    }

    public void Respawn()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("respawnscene"));
        player.GetComponent<PlayerMovement>().dead = false;
    }

    public void OpenSettings()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void CloseNoSave()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void CloseSave()
    {
        prevW = PlayerPrefs.GetInt("resolutionW");
        prevH = PlayerPrefs.GetInt("resolutionH");
        prevFS = fullScreen;
        prevFR = PlayerPrefs.GetInt("framerate");
        if (resolution.GetComponent<Dropdown>().value == 0)
        {
            PlayerPrefs.SetInt("resolutionW", 640);
            PlayerPrefs.SetInt("resolutionH", 480);
        }
        else if (resolution.GetComponent<Dropdown>().value == 1)
        {
            PlayerPrefs.SetInt("resolutionW", 800);
            PlayerPrefs.SetInt("resolutionH", 600);
        }
        else if (resolution.GetComponent<Dropdown>().value == 2)
        {
            PlayerPrefs.SetInt("resolutionW", 1024);
            PlayerPrefs.SetInt("resolutionH", 576);
        }
        else if (resolution.GetComponent<Dropdown>().value == 3)
        {
            PlayerPrefs.SetInt("resolutionW", 1024);
            PlayerPrefs.SetInt("resolutionH", 768);
        }
        else if (resolution.GetComponent<Dropdown>().value == 4)
        {
            PlayerPrefs.SetInt("resolutionW", 1152);
            PlayerPrefs.SetInt("resolutionH", 648);
        }
        else if (resolution.GetComponent<Dropdown>().value == 5)
        {
            PlayerPrefs.SetInt("resolutionW", 1280);
            PlayerPrefs.SetInt("resolutionH", 720);
        }
        else if (resolution.GetComponent<Dropdown>().value == 6)
        {
            PlayerPrefs.SetInt("resolutionW", 1280);
            PlayerPrefs.SetInt("resolutionH", 800);
        }
        else if (resolution.GetComponent<Dropdown>().value == 7)
        {
            PlayerPrefs.SetInt("resolutionW", 1280);
            PlayerPrefs.SetInt("resolutionH", 960);
        }
        else if (resolution.GetComponent<Dropdown>().value == 8)
        {
            PlayerPrefs.SetInt("resolutionW", 1366);
            PlayerPrefs.SetInt("resolutionH", 768);
        }
        else if (resolution.GetComponent<Dropdown>().value == 9)
        {
            PlayerPrefs.SetInt("resolutionW", 1400);
            PlayerPrefs.SetInt("resolutionH", 1050);
        }
        else if (resolution.GetComponent<Dropdown>().value == 10)
        {
            PlayerPrefs.SetInt("resolutionW", 1440);
            PlayerPrefs.SetInt("resolutionH", 900);
        }
        else if (resolution.GetComponent<Dropdown>().value == 11)
        {
            PlayerPrefs.SetInt("resolutionW", 1440);
            PlayerPrefs.SetInt("resolutionH", 1080);
        }
        else if (resolution.GetComponent<Dropdown>().value == 12)
        {
            PlayerPrefs.SetInt("resolutionW", 1600);
            PlayerPrefs.SetInt("resolutionH", 900);
        }
        else if (resolution.GetComponent<Dropdown>().value == 13)
        {
            PlayerPrefs.SetInt("resolutionW", 1600);
            PlayerPrefs.SetInt("resolutionH", 1200);
        }
        else if (resolution.GetComponent<Dropdown>().value == 14)
        {
            PlayerPrefs.SetInt("resolutionW", 1856);
            PlayerPrefs.SetInt("resolutionH", 1392);
        }
        else if (resolution.GetComponent<Dropdown>().value == 15)
        {
            PlayerPrefs.SetInt("resolutionW", 1920);
            PlayerPrefs.SetInt("resolutionH", 1080);
        }
        else if (resolution.GetComponent<Dropdown>().value == 16)
        {
            PlayerPrefs.SetInt("resolutionW", 1920);
            PlayerPrefs.SetInt("resolutionH", 1200);
        }
        else if (resolution.GetComponent<Dropdown>().value == 17)
        {
            PlayerPrefs.SetInt("resolutionW", 1920);
            PlayerPrefs.SetInt("resolutionH", 1440);
        }
        else if (resolution.GetComponent<Dropdown>().value == 18)
        {
            PlayerPrefs.SetInt("resolutionW", 2048);
            PlayerPrefs.SetInt("resolutionH", 1536);
        }
        else if (resolution.GetComponent<Dropdown>().value == 19)
        {
            PlayerPrefs.SetInt("resolutionW", 2560);
            PlayerPrefs.SetInt("resolutionH", 1440);
        }
        else if (resolution.GetComponent<Dropdown>().value == 20)
        {
            PlayerPrefs.SetInt("resolutionW", 2560);
            PlayerPrefs.SetInt("resolutionH", 1600);
        }
        else if (resolution.GetComponent<Dropdown>().value == 21)
        {
            PlayerPrefs.SetInt("resolutionW", 3840);
            PlayerPrefs.SetInt("resolutionH", 2160);
        }
        if (framerate.GetComponent<Dropdown>().value == 0) PlayerPrefs.SetInt("framerate", 30);
        else if (framerate.GetComponent<Dropdown>().value == 1) PlayerPrefs.SetInt("framerate", 60);
        else if (framerate.GetComponent<Dropdown>().value == 2) PlayerPrefs.SetInt("framerate", 90);
        else if (framerate.GetComponent<Dropdown>().value == 3) PlayerPrefs.SetInt("framerate", 120);
        else if (framerate.GetComponent<Dropdown>().value == 4) PlayerPrefs.SetInt("framerate", 144);
        else if (framerate.GetComponent<Dropdown>().value == 5) PlayerPrefs.SetInt("framerate", 0);
        if (fullScreenToggle.GetComponent<Toggle>().isOn) PlayerPrefs.SetInt("fullScreen", 1);
        else PlayerPrefs.SetInt("fullScreen", 0);
        fullScreen = fullScreenToggle.GetComponent<Toggle>().isOn;

        Screen.SetResolution(PlayerPrefs.GetInt("resolutionW"), PlayerPrefs.GetInt("resolutionH"), fullScreenToggle.GetComponent<Toggle>().isOn, PlayerPrefs.GetInt("framerate"));

        if (PlayerPrefs.GetInt("resolutionW") == 640)
        {
            cursorArrow = cursorArrow16;
            cursorSword = cursorSword16;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 800)
        {
            cursorArrow = cursorArrow20;
            cursorSword = cursorSword20;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1024)
        {
            cursorArrow = cursorArrow26;
            cursorSword = cursorSword26;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1152)
        {
            cursorArrow = cursorArrow29;
            cursorSword = cursorSword29;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1280)
        {
            cursorArrow = cursorArrow32;
            cursorSword = cursorSword32;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1366)
        {
            cursorArrow = cursorArrow34;
            cursorSword = cursorSword34;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1400)
        {
            cursorArrow = cursorArrow35;
            cursorSword = cursorSword35;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1440)
        {
            cursorArrow = cursorArrow36;
            cursorSword = cursorSword36;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1600)
        {
            cursorArrow = cursorArrow40;
            cursorSword = cursorSword40;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1856)
        {
            cursorArrow = cursorArrow46;
            cursorSword = cursorSword46;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1920)
        {
            cursorArrow = cursorArrow48;
            cursorSword = cursorSword48;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 2048)
        {
            cursorArrow = cursorArrow51;
            cursorSword = cursorSword51;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 2560)
        {
            cursorArrow = cursorArrow64;
            cursorSword = cursorSword64;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 3840)
        {
            cursorArrow = cursorArrow96;
            cursorSword = cursorSword96;
        }
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
        resolutionTime = Time.fixedTime;
        confirmationMenu.SetActive(true);
    }

    public void ConfirmResolution()
    {
        confirmationMenu.SetActive(false);
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void ReturnResolution()
    {
        PlayerPrefs.SetInt("resolutionW", prevW);
        PlayerPrefs.SetInt("resolutionH", prevH);
        if (prevFS) PlayerPrefs.SetInt("fullScreen", 1);
        else PlayerPrefs.SetInt("fullScreen", 0);
        fullScreen = prevFS;
        PlayerPrefs.SetInt("framerate", prevFR);

        Screen.SetResolution(PlayerPrefs.GetInt("resolutionW"), PlayerPrefs.GetInt("resolutionH"), fullScreen, PlayerPrefs.GetInt("framerate"));

        if (PlayerPrefs.GetInt("resolutionW") == 640)
        {
            cursorArrow = cursorArrow16;
            cursorSword = cursorSword16;
            resolution.GetComponent<Dropdown>().value = 0;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 800)
        {
            cursorArrow = cursorArrow20;
            cursorSword = cursorSword20;
            resolution.GetComponent<Dropdown>().value = 1;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1024)
        {
            cursorArrow = cursorArrow26;
            cursorSword = cursorSword26;
            if (PlayerPrefs.GetInt("resolutionH") == 576) resolution.GetComponent<Dropdown>().value = 2;
            else resolution.GetComponent<Dropdown>().value = 3;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1152)
        {
            cursorArrow = cursorArrow29;
            cursorSword = cursorSword29;
            resolution.GetComponent<Dropdown>().value = 4;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1280)
        {
            cursorArrow = cursorArrow32;
            cursorSword = cursorSword32;
            if (PlayerPrefs.GetInt("resolutionH") == 720) resolution.GetComponent<Dropdown>().value = 5;
            else if (PlayerPrefs.GetInt("resolutionH") == 800) resolution.GetComponent<Dropdown>().value = 6;
            else resolution.GetComponent<Dropdown>().value = 7;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1366)
        {
            cursorArrow = cursorArrow34;
            cursorSword = cursorSword34;
            resolution.GetComponent<Dropdown>().value = 8;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1400)
        {
            cursorArrow = cursorArrow35;
            cursorSword = cursorSword35;
            resolution.GetComponent<Dropdown>().value = 9;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1440)
        {
            cursorArrow = cursorArrow36;
            cursorSword = cursorSword36;
            if (PlayerPrefs.GetInt("resolutionH") == 900) resolution.GetComponent<Dropdown>().value = 10;
            else resolution.GetComponent<Dropdown>().value = 11;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1600)
        {
            cursorArrow = cursorArrow40;
            cursorSword = cursorSword40;
            if (PlayerPrefs.GetInt("resolutionH") == 900) resolution.GetComponent<Dropdown>().value = 12;
            else resolution.GetComponent<Dropdown>().value = 13;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1856)
        {
            cursorArrow = cursorArrow46;
            cursorSword = cursorSword46;
            resolution.GetComponent<Dropdown>().value = 14;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1920)
        {
            cursorArrow = cursorArrow48;
            cursorSword = cursorSword48;
            if (PlayerPrefs.GetInt("resolutionH") == 1080) resolution.GetComponent<Dropdown>().value = 15;
            else if (PlayerPrefs.GetInt("resolutionH") == 1200) resolution.GetComponent<Dropdown>().value = 16;
            else resolution.GetComponent<Dropdown>().value = 17;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 2048)
        {
            cursorArrow = cursorArrow51;
            cursorSword = cursorSword51;
            resolution.GetComponent<Dropdown>().value = 18;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 2560)
        {
            cursorArrow = cursorArrow64;
            cursorSword = cursorSword64;
            if (PlayerPrefs.GetInt("resolutionH") == 1440) resolution.GetComponent<Dropdown>().value = 19;
            else resolution.GetComponent<Dropdown>().value = 20;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 3840)
        {
            cursorArrow = cursorArrow96;
            cursorSword = cursorSword96;
            resolution.GetComponent<Dropdown>().value = 21;
        }
        if (PlayerPrefs.GetInt("framerate") == 30) framerate.GetComponent<Dropdown>().value = 0;
        else if (PlayerPrefs.GetInt("framerate") == 60) framerate.GetComponent<Dropdown>().value = 1;
        else if (PlayerPrefs.GetInt("framerate") == 90) framerate.GetComponent<Dropdown>().value = 2;
        else if (PlayerPrefs.GetInt("framerate") == 120) framerate.GetComponent<Dropdown>().value = 3;
        else if (PlayerPrefs.GetInt("framerate") == 144) framerate.GetComponent<Dropdown>().value = 4;
        else if (PlayerPrefs.GetInt("framerate") == 0) framerate.GetComponent<Dropdown>().value = 5;
        fullScreenToggle.GetComponent<Toggle>().isOn = fullScreen;
        Cursor.SetCursor(cursorSword, Vector2.zero, CursorMode.ForceSoftware);

        confirmationMenu.SetActive(false);
    }

}
