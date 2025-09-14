using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject tutorialPanel;
    public AudioClip buttonClickSFX;
    public AudioSource audioSource;
    public void Play()
    {
        PlaySFX();
        SceneManager.LoadScene("GameScene");
    }
    
    public void Quit()
    {
        PlaySFX();
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }

    public void OpenTutorial()
    {
        PlaySFX();
        tutorialPanel.SetActive(true);
    }

    public void CloseTutorial()
    {
        PlaySFX();
        tutorialPanel.SetActive(false);
    }

    private void PlaySFX()
    {
        if (audioSource != null && buttonClickSFX != null)
        {
            audioSource.PlayOneShot(buttonClickSFX);
        }
    }
}
