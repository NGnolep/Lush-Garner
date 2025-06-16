using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlantGrowth : MonoBehaviour
{
    public float growAmount = 0.1f;
    public float maxY = 0.1f;
    private bool isFullyGrown = false;
    public TimerMechanics timerMechanics;

    public void Water()
    {
        if (isFullyGrown) return;

        Vector3 newPosition = transform.position + Vector3.up * growAmount;

        newPosition.y = Mathf.Min(newPosition.y, maxY);
        transform.position = newPosition;

        if (transform.position.y >= maxY)
        {
            isFullyGrown = true;
            timerMechanics.SuccessMinigame();
        }
    }
}
