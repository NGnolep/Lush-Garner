using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShippingBox : MonoBehaviour
{
    public GameObject interactPrompt;
    public GameObject questPanel;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI questDescription;

    private bool isInTrigger = false;
    private bool timerRunning = false;
    private float timer = 0f;
    private float questTime = 60f;
    // Update is called once per frame
    void Update()
    {
        if (isInTrigger)
        {
            interactPrompt.SetActive(true);

            if(Input.GetKeyDown(KeyCode.F) && !questPanel.activeSelf)
            {
                OpenPanel();
            }
        }
        else
        {
            interactPrompt.SetActive(false);
        }

        if (timerRunning)
        {
            timer -= Time.unscaledDeltaTime;
            if (timer < 0) timer = 0;
            UpdateTimerText();
        }

    }

    void OpenPanel()
    {
        questPanel.SetActive(true);
        Time.timeScale = 0f;

        if(questDescription != null)
        {
            questDescription.text = "Gather 3 apples in 60 second!";
        }
    }

    public void ClosePanel()
    {
        questPanel.SetActive(false);
        Time.timeScale = 1f;

        timer = questTime;
        timerRunning = true;
        timerText.gameObject.SetActive(true);
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInTrigger = false;
        }
    }
}
