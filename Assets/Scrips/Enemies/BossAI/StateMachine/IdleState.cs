using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private BossBase _bossBase;
    private Animator _animator;
    private StateMachine _stateMachine;
    private float _timerBeforeAttack;
    private float _timeBeforAttack;

    public IdleState(BossBase bossBase, StateMachine stateMachine)
    {
        this._bossBase = bossBase;
        _stateMachine = stateMachine;
    }

    public void EnterState()
    {
        _animator = _bossBase.GetComponent<Animator>();
        //_animator.SetBool(ActionState.isMoving.ToString(), false);
        _timeBeforAttack = 2f;
        _timerBeforeAttack = _timeBeforAttack;
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
        if (_timerBeforeAttack >= 0)
        {
            _timerBeforeAttack -= Time.deltaTime;
        }
        else
        {
            if (_bossBase.MetaStateMachine.CurrentPhaseState is NormalPhase)
            {
                _bossBase.HandleChasing();
            }
            else
            {
                var stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
                if (stateInfo.IsName("Tranformation")) return;
                _stateMachine.ChangeState(new RunState(_bossBase, _stateMachine));
            }
        }
    }

    private void HandleTakeDamage()
    {
        _animator.SetTrigger(ActionState.castSkill.ToString());
        _stateMachine.ChangeState(new CastSkillState(_bossBase, ActionState.knifeSkill.ToString(), _stateMachine));
    }
}
