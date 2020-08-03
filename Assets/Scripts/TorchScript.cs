using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;

public class TorchScript : MonoBehaviour
{
    //The player
    private GameObject player;
    //A boolean to see if the player is near this torch
    private bool playerNear;
    //The flame of the torch
    private GameObject flame;
    //The light of the torch
    private Light2D torchLight;
    //The mana bar
    private GameObject manaBar;
    //The prefab of the energy
    public GameObject energyPrefab;
    //The variable we are going to use to store the energy
    private GameObject energy;
    //A bool to see if the torch is blue
    public bool blueTorch;
    //A bool to see if it's the boss light
    public bool bossLight;
    //The time the last absorbtion was done
    private float absorbTime;
    //A bool to see if the player is absorbing
    private bool absorbing;

    void Start()
    {
        //Find all the gameobjects
        player = GameObject.Find("Player");
        if (!bossLight) flame = gameObject.transform.GetChild(1).gameObject;
        torchLight = gameObject.transform.GetChild(0).gameObject.GetComponent<Light2D>();
        manaBar = GameObject.Find("Manabar");
        //Initialize the playerNear boolean and the absorb time float
        playerNear = false;
        absorbTime = Time.fixedTime;
    }

    //If a player enters the trigger the player near boolean will be set to true
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == player.GetComponent<Collider2D>())
        {
            playerNear = true;
        }
    }

    //If the player stays in the trigger the canAbsorb bool will be set to true
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision == player.GetComponent<Collider2D>())
        {
            player.GetComponent<PlayerMovement>().canAbsorb = true;
        }
    }

    //If the player exits the trigger the playerNear and canAbsorb bools will be set to false
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == player.GetComponent<Collider2D>())
        {
            playerNear = false;
            player.GetComponent<PlayerMovement>().canAbsorb = false;
        }
    }

    void FixedUpdate()
    {
        //If a player absorbs the light, the flames scale and the light will decrease. In the case of the kings light only the light will decrease.
        if (player.GetComponent<PlayerMovement>().isAbsorbing && playerNear)
        {
            if (!absorbing)
            {
                absorbing = true;
                gameObject.GetComponent<AudioSource>().Play();
            }
            if (blueTorch) absorbTime = Time.fixedTime;
            if(!bossLight) flame.transform.localScale -= new Vector3(0.01f, 0.01f, 0.0f);
            torchLight.pointLightOuterRadius -= 0.045f;
            manaBar.GetComponent<ManaController>().mana += 0.2f;
            Vector2 dir = new Vector3(player.transform.position.x, player.transform.position.y + 0.3f) - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            var rot = Quaternion.AngleAxis(angle, Vector3.forward);
            energy = Instantiate(energyPrefab, new Vector2(transform.position.x, transform.position.y-0.3007f), rot);
            dir.Normalize();
            energy.GetComponent<Rigidbody2D>().velocity = dir * 5.0f;
        }
        else
        {
            absorbing = false;
            gameObject.GetComponent<AudioSource>().Stop();
        }
        if (torchLight.pointLightOuterRadius <= 0.0f)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            if (!bossLight) flame.SetActive(false);
            torchLight.enabled = false;
        }
        //The blue torches and the boss light will recharge 5 seconds after the last absorbing
        if ((blueTorch || bossLight) && torchLight.pointLightOuterRadius < 4.5f && !player.GetComponent<PlayerMovement>().isAbsorbing && (Time.fixedTime - absorbTime) > 5.0f)
        {
            torchLight.pointLightOuterRadius += 0.006f;
            if (!gameObject.GetComponent<BoxCollider2D>().enabled)
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
                torchLight.enabled = true;
                if (!bossLight) flame.SetActive(true);
            }   
            if(!bossLight) flame.transform.localScale += new Vector3(0.001333f, 0.001333f, 0.001333f);
        }
        else if(blueTorch && torchLight.pointLightOuterRadius == 4.5f && !bossLight) flame.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        //The red torches will recharge when the player sleeps
        if (player.GetComponent<PlayerMovement>().sleeping && !(blueTorch || bossLight) && torchLight.pointLightOuterRadius < 4.5f)
        {
            torchLight.pointLightOuterRadius = 4.5f;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            torchLight.enabled = true;
            flame.SetActive(true);
            flame.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }
}
