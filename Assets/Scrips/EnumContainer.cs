using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MetaStatus
{
    Normal,
    Rage,
    PhaseTwo,
    Dying,
}

public enum  ActionState
{
    isMoving,
    castSkill,
    getHit,
    doTransform,
    knifeSkill,
    magicSkill,
    jumpSkill,
    spitFireSkill,
    isDead,
}

public enum BossState
{
    Idle,
    Chasing,
    Attacking,
    UsingSkill,
    Enraged,
    Dead
}