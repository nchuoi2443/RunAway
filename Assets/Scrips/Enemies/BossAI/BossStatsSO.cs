using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "BossNormalPhaseStats", menuName = "ScriptableObjects/BossNormalPhaseStatsSO", order = 1)]
public class BossStatsSO : ScriptableObject
{
    public float Health;
    public float Damage;
    public float Speed;
    public float AttackRange;
    public float Defense;

}

