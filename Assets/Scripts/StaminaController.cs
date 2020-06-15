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


    // Start is called before the first frame update
    void Start()
    {
        maxStamina = (Mathf.Sqrt(2000 * PlayerPrefs.GetInt("staminaLevel")) / 2 + 2.639f);
        player = GameObject.Find("Player");
        staminaBar = GameObject.Find("Playerstamina");
        stamina = maxStamina;
        staminaTime = Time.fixedTime;
    }

    // Update is called once per frame
    void Update()
    {
        staminaBar.GetComponent<Image>().fillAmount = stamina / maxStamina;
    }

    void FixedUpdate()
    {
        if (stamina > player.GetComponent<PlayerMovement>().staminaSpent && player.GetComponent<PlayerMovement>().staminaSpent !=0)
        {
            stamina -= player.GetComponent<PlayerMovement>().staminaSpent;
            staminaTime = Time.fixedTime;
            player.GetComponent<PlayerMovement>().staminaSpent = 0;
        }
        else if (player.GetComponent<PlayerMovement>().staminaSpent != 0)
        {
            stamina = 0;
            staminaTime = Time.fixedTime + 1.0f;
            player.GetComponent<PlayerMovement>().hasStamina = false;
            player.GetComponent<PlayerMovement>().staminaSpent = 0;
        }
        if ((Time.fixedTime - staminaTime) > 1.0f && stamina < maxStamina)
        {
            stamina += 0.2f;
            player.GetComponent<PlayerMovement>().hasStamina = true;
        }
    }

}
