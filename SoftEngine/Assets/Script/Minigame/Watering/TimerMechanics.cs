using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TimerMechanics : MonoBehaviour
{
    public float timeLimit = 5f;
    public TextMeshProUGUI timerText;
    private float timeRemaining;
    private bool isRunning = true;

    void Start()
    {
        timeRemaining = timeLimit;
    }

    void Update()
    {
        HandleTimer();
    }

    void HandleTimer()
    {
        if (!isRunning) return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            isRunning = false;
            FailedMinigame();
        }

        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int seconds = Mathf.CeilToInt(timeRemaining);
            timerText.text = $"Time Left: {seconds}s";
        }
    }


    public void SuccessMinigame()
    {
        isRunning = false;
        MinigameInfo.minigameSuccess = true;
        GameObject player = GameObject.FindWithTag("Player");

        player.GetComponent<PlayerMovement>().enabled = true;

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        rb.isKinematic = false;

        PlantingMechanics planting = player.GetComponent<PlantingMechanics>();
        planting.ReplacePlantedTiles();

        foreach (var droplet in GameObject.FindGameObjectsWithTag("Droplet"))
        {
            Destroy(droplet);
        }

        SceneManager.UnloadSceneAsync("Watering");
        Time.timeScale = 1f;

        MinigameResult result = FindObjectOfType<MinigameResult>();
        result.ShowResult(MinigameInfo.minigameSuccess);
    }
    public void FailedMinigame()
    {
        isRunning = false;
        MinigameInfo.minigameSuccess = false;

        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerMovement>().enabled = true;

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        rb.isKinematic = false;

        foreach (var droplet in GameObject.FindGameObjectsWithTag("Droplet"))
        {
            Destroy(droplet);
        }

        SceneManager.UnloadSceneAsync("Watering");
        Time.timeScale = 1f;

        MinigameResult result = FindObjectOfType<MinigameResult>();
        result.ShowResult(MinigameInfo.minigameSuccess);
    }
}
