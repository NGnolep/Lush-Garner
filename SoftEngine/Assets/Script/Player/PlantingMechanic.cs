using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class PlantingMechanics : MonoBehaviour
{
    public Tilemap groundTilemap;
    public TileBase plantedTile;
    public TileBase grownTile;
    public TileBase insectFreeTile;
    public TileBase resetTile;
    public TextMeshProUGUI shortText;
    public TextMeshProUGUI longText;
    public bool shortShown = false;
    public bool longShown = false;
    private Dictionary<Vector3Int, Item> plantedSeeds = new Dictionary<Vector3Int, Item>();
    private Hotbar hotbar;
    private Inventory inventory;

    [Header("Seed Items")]
    public Item potatoSeed;
    public Item carrotSeed;
    public Item cornSeed;
    public Item tomatoSeed;

    [Header("Harvested Items")]
    public Item potato;
    public Item carrot;
    public Item corn;
    public Item tomato;

    void Start()
    {
        hotbar = FindObjectOfType<Hotbar>();
        inventory = FindObjectOfType<Inventory>();
    }
    void Update()
    {
        ShowOrHideEText();

        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector3 playerPos = transform.position + new Vector3(0, -0.1f, 0);
            Vector3Int cellPos = groundTilemap.WorldToCell(playerPos);
            TileBase currentTile = groundTilemap.GetTile(cellPos);

            if (IsUnplantedDirt(currentTile))
            {
                var selectedItem = hotbar.GetSelectedItem();
                if (selectedItem != null && selectedItem.itemType == ItemType.Seed && GetItemQuantityInInventory(selectedItem) > 0)
                {
                    groundTilemap.SetTile(cellPos, plantedTile);
                    plantedSeeds[cellPos] = selectedItem; // Track which seed was planted here
                    RemoveItemFromInventory(selectedItem, 1);
                }
                else
                {
                    Debug.Log("No seed selected or out of stock!");
                }
            }
            else if (currentTile == plantedTile)
            {
                SceneManager.LoadScene("Watering", LoadSceneMode.Additive);
            }
            else if (currentTile == grownTile)
            {
                SceneManager.LoadScene("InsectDefend", LoadSceneMode.Additive);
            }
            else if (currentTile == insectFreeTile)
            {
                SceneManager.LoadScene("Harvesting", LoadSceneMode.Additive);
            }
        }
    }

    void ShowOrHideEText()
    {
        Vector3 playerPos = transform.position + new Vector3(0, -0.1f, 0);
        Vector3Int cellPos = groundTilemap.WorldToCell(playerPos);
        TileBase currentTile = groundTilemap.GetTile(cellPos);

        var selectedItem = hotbar.GetSelectedItem();
        int selectedQuantity = selectedItem != null ? GetItemQuantityInInventory(selectedItem) : 0;

        // Reset all text visibility
        shortText.gameObject.SetActive(false);
        longText.gameObject.SetActive(false);
        shortShown = false;
        longShown = false;
        if (IsUnplantedDirt(currentTile) && selectedItem != null && selectedItem.itemType == ItemType.Seed && selectedQuantity > 0)
        {
            shortText.gameObject.SetActive(true);
            shortText.text = "to Plant";
            shortShown = true;
        }
        else if (currentTile == plantedTile)
        {
            shortText.gameObject.SetActive(true);
            shortText.text = "to Water";
            shortShown = true;
        }
        else if (currentTile == grownTile)
        {
            longText.gameObject.SetActive(true);
            longText.text = "to Defend";
            longShown = true;
        }
        else if (currentTile == insectFreeTile)
        {
            longText.gameObject.SetActive(true);
            longText.text = "to Harvest";
            longShown = true;
        }
    }

    public void ReplacePlantedTiles()
    {
        BoundsInt bounds = groundTilemap.cellBounds;
    
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int cell = new Vector3Int(x, y, 0);
                TileBase tile = groundTilemap.GetTile(cell);
    
                if (tile == plantedTile)
                {
                    if (plantedSeeds.ContainsKey(cell))
                    {
                        Debug.Log("Growing crop from seed: " + plantedSeeds[cell].itemName + " at " + cell);
                    }
                    groundTilemap.SetTile(cell, grownTile);
                }
            }
        }
    }
    public void ReplaceGrownTiles()
    {
        BoundsInt bounds = groundTilemap.cellBounds;

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int cell = new Vector3Int(x, y, 0);
                if (groundTilemap.GetTile(cell) == grownTile)
                {
                    groundTilemap.SetTile(cell, insectFreeTile);
                }
            }
        }
    }

    public void ResetAllHarvestedTiles()
    {
        BoundsInt bounds = groundTilemap.cellBounds;

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int cell = new Vector3Int(x, y, 0);
                if (groundTilemap.GetTile(cell) == insectFreeTile)
                {
                    if(plantedSeeds.TryGetValue(cell, out Item seed))
                    {
                        Item harvest = GetHarvestItemFromSeed(seed);
                        if (harvest != null)
                        {
                            AddItemToInventory(harvest, 1);
                            Debug.Log($"Harvested {harvest.itemName} from seed {seed.itemName} at {cell}");
                        }
                        plantedSeeds.Remove(cell);
                    }
                    groundTilemap.SetTile(cell, resetTile);
                }
            }
        }
    }
    bool IsUnplantedDirt(TileBase tile)
    {
        if (tile == null) return false;
        return tile.name.ToLower().Contains("plantabledirt");
    }

    int GetItemQuantityInInventory(Item item)
    {
        foreach (InventorySlot slot in inventory.slots)
        {
            if (slot.item == item)
            {
                return slot.quantity;
            }
        }
        return 0;
    }

    void RemoveItemFromInventory(Item item, int amount)
    {
        foreach (InventorySlot slot in inventory.slots)
        {
            if (slot.item == item && slot.quantity >= amount)
            {
                slot.quantity -= amount;
                if (slot.quantity <= 0)
                {
                    slot.quantity = 0;
                }
                break;
            }
        }
        hotbar.SyncWithInventory(inventory);
    }
    void AddItemToInventory(Item item, int amount)
    {
        foreach (InventorySlot slot in inventory.slots)
        {
            if (slot.item == item)
            {
                slot.quantity += amount;
                return;
            }
        }
        hotbar.SyncWithInventory(inventory);
    }
    Item GetHarvestItemFromSeed(Item seed)
    {
        if (seed == potatoSeed) return potato;
        if (seed == carrotSeed) return carrot;
        if (seed == cornSeed) return corn;
        if (seed == tomatoSeed) return tomato;

        Debug.LogWarning("No harvest match for: " + seed.itemName);
        return null;
    }
}
