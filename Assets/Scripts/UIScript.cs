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
    private float prevMaster;
    private float prevMusic;
    private float prevEffect;
    private int prevLanguage;
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
    //The sound sliders
    private Slider MasterSlider;
    private Slider MusicSlider;
    private Slider EffectsSlider;
    //The time the resolution changes were made
    private float resolutionTime;
    //The return configuration text
    private Text returnText;
    //A boolean to see if the sword cursor is active
    private bool swordCursorActive;
    //The texts we need to translate
    private Text PauseTitle;
    private Text ContinueText;
    private Text MainMenuText;
    private Text SettingsText;
    private Text ExitText;
    private GameObject finalText;
    private Text FinalReturnText;
    private Text DieText;
    private Text RespawnText;
    private Text UpgradeTitleText;
    private Text UpgradeCostText;
    private Text PlayerLevelText;
    private Text LightLeftText;
    private Text HealthDecription;
    private Text ManaDescription;
    private Text StaminaDescription;
    private Text DamageDealtDescription;
    private Text HealingDescription;
    private Text DamageResistanceDescription;
    private Text ExpGainDescription;
    private Text ConfirmUpgradeText;
    private Text DialogueNextText;

    private Text SettingsTitle;
    private Text ResolutionText;
    private Text FullScreenText;
    private Text FrameRateText;
    private Text MainVolumeText;
    private Text MusicText;
    private Text EffectsText;
    private Text LanguageText;
    private Dropdown LanguageDropdown;
    private Text ReturnSaveText;
    private Text ReturnNoSaveText;
    private Text ConfirmationText;
    private Text SaveConfirmText;

    private void Start()
    {
        swordCursorActive = true;
        resolutionTime = 0.0f;
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
        PauseTitle = GameObject.Find("PauseTitle").GetComponent<Text>();
        ContinueText = GameObject.Find("ContinueText").GetComponent<Text>();
        MainMenuText = GameObject.Find("MainMenuText").GetComponent<Text>();
        SettingsText = GameObject.Find("SettingsText").GetComponent<Text>();
        ExitText = GameObject.Find("ExitText").GetComponent<Text>();
        FinalReturnText = GameObject.Find("FinalReturnText").GetComponent<Text>();
        DieText = GameObject.Find("DieText").GetComponent<Text>();
        RespawnText = GameObject.Find("RespawnText").GetComponent<Text>();
        UpgradeTitleText = GameObject.Find("UpgradeTitleText").GetComponent<Text>();
        UpgradeCostText = GameObject.Find("UpgradeCostText").GetComponent<Text>();
        PlayerLevelText = GameObject.Find("PlayerLevelText").GetComponent<Text>();
        LightLeftText = GameObject.Find("LightLeftText").GetComponent<Text>();
        HealthDecription = GameObject.Find("HealthDescription").GetComponent<Text>();
        ManaDescription = GameObject.Find("ManaDescription").GetComponent<Text>();
        StaminaDescription = GameObject.Find("StaminaDescription").GetComponent<Text>();
        DamageDealtDescription = GameObject.Find("DamageDealtDescription").GetComponent<Text>();
        HealingDescription = GameObject.Find("HealingDescription").GetComponent<Text>();
        DamageResistanceDescription = GameObject.Find("DamageResistanceDescription").GetComponent<Text>();
        ExpGainDescription = GameObject.Find("ExpGainDescription").GetComponent<Text>();
        ConfirmUpgradeText = GameObject.Find("ConfirmUpgradeText").GetComponent<Text>();


        SettingsTitle = GameObject.Find("SettingsTitle").GetComponent<Text>();
        ResolutionText = GameObject.Find("ResolutionText").GetComponent<Text>();
        FullScreenText = GameObject.Find("FullScreenText").GetComponent<Text>();
        FrameRateText = GameObject.Find("FrameRateText").GetComponent<Text>();
        MainVolumeText = GameObject.Find("MasterSoundText").GetComponent<Text>();
        MusicText = GameObject.Find("MusicText").GetComponent<Text>();
        EffectsText = GameObject.Find("EffectsText").GetComponent<Text>();
        LanguageText = GameObject.Find("LanguageText").GetComponent<Text>();
        LanguageDropdown = GameObject.Find("DropdownLanguage").GetComponent<Dropdown>();
        ReturnSaveText = GameObject.Find("ReturnSaveText").GetComponent<Text>();
        ReturnNoSaveText = GameObject.Find("ReturnNoSaveText").GetComponent<Text>();
        ConfirmationText = GameObject.Find("ConfirmationText").GetComponent<Text>();
        SaveConfirmText = GameObject.Find("SaveConfirmText").GetComponent<Text>();
        MasterSlider = GameObject.Find("MasterSoundSlider").GetComponent<Slider>();
        MusicSlider = GameObject.Find("MusicSlider").GetComponent<Slider>();
        EffectsSlider = GameObject.Find("EffectsSlider").GetComponent<Slider>();

        MasterSlider.value = PlayerPrefs.GetFloat("masterAudio");
        MusicSlider.value = PlayerPrefs.GetFloat("musicAudio");
        EffectsSlider.value = PlayerPrefs.GetFloat("effectsAudio");

        pauseMenu.SetActive(false);
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
            resolution.GetComponent<Dropdown>().value = 0;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 800)
        {
            resolution.GetComponent<Dropdown>().value = 1;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1024)
        {
            if (PlayerPrefs.GetInt("resolutionH") == 576) resolution.GetComponent<Dropdown>().value = 2;
            else resolution.GetComponent<Dropdown>().value = 3;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1152)
        {
            resolution.GetComponent<Dropdown>().value = 4;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1280)
        {
            if (PlayerPrefs.GetInt("resolutionH") == 720) resolution.GetComponent<Dropdown>().value = 5;
            else if (PlayerPrefs.GetInt("resolutionH") == 800) resolution.GetComponent<Dropdown>().value = 6;
            else resolution.GetComponent<Dropdown>().value = 7;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1366)
        {
            resolution.GetComponent<Dropdown>().value = 8;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1400)
        {
            resolution.GetComponent<Dropdown>().value = 9;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1440)
        {
            if (PlayerPrefs.GetInt("resolutionH") == 900) resolution.GetComponent<Dropdown>().value = 10;
            else resolution.GetComponent<Dropdown>().value = 11;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1600)
        {
            if (PlayerPrefs.GetInt("resolutionH") == 900) resolution.GetComponent<Dropdown>().value = 12;
            else resolution.GetComponent<Dropdown>().value = 13;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1856)
        {
            resolution.GetComponent<Dropdown>().value = 14;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1920)
        {
            if (PlayerPrefs.GetInt("resolutionH") == 1080) resolution.GetComponent<Dropdown>().value = 15;
            else if (PlayerPrefs.GetInt("resolutionH") == 1200) resolution.GetComponent<Dropdown>().value = 16;
            else resolution.GetComponent<Dropdown>().value = 17;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 2048)
        {
            resolution.GetComponent<Dropdown>().value = 18;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 2560)
        {
            if (PlayerPrefs.GetInt("resolutionH") == 1440) resolution.GetComponent<Dropdown>().value = 19;
            else resolution.GetComponent<Dropdown>().value = 20;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 3840)
        {
            resolution.GetComponent<Dropdown>().value = 21;
        }
        if (PlayerPrefs.GetInt("framerate") == 30) framerate.GetComponent<Dropdown>().value = 0;
        else if (PlayerPrefs.GetInt("framerate") == 60) framerate.GetComponent<Dropdown>().value = 1;
        else if (PlayerPrefs.GetInt("framerate") == 90) framerate.GetComponent<Dropdown>().value = 2;
        else if (PlayerPrefs.GetInt("framerate") == 120) framerate.GetComponent<Dropdown>().value = 3;
        else if (PlayerPrefs.GetInt("framerate") == 144) framerate.GetComponent<Dropdown>().value = 4;
        else if (PlayerPrefs.GetInt("framerate") == 0) framerate.GetComponent<Dropdown>().value = 5;
        fullScreenToggle.GetComponent<Toggle>().isOn = fullScreen;
        setSwordCursor(PlayerPrefs.GetInt("resolutionW"));

        if (PlayerPrefs.GetInt("language") == 0)
        {
            LanguageDropdown.value = 0;
            PauseTitle.text = "Paused";
            ContinueText.text = "Continue";
            MainMenuText.text = "Menu";
            SettingsText.text = "Settings";
            ExitText.text = "Exit";
            finalText.GetComponent<Text>().text = "Thanks for playing!";
            FinalReturnText.text = "Return to main menu";
            DieText.text = "YOU DIED";
            RespawnText.text = "Respawn";
            UpgradeTitleText.text = "Upgrade";
            UpgradeCostText.text = "Upgrade cost:";
            PlayerLevelText.text = "Level:";
            LightLeftText.text = "Light left:";
            HealthDecription.text = "Max health:";
            ManaDescription.text = "Max mana:";
            StaminaDescription.text = "Max stamina:";
            DamageDealtDescription.text = "Damage dealt:";
            HealingDescription.text = "Heal per second:";
            DamageResistanceDescription.text = "Damage reduction(%):";
            ExpGainDescription.text = "Exp multiplier";
            ConfirmUpgradeText.text = "Confirm changes";
            //DialogueNextText.text = "Continue >>";

            SettingsTitle.text = "Settings";
            ResolutionText.text = "Resolution";
            FullScreenText.text = "Full Screen";
            FrameRateText.text = "Framerate";
            MainVolumeText.text = "Main volume";
            MusicText.text = "Music";
            EffectsText.text = "Effects";
            LanguageText.text = "Language";
            ReturnSaveText.text = "Save and return";
            ReturnNoSaveText.text = "Return without saving";
            ConfirmationText.text = "Confirm if you can see this window correctly. You will return to the previous configuration if you do not confirm.";
            SaveConfirmText.text = "Save changes";
        }
        else if (PlayerPrefs.GetInt("language") == 1)
        {
            LanguageDropdown.value = 1;
            PauseTitle.text = "Pausado";
            ContinueText.text = "Continuar";
            MainMenuText.text = "Menú";
            SettingsText.text = "Ajustes";
            ExitText.text = "Salir";
            finalText.GetComponent<Text>().text = "¡Gracias por jugar!";
            FinalReturnText.text = "Volver al menú principal";
            DieText.text = "HAS MUERTO";
            RespawnText.text = "Reaparecer";
            UpgradeTitleText.text = "Mejorar";
            UpgradeCostText.text = "Coste de mejora:";
            PlayerLevelText.text = "Nivel:";
            LightLeftText.text = "Luz restante:";
            HealthDecription.text = "Vida max:";
            ManaDescription.text = "Mana max:";
            StaminaDescription.text = "Resistencia max:";
            DamageDealtDescription.text = "Daño realizado:";
            HealingDescription.text = "Cura por segundo:";
            DamageResistanceDescription.text = "Reducción de daño(%):";
            ExpGainDescription.text = "Multiplicador de exp:";
            ConfirmUpgradeText.text = "Confirmar cambios";
            //DialogueNextText.text = "Continuar >>";

            SettingsTitle.text = "Ajustes";
            ResolutionText.text = "Resolución";
            FullScreenText.text = "Pantalla completa";
            FrameRateText.text = "Imágenes por segundo";
            MainVolumeText.text = "Volumen maestro";
            MusicText.text = "Música";
            EffectsText.text = "Efectos";
            LanguageText.text = "Idioma";
            ReturnSaveText.text = "Guardar y volver";
            ReturnNoSaveText.text = "Volver sin guardar";
            ConfirmationText.text = "Confirma que puedes ver esta ventana correctamente. Volverás a la configuración previa si no confirmas.";
            SaveConfirmText.text = "Guardar los cambios";
        }
        else if (PlayerPrefs.GetInt("language") == 2)
        {
            LanguageDropdown.value = 2;
            PauseTitle.text = "Pausatua";
            ContinueText.text = "Jarraitu";
            MainMenuText.text = "Menua";
            SettingsText.text = "Ezarpenak";
            ExitText.text = "Irten";
            finalText.GetComponent<Text>().text = "Eskerrik asko jolasteagatik!";
            FinalReturnText.text = "Hasiera menura itzuli";
            DieText.text = "HIL ZARA";
            RespawnText.text = "Biragertu";
            UpgradeTitleText.text = "Hobetu";
            UpgradeCostText.text = "Hobetzearen kostua:";
            PlayerLevelText.text = "Nibela:";
            LightLeftText.text = "Gainerako argia:";
            HealthDecription.text = "Bizitza max:";
            ManaDescription.text = "Mana max:";
            StaminaDescription.text = "Erresistentzia max:";
            DamageDealtDescription.text = "Egindako mina:";
            HealingDescription.text = "Sendaketa segunduko:";
            DamageResistanceDescription.text = "Min erredukzioa(%):";
            ExpGainDescription.text = "Exp bidertzailea:";
            ConfirmUpgradeText.text = "Aldaketak gorde";
            //DialogueNextText.text = "Jarraitu >>";

            SettingsTitle.text = "Ezarpenak";
            ResolutionText.text = "Erresoluzioa";
            FullScreenText.text = "Pantaila osoa";
            FrameRateText.text = "Irudiak segunduko";
            MainVolumeText.text = "Bolumen nagusia";
            MusicText.text = "Musika";
            EffectsText.text = "Efektuak";
            LanguageText.text = "Hizkuntzak";
            ReturnSaveText.text = "Gorde eta itzuli";
            ReturnNoSaveText.text = "Itzuli gorde gabe";
            ConfirmationText.text = "Lehio hau ondo ikus dezakezula ziurtatu. Ez baduzu baieztatzen lehengo konfiguraziora itzuliko zara.";
            SaveConfirmText.text = "Aldaketak gorde";
        }

    }


    void Update()
    {
        if (player.GetComponent<PlayerMovement>().ended)
        {
            finalText.SetActive(true);
            PlayerPrefs.SetInt("lastDialogue", 15);
        }
        else finalText.SetActive(false);
        if (player.GetComponent<PlayerMovement>().paused && (!pauseMenu.activeSelf && !settingsMenu.activeSelf))
        {
            pauseMenu.SetActive(true);
            setArrowCursor(PlayerPrefs.GetInt("resolutionW"));
        }
        if (dieScreen.activeSelf && swordCursorActive) setArrowCursor(PlayerPrefs.GetInt("resolutionW"));
        if (player.GetComponent<PlayerMovement>().talking && swordCursorActive) setArrowCursor(PlayerPrefs.GetInt("resolutionW"));
        if (!player.GetComponent<PlayerMovement>().paused && !dieScreen.activeSelf && !player.GetComponent<PlayerMovement>().talking && !player.GetComponent<PlayerMovement>().sleeping && !swordCursorActive) setSwordCursor(PlayerPrefs.GetInt("resolutionW"));
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
            setArrowCursor(PlayerPrefs.GetInt("resolutionW"));
        }
        //Show the light number on the interface
        exp.text = PlayerPrefs.GetInt("exp").ToString();
        if (PlayerPrefs.GetInt("needExp") > PlayerPrefs.GetInt("exp")) playerLight.pointLightOuterRadius = (float)PlayerPrefs.GetInt("exp") / (float)PlayerPrefs.GetInt("needExp") * 3.0f;
        else playerLight.pointLightOuterRadius = 3.0f;

        //if to return to previous resolution automatically
        if (resolutionTime != 0.0f && (Time.realtimeSinceStartup - resolutionTime) >= 10)
        {
            resolutionTime = 0.0f;
            ReturnResolution();
        }
        else if (resolutionTime != 0.0f) returnText.text = "Return (" + (10 - (int)(Time.realtimeSinceStartup - resolutionTime)).ToString() + ")";
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
        setSwordCursor(PlayerPrefs.GetInt("resolutionW"));
    }
    public void loadMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }

    public void continueLevel()
    {
        pauseMenu.SetActive(false);
        setSwordCursor(PlayerPrefs.GetInt("resolutionW"));
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
        prevMaster = PlayerPrefs.GetFloat("masterAudio");
        prevMusic = PlayerPrefs.GetFloat("musicAudio");
        prevEffect = PlayerPrefs.GetFloat("effectsAudio");
        prevLanguage = PlayerPrefs.GetInt("language");
    }

    public void CloseNoSave()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
        if (PlayerPrefs.GetInt("resolutionW") == 640)
        {
            Cursor.SetCursor(cursorArrow16, Vector2.zero, CursorMode.ForceSoftware);
            resolution.GetComponent<Dropdown>().value = 0;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 800)
        {
            Cursor.SetCursor(cursorArrow20, Vector2.zero, CursorMode.ForceSoftware);
            resolution.GetComponent<Dropdown>().value = 1;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1024)
        {
            Cursor.SetCursor(cursorArrow26, Vector2.zero, CursorMode.ForceSoftware);
            if (PlayerPrefs.GetInt("resolutionH") == 576) resolution.GetComponent<Dropdown>().value = 2;
            else resolution.GetComponent<Dropdown>().value = 3;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1152)
        {
            Cursor.SetCursor(cursorArrow29, Vector2.zero, CursorMode.ForceSoftware);
            resolution.GetComponent<Dropdown>().value = 4;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1280)
        {
            Cursor.SetCursor(cursorArrow32, Vector2.zero, CursorMode.ForceSoftware);
            if (PlayerPrefs.GetInt("resolutionH") == 720) resolution.GetComponent<Dropdown>().value = 5;
            else if (PlayerPrefs.GetInt("resolutionH") == 800) resolution.GetComponent<Dropdown>().value = 6;
            else resolution.GetComponent<Dropdown>().value = 7;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1366)
        {
            Cursor.SetCursor(cursorArrow34, Vector2.zero, CursorMode.ForceSoftware);
            resolution.GetComponent<Dropdown>().value = 8;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1400)
        {
            Cursor.SetCursor(cursorArrow35, Vector2.zero, CursorMode.ForceSoftware);
            resolution.GetComponent<Dropdown>().value = 9;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1440)
        {
            Cursor.SetCursor(cursorArrow36, Vector2.zero, CursorMode.ForceSoftware);
            if (PlayerPrefs.GetInt("resolutionH") == 900) resolution.GetComponent<Dropdown>().value = 10;
            else resolution.GetComponent<Dropdown>().value = 11;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1600)
        {
            Cursor.SetCursor(cursorArrow40, Vector2.zero, CursorMode.ForceSoftware);
            if (PlayerPrefs.GetInt("resolutionH") == 900) resolution.GetComponent<Dropdown>().value = 12;
            else resolution.GetComponent<Dropdown>().value = 13;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1856)
        {
            Cursor.SetCursor(cursorArrow46, Vector2.zero, CursorMode.ForceSoftware);
            resolution.GetComponent<Dropdown>().value = 14;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1920)
        {
            Cursor.SetCursor(cursorArrow48, Vector2.zero, CursorMode.ForceSoftware);
            if (PlayerPrefs.GetInt("resolutionH") == 1080) resolution.GetComponent<Dropdown>().value = 15;
            else if (PlayerPrefs.GetInt("resolutionH") == 1200) resolution.GetComponent<Dropdown>().value = 16;
            else resolution.GetComponent<Dropdown>().value = 17;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 2048)
        {
            Cursor.SetCursor(cursorArrow51, Vector2.zero, CursorMode.ForceSoftware);
            resolution.GetComponent<Dropdown>().value = 18;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 2560)
        {
            Cursor.SetCursor(cursorArrow64, Vector2.zero, CursorMode.ForceSoftware);
            if (PlayerPrefs.GetInt("resolutionH") == 1440) resolution.GetComponent<Dropdown>().value = 19;
            else resolution.GetComponent<Dropdown>().value = 20;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 3840)
        {
            Cursor.SetCursor(cursorArrow96, Vector2.zero, CursorMode.ForceSoftware);
            resolution.GetComponent<Dropdown>().value = 21;
        }
        if (PlayerPrefs.GetInt("framerate") == 30) framerate.GetComponent<Dropdown>().value = 0;
        else if (PlayerPrefs.GetInt("framerate") == 60) framerate.GetComponent<Dropdown>().value = 1;
        else if (PlayerPrefs.GetInt("framerate") == 90) framerate.GetComponent<Dropdown>().value = 2;
        else if (PlayerPrefs.GetInt("framerate") == 120) framerate.GetComponent<Dropdown>().value = 3;
        else if (PlayerPrefs.GetInt("framerate") == 144) framerate.GetComponent<Dropdown>().value = 4;
        else if (PlayerPrefs.GetInt("framerate") == 0) framerate.GetComponent<Dropdown>().value = 5;
        fullScreenToggle.GetComponent<Toggle>().isOn = fullScreen;
        LanguageDropdown.value = prevLanguage;
        MasterSlider.value = prevMaster;
        MusicSlider.value = prevMusic;
        EffectsSlider.value = prevEffect;
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
        
        setArrowCursor(PlayerPrefs.GetInt("resolutionW"));

        if (prevW == PlayerPrefs.GetInt("resolutionW") && prevH == PlayerPrefs.GetInt("resolutionH") && prevFS == fullScreen)
        {
            settingsMenu.SetActive(false);
            pauseMenu.SetActive(true);
        }
        else
        {
            resolutionTime = Time.realtimeSinceStartup;
            confirmationMenu.SetActive(true);
        }
        PlayerPrefs.SetFloat("masterAudio", MasterSlider.value);
        PlayerPrefs.SetFloat("musicAudio", MusicSlider.value);
        PlayerPrefs.SetFloat("effectsAudio", EffectsSlider.value);
    }

    public void ConfirmResolution()
    {
        confirmationMenu.SetActive(false);
        settingsMenu.SetActive(false);
        resolutionTime = 0.0f;
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
            resolution.GetComponent<Dropdown>().value = 0;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 800)
        {
            resolution.GetComponent<Dropdown>().value = 1;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1024)
        {
            if (PlayerPrefs.GetInt("resolutionH") == 576) resolution.GetComponent<Dropdown>().value = 2;
            else resolution.GetComponent<Dropdown>().value = 3;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1152)
        {
            resolution.GetComponent<Dropdown>().value = 4;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1280)
        {
            if (PlayerPrefs.GetInt("resolutionH") == 720) resolution.GetComponent<Dropdown>().value = 5;
            else if (PlayerPrefs.GetInt("resolutionH") == 800) resolution.GetComponent<Dropdown>().value = 6;
            else resolution.GetComponent<Dropdown>().value = 7;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1366)
        {
            resolution.GetComponent<Dropdown>().value = 8;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1400)
        {
            resolution.GetComponent<Dropdown>().value = 9;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1440)
        {
            if (PlayerPrefs.GetInt("resolutionH") == 900) resolution.GetComponent<Dropdown>().value = 10;
            else resolution.GetComponent<Dropdown>().value = 11;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1600)
        {
            if (PlayerPrefs.GetInt("resolutionH") == 900) resolution.GetComponent<Dropdown>().value = 12;
            else resolution.GetComponent<Dropdown>().value = 13;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1856)
        {
            resolution.GetComponent<Dropdown>().value = 14;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1920)
        {
            if (PlayerPrefs.GetInt("resolutionH") == 1080) resolution.GetComponent<Dropdown>().value = 15;
            else if (PlayerPrefs.GetInt("resolutionH") == 1200) resolution.GetComponent<Dropdown>().value = 16;
            else resolution.GetComponent<Dropdown>().value = 17;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 2048)
        {
            resolution.GetComponent<Dropdown>().value = 18;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 2560)
        {
            if (PlayerPrefs.GetInt("resolutionH") == 1440) resolution.GetComponent<Dropdown>().value = 19;
            else resolution.GetComponent<Dropdown>().value = 20;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 3840)
        {
            resolution.GetComponent<Dropdown>().value = 21;
        }
        setArrowCursor(PlayerPrefs.GetInt("resolutionW"));
        if (PlayerPrefs.GetInt("framerate") == 30) framerate.GetComponent<Dropdown>().value = 0;
        else if (PlayerPrefs.GetInt("framerate") == 60) framerate.GetComponent<Dropdown>().value = 1;
        else if (PlayerPrefs.GetInt("framerate") == 90) framerate.GetComponent<Dropdown>().value = 2;
        else if (PlayerPrefs.GetInt("framerate") == 120) framerate.GetComponent<Dropdown>().value = 3;
        else if (PlayerPrefs.GetInt("framerate") == 144) framerate.GetComponent<Dropdown>().value = 4;
        else if (PlayerPrefs.GetInt("framerate") == 0) framerate.GetComponent<Dropdown>().value = 5;
        fullScreenToggle.GetComponent<Toggle>().isOn = fullScreen;

        confirmationMenu.SetActive(false);
    }


    public void setArrowCursor(int resolutionW)
    {
        Debug.Log("fleicha");
        swordCursorActive = false;
        if (resolutionW == 640) Cursor.SetCursor(cursorArrow16, Vector2.zero, CursorMode.ForceSoftware);
        else if (resolutionW == 800) Cursor.SetCursor(cursorArrow20, Vector2.zero, CursorMode.ForceSoftware);
        else if (resolutionW == 1024) Cursor.SetCursor(cursorArrow26, Vector2.zero, CursorMode.ForceSoftware);
        else if (resolutionW == 1152) Cursor.SetCursor(cursorArrow29, Vector2.zero, CursorMode.ForceSoftware);
        else if (resolutionW == 1280) Cursor.SetCursor(cursorArrow32, Vector2.zero, CursorMode.ForceSoftware);
        else if (resolutionW == 1366) Cursor.SetCursor(cursorArrow34, Vector2.zero, CursorMode.ForceSoftware);
        else if (resolutionW == 1400) Cursor.SetCursor(cursorArrow35, Vector2.zero, CursorMode.ForceSoftware);
        else if (resolutionW == 1440) Cursor.SetCursor(cursorArrow36, Vector2.zero, CursorMode.ForceSoftware);
        else if (resolutionW == 1600) Cursor.SetCursor(cursorArrow40, Vector2.zero, CursorMode.ForceSoftware);
        else if (resolutionW == 1856) Cursor.SetCursor(cursorArrow46, Vector2.zero, CursorMode.ForceSoftware);
        else if (resolutionW == 1920) Cursor.SetCursor(cursorArrow48, Vector2.zero, CursorMode.ForceSoftware);
        else if (resolutionW == 2048) Cursor.SetCursor(cursorArrow51, Vector2.zero, CursorMode.ForceSoftware);
        else if (resolutionW == 2560) Cursor.SetCursor(cursorArrow64, Vector2.zero, CursorMode.ForceSoftware);
        else if (resolutionW == 3840) Cursor.SetCursor(cursorArrow96, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void setSwordCursor(int resolutionW)
    {
        Debug.Log("eispada");
        swordCursorActive = true;
        if (resolutionW == 640) Cursor.SetCursor(cursorSword16, Vector2.zero, CursorMode.ForceSoftware);
        else if (resolutionW == 800) Cursor.SetCursor(cursorSword20, Vector2.zero, CursorMode.ForceSoftware);
        else if (resolutionW == 1024) Cursor.SetCursor(cursorSword26, Vector2.zero, CursorMode.ForceSoftware);
        else if (resolutionW == 1152) Cursor.SetCursor(cursorSword29, Vector2.zero, CursorMode.ForceSoftware);
        else if (resolutionW == 1280) Cursor.SetCursor(cursorSword32, Vector2.zero, CursorMode.ForceSoftware);
        else if (resolutionW == 1366) Cursor.SetCursor(cursorSword34, Vector2.zero, CursorMode.ForceSoftware);
        else if (resolutionW == 1400) Cursor.SetCursor(cursorSword35, Vector2.zero, CursorMode.ForceSoftware);
        else if (resolutionW == 1440) Cursor.SetCursor(cursorSword36, Vector2.zero, CursorMode.ForceSoftware);
        else if (resolutionW == 1600) Cursor.SetCursor(cursorSword40, Vector2.zero, CursorMode.ForceSoftware);
        else if (resolutionW == 1856) Cursor.SetCursor(cursorSword46, Vector2.zero, CursorMode.ForceSoftware);
        else if (resolutionW == 1920) Cursor.SetCursor(cursorSword48, Vector2.zero, CursorMode.ForceSoftware);
        else if (resolutionW == 2048) Cursor.SetCursor(cursorSword51, Vector2.zero, CursorMode.ForceSoftware);
        else if (resolutionW == 2560) Cursor.SetCursor(cursorSword64, Vector2.zero, CursorMode.ForceSoftware);
        else if (resolutionW == 3840) Cursor.SetCursor(cursorSword96, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void ChangeLanguage()
    {
        PlayerPrefs.SetInt("language", LanguageDropdown.value);
        if (PlayerPrefs.GetInt("language") == 0)
        {
            PauseTitle.text = "Paused";
            ContinueText.text = "Continue";
            MainMenuText.text = "Menu";
            SettingsText.text = "Settings";
            ExitText.text = "Exit";
            finalText.GetComponent<Text>().text = "Thanks for playing!";
            FinalReturnText.text = "Return to main menu";
            DieText.text = "YOU DIED";
            RespawnText.text = "Respawn";
            UpgradeTitleText.text = "Upgrade";
            UpgradeCostText.text = "Upgrade cost:";
            PlayerLevelText.text = "Level:";
            LightLeftText.text = "Light left:";
            HealthDecription.text = "Max health:";
            ManaDescription.text = "Max mana:";
            StaminaDescription.text = "Max stamina:";
            DamageDealtDescription.text = "Damage dealt:";
            HealingDescription.text = "Heal per second:";
            DamageResistanceDescription.text = "Damage reduction(%):";
            ExpGainDescription.text = "Exp multiplier";
            ConfirmUpgradeText.text = "Confirm changes";
            //DialogueNextText.text = "Continue >>";

            SettingsTitle.text = "Settings";
            ResolutionText.text = "Resolution";
            FullScreenText.text = "Full Screen";
            FrameRateText.text = "Framerate";
            MainVolumeText.text = "Main volume";
            MusicText.text = "Music";
            EffectsText.text = "Effects";
            LanguageText.text = "Language";
            ReturnSaveText.text = "Save and return";
            ReturnNoSaveText.text = "Return without saving";
            ConfirmationText.text = "Confirm if you can see this window correctly. You will return to the previous configuration if you do not confirm.";
            SaveConfirmText.text = "Save changes";
        }
        else if (PlayerPrefs.GetInt("language") == 1)
        {
            PauseTitle.text = "Pausado";
            ContinueText.text = "Continuar";
            MainMenuText.text = "Menú";
            SettingsText.text = "Ajustes";
            ExitText.text = "Salir";
            finalText.GetComponent<Text>().text = "¡Gracias por jugar!";
            FinalReturnText.text = "Volver al menú principal";
            DieText.text = "HAS MUERTO";
            RespawnText.text = "Reaparecer";
            UpgradeTitleText.text = "Mejorar";
            UpgradeCostText.text = "Coste de mejora:";
            PlayerLevelText.text = "Nivel:";
            LightLeftText.text = "Luz restante:";
            HealthDecription.text = "Vida max:";
            ManaDescription.text = "Mana max:";
            StaminaDescription.text = "Resistencia max:";
            DamageDealtDescription.text = "Daño realizado:";
            HealingDescription.text = "Cura por segundo:";
            DamageResistanceDescription.text = "Reducción de daño(%):";
            ExpGainDescription.text = "Multiplicador de exp:";
            ConfirmUpgradeText.text = "Confirmar cambios";
            //DialogueNextText.text = "Continuar >>";

            SettingsTitle.text = "Ajustes";
            ResolutionText.text = "Resolución";
            FullScreenText.text = "Pantalla completa";
            FrameRateText.text = "Imágenes por segundo";
            MainVolumeText.text = "Volumen maestro";
            MusicText.text = "Música";
            EffectsText.text = "Efectos";
            LanguageText.text = "Idioma";
            ReturnSaveText.text = "Guardar y volver";
            ReturnNoSaveText.text = "Volver sin guardar";
            ConfirmationText.text = "Confirma que puedes ver esta ventana correctamente. Volverás a la configuración previa si no confirmas.";
            SaveConfirmText.text = "Guardar los cambios";
        }
        else if (PlayerPrefs.GetInt("language") == 2)
        {
            PauseTitle.text = "Pausatua";
            ContinueText.text = "Jarraitu";
            MainMenuText.text = "Menua";
            SettingsText.text = "Ezarpenak";
            ExitText.text = "Irten";
            finalText.GetComponent<Text>().text = "Eskerrik asko jolasteagatik!";
            FinalReturnText.text = "Hasiera menura itzuli";
            DieText.text = "HIL ZARA";
            RespawnText.text = "Biragertu";
            UpgradeTitleText.text = "Hobetu";
            UpgradeCostText.text = "Hobetzearen kostua:";
            PlayerLevelText.text = "Nibela:";
            LightLeftText.text = "Gainerako argia:";
            HealthDecription.text = "Bizitza max:";
            ManaDescription.text = "Mana max:";
            StaminaDescription.text = "Erresistentzia max:";
            DamageDealtDescription.text = "Egindako mina:";
            HealingDescription.text = "Sendaketa segunduko:";
            DamageResistanceDescription.text = "Min erredukzioa(%):";
            ExpGainDescription.text = "Exp bidertzailea:";
            ConfirmUpgradeText.text = "Aldaketak gorde";
            //DialogueNextText.text = "Jarraitu >>";

            SettingsTitle.text = "Ezarpenak";
            ResolutionText.text = "Erresoluzioa";
            FullScreenText.text = "Pantaila osoa";
            FrameRateText.text = "Irudiak segunduko";
            MainVolumeText.text = "Bolumen nagusia";
            MusicText.text = "Musika";
            EffectsText.text = "Efektuak";
            LanguageText.text = "Hizkuntzak";
            ReturnSaveText.text = "Gorde eta itzuli";
            ReturnNoSaveText.text = "Itzuli gorde gabe";
            ConfirmationText.text = "Lehio hau ondo ikus dezakezula ziurtatu. Ez baduzu baieztatzen lehengo konfiguraziora itzuliko zara.";
            SaveConfirmText.text = "Aldaketak gorde";
        }
    }
}
