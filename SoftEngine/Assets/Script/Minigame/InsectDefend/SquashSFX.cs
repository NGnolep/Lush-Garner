using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquashSFX : MonoBehaviour
{
    public static SquashSFX Instance;

    [SerializeField] private AudioClip squashClip; // Assign in Inspector
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
    }

    public void PlaySquashSound()
    {
        if (squashClip != null)
        {
            audioSource.PlayOneShot(squashClip);
        }
    }
}
