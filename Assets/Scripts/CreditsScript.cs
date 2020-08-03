using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScript : MonoBehaviour
{
    //The music source and the clip
    private AudioSource musicSource;
    public AudioClip creditsMusic;

    void Start()
    {
        //We find the music source
        musicSource = GameObject.Find("MusicSource").GetComponent<AudioSource>();
    }

    //function to start the credits music
    public void CreditMusic()
    {
        musicSource.clip = creditsMusic;
        musicSource.Play();
    }

    //function to open the main menu
    public void OpenMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
