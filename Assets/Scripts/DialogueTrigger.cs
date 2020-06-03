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


    void Start()
    {
        player = GameObject.Find("Player");
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == player.GetComponent<Collider2D>())
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue,gravity1,gravity2);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
