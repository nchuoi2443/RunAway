using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine 
{
    private IState _currentState;
    private BossBase _bossBase;

    public StateMachine(BossBase bossBase)
    {
        _bossBase = bossBase;
    }

    public void Init()
    {
        _currentState = new IdleState(_bossBase, this);
        _currentState.EnterState();
    }

    public void ChangeState(IState state)
    {
        if (_currentState != null)
        {
            _currentState.ExitState();
        }
        _currentState = state;
        _currentState.EnterState();
    }

    public void StateMachineUpdate()
    {
        _currentState.UpdateState();
    }


}
