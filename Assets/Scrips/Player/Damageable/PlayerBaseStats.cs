using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseStats : MonoBehaviour
{
    public static PlayerBaseStats Instance;

    private float maxHealth;
    private float baseAtk;
    private float baseDef;
    private float baseSpeed;
    private float baseCrit;
    private float baseCritDmg;
    private float baseSelfHealingRate;
    private bool isCrit;
    public bool IsCrit { get { return isCrit; } set { isCrit = value; } }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float BaseAtk { get => baseAtk; set => baseAtk = value; }
    public float BaseDef { get => baseDef; set => baseDef = value; }
    public float BaseSpeed { get => baseSpeed; set => baseSpeed = value; }
    public float BaseCrit { get => baseCrit; set => baseCrit = value; }
    public float BaseCritDmg { get => baseCritDmg; set => baseCritDmg = value; }
    public float BaseSelfHealingRate { get => baseSelfHealingRate; set => baseSelfHealingRate = value; }

    private void Awake()
    {
        Instance = this;

        maxHealth = PlayerHealth.Instance.MaxHealth;
        baseAtk = 2f;
        baseDef = 0.2f;
        baseSpeed = 4;
        baseCrit = 5;
        baseCritDmg = 50;
        baseSelfHealingRate = 0f;
        isCrit = false;
    }

    public float CalculateDamage(float weaponDamage)
    {
        // Example calculation, you can adjust this formula as needed
        float damage = weaponDamage + BaseAtk;
        if (Random.value * 100 <= BaseCrit)
        {
            damage *= (1 + BaseCritDmg/100);
            isCrit = true;
        }
        return damage;
    }

    public float CalculateDamagePlayerReceived(float dmgTaken)
    {
        return dmgTaken - baseDef;
    }

}
