﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
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
    //The main menu
    private GameObject mainMenu;
    //The load button
    private GameObject LoadButton;
    //The settings menu
    private GameObject settingsMenu;
    //The resolution selector dropdown
    private GameObject resolution;
    //The toggle of the full screen mode
    private GameObject fullScreenToggle;
    //the screen mode
    private bool fullScreen;
    //The framerate selector dropdown
    private GameObject framerate;
    //The previous screen configuration
    private int prevW;
    private int prevH;
    private bool prevFS;
    private int prevFR;
    private float prevMaster;
    private float prevMusic;
    private float prevEffect;
    private int prevLanguage;
    //The return configuration text
    private Text returnText;
    //The time the resolution changes were made
    private float resolutionTime;
    //The confirmation window
    private GameObject confirmationMenu;
    //The language selection menu
    private GameObject LanguageSelectionMenu;
    //The sound sliders
    private Slider MasterSlider;
    private Slider MusicSlider;
    private Slider EffectsSlider;
    //The texts we need to translate
    private Text NewGameText;
    private Text LoadText;
    private Text SettingsText;
    private Text ExitText;
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
    private Text LanguageChooseText;
    private Text SaveLanguageText;
    private Dropdown LanguageChooseDropdown;
    
    void Start()
    {
        //We put the resolution time to 0
        resolutionTime = 0.0f;
        //We find all the gameobjects
        mainMenu = GameObject.Find("MainMenu");
        LoadButton = GameObject.Find("Load");
        settingsMenu = GameObject.Find("SettingsMenu");
        resolution = GameObject.Find("DropdownResolution");
        framerate = GameObject.Find("DropdownFrameRate");
        fullScreenToggle = GameObject.Find("Windowed");
        confirmationMenu = GameObject.Find("ConfirmMenu");
        LanguageSelectionMenu = GameObject.Find("LanguageMenu");
        returnText = GameObject.Find("ReturnText").GetComponent<Text>();
        NewGameText = GameObject.Find("NewGameText").GetComponent<Text>();
        LoadText = GameObject.Find("LoadText").GetComponent<Text>();
        SettingsText = GameObject.Find("SettingsText").GetComponent<Text>();
        ExitText = GameObject.Find("ExitText").GetComponent<Text>();
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
        LanguageChooseText = GameObject.Find("LanguageChooseText").GetComponent<Text>();
        LanguageChooseDropdown = GameObject.Find("DropdownChooseLanguage").GetComponent<Dropdown>();
        SaveLanguageText = GameObject.Find("SaveLanguageText").GetComponent<Text>();
        MasterSlider = GameObject.Find("MasterSoundSlider").GetComponent<Slider>();
        MusicSlider = GameObject.Find("MusicSlider").GetComponent<Slider>();
        EffectsSlider = GameObject.Find("EffectsSlider").GetComponent<Slider>();
        settingsMenu.SetActive(false);
        confirmationMenu.SetActive(false);
        //We initialize all the playerprefs
        //The resolution width   
        if (!PlayerPrefs.HasKey("resolutionW")) PlayerPrefs.SetInt("resolutionW", 1280);
        //The resolution height
        if (!PlayerPrefs.HasKey("resolutionH")) PlayerPrefs.SetInt("resolutionH", 720);
        //The full screen mode: 0-> windowed, 1-> full screen
        if (!PlayerPrefs.HasKey("fullScreen")) PlayerPrefs.SetInt("fullScreen", 1);
        //The preferred refresh rate
        if (!PlayerPrefs.HasKey("framerate")) PlayerPrefs.SetInt("framerate", 0);
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
        if (!PlayerPrefs.HasKey("respawnscene")) PlayerPrefs.SetInt("respawnscene", 1);
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
        if (!PlayerPrefs.HasKey("diedscene")) PlayerPrefs.SetInt("diedscene", 1);
        //An int to save if the die tutorial has been seen
        if (!PlayerPrefs.HasKey("dieTutorial")) PlayerPrefs.SetInt("dieTutorial", 0);
        //The health level 
        if (!PlayerPrefs.HasKey("healthLevel")) PlayerPrefs.SetInt("healthLevel", 1);
        //An int to save if the player has recovered the lost exp
        if (!PlayerPrefs.HasKey("recoveredExp")) PlayerPrefs.SetInt("recoveredExp", 0);
        //A float to save the current health
        if (!PlayerPrefs.HasKey("health")) PlayerPrefs.SetFloat("health", Mathf.Sqrt(2000 * PlayerPrefs.GetInt("healthLevel")) + 55);
        //An int to save the language
        if (!PlayerPrefs.HasKey("language")) PlayerPrefs.SetInt("language", 0);
        //An int to save if the player has choosen any language
        if (!PlayerPrefs.HasKey("languageChoosen")) PlayerPrefs.SetInt("languageChoosen", 0);
        //A float to set the Master audio mixer
        if (!PlayerPrefs.HasKey("masterAudio")) PlayerPrefs.SetFloat("masterAudio", 1.0f);
        //A float to set the music audio mixer
        if (!PlayerPrefs.HasKey("musicAudio")) PlayerPrefs.SetFloat("musicAudio", 1.0f);
        //A float to set the effects audio mixer
        if (!PlayerPrefs.HasKey("effectsAudio")) PlayerPrefs.SetFloat("effectsAudio", 1.0f);
        MasterSlider.value = PlayerPrefs.GetFloat("masterAudio");
        MusicSlider.value = PlayerPrefs.GetFloat("musicAudio");
        EffectsSlider.value = PlayerPrefs.GetFloat("effectsAudio");
        //We check if the player has choosen the language
        if (PlayerPrefs.GetInt("languageChoosen") == 0)
        {
            LanguageSelectionMenu.SetActive(true);
            PlayerPrefs.SetInt("languageChoosen", 1);
        }else LanguageSelectionMenu.SetActive(false);
        //We check if the player has a save file
        if (PlayerPrefs.GetInt("lastDialogue") == 0) LoadButton.GetComponent<Button>().interactable = false;
        else LoadButton.GetComponent<Button>().interactable = true;
        //We check the settings
        if (PlayerPrefs.GetInt("fullScreen") == 0) fullScreen = false;
        else fullScreen = true;
        Screen.SetResolution(PlayerPrefs.GetInt("resolutionW"), PlayerPrefs.GetInt("resolutionH"), fullScreen, PlayerPrefs.GetInt("framerate"));
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
            if(PlayerPrefs.GetInt("resolutionH") == 900) resolution.GetComponent<Dropdown>().value = 10;
            else resolution.GetComponent<Dropdown>().value = 11;
        }
        else if (PlayerPrefs.GetInt("resolutionW") == 1600)
        {
            Cursor.SetCursor(cursorArrow40, Vector2.zero, CursorMode.ForceSoftware);
            if(PlayerPrefs.GetInt("resolutionH") == 900) resolution.GetComponent<Dropdown>().value = 12;
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
            if(PlayerPrefs.GetInt("resolutionH") == 1080) resolution.GetComponent<Dropdown>().value = 15;
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
            if(PlayerPrefs.GetInt("resolutionH") == 1440) resolution.GetComponent<Dropdown>().value = 19;
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
        if (PlayerPrefs.GetInt("language") == 0)
        {
            LanguageDropdown.value = 0;
            NewGameText.text = "New game";
            LoadText.text = "Load";
            SettingsText.text = "Settings";
            ExitText.text = "Exit";
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
            LanguageChooseText.text = "Select the language you want the game to be. \nYou can change it whenever you want on the settings.";
            SaveLanguageText.text = "Save language";
        }
        else if (PlayerPrefs.GetInt("language") == 1)
        {
            LanguageDropdown.value = 1;
            NewGameText.text = "Nueva partida";
            LoadText.text = "Cargar";
            SettingsText.text = "Ajustes";
            ExitText.text = "Salir";
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
            LanguageChooseText.text = "Selecciona el idioma en el que quieres que esté el juego. \nPuedes cambiarlo cuando quieras en los ajustes.";
            SaveLanguageText.text = "Guardar idioma";
        }
        else if (PlayerPrefs.GetInt("language") == 2)
        {
            LanguageDropdown.value = 2;
            NewGameText.text = "Partida berria";
            LoadText.text = "Kargatu";
            SettingsText.text = "Ezarpenak";
            ExitText.text = "Irten";
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
            LanguageChooseText.text = "Erabaki zein hizkuntzatan izan nahi duzun jolasa. \nNahi duzunean alda dezakezu ezarpenetan.";
            SaveLanguageText.text = "Hizkuntza gorde";
        }
    }
    //A function to start a new game
    public void NewGame()
    {
        //The mana level
        PlayerPrefs.SetInt("manaLevel", 1);
        //The dealt damage level
        PlayerPrefs.SetInt("dealtDamageLevel", 1);
        //The stamina level
        PlayerPrefs.SetInt("staminaLevel", 1);
        //The healing level
        PlayerPrefs.SetInt("healingLevel", 1);
        //The damage resistance level
        PlayerPrefs.SetInt("damageResistanceLevel", 1);
        //The exp gaining level
        PlayerPrefs.SetInt("expGainingLevel", 1);
        //The total lvl of the player
        PlayerPrefs.SetInt("lvl", 1);
        //The needed exp to lvl up
        PlayerPrefs.SetInt("needExp", 30);
        //The exp 
        PlayerPrefs.SetInt("exp", 0);
        //A float to save the current mana
        PlayerPrefs.SetFloat("mana", 0.0f);
        //An int to see if the exp tutorial has been shown
        PlayerPrefs.SetInt("expTutorial", 0);
        //A float to save the x of the respawn point
        PlayerPrefs.SetFloat("respawnx", -49.826f);
        //A float to save the y of the respawn point
        PlayerPrefs.SetFloat("respawny", -3.367f);
        //A float to save the side the player is facing on the respawn point. 0-> left, 1-> right
        PlayerPrefs.SetInt("respawnface", 1);
        //An int to save the scene of the respawn point
        PlayerPrefs.SetInt("respawnscene", 1);
        //An int to save the number of the las dialogue
        PlayerPrefs.SetInt("lastDialogue", 0);
        //0 -> scene change, 1 -> to respawn when loading the game, 2 -> Player died
        PlayerPrefs.SetInt("hasDied", 1);
        //A float to save the x of the spawn point (when we move from one scene to another without dieing)
        PlayerPrefs.SetFloat("spawnx", -49.76f);
        //A float to save the y of the spawn point (when we move from one scene to another without dieing)
        PlayerPrefs.SetFloat("spawny", -3.367f);
        //A float to save the side the player is facing on the spawn point. 0-> left, 1-> right
        PlayerPrefs.SetInt("spawnface", 0);
        //A float to see if the player has fallen into the trap of level 1-2
        PlayerPrefs.SetInt("trap", 0);
        //An int to save the exp the player has when resting
        PlayerPrefs.SetInt("restExp", 0);
        //A float to save the x where the player died
        PlayerPrefs.SetFloat("diedx", 0);
        //A float to save the y where the player died
        PlayerPrefs.SetFloat("diedy", 0);
        //An int to save the exp lost when dieing
        PlayerPrefs.SetInt("diedexp", 0);
        //An int to save the scene the player died
        PlayerPrefs.SetInt("diedscene", 1);
        //An int to save if the die tutorial has been seen
        PlayerPrefs.SetInt("dieTutorial", 0);
        //The health level 
        PlayerPrefs.SetInt("healthLevel", 1);
        //An int to save if the player has recovered the lost exp
        PlayerPrefs.SetInt("recoveredExp", 0);
        //A float to save the current health
        PlayerPrefs.SetFloat("health", Mathf.Sqrt(2000 * PlayerPrefs.GetInt("healthLevel")) + 55);
        SceneManager.LoadScene(1);
    }
    void FixedUpdate()
    {
        //We check how much time has passed from the moment the player saved the resolution, if the player needs more than 10 seconds to confirm we return to the previous resolution
        if (resolutionTime != 0.0f && (Time.fixedTime - resolutionTime) >= 10)
        {
            resolutionTime = 0.0f;
            ReturnResolution();
        }
        else if (resolutionTime != 0.0f)
        {
            if (PlayerPrefs.GetInt("language") == 0) returnText.text = "Return (" + (10 - (int)(Time.fixedTime - resolutionTime)).ToString() + ")";
            else if (PlayerPrefs.GetInt("language") == 1) returnText.text = "Volver (" + (10 - (int)(Time.fixedTime - resolutionTime)).ToString() + ")";
            else if (PlayerPrefs.GetInt("language") == 2) returnText.text = "Itzuli (" + (10 - (int)(Time.fixedTime - resolutionTime)).ToString() + ")";
        }
    }

    //Function to load the saved file
    public void LoadLevel()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("respawnscene"));
    }

    //Function to close the game
    public void CloseGame()
    {
        Debug.Log("Closing game");
        Application.Quit();
    }

    //Function to save the settings
    public void OpenSettings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
        prevMaster = PlayerPrefs.GetFloat("masterAudio"); 
        prevMusic = PlayerPrefs.GetFloat("musicAudio");
        prevEffect = PlayerPrefs.GetFloat("effectsAudio");
        prevLanguage = PlayerPrefs.GetInt("language");
    }

    //Function to close the settings without saving. We return the values to their actual value.
    public void CloseNoSave()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
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

    //Function to save the changes made on the settings
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

        if (PlayerPrefs.GetInt("resolutionW") == 640) Cursor.SetCursor(cursorArrow16, Vector2.zero, CursorMode.ForceSoftware);
        else if (PlayerPrefs.GetInt("resolutionW") == 800) Cursor.SetCursor(cursorArrow20, Vector2.zero, CursorMode.ForceSoftware);
        else if (PlayerPrefs.GetInt("resolutionW") == 1024) Cursor.SetCursor(cursorArrow26, Vector2.zero, CursorMode.ForceSoftware);
        else if (PlayerPrefs.GetInt("resolutionW") == 1152) Cursor.SetCursor(cursorArrow29, Vector2.zero, CursorMode.ForceSoftware);
        else if (PlayerPrefs.GetInt("resolutionW") == 1280) Cursor.SetCursor(cursorArrow32, Vector2.zero, CursorMode.ForceSoftware);
        else if (PlayerPrefs.GetInt("resolutionW") == 1366) Cursor.SetCursor(cursorArrow34, Vector2.zero, CursorMode.ForceSoftware);
        else if (PlayerPrefs.GetInt("resolutionW") == 1400) Cursor.SetCursor(cursorArrow35, Vector2.zero, CursorMode.ForceSoftware);
        else if (PlayerPrefs.GetInt("resolutionW") == 1440) Cursor.SetCursor(cursorArrow36, Vector2.zero, CursorMode.ForceSoftware);
        else if (PlayerPrefs.GetInt("resolutionW") == 1600) Cursor.SetCursor(cursorArrow40, Vector2.zero, CursorMode.ForceSoftware);
        else if (PlayerPrefs.GetInt("resolutionW") == 1856) Cursor.SetCursor(cursorArrow46, Vector2.zero, CursorMode.ForceSoftware);
        else if (PlayerPrefs.GetInt("resolutionW") == 1920) Cursor.SetCursor(cursorArrow48, Vector2.zero, CursorMode.ForceSoftware);
        else if (PlayerPrefs.GetInt("resolutionW") == 2048) Cursor.SetCursor(cursorArrow51, Vector2.zero, CursorMode.ForceSoftware);
        else if (PlayerPrefs.GetInt("resolutionW") == 2560) Cursor.SetCursor(cursorArrow64, Vector2.zero, CursorMode.ForceSoftware);
        else if (PlayerPrefs.GetInt("resolutionW") == 3840) Cursor.SetCursor(cursorArrow96, Vector2.zero, CursorMode.ForceSoftware);

        //If we change the resolution or the full screen mode we ask the player if them can see the window correctly
        if(prevW == PlayerPrefs.GetInt("resolutionW") && prevH == PlayerPrefs.GetInt("resolutionH") && prevFS == fullScreen)
        {
            settingsMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
        else
        {
            resolutionTime = Time.fixedTime;
            confirmationMenu.SetActive(true);
        }
        PlayerPrefs.SetFloat("masterAudio", MasterSlider.value);
        PlayerPrefs.SetFloat("musicAudio", MusicSlider.value);
        PlayerPrefs.SetFloat("effectsAudio", EffectsSlider.value);
    }

    //Function to confirm that the player can see the resolution changes correctly
    public void ConfirmResolution()
    {
        confirmationMenu.SetActive(false);
        settingsMenu.SetActive(false);
        resolutionTime = 0.0f;
        mainMenu.SetActive(true);
    }

    //Function to return to the previous resolution because the player can't see the window correctly
    public void ReturnResolution()
    {
        PlayerPrefs.SetInt("resolutionW", prevW);
        PlayerPrefs.SetInt("resolutionH", prevH);
        if(prevFS) PlayerPrefs.SetInt("fullScreen", 1);
        else PlayerPrefs.SetInt("fullScreen", 0);
        fullScreen = prevFS;
        PlayerPrefs.SetInt("framerate", prevFR);

        Screen.SetResolution(PlayerPrefs.GetInt("resolutionW"), PlayerPrefs.GetInt("resolutionH"), fullScreen, PlayerPrefs.GetInt("framerate"));


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

        confirmationMenu.SetActive(false);
    }

    //Function to change the language
    public void ChangeLanguage(bool first)
    {
        if (first) PlayerPrefs.SetInt("language", LanguageChooseDropdown.value);
        else PlayerPrefs.SetInt("language", LanguageDropdown.value);
        if (PlayerPrefs.GetInt("language") == 0)
        {
            NewGameText.text = "New game";
            LoadText.text = "Load";
            SettingsText.text = "Settings";
            ExitText.text = "Exit";
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
            LanguageChooseText.text = "Select the language you want the game to be. \nYou can change it whenever you want on the settings.";
            SaveLanguageText.text = "Save language";

        }
        else if (PlayerPrefs.GetInt("language") == 1)
        {
            NewGameText.text = "Nueva partida";
            LoadText.text = "Cargar";
            SettingsText.text = "Ajustes";
            ExitText.text = "Salir";
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
            LanguageChooseText.text = "Selecciona el idioma en el que quieres que esté el juego. \nPuedes cambiarlo cuando quieras en los ajustes.";
            SaveLanguageText.text = "Guardar idioma";
        }
        else if (PlayerPrefs.GetInt("language") == 2)
        {
            NewGameText.text = "Partida berria";
            LoadText.text = "Kargatu";
            SettingsText.text = "Ezarpenak";
            ExitText.text = "Irten";
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
            LanguageChooseText.text = "Erabaki zein hizkuntzatan izan nahi duzun jolasa. \nNahi duzunean alda dezakezu ezarpenetan.";
            SaveLanguageText.text = "Hizkuntza gorde";
        }
    }

    //Function to close the choose language window
    public void CloseChooseLanguage()
    {
        LanguageSelectionMenu.SetActive(false);
        if (PlayerPrefs.GetInt("language") == 0) LanguageDropdown.value = 0;
        else if (PlayerPrefs.GetInt("language") == 1) LanguageDropdown.value = 1;
        else if (PlayerPrefs.GetInt("language") == 2) LanguageDropdown.value = 2;
    }
}
