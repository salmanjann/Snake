using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic Instance; // Singleton reference
    public static AudioSource audioSource; // Static reference for easy access
    public AudioSource audioSourceInstance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Set the static instance
            audioSource = audioSourceInstance; // Assign the AudioSource reference
            DontDestroyOnLoad(gameObject); // Persist this GameObject across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy any duplicate instances
            return;
        }
    }
}
