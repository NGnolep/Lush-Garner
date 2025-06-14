using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameManager : MonoBehaviour
{
    public void ReturnToMainGame()
    {
        SceneManager.UnloadSceneAsync("Watering");

        Time.timeScale = 1f;
    }
}