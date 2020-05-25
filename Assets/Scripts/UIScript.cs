using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    //4 gameobjects to represent the gravity to each side
    private GameObject g0;
    private GameObject g1;
    private GameObject g2;
    private GameObject g3;
    //The gameobject of the player
    private GameObject player;
    //The gameobject of the lvl up interface
    private GameObject lvlUp;

    private void Start()
    {
        //Find all the gameobjects and save them
        g0 = GameObject.Find("g0");
        g1 = GameObject.Find("g1");
        g2 = GameObject.Find("g2");
        g3 = GameObject.Find("g3");
        player = GameObject.Find("Player");
        lvlUp = GameObject.Find("Upgrade player");
    }


    void Update()
    {
        lvlUp.SetActive(player.GetComponent<PlayerMovement>().sleeping);
        //Check if we are changing gravity and if so make the gameobjects active
        g0.SetActive(player.GetComponent<PlayerMovement>().changingGravity && !player.GetComponent<PlayerMovement>().rotating);
        g1.SetActive(player.GetComponent<PlayerMovement>().changingGravity && !player.GetComponent<PlayerMovement>().rotating);
        g2.SetActive(player.GetComponent<PlayerMovement>().changingGravity && !player.GetComponent<PlayerMovement>().rotating);
        g3.SetActive(player.GetComponent<PlayerMovement>().changingGravity && !player.GetComponent<PlayerMovement>().rotating);
        //Write the gravity applied to each side
        g0.GetComponent<Text>().text = player.GetComponent<PlayerMovement>().prevGravityDown.ToString("F1");
        g1.GetComponent<Text>().text = player.GetComponent<PlayerMovement>().prevGravityUp.ToString("F1");
        g2.GetComponent<Text>().text = player.GetComponent<PlayerMovement>().prevGravityLeft.ToString("F1");
        g3.GetComponent<Text>().text = player.GetComponent<PlayerMovement>().prevGravityRight.ToString("F1");
    }
}
