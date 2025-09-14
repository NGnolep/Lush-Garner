using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringSFX : MonoBehaviour
{
    public static WateringSFX Instance;

    [SerializeField] private AudioClip waterClip; // Assign in Inspector
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

    public void PlayWaterSound()
    {
        if (waterClip != null)
        {
            audioSource.PlayOneShot(waterClip);
        }
    }
}
