using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlantGrowth : MonoBehaviour
{
    public float growSpeed = 1f;
    public float maxY = 0.001f;
    private bool isGrowing = false;

    void Update()
    {
        if (isGrowing && transform.position.y < maxY)
        {
            transform.position += Vector3.up * growSpeed * Time.deltaTime;

            if (transform.position.y >= maxY)
            {
                FinishMinigame();
            }
        }
    }

    public void StartGrowing()
    {
        isGrowing = true;
    }

    void FinishMinigame()
    {
        foreach (var droplet in GameObject.FindGameObjectsWithTag("Droplet"))
        {
            Destroy(droplet);
        }

        SceneManager.UnloadSceneAsync("Watering");
        Time.timeScale = 1f;
    }
}
