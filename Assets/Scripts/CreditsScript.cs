using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScript : MonoBehaviour
{
    private AudioSource musicSource;
    public AudioClip creditsMusic;

    void Start()
    {
        musicSource = GameObject.Find("MusicSource").GetComponent<AudioSource>();
    }

    public void CreditMusic()
    {
        musicSource.clip = creditsMusic;
        musicSource.Play();
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
