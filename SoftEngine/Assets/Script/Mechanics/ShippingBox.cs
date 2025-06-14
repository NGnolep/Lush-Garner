using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.ComponentModel;
public class ShippingBox : MonoBehaviour
{
    public GameObject interactPrompt;
    public GameObject questPanel;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI questDescription;

    private bool isInTrigger = false;

    public List<Quest> availableQuests;
    private Quest currentQuest;
    // Update is called once per frame

    public PlayerGold playerGold;
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

        if (currentQuest != null && currentQuest.isActive)
        {
            currentQuest.timeRemaining -= Time.unscaledDeltaTime;
            if (currentQuest.timeRemaining <= 0f)
            {
                currentQuest.timeRemaining = 0f;
                timerText.text = "00:00";
                currentQuest.isActive = false;
                timerText.gameObject.SetActive(false);
                Debug.Log("Quest failed!");
            }
            else
            {
                UpdateTimerText(currentQuest.timeRemaining);
            }
        }
    }

    void OpenPanel()
    {
        questPanel.SetActive(true);
        Time.timeScale = 0f;

        if (currentQuest == null)
        {
            currentQuest = GetRandomQuest();
        }

        if (questDescription != null && currentQuest != null)
        {
            questDescription.text = currentQuest.description;
        }
    }

    public void ClosePanel()
    {
        questPanel.SetActive(false);
        Time.timeScale = 1f;

        if (currentQuest != null && !currentQuest.isActive)
        {
            currentQuest.timeRemaining = currentQuest.timeLimit;
            currentQuest.isActive = true;
            timerText.gameObject.SetActive(true);
        }
    }

    void UpdateTimerText(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
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

    public void CompleteQuest()
    {
        if (currentQuest != null && currentQuest.isActive)
        {
            currentQuest.isActive = false;
            currentQuest.isCompleted = true;
            timerText.gameObject.SetActive(false);

            playerGold.AddGold(currentQuest.rewardGold);

            Debug.Log($"Quest completed! Earned {currentQuest.rewardGold} gold.");

            currentQuest = null;
        }
    }
    private Quest GetRandomQuest()
    {
        if (availableQuests.Count == 0) return null;
        int randomIndex = Random.Range(0, availableQuests.Count);
        return Instantiate(availableQuests[randomIndex]); // Instantiate to avoid modifying original
    }
}
