using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameManager : MonoBehaviour
{
    public void ReturnToMainGame()
    {
        // Unload the minigame scene
        SceneManager.UnloadSceneAsync("Minigame1");

        // Resume time if it was paused (just in case)
        Time.timeScale = 1f;
    }
}