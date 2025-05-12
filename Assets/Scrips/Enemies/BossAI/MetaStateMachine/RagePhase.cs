using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagePhase : IPhaseState
{
    private BossBase _bossBase;
    private StateMachine _stateMachine;
    private MetaStateMachine _metaStateMachine;

    public RagePhase(BossBase bossBase, MetaStateMachine metaStateMachine)
    {
        this._bossBase = bossBase;
        _stateMachine = new StateMachine(_bossBase);
        _stateMachine.Init();
        _metaStateMachine = metaStateMachine;
    }

    public void EnterPhase()
    {
        _stateMachine.ChangeState(new IdleState(_bossBase, _stateMachine));
    }
    public void UpdatePhase()
    {
        _stateMachine.StateMachineUpdate();
    }

    public void ExitPhase()
    {

    }
}
