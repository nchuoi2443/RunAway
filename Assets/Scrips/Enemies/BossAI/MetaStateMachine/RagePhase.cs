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
        _stateMachine = new StateMachine();
        _metaStateMachine = metaStateMachine;
    }

    public void EnterPhase()
    {
        _stateMachine.ChangeState(new IdleState(_bossBase)); //this will be tranfomation state
    }
    public void UpdatePhase()
    {
        _stateMachine.StateMachineUpdate();
    }

    public void ExitPhase()
    {

    }
}
