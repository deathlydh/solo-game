using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlaylist : MonoBehaviour
{
    public AudioClip[] clips;
    private AudioSource audioSource;
    private int clipOrder = 0; // for ordered playlist
    private string currentScene;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        DontDestroyOnLoad(gameObject);

        currentScene = SceneManager.GetActiveScene().name;

        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the scene loaded event
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string newScene = scene.name;
        if (newScene != currentScene)
        {
            currentScene = newScene;
            ChangeClip();
            audioSource.Play();
        }
    }

    void ChangeClip()
    {
        audioSource.clip = clips[clipOrder];
        clipOrder = (clipOrder + 1) % clips.Length; // Use modulo to loop through the clips
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            ChangeClip(); // Change the clip when it's not playing
            audioSource.Play();
        }
    }
}
