using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    [SerializeField] private AudioSource audioSource;

    [Header("Sound Effects")]
    public AudioClip purchaseSFX;
    public AudioClip incorrectSFX;
    public AudioClip walkingSFX;
    public AudioClip walking2SFX;
    public AudioClip plantingSFX;
    public AudioClip buttonSFX;
    private bool useFirstWalkingSFX = true;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    // Convenient named functions
    public void PlayPurchase() => PlaySFX(purchaseSFX);
    public void PlayIncorrect() => PlaySFX(incorrectSFX);
    public void PlayWalking()
    {
        if (audioSource == null) return;

        AudioClip clipToPlay = useFirstWalkingSFX ? walkingSFX : walking2SFX;
        useFirstWalkingSFX = !useFirstWalkingSFX;

        PlaySFX(clipToPlay);
    }
    public void PlayPlanting() => PlaySFX(plantingSFX);
    public void PlayButton() => PlaySFX(buttonSFX);
}
