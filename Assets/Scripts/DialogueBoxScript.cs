using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBoxScript : MonoBehaviour
{
    //The next button and the text areas
    private GameObject next;
    private Text nameText;
    private Text dialogueText;

    //We find the next button and the text areas
    void Start()
    {
        nameText = GameObject.Find("DialogueName").GetComponent<Text>();
        dialogueText = GameObject.Find("DialogueText").GetComponent<Text>();
        next = GameObject.Find("DialogueNext");
    }

    //Function to make the dialogue box appear
    public void appear()
    {
        nameText.enabled = true;
        dialogueText.enabled = true;
        next.GetComponent<Button>().interactable = true;
        next.transform.GetChild(0).gameObject.SetActive(true);
    }

    //Function to make the dialogue box disappear
    public void disappear()
    {
        nameText.enabled = false;
        dialogueText.enabled = false;
        next.GetComponent<Button>().interactable = false;
        next.transform.GetChild(0).gameObject.SetActive(false);
    }
}
