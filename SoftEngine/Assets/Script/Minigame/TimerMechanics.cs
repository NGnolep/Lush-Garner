using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TimerMechanics : MonoBehaviour
{
    public float timeLimit = 7f;
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
            string sceneName = "";
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                string name = scene.name;

                if (name == "Watering" || name == "InsectDefend" || name == "Harvesting")
                {
                    sceneName = name;
                    break;
                }
            }

            timeRemaining = 0f;
            isRunning = false;
            MinigameInfo.minigameSuccess = false;
            MinigameOutcome(sceneName);
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


    public void MinigameOutcome(string minigame)
    {
        isRunning = false;
        GameObject player = GameObject.FindWithTag("Player");

        if (minigame == "Watering")
        {
            foreach (var droplet in GameObject.FindGameObjectsWithTag("Droplet"))
            {
                Destroy(droplet);
            }

            if (MinigameInfo.minigameSuccess)
            {
                PlantingMechanics planting = player.GetComponent<PlantingMechanics>();
                planting.ReplacePlantedTiles();
            }
        }
        else if (minigame == "InsectDefend")
        {
            if (MinigameInfo.minigameSuccess)
            {
                PlantingMechanics planting = player.GetComponent<PlantingMechanics>();
                planting.ReplaceGrownTiles();
            }
        }
        else if (minigame == "Harvesting")
        {
            if (MinigameInfo.minigameSuccess)
            {
                PlantingMechanics planting = player.GetComponent<PlantingMechanics>();
                planting.ResetAllHarvestedTiles();
            }
        }



        BGMPlayer.Instance.FadeInBGM();
        SceneManager.UnloadSceneAsync(minigame);
        Time.timeScale = 1f;

        MinigameResult result = FindObjectOfType<MinigameResult>();
        result.ShowResult(MinigameInfo.minigameSuccess);
    }
}
