using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MinigameResult : MonoBehaviour
{
    public TextMeshProUGUI resultText;
    private float displayDuration = 0.5f;
    private float timer = 0f;
    private bool showing = false;

    void Start()
    {
        resultText.gameObject.SetActive(false);
    }
    void Update()
    {
        if (showing)
        {
            timer += Time.deltaTime;
            if (timer >= displayDuration)
            {
                resultText.gameObject.SetActive(false);
                showing = false;
                PlantingMechanics plantingMechanics = gameObject.GetComponent<PlantingMechanics>();
                plantingMechanics.longShown = false;
                plantingMechanics.shortShown = false;
            }
        }
    }
    public void ShowResult(bool success)
    {
        PlantingMechanics plantingMechanics = gameObject.GetComponent<PlantingMechanics>();
        plantingMechanics.longShown = true;
        plantingMechanics.shortShown = true;

        resultText.gameObject.SetActive(true);
        resultText.text = success ? "Success" : "You ran out of time";
        timer = 0f;
        showing = true;
    }
}
