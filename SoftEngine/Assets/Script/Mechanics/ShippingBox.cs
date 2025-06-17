using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.ComponentModel;
public class ShippingBox : MonoBehaviour
{
    public Inventory playerInventory;
    public GameObject acceptButton;
    public GameObject submitButton;

    public GameObject interactPrompt;
    public GameObject questPanel;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI questDescription;

    private bool isInTrigger = false;

    public List<Quest> availableQuests;
    private Quest currentQuest;
    // Update is called once per frame

    public PlayerGold playerGold;

    public TextMeshProUGUI feedbackText;
    public float feedbackDuration = 2f;

    public HotbarUI hotbarUI;
    public Hotbar hotbar;

    public GameObject pausePanel;
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

        if (pausePanel != null && pausePanel.activeSelf)
            return;

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
                FindObjectOfType<GameOverHandler>().TriggerGameOver();
            }
            else
            {
                UpdateTimerText(currentQuest.timeRemaining);
            }
        }
    }

    void OpenPanel()
    {
        SFXManager.Instance.PlayButton();
        questPanel.SetActive(true);
        Time.timeScale = 0f;

        if (currentQuest == null)
        {
            currentQuest = GetRandomQuest();
        }

        if (questDescription != null && currentQuest != null)
        {
            string fullDesc = currentQuest.description;
            foreach (var req in currentQuest.requirements)
            {
                fullDesc += $"\n- {req.item.itemName} x{req.amount}";
            }
            questDescription.text = fullDesc;
        }

        acceptButton.SetActive(!currentQuest.isActive);
        submitButton.SetActive(currentQuest.isActive);
    }

    public void ClosePanel()
    {
        SFXManager.Instance.PlayButton();
        questPanel.SetActive(false);
        Time.timeScale = 1f;
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
    private Quest GetRandomQuest()
    {
        if (availableQuests.Count == 0) return null;
        int randomIndex = Random.Range(0, availableQuests.Count);
        return Instantiate(availableQuests[randomIndex]); // Instantiate to avoid modifying original
    }

    public void AcceptQuest()
    {
        if (currentQuest != null && !currentQuest.isActive)
        {
            SFXManager.Instance.PlayButton();
            currentQuest.timeRemaining = currentQuest.timeLimit;
            currentQuest.isActive = true;
            timerText.gameObject.SetActive(true);
            UpdateTimerText(currentQuest.timeRemaining);

            acceptButton.SetActive(false);
            submitButton.SetActive(true);

            questPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void SubmitQuest()
    {
        if (currentQuest != null && currentQuest.isActive)
        {
            bool hasAllItems = true;

            foreach (QuestRequirement req in currentQuest.requirements)
            {
                bool found = false;

                foreach (var slot in playerInventory.slots)
                {
                    if (slot.item == req.item && slot.quantity >= req.amount)
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    hasAllItems = false;
                    break;
                }
            }

            if (hasAllItems)
            {
                // Remove required items
                foreach (QuestRequirement req in currentQuest.requirements)
                {
                    for (int i = 0; i < playerInventory.slots.Count; i++)
                    {
                        if (playerInventory.slots[i].item == req.item)
                        {
                            playerInventory.slots[i].quantity -= req.amount;

                            if (playerInventory.slots[i].quantity <= 0)
                            {
                                playerInventory.slots[i].quantity = 0;
                            }

                            break;
                        }
                    }
                }
                if (hotbar != null)
                    hotbar.SyncWithInventory(playerInventory);
                SFXManager.Instance.PlayButton();
                playerGold.AddGold(currentQuest.rewardGold);
                currentQuest.isActive = false;
                currentQuest.isCompleted = true;
                timerText.gameObject.SetActive(false);
                ShowFeedback("Quest completed!");
                currentQuest = null;
                ClosePanel();
            }
            else
            {
                SFXManager.Instance.PlayButton();
                SFXManager.Instance.PlayIncorrect();
                ShowFeedback("Not enough items!");
            }
        }
    }
    private int GetTotalItemCount(Item targetItem)
    {
        int total = 0;
        foreach (var slot in playerInventory.slots)
        {
            if (slot.item == targetItem)
            {
                total += slot.quantity;
            }
        }
        return total;
    }

    private void ShowFeedback(string message)
    {
        if (feedbackText == null) return;
        feedbackText.text = message;
        CancelInvoke(nameof(HideFeedback));
        Invoke(nameof(HideFeedback), feedbackDuration);
    }

    private void HideFeedback()
    {
        if (feedbackText == null) return;
        feedbackText.text = "";
    }
}
