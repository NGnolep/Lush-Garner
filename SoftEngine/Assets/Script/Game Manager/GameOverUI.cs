using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "GameScene";

    // Called by "Play Again" button
    public void PlayAgain()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    // Called by "Quit" button
    public void QuitGame()
    {
        Application.Quit();
    }
}
