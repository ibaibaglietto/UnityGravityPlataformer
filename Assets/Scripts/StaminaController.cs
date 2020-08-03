using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaController : MonoBehaviour
{
    //The player
    private GameObject player;
    //The stamina bar
    private GameObject staminaBar;
    //The actual stamina the player has
    public float stamina;
    //The maximum stamina the player can have
    public float maxStamina;
    //The last time stamina was spent
    private float staminaTime;


    void Start()
    {
        //we initialize the stamina depending on the players level
        maxStamina = (Mathf.Sqrt(2000 * PlayerPrefs.GetInt("staminaLevel")) / 2 + 2.639f);
        stamina = maxStamina;
        //We find the player and the stamina bar
        player = GameObject.Find("Player");
        staminaBar = GameObject.Find("Playerstamina");
        //We initialize the stamina time
        staminaTime = Time.fixedTime;
    }

    //We update the stamina bar every frame
    void Update()
    {
        staminaBar.GetComponent<Image>().fillAmount = stamina / maxStamina;
    }

    void FixedUpdate()
    {
        //If the player has stamina it can be spent
        if (stamina > player.GetComponent<PlayerMovement>().staminaSpent && player.GetComponent<PlayerMovement>().staminaSpent !=0)
        {
            stamina -= player.GetComponent<PlayerMovement>().staminaSpent;
            staminaTime = Time.fixedTime;
            player.GetComponent<PlayerMovement>().staminaSpent = 0;
        }
        //If the player spents all the stamina they will need to wait one more second to start the refilling
        else if (player.GetComponent<PlayerMovement>().staminaSpent != 0)
        {
            stamina = 0;
            staminaTime = Time.fixedTime + 1.0f;
            player.GetComponent<PlayerMovement>().hasStamina = false;
            player.GetComponent<PlayerMovement>().staminaSpent = 0;
        }
        //If one second has passed from the last stamina spent it will start refilling
        if ((Time.fixedTime - staminaTime) > 1.0f && stamina < maxStamina)
        {
            stamina += 0.3f;
            player.GetComponent<PlayerMovement>().hasStamina = true;
        }
    }

}
