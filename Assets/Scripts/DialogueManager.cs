using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //The text areas
    private Text nameText;
    private Text dialogueText;
    //The animator of the dialogue box
    private Animator animator;
    //The next button
    private GameObject next;
    //The player
    private GameObject player;
    //The speak sound source
    private AudioSource speak;
    //The sentences
    private Queue<string> sentences;

    void Start()
    {
        //We find everything
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
        speak = GameObject.Find("ConversationSource").GetComponent<AudioSource>();
    }

    //Function to start the dialogue
    public void StartDialogue (Dialogue dialogue, int gravity1, int gravity2, int faceRight)
    {
        //We put the player in a normal state, not consuming mana
        player.GetComponent<PlayerMovement>().spendingMana = 0.0f;
        Time.timeScale = 1.0f;
        if (player.GetComponent<PlayerMovement>().changingGravity) player.GetComponent<PlayerMovement>().changingGravity = false;
        if (player.GetComponent<PlayerMovement>().healing) player.GetComponent<PlayerMovement>().healing = false;
        //We check what gravity we need the player to have
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
        //We check where the player is facing
        if ((faceRight == 1 && !player.GetComponent<PlayerMovement>().m_FacingRight) || (faceRight == 0 && player.GetComponent<PlayerMovement>().m_FacingRight)) player.GetComponent<PlayerMovement>().Flip();

        //We put the talking state
        player.GetComponent<PlayerMovement>().talking = true;
        //We open the dialogue box
        animator.SetBool("Open", true);
        //Set the name of the speaker
        nameText.text = dialogue.name;
        //Clear the previous sentences
        sentences.Clear();
        //Check the language and enqueue the sentences
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
        //Display the next sentence
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        //When we dont have more sentences we end the dialogue
        if (sentences.Count == 0)
        {
            EndDialogue();
            StopAllCoroutines();
            return;
        }
        //If we have more sentences we dequeue them and type them
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        //We stop the coroutines to be able to pass a sentence without reading it
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    //Function to write a sentence letter by letter
    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            speak.Play();
            dialogueText.text += letter;
            yield return new WaitForFixedUpdate();
        }
    }

    //Function to end the dialogue, putting the player in her respectiong gravity and starting to spend mana again
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
