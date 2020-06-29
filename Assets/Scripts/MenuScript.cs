using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    private GameObject LoadButton;
    // Start is called before the first frame update
    void Start()
    {
        LoadButton = GameObject.Find("Load");
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
        //A float to save the current health
        if (!PlayerPrefs.HasKey("health")) PlayerPrefs.SetFloat("health", Mathf.Sqrt(2000 * PlayerPrefs.GetInt("healthLevel")) + 55);
        if (PlayerPrefs.GetInt("lastDialogue") == 0) LoadButton.GetComponent<Button>().interactable = false;
        else LoadButton.GetComponent<Button>().interactable = true;
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
        Application.Quit();
        Debug.Log("Closing game");
    }
}
