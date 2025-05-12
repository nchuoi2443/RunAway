using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHitState : IState
{
    private BossBase _bossBase;
    private Animator _animator;
    private StateMachine _stateMachine;
    private float _timerBeforeAttack;
    private float _timeBeforAttack;

    public GetHitState(BossBase bossBase, StateMachine stateMachine)
    {
        this._bossBase = bossBase;
        _stateMachine = stateMachine;
    }

    public void EnterState()
    {
        
    }
    public void UpdateState()
    {
        
    }

    public void ExitState()
    {
        
    }

}
