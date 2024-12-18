using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : Singleton<ShopManager>
{

    [SerializeField] public TMP_Text goldText;
    [SerializeField] private ShopScriptableO[] shopItems;
    [SerializeField] ShopTemplate[] shopTemplates;
    [SerializeField] Button[] buyButtons;
    private int[] randomIndex;
    private bool isBuy;

    public bool IsBuy { get {  return isBuy; } }
    private void Start()
    {
        goldText.text = "Coins:" + EconomyManager.Instance.GetCurrentCoin().ToString("D3");
        gameObject.SetActive(false);
        isBuy = false;
        
        goldText.color = Color.white;
        LoadPanels();
        CheckPurchasable();
    }

    private void Update()
    {
        goldText.text = "Coins:" + EconomyManager.Instance.GetCurrentCoin().ToString("D3");
        CheckPurchasable();
    }

    public void AddCoin()
    {
        EconomyManager.Instance.UpdateCurrentCoin();
        goldText.text = "Coins:" + EconomyManager.Instance.GetCurrentCoin().ToString("D3");
    }

    public void LoadPanels()
    {
        RandomizeShopItems();
        for (int i = 0; i < 3; i++)
        {
            shopTemplates[i].itemNameTxt.text = shopItems[randomIndex[i]].itemName;
            shopTemplates[i].shortDescriptTxt.text = shopItems[randomIndex[i]].shortDescription;
            shopTemplates[i].priceTxt.text = "Cost: " + shopItems[randomIndex[i]].itemPrice.ToString("D3");
            shopTemplates[i].itemImage.sprite = shopItems[randomIndex[i]].itemImage;
        }
    }

    private void CheckPurchasable()
    {
        for (int i = 0; i < 3; i++)
        {
            if (EconomyManager.Instance.GetCurrentCoin() >= shopItems[randomIndex[i]].itemPrice)
            {
                buyButtons[i].interactable = true;
            }
            else
            {
                buyButtons[i].interactable = false;
            }
        }
    }

    public void PurchaseItem()
    {
        int currentCoin = EconomyManager.Instance.GetCurrentCoin();
        if (currentCoin >= shopItems[randomIndex[0]].itemPrice)
        {

            EconomyManager.Instance.SetCurrentCoin(currentCoin - shopItems[randomIndex[0]].itemPrice);
            isBuy = true;
            //shopItems[randomIndex[0]].itemEffect.Invoke();
            LoadPanels();
        }
    }

    private void RandomizeShopItems()
    {
        randomIndex = new int[shopItems.Length];
        for (int i = 0; i < 3; i++)
        {
            randomIndex[i] = Random.Range(0, shopItems.Length);
        }
    }

    public void Exis()
    {
        gameObject.SetActive(false);
        Inventory.Instance.gameObject.SetActive(true);
        Time.timeScale = 1;
    }
}