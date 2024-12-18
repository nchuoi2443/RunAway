using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseStats : MonoBehaviour
{
    private float maxHealth = PlayerHealth.Instance.MaxHealth;
    private float baseAtk = 1;
    private float baseDef = 1;
    private float baseSpeed = 1;
    private float baseAtkSpeed = 1;
    private float baseCrit= 0;
    private float baseCritDmg = 5;
    private float baseLifeSteal = 0;
    [Range(1, 3)]
    private int baseStamina = 1;

}
