using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ShopLogic : MonoBehaviour
{
    [System.Serializable]
    public class ShopItem
    {
        public Item item;
        public int price;

        [HideInInspector] public int quantityToBuy = 0;

        public Button plusButton;
        public Button minusButton;
        public TextMeshProUGUI quantityText;

        public void UpdateQuantityText()
        {
            quantityText.text = quantityToBuy.ToString();
        }
    }

    public List<ShopItem> shopItems;
    public Button buyButton;

    public PlayerGold playerGold;
    public Inventory playerInventory;
    public TextMeshProUGUI goldText;

    [Header("Feedback")]
    public TextMeshProUGUI feedbackText;
    public float feedbackDuration = 2f;

    public TextMeshProUGUI totalCostText;

    private Dictionary<string, int> itemToSlotIndex = new Dictionary<string, int>
    {
        { "Tomato Seed", 0 },
        { "Potato Seed", 1 },
        { "Carrot Seed", 2 },
        { "Corn Seed", 3 }
    };

    void Update()
    {
        UpdateGoldUI();    
    }
    void Start()
    {
        foreach (var shopItem in shopItems)
        {
            shopItem.UpdateQuantityText();

            ShopItem localItem = shopItem; // needed to capture the loop variable

            shopItem.plusButton.onClick.AddListener(() =>
            {
                SFXManager.Instance.PlayButton();
                localItem.quantityToBuy++;
                localItem.UpdateQuantityText();
                UpdateTotalCost();
            });

            shopItem.minusButton.onClick.AddListener(() =>
            {
                SFXManager.Instance.PlayButton();
                localItem.quantityToBuy = Mathf.Max(0, localItem.quantityToBuy - 1);
                localItem.UpdateQuantityText();
                UpdateTotalCost();
            });
        }

        buyButton.onClick.AddListener(BuyAllSelectedItems);

        if (feedbackText != null)
        {
            feedbackText.text = "";
            feedbackText.gameObject.SetActive(false);
        }

        UpdateGoldUI();
        UpdateTotalCost();
    }

    public void OpenShop()
    {
        SFXManager.Instance.PlayButton();
        UpdateGoldUI();
        UpdateTotalCost();
    }

    void UpdateTotalCost()
    {
        int total = 0;
        foreach (var item in shopItems)
        {
            total += item.quantityToBuy * item.price;
        }

        if (totalCostText != null)
        {
            totalCostText.text = total.ToString();
        }
    }
        void UpdateGoldUI()
    {
        if (goldText != null && playerGold != null)
        {
            goldText.text = playerGold.currentGold.ToString();
        }
    }
    public void CloseShop()
    {
        // Any logic on shop close (optional)
    }

    void BuyAllSelectedItems()
    {
        int totalCost = 0;

        foreach (var shopItem in shopItems)
        {
            totalCost += shopItem.quantityToBuy * shopItem.price;
        }

        if (playerGold.currentGold < totalCost)
        {
            ShowFeedback("Not enough gold!");
            SFXManager.Instance.PlayButton();
            SFXManager.Instance.PlayIncorrect();
            return;
        }

        // Deduct gold
        playerGold.SubtractGold(totalCost);

        // Add items to inventory
        foreach (var shopItem in shopItems)
        {
            if (shopItem.quantityToBuy > 0)
            {
                AddToInventory(shopItem.item, shopItem.quantityToBuy);
                shopItem.quantityToBuy = 0;
                shopItem.UpdateQuantityText();
            }
        }

        UpdateGoldUI();
        UpdateTotalCost();
        ShowFeedback("Purchased!");
        SFXManager.Instance.PlayButton();
        SFXManager.Instance.PlayPurchase();
        FindObjectOfType<Hotbar>()?.SyncWithInventory(playerInventory);
    }

    void AddToInventory(Item item, int amount)
    {
        if (itemToSlotIndex.TryGetValue(item.itemName, out int slotIndex))
        {
            while (playerInventory.slots.Count <= slotIndex)
            {
                playerInventory.slots.Add(new InventorySlot(null, 0));
            }

            var slot = playerInventory.slots[slotIndex];
            if (slot.item == null)
            {
                playerInventory.slots[slotIndex] = new InventorySlot(item, amount);
            }
            else
            {
                slot.quantity += amount;
            }
        }
        else
        {
            Debug.LogWarning("Item not mapped to slot index: " + item.itemName);
        }
    }

    void ShowFeedback(string message)
    {
        if (feedbackText != null)
        {
            StopAllCoroutines();
            feedbackText.text = message;
            feedbackText.gameObject.SetActive(true);
            StartCoroutine(HideFeedbackAfterDelay());
        }
    }

    IEnumerator HideFeedbackAfterDelay()
    {
        yield return new WaitForSecondsRealtime(feedbackDuration);
        feedbackText.gameObject.SetActive(false);
    }
}
