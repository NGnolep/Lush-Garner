using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private string gameOverSceneName = "GameOverScene";

    public void TriggerGameOver()
    {
        SceneManager.LoadScene(gameOverSceneName);
    }
}

