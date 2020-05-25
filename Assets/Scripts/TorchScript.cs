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


    void Start()
    {
        player = GameObject.Find("Player");
        flame = gameObject.transform.GetChild(1).gameObject;
        torchLight = gameObject.transform.GetChild(2).gameObject.GetComponent<Light2D>();
        playerNear = false;
        manaBar = GameObject.Find("Manabar");
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == player.GetComponent<Collider2D>())
        {
            playerNear = true;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision == player.GetComponent<Collider2D>())
        {
            player.GetComponent<PlayerMovement>().canAbsorb = true;
        }
    }

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
        if (player.GetComponent<PlayerMovement>().isAbsorbing && playerNear)
        {
            flame.transform.localScale -= new Vector3(0.01f, 0.01f, 0.0f);
            torchLight.pointLightOuterRadius -= 0.045f;
            manaBar.GetComponent<ManaController>().mana += 0.2f;
            Vector2 dir = new Vector3(player.transform.position.x, player.transform.position.y + 0.3f) - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            var rot = Quaternion.AngleAxis(angle, Vector3.forward);
            energy = Instantiate(energyPrefab, new Vector2(transform.position.x, transform.position.y-0.3007f), rot);
            dir.Normalize();
            energy.GetComponent<Rigidbody2D>().velocity = dir * 5.0f;
        }
        if (flame.transform.localScale.x <= 0.0f)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            flame.SetActive(false);
            torchLight.enabled = false;
        }
    }
}
