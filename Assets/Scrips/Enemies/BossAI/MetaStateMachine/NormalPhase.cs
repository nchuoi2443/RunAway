using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NormalPhase : IPhaseState
{
    private BossBase _bossBase;
    private StateMachine _stateMachine;
    private MetaStateMachine _metaStateMachine;
    public NormalPhase(BossBase bossBase, MetaStateMachine metaStateMachine)
    {
        this._bossBase = bossBase;
        _stateMachine = new StateMachine();
        _metaStateMachine = metaStateMachine;
    }
    public void EnterPhase()
    {
        StateMachine.Instance.ChangeState(new IdleState(_bossBase));
    }
    public void UpdatePhase()
    {
        _stateMachine.StateMachineUpdate();

        if (_bossBase.CurrentHealth < 30)
        {
            _metaStateMachine.ChangePhase(new RagePhase(_bossBase, _metaStateMachine));
            ExitPhase();
        }
    }
    public void ExitPhase()
    {
        //clean up data
        
    }
}