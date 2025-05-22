using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerGold : MonoBehaviour
{
    public int currentGold = 500;
    public TextMeshProUGUI goldText;
    void Start()
    {
        UpdateGold();
    }

    public void AddGold(int amount)
    {
        currentGold += amount;
        UpdateGold();
    }

    public void SubtractGold(int amount)
    {
        currentGold -= amount;
        UpdateGold();
    }

    void UpdateGold()
    {
        if(goldText != null)
        {
            goldText.text = currentGold.ToString() + "G";
        }
    }
}
