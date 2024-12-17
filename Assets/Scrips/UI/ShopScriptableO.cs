using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ShopItem", menuName = "Scriptable Object/New Shop Item", order = 1)]
public class ShopScriptableO : ScriptableObject
{
    public string itemName;
    public string shortDescription;
    public string longDescription;
    public Sprite itemImage;
    public int itemPrice;
    public bool isPurchased;
}
