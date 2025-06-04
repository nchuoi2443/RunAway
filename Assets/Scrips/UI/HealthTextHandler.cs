using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class HealthTextHandler : MonoBehaviour
{
    [SerializeField] private GameObject critText;
    [SerializeField] private GameObject nonCritText;

    private Canvas canvas;

    private void Awake()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        CharacterEvents.characterTookDmg.AddListener((character, damageReceived, isCrit) => TookDamage(character, damageReceived,ref isCrit));
    }

    public void TookDamage(GameObject character, float damageReceived,ref bool isCrit)
    {
        Vector3 spawnPos = Camera.main.WorldToScreenPoint(character.transform.position);
        GameObject textPrefab = isCrit ? critText : nonCritText;
        PlayerBaseStats.Instance.IsCrit = false;
        GameObject textInstance = Instantiate(textPrefab, spawnPos, Quaternion.identity, canvas.transform);
        TMP_Text tmpText = textInstance.GetComponent<TMP_Text>();

        if (damageReceived - (int)damageReceived != 0)
        {
            tmpText.text = damageReceived.ToString("F2");

        }
        else
        {
            tmpText.text = damageReceived.ToString("F0");
        }


        // Thiết lập target cho HealthText
        HealthText healthText = textInstance.GetComponent<HealthText>();
        healthText.SetTarget(character.transform);
    }
}
