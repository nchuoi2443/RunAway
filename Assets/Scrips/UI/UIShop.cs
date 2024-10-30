using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : MonoBehaviour
{
    private Transform container;
    private Transform shopItemTemplate;

    private Transform inventoryContainer;
    private Transform inventoryItemTemplate;
    private void Awake()
    {
        container = transform.Find("Container");
        shopItemTemplate = container.Find("ShopItemTemplate");

        shopItemTemplate.gameObject.SetActive(true);

        /*inventoryContainer = transform.Find("InventoryContainer");
        inventoryItemTemplate = inventoryContainer.Find("InventoryItemTemplate");*/
    }

    private void Start()
    {
        CreateShopItem(Items.ItemType.MaxHealth, Items.GetSprite(Items.ItemType.MaxHealth), "Max Health", Items.GetCost(Items.ItemType.MaxHealth), 0);
        CreateShopItem(Items.ItemType.MaxStamina, Items.GetSprite(Items.ItemType.MaxStamina), "Max Stamina", Items.GetCost(Items.ItemType.MaxStamina), 1);
        CreateShopItem(Items.ItemType.Damage, Items.GetSprite(Items.ItemType.Damage), "Damage", Items.GetCost(Items.ItemType.Damage), 2);
        CreateShopItem(Items.ItemType.Sword, Items.GetSprite(Items.ItemType.Sword), "Sword", Items.GetCost(Items.ItemType.Sword), 3);
        CreateShopItem(Items.ItemType.Shield, Items.GetSprite(Items.ItemType.Shield), "Shield", Items.GetCost(Items.ItemType.Shield), 4);
        CreateShopItem(Items.ItemType.Bow, Items.GetSprite(Items.ItemType.Bow), "Bow", Items.GetCost(Items.ItemType.Bow), 5);
        CreateShopItem(Items.ItemType.Staff, Items.GetSprite(Items.ItemType.Staff), "Staff", Items.GetCost(Items.ItemType.Staff), 6);

        /*CreateInventoryItem(Items.GetSprite(Items.ItemType.Sword), 0);
        CreateInventoryItem(Items.GetSprite(Items.ItemType.Bow), 1);
        CreateInventoryItem(Items.GetSprite(Items.ItemType.Staff), 2);*/
    }

    private void CreateShopItem(Items.ItemType itemType, Sprite itemSprite, string itemName, int itemPrice, int positionIndex)
    {
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();
        float shopItemHeight = 100f;
        shopItemRectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * positionIndex);
        shopItemTransform.Find("PriceText").GetComponent<TextMeshProUGUI>().SetText(itemPrice.ToString());
        shopItemTransform.Find("itemImage").GetComponent<Image>().sprite = itemSprite;
        shopItemTransform.Find("NameText").GetComponent<TextMeshProUGUI>().SetText(itemName);

        shopItemTransform.GetComponent<Button_UI>().ClickFunc = () =>
        {
            TryBuyItem(itemType);
        };
    }

   /* private void CreateInventoryItem(Sprite itemSprite, int positionIndex)
    {
        Transform inventoryItemTransform = Instantiate(inventoryItemTemplate, inventoryContainer);
        RectTransform inventoryItemRectTransform = inventoryItemTransform.GetComponent<RectTransform>();
        float inventoryItemWidth = 140f;
        inventoryItemRectTransform.anchoredPosition = new Vector2(inventoryItemWidth * positionIndex, 0);
        inventoryItemTransform.Find("itemImage").GetComponent<Image>().sprite = itemSprite;
    }*/
    private void TryBuyItem(Items.ItemType itemType)
    {
        
    }
}
