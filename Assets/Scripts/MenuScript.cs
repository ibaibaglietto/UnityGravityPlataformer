using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    //The cursor
    public Texture2D cursorArrow;
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
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
        mainMenu = GameObject.Find("MainMenu");
        LoadButton = GameObject.Find("Load");
        settingsMenu = GameObject.Find("SettingsMenu");
        resolution = GameObject.Find("DropdownResolution");
        fullScreenToggle = GameObject.Find("Windowed");
        settingsMenu.SetActive(false);
        //We initialize all the playerprefs on the awake
        //The resolution width
        if (!PlayerPrefs.HasKey("resolutionW")) PlayerPrefs.SetInt("resolutionW", 1280);
        //The resolution height
        if (!PlayerPrefs.HasKey("resolutionH")) PlayerPrefs.SetInt("resolutionH", 720);
        //The full screen mode: 0-> windowed, 1-> full screen
        if (!PlayerPrefs.HasKey("fullScreen")) PlayerPrefs.SetInt("fullScreen", 0);
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
        if (PlayerPrefs.GetInt("lastDialogue") == 0) LoadButton.GetComponent<Button>().interactable = false;
        else LoadButton.GetComponent<Button>().interactable = true;
        if (PlayerPrefs.GetInt("fullScreen") == 0) fullScreen = false;
        else fullScreen = true;
        Screen.SetResolution(PlayerPrefs.GetInt("resolutionW"), PlayerPrefs.GetInt("resolutionH"), fullScreen);
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
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
        //A float to save the current health
        PlayerPrefs.SetFloat("health", Mathf.Sqrt(2000 * PlayerPrefs.GetInt("healthLevel")) + 55);
        SceneManager.LoadScene(1);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("respawnscene"));
    }

    public void CloseGame()
    {
        Debug.Log("Closing game");
        Application.Quit();
    }

    public void OpenSettings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void CloseNoSave()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void CloseSave()
    {
        if(resolution.GetComponent<Dropdown>().value == 0)
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
        Screen.SetResolution(PlayerPrefs.GetInt("resolutionW"), PlayerPrefs.GetInt("resolutionH"), fullScreenToggle.GetComponent<Toggle>().isOn);

        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}
