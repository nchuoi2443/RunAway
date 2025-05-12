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
    getHit_Base,
    getHit_Sub,
    doTransform,
    knifeSkill,
    magicSkill,
    jumpSkill,
    spitFireSkill,
    isDeath,
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