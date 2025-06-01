using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopTemplate : MonoBehaviour
{
    public TMP_Text itemNameTxt;
    public TMP_Text shortDescriptTxt;
    public TMP_Text priceTxt;
    public Image itemImage;

    private ShopScriptableO _itemShopData;

    public void SetShopItemData(ShopScriptableO itemShopData)
    {
        _itemShopData = itemShopData;
        itemNameTxt.text = _itemShopData.itemName;
        shortDescriptTxt.text = _itemShopData.shortDescription;
        priceTxt.text = _itemShopData.itemPrice.ToString("D3");
        itemImage.sprite = _itemShopData.itemImage;
    }
}
