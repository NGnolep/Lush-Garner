using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestSFX : MonoBehaviour
{
    public static HarvestSFX Instance;

    [SerializeField] private AudioClip harvestClip; // Assign in Inspector
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

    public void PlayHarvestSound()
    {
        if (harvestClip != null)
        {
            audioSource.PlayOneShot(harvestClip);
        }
    }
}
