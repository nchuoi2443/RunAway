using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private BossBase _bossBase;
    private Animator _animator;

    public IdleState(BossBase bossBase)
    {
        this._bossBase = bossBase;
    }

    public void EnterState()
    {
        _animator = _bossBase.GetComponent<Animator>();
        _animator.SetBool(ActionState.isMoving.ToString(), false);
        if (_bossBase.MetaStateMachine.CurrentPhaseState is RagePhase)
        {
            _bossBase.OnTakeDamage += HandleTakeDamage;
        }
    }

    public void ExitState()
    {
        
    }

    public void UpdateState()
    {
        
    }

    private void HandleTakeDamage()
    {
        _animator.SetTrigger(ActionState.castSkill.ToString());
        StateMachine.Instance.ChangeState(new CastSkillState(_bossBase, ActionState.knifeSkill.ToString()));
    }
}
