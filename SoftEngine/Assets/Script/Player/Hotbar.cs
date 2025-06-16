using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotbar : MonoBehaviour
{
    public List<InventorySlot> hotbarSlots = new List<InventorySlot>(8);
    public int selectedIndex = 0;

    void Update()
    {
        // Scroll or number key to change slot
        if (Input.mouseScrollDelta.y > 0) selectedIndex = (selectedIndex + 1) % 8;
        if (Input.mouseScrollDelta.y < 0) selectedIndex = (selectedIndex - 1 + 8) % 8;

        for (int i = 0; i < 8; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                selectedIndex = i;
                break;
            }
        }
    }

    public Item GetSelectedItem()
    {
        if (hotbarSlots[selectedIndex] != null)
            return hotbarSlots[selectedIndex].item;
        return null;
    }

    public void SyncWithInventory(Inventory inventory)
    {
        for (int i = 0; i < hotbarSlots.Count; i++)
        {
            if (i < inventory.slots.Count)
            {
                hotbarSlots[i] = inventory.slots[i];
            }
        }
    }
}
