using System.Collections;
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


    void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if(PlayerPrefs.GetInt("expTutorial") == 1 && gravity1 == 4)
        {
            PlayerPrefs.SetInt("expTutorial", 2);
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue,0,0);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == player.GetComponent<Collider2D>())
        {
            if (PlayerPrefs.GetInt("lastDialogue") < dialogueNumber)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue, gravity1, gravity2);
                PlayerPrefs.SetInt("lastDialogue", dialogueNumber);
            }
            
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
