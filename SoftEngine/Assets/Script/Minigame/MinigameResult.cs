using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MinigameResult : MonoBehaviour
{

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;
    private float displayDuration = 2f;
    private float timer = 0f;
    private bool showing = false;

    void Start()
    {
        resultPanel.SetActive(false);
    }
    void Update()
    {
        if (showing)
        {
            timer += Time.deltaTime;
            if (timer >= displayDuration)
            {
                resultPanel.SetActive(false);
                showing = false;
            }
        }
        Debug.Log(MinigameInfo.minigameSuccess);
    }
    public void ShowResult(bool success)
    {
        resultPanel.SetActive(true);
        resultText.text = success ? "Success!" : "You ran out of time!";
        timer = 0f;
        showing = true;
    }
}
