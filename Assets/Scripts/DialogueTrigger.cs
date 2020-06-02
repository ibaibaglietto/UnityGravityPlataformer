using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    //the dialogue
    public Dialogue dialogue;
    //the player
    private GameObject player;


    void Start()
    {
        player = GameObject.Find("Player");
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == player.GetComponent<Collider2D>())
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
