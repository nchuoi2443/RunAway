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
        baseAtk = 1;
        baseDef = 1;
        baseSpeed = 1;
        baseAtkSpeed = 1;
        baseCrit = 0;
        baseCritDmg = 5;
        baseLifeSteal = 0;
        baseStamina = 1;
    }

}
