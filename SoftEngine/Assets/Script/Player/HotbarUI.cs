using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HotbarUI : MonoBehaviour
{
    public Hotbar hotbar;
    public Image[] slotImages; // Main slot backgrounds
    public Image[] itemIcons;
    public TextMeshProUGUI[] quantities;
    public Color selectedColor = new Color(1f, 0.3f, 0.3f);
    public Color defaultColor = Color.white;

    void Update()
    {
        for (int i = 0; i < slotImages.Length; i++)
        {
            var slot = hotbar.hotbarSlots[i];

            if (slot != null && slot.item != null)
            {
                itemIcons[i].sprite = slot.item.icon;
                itemIcons[i].enabled = true;
                quantities[i].text = slot.quantity > 1 ? slot.quantity.ToString() : "";
            }
            else
            {
                itemIcons[i].enabled = false;
                quantities[i].text = "";
            }

            if (i == hotbar.selectedIndex)
            {
                slotImages[i].color = new Color(1f, 0.5f, 0f); // Bright orange
                slotImages[i].rectTransform.localScale = Vector3.one * 1.2f; // Make it bigger
            }
            else
            {
                slotImages[i].color = Color.white;
                slotImages[i].rectTransform.localScale = Vector3.one;
            }
        }
    }

}
