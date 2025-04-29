using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private BossBase _bossBase;

    public IdleState(BossBase bossBase)
    {
        this._bossBase = bossBase;
    }

    public void EnterState()
    {
        _bossBase.GetComponent<Animator>().SetBool(ActionState.Idle.ToString(), true);
    }

    public void ExitState()
    {
        
    }

    public void UpdateState()
    {
        
    }
}
