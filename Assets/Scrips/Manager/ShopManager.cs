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
    [SerializeField] TMP_Text[] playerStatsText;
    private PlayerBaseStats playerBaseStats;
    private int[] randomIndex;
    
    private void Start()
    {
        goldText.text = "Coins:" + EconomyManager.Instance.GetCurrentCoin().ToString("D3");
        gameObject.SetActive(false);

        playerBaseStats =  GetComponent<PlayerBaseStats>();
        goldText.color = Color.white;
        LoadPanels();
        LoadPlayerStats();
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

    public void LoadPlayerStats()
    {
        playerStatsText[0].text = "Max Health: " + playerBaseStats.MaxHealth;
        playerStatsText[1].text = "Attack: " + playerBaseStats.BaseAtk;
        playerStatsText[2].text = "Defense: " + playerBaseStats.BaseDef;
        playerStatsText[3].text = "Speed: " + playerBaseStats.BaseSpeed;
        playerStatsText[4].text = "Attack Speed: " + playerBaseStats.BaseAtkSpeed;
        playerStatsText[5].text = "Crit Chance: " + playerBaseStats.BaseCrit;
        playerStatsText[6].text = "Crit Damage: " + playerBaseStats.BaseCritDmg;
        playerStatsText[8].text = "Life Steal: " + playerBaseStats.BaseLifeSteal;
        playerStatsText[7].text = "Stamina: " + playerBaseStats.BaseStamina;
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

            //shopItems[randomIndex[0]].itemEffect.Invoke();
            LoadPanels();
        }
    }

    public void UpdatePlayerStats()
    {
        playerBaseStats.MaxHealth += shopItems[randomIndex[0]].itemStats.health;
        playerBaseStats.BaseAtk += shopItems[randomIndex[0]].itemStats.atk;
        playerBaseStats.BaseDef += shopItems[randomIndex[0]].itemStats.def;
        playerBaseStats.BaseSpeed += shopItems[randomIndex[0]].itemStats.speed;
        playerBaseStats.BaseAtkSpeed += shopItems[randomIndex[0]].itemStats.atkSpeed;
        playerBaseStats.BaseCrit += shopItems[randomIndex[0]].itemStats.crit;
        playerBaseStats.BaseCritDmg += shopItems[randomIndex[0]].itemStats.critDmg;
        playerBaseStats.BaseLifeSteal += shopItems[randomIndex[0]].itemStats.lifeSteal;
        playerBaseStats.BaseStamina += shopItems[randomIndex[0]].itemStats.stamina;
        LoadPlayerStats();
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