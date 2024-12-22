using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseStats : MonoBehaviour
{
    private float maxHealth;
    private float baseAtk;
    private float baseDef;
    private float baseSpeed;
    private float baseAtkSpeed;
    private float baseCrit;
    private float baseCritDmg;
    private float baseLifeSteal;
    [Range(1, 3)]
    private int baseStamina;
    private bool isCrit;
    public bool IsCrit { get { return isCrit; } set { isCrit = value; } }

    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float BaseAtk { get => baseAtk; set => baseAtk = value; }
    public float BaseDef { get => baseDef; set => baseDef = value; }
    public float BaseSpeed { get => baseSpeed; set => baseSpeed = value; }
    public float BaseAtkSpeed { get => baseAtkSpeed; set => baseAtkSpeed = value; }
    public float BaseCrit { get => baseCrit; set => baseCrit = value; }
    public float BaseCritDmg { get => baseCritDmg; set => baseCritDmg = value; }
    public float BaseLifeSteal { get => baseLifeSteal; set => baseLifeSteal = value; }
    public int BaseStamina { get => baseStamina; set => baseStamina = value; }

    private void Awake()
    {
        maxHealth = PlayerHealth.Instance.MaxHealth;
        baseAtk = 1f;
        baseDef = 1;
        baseSpeed = 1;
        baseAtkSpeed = 1;
        baseCrit = 5;
        baseCritDmg = 50;
        baseLifeSteal = 0;
        baseStamina = 1;
        isCrit = false;
    }

    public float CalculateDamage(float weaponDamage)
    {
        // Example calculation, you can adjust this formula as needed
        float damage = weaponDamage + BaseAtk;
        if (Random.value * 100 <= BaseCrit)
        {
            damage *= 1 + BaseCritDmg/100;
            isCrit = true;
        }
        return damage;
    }



}
