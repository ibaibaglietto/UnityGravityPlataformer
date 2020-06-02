using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBoxScript : MonoBehaviour
{

    private GameObject next;
    private Text nameText;
    private Text dialogueText;

    void Start()
    {
        nameText = GameObject.Find("DialogueName").GetComponent<Text>();
        dialogueText = GameObject.Find("DialogueText").GetComponent<Text>();
        next = GameObject.Find("DialogueNext");
    }

    public void appear()
    {
        nameText.enabled = true;
        dialogueText.enabled = true;
        next.SetActive(true);
    }

    public void disappear()
    {
        nameText.enabled = false;
        dialogueText.enabled = false;
        next.SetActive(false);
    }
}
