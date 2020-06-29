﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    //the dialogue
    public Dialogue dialogue;
    //the player
    private GameObject player;
    //The gravities the player can have during the dialogue
    public int gravity1;
    public int gravity2;
    //The number of the dialogue
    public int dialogueNumber;
    //The boss health bar
    private GameObject bossBar;
    //The animator off the boss fight arena
    private Animator bossArena;

    void Start()
    {
        player = GameObject.Find("Player");
        bossBar = GameObject.Find("BossHealthBar");
        if(player.GetComponent<PlayerMovement>().scene == 3) bossArena = GameObject.Find("Boss platform").GetComponent<Animator>();
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("expTutorial") == 1 && gravity1 == 4)
        {
            PlayerPrefs.SetInt("expTutorial", 2);
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue, 0, 0);
        }
        if (PlayerPrefs.GetInt("trap") == 1 && gravity1 == 5)
        {
            PlayerPrefs.SetInt("trap", 2);
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue, 0, 0);
            PlayerPrefs.SetInt("lastDialogue", dialogueNumber);
        }
        if (PlayerPrefs.GetInt("dieTutorial") == 1 && gravity1 == 6)
        {
            PlayerPrefs.SetInt("dieTutorial", 2);
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue, 0, 0);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == player.GetComponent<Collider2D>())
        {
            if (PlayerPrefs.GetInt("lastDialogue") < dialogueNumber || dialogueNumber == 15)
            {
                if(dialogueNumber == 15)
                {
                    bossArena.SetBool("Close",true);
                    player.GetComponent<PlayerMovement>().approach = false;
                    bossBar.GetComponent<BossHealthController>().fighting = true;
                }
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue, gravity1, gravity2);
                PlayerPrefs.SetInt("lastDialogue", dialogueNumber);
            }
            
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
