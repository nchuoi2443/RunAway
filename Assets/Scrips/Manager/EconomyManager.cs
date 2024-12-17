using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EconomyManager : Singleton<EconomyManager>
{
    private TMP_Text goldText;
    private int currentGold = 0;

    const string MONEY_KEY = "AmountCoin";

    public void UpdateCurrentCoin()
    {
        currentGold += 1;
        if (goldText == null)
        {
            goldText = GameObject.Find(MONEY_KEY).GetComponent<TMP_Text>();
        }
        goldText.text = currentGold.ToString("D3");
    }

    public int GetCurrentCoin() { return currentGold; }
    public void SetCurrentCoin(int coin) { currentGold = coin; }
}
