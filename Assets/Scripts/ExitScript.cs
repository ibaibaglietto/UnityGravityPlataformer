using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitScript : MonoBehaviour
{
    public float spawnx;
    public float spawny;
    public int scene;
    public int spawnface; //0 left, 1 right
    public int exitSide; //0 left, 1 right
    private GameObject manaBar;
    private GameObject player;
    private GameObject healthBar;
    //The gameobject of the black image to fade in and out
    private GameObject fadeInOut;
    //A boolean to see if the player entered this exit
    private bool thisExit;

    private void Start()
    {
        manaBar = GameObject.Find("Manabar");
        healthBar = GameObject.Find("Healthbar");
        player = GameObject.Find("Player");
        //Save the gameobject of the fadeInOut
        fadeInOut = GameObject.Find("FadeInOut");
        thisExit = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!player.GetComponent<PlayerMovement>().enteringScene && collision == player.GetComponent<Collider2D>())
        {
            if (player.GetComponent<PlayerMovement>().changingGravity) player.GetComponent<PlayerMovement>().changingGravity = false;
            if (player.GetComponent<PlayerMovement>().healing) player.GetComponent<PlayerMovement>().healing = false;
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            player.GetComponent<PlayerMovement>().changeGravity(true, 0.5f, 0.0f, 0.0f, 0.0f);
            if (player.GetComponent<PlayerMovement>().m_FacingRight && exitSide == 0) player.GetComponent<PlayerMovement>().Flip();
            else if (!player.GetComponent<PlayerMovement>().m_FacingRight && exitSide == 1) player.GetComponent<PlayerMovement>().Flip();
            player.GetComponent<PlayerMovement>().changingScene = true;
            thisExit = true;
            PlayerPrefs.SetInt("hasDied", 0);
            PlayerPrefs.SetFloat("spawnx", spawnx);
            PlayerPrefs.SetFloat("spawny", spawny);
            PlayerPrefs.SetInt("spawnface", spawnface);
            PlayerPrefs.SetFloat("mana", manaBar.GetComponent<ManaController>().mana);
            PlayerPrefs.SetFloat("health", healthBar.GetComponent<PlayerLifeController>().health);
            PlayerPrefs.SetInt("spawnscene", scene);
        }        
    }

    private void Update()
    {
        if (player.GetComponent<PlayerMovement>().changingScene && fadeInOut.GetComponent<Image>().color.a == 1 && thisExit)
        {
            SceneManager.LoadScene(scene);
        }
    }
}
