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

    public void StartDialogue (Dialogue dialogue, int gravity1, int gravity2, int faceRight)
    {
        player.GetComponent<PlayerMovement>().spendingMana = 0.0f;
        Time.timeScale = 1.0f;
        if (player.GetComponent<PlayerMovement>().changingGravity) player.GetComponent<PlayerMovement>().changingGravity = false;
        if (player.GetComponent<PlayerMovement>().healing) player.GetComponent<PlayerMovement>().healing = false;
        if ((player.GetComponent<PlayerMovement>().gravity != gravity1) && (player.GetComponent<PlayerMovement>().gravity != gravity2))
        {
            if(gravity1 == 0)
            {
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                player.GetComponent<PlayerMovement>().changeGravity(true, 0.5f, 0.0f, 0.0f, 0.0f);
            }
            else if (gravity1 == 1)
            {
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                player.GetComponent<PlayerMovement>().changeGravity(true, 0.0f, 0.5f, 0.0f, 0.0f);
            }
            else if (gravity1 == 2)
            {
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                player.GetComponent<PlayerMovement>().changeGravity(true, 0.0f, 0.0f, 0.5f, 0.0f);
            }
            else if (gravity1 == 3)
            {
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                player.GetComponent<PlayerMovement>().changeGravity(true, 0.0f, 0.0f, 0.0f, 0.5f);
            }
        }
        if ((faceRight == 1 && !player.GetComponent<PlayerMovement>().m_FacingRight) || (faceRight == 0 && player.GetComponent<PlayerMovement>().m_FacingRight)) player.GetComponent<PlayerMovement>().Flip();

        player.GetComponent<PlayerMovement>().talking = true;

        animator.SetBool("Open", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        if(PlayerPrefs.GetInt("language") == 0)
        {
            foreach (string sentence in dialogue.sentencesEnglish)
            {
                sentences.Enqueue(sentence);
            }
        }
        else if (PlayerPrefs.GetInt("language") == 1)
        {
            foreach (string sentence in dialogue.sentencesSpanish)
            {
                sentences.Enqueue(sentence);
            }
        }
        else if (PlayerPrefs.GetInt("language") == 2)
        {
            foreach (string sentence in dialogue.sentencesBasque)
            {
                sentences.Enqueue(sentence);
            }
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
        if (player.GetComponent<PlayerMovement>().gravity == 0) player.GetComponent<PlayerMovement>().changeGravity(true, 1.0f, 0.0f, 0.0f, 0.0f);
        else if (player.GetComponent<PlayerMovement>().gravity == 1) player.GetComponent<PlayerMovement>().changeGravity(true, 0.0f, 1.0f, 0.0f, 0.0f);
        else if (player.GetComponent<PlayerMovement>().gravity == 2) player.GetComponent<PlayerMovement>().changeGravity(true, 0.0f, 0.0f, 1.0f, 0.0f);
        else if (player.GetComponent<PlayerMovement>().gravity == 3) player.GetComponent<PlayerMovement>().changeGravity(true, 0.0f, 0.0f, 0.0f, 1.0f);
        player.GetComponent<PlayerMovement>().spendingMana = ((player.GetComponent<PlayerMovement>().gravityUp) + Mathf.Abs(player.GetComponent<PlayerMovement>().gravityDown - 1.0f) + player.GetComponent<PlayerMovement>().gravityLeft + player.GetComponent<PlayerMovement>().gravityRight) / 50.0f;
    }
       

}
