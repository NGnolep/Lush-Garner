using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventorySlot> slots = new List<InventorySlot>(8); // Pre-populated

    public void AddItem(Item item, int amount)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item == item)
            {
                slots[i].quantity += amount;
                return;
            }
        }

        Debug.LogWarning("Tried to add item not found in predefined inventory: " + item.name);
    }
}
