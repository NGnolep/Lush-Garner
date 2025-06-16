using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType { Seed, Plant }
[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public ItemType itemType;
}
