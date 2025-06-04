using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }
    [SerializeField] public TextMeshProUGUI goldText;
    [SerializeField] private ShopScriptableO[] shopItems;
    [SerializeField] ShopTemplate[] shopTemplates;
    [SerializeField] Button[] buyButtons;
    [SerializeField] List<TextMeshProUGUI> playerStatsText;
    [SerializeField] private PlayerHealth _playerHealth;
    private PlayerBaseStats playerBaseStats;
    private List<ShopScriptableO> currentIntemsInShop;
    private bool isBuying = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        playerBaseStats = GameObject.Find("Player").GetComponent<PlayerBaseStats>();
        _playerHealth = FindObjectOfType<PlayerHealth>();
    }

    private void Start()
    {

        goldText.text = "Coins:" + EconomyManager.Instance.GetCurrentCoin().ToString("D3");
        for (int i = 0; i < buyButtons.Length; i++)
        {
            int index = i;
            buyButtons[i].onClick.AddListener(() => UpdatePlayerStats(index));
        }
    }

    private void OnEnable()
    {
        transform.localScale = Vector3.one * .5f;
        transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);

        LoadPanels();
        LoadPlayerStats();
        CheckPurchasable();
    }

    private void OnDisable()
    {
        EconomyManager.Instance.UpdateGoldText();
    }

    public void LoadPanels()
    {
        RandomizeShopItems();
        for (int i = 0; i < 3; i++)
        {
            shopTemplates[i].SetShopItemData(currentIntemsInShop[i]);
        }
    }

    public void LoadPlayerStats()
    {
        playerStatsText[0].text = "Max Health: " + playerBaseStats.MaxHealth;
        playerStatsText[1].text = "Attack: " + playerBaseStats.BaseAtk;
        playerStatsText[2].text = "Def: " + playerBaseStats.BaseDef;
        playerStatsText[3].text = "Speed: " + playerBaseStats.BaseSpeed;
        playerStatsText[4].text = "Crit Rate: " + playerBaseStats.BaseCrit;
        playerStatsText[5].text = "Crit Damage: " + playerBaseStats.BaseCritDmg;
        playerStatsText[6].text = "HpRes Rate: " + playerBaseStats.BaseSelfHealingRate;
    }

    private void CheckPurchasable()
    {
        for (int i = 0; i < 3; i++)
        {
            if (EconomyManager.Instance.GetCurrentCoin() >= currentIntemsInShop[i].itemPrice)
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
        if (currentCoin >= currentIntemsInShop[0].itemPrice)
        {

            EconomyManager.Instance.SetCurrentCoin(currentCoin - currentIntemsInShop[0].itemPrice);
            goldText.text = "Coins:" + EconomyManager.Instance.GetCurrentCoin().ToString("D3");
            CheckPurchasable();
            LoadPanels();
        }
    }

    public void UpdatePlayerStats(int index)
    {
        if (isBuying) return;
        playerBaseStats.MaxHealth += currentIntemsInShop[index].itemStats.health;
        playerBaseStats.BaseAtk += currentIntemsInShop[index].itemStats.atk;
        playerBaseStats.BaseDef += currentIntemsInShop[index].itemStats.def;
        playerBaseStats.BaseSpeed += currentIntemsInShop[index].itemStats.speed;
        playerBaseStats.BaseCrit += currentIntemsInShop[index].itemStats.crit;
        playerBaseStats.BaseCritDmg += currentIntemsInShop[index].itemStats.critDmg;
        playerBaseStats.BaseSelfHealingRate += currentIntemsInShop[index].itemStats.hpResRate;
        _playerHealth.MaxHealth = playerBaseStats.MaxHealth;
        _playerHealth.UpdateHealSlider();
        isBuying = true;
        StartCoroutine(DelayLoadShop());
    }

    private IEnumerator DelayLoadShop()
    {
        yield return new WaitForSeconds(0.2f);
        isBuying = false;
        LoadPlayerStats();
    }

    private void RandomizeShopItems()
    {

        currentIntemsInShop = new List<ShopScriptableO>();
        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, shopItems.Length);
            ShopScriptableO item = shopItems[randomIndex];
            // Kiểm tra xem item đã được thêm vào danh sách chưa
            while (currentIntemsInShop.Contains(item) && currentIntemsInShop.Count < shopItems.Length)
            {
                randomIndex = Random.Range(0, shopItems.Length);
                item = shopItems[randomIndex];
            }
            currentIntemsInShop.Add(item);
        }
    }

    public void Exis()
    {
        gameObject.SetActive(false);
        Inventory.Instance.gameObject.SetActive(true);
        Time.timeScale = 1;
    }
}