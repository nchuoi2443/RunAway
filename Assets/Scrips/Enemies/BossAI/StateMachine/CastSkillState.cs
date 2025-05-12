using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastSkillState : IState
{
    public SkillManager SkillManager;
    private BossBase _bossBase;
    private Animator _animator;
    private string _skillName;
    private StateMachine _stateMachine;

    public CastSkillState(BossBase bossBase, string skillName, StateMachine stateMachine)
    {
        _bossBase = bossBase;
        _skillName = skillName;
        _stateMachine = stateMachine;
    }

    public void EnterState()
    {
        _animator = _bossBase.GetComponent<Animator>();
        _animator.SetTrigger(_skillName);
    }

    public void UpdateState()
    {
        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName(_skillName) && stateInfo.normalizedTime >= 1.0f)
        {
            _stateMachine.ChangeState(new IdleState(_bossBase, _stateMachine));
        }
    }

    public void ExitState()
    {
        _skillName = null;
        _bossBase = null;
        _animator = null;
        SkillManager = null;
    }
    
}
