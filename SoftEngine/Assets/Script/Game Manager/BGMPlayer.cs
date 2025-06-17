using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip introClip;   // First track
    [SerializeField] private AudioClip loopClip;    // Track to play after intro
    public static BGMPlayer Instance;
    [SerializeField] private float fadeDuration = 0.5f;

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
    private void Start()
    {
        if (introClip != null && loopClip != null && audioSource != null)
        {
            audioSource.clip = introClip;
            audioSource.loop = false;
            audioSource.volume = 0.05f;
            audioSource.Play();
            Invoke(nameof(PlayLoopClip), introClip.length);
        }
        else if (loopClip != null && audioSource != null)
        {
            // Fallback: only play loopClip if intro is missing
            audioSource.clip = loopClip;
            audioSource.loop = true;
            audioSource.volume = 0.05f;
            audioSource.Play();
        }
    }

    private void PlayLoopClip()
    {
        audioSource.clip = loopClip;
        audioSource.loop = true;
        audioSource.volume = 0.05f;
        audioSource.Play();
    }

    public void FadeOutBGM()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutCoroutine());
    }

    public void FadeInBGM()
    {
        StopAllCoroutines();
        StartCoroutine(FadeInCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0f)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.Pause();
        audioSource.volume = startVolume; // Reset volume for next time
    }

    private IEnumerator FadeInCoroutine()
    {
        audioSource.UnPause();
        float targetVolume = 0.05f;
        audioSource.volume = 0f;

        while (audioSource.volume < targetVolume)
        {
            audioSource.volume += targetVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.volume = targetVolume;
    }
}