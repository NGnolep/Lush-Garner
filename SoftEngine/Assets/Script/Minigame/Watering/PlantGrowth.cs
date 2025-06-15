using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlantGrowth : MonoBehaviour
{
    public float growAmount = 0.1f;
    public float maxY = 0.1f;
    private bool isFullyGrown = false;

    public void Water()
    {
        if (isFullyGrown) return;

        Vector3 newPosition = transform.position + Vector3.up * growAmount;

        newPosition.y = Mathf.Min(newPosition.y, maxY);
        transform.position = newPosition;

        if (transform.position.y >= maxY)
        {
            isFullyGrown = true;
            FinishMinigame();
        }
    }

    void FinishMinigame()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            PlantingMechanics planting = player.GetComponent<PlantingMechanics>();
            if (planting != null)
            {
                planting.ReplacePlantedTiles();
            }
        }

        foreach (var droplet in GameObject.FindGameObjectsWithTag("Droplet"))
        {
            Destroy(droplet);
        }

        SceneManager.UnloadSceneAsync("Watering");
        Time.timeScale = 1f;
    }
}
