using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Text nameText;
    private Text dialogueText;
    private Animator animator;
    private GameObject next;
    private GameObject player;

    private Queue<string> sentences;

    void Start()
    {
        player = GameObject.Find("Player");
        sentences = new Queue<string>();
        nameText = GameObject.Find("DialogueName").GetComponent<Text>();
        dialogueText = GameObject.Find("DialogueText").GetComponent<Text>();
        animator = GameObject.Find("DialogueBox").GetComponent<Animator>();
        next = GameObject.Find("DialogueNext");
        next.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        next.GetComponent<Button>().interactable = false;
        nameText.enabled = false;
        dialogueText.enabled = false;
    }

    public void StartDialogue (Dialogue dialogue, int gravity1, int gravity2)
    {
        if (player.GetComponent<PlayerMovement>().changingGravity) player.GetComponent<PlayerMovement>().changingGravity = false;
        if (player.GetComponent<PlayerMovement>().healing) player.GetComponent<PlayerMovement>().healing = false;
        if ((player.GetComponent<PlayerMovement>().gravity != gravity1) && (player.GetComponent<PlayerMovement>().gravity != gravity2))
        {
            if(gravity1 == 0)
            {
                player.GetComponent<PlayerMovement>().changeGravity(true, 0.1f, 0.0f, 0.0f, 0.0f);
            }
            else if (gravity1 == 1)
            {
                player.GetComponent<PlayerMovement>().changeGravity(true, 0.0f, 0.1f, 0.0f, 0.0f);
            }
            else if (gravity1 == 2)
            {
                player.GetComponent<PlayerMovement>().changeGravity(true, 0.0f, 0.0f, 0.1f, 0.0f);
            }
            else if (gravity1 == 3)
            {
                player.GetComponent<PlayerMovement>().changeGravity(true, 0.0f, 0.0f, 0.0f, 0.1f);
            }
        }

        player.GetComponent<PlayerMovement>().talking = true;

        animator.SetBool("Open", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForFixedUpdate();
        }
    }

    void EndDialogue()
    {
        player.GetComponent<PlayerMovement>().talking = false;
        animator.SetBool("Open", false);
    }
       

}
