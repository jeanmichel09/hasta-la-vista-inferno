using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    public enum ItemType
    {
        Sword,
        HealthPotion,
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Sword: return ItemAssets.Instance.swordSprite;
            case ItemType.HealthPotion:  return ItemAssets.Instance.healthPotionSprite;
        }
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            default:

            case ItemType.HealthPotion:
                return true;

            case ItemType.Sword:
            
                return false;
        }
    }
}
