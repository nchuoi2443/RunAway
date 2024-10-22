using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public enum ItemType{
        MaxHealth,
        MaxStamina,
        Damage,
        Sword,
        Shield,
        Bow,
        Staff
    }

    public static int GetCost(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.MaxHealth: return 100;
            case ItemType.MaxStamina: return 100;
            case ItemType.Damage: return 150;
            case ItemType.Sword: return 200;
            case ItemType.Shield: return 200;
            case ItemType.Bow: return 250;
            case ItemType.Staff: return 250;
        }
    }

    public static Sprite GetSprite(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.MaxHealth: return GameAsset.Instance.MaxHealthSprite;
            case ItemType.MaxStamina: return GameAsset.Instance.MaxStaminaSprite;
            case ItemType.Damage: return GameAsset.Instance.DamageSprite;
            case ItemType.Sword: return GameAsset.Instance.SwordSprite;
            case ItemType.Shield: return GameAsset.Instance.ShieldSprite;
            case ItemType.Bow: return GameAsset.Instance.BowSprite;
            case ItemType.Staff: return GameAsset.Instance.StaffSprite;
        }
    }
}
