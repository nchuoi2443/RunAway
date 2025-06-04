using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleState : IState
{
    BossBase _bossBase;
    StateMachine _stateMachine;
    float _invisibleTime;
    

    InvisibleState(float invisibleTime, BossBase boss, StateMachine stateMachine)
    {
        _invisibleTime = invisibleTime;
        _bossBase = boss;
        _stateMachine = stateMachine;
    }

    public void EnterState()
    {
        
    }

    public void ExitState()
    {
        
    }

    public void UpdateState()
    {
        
    }
}
