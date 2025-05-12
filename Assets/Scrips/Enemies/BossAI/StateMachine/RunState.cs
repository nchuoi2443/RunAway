using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : IState
{
    private BossBase _bossBase;
    private Animator _animator;
    private float _timer;
    private float _maxChasingTime;
    private StateMachine _stateMachine;
    private SkillManager _skillManager;

    private float _meleeEnterTime = -1f;
    private float _spitFireEnterTime = -1f;
    private float _requiredStayTime = 1f;

    public RunState(BossBase bossBase, StateMachine stateMachine)
    {
        _bossBase = bossBase;
        _stateMachine = stateMachine;
    }

    public void EnterState()
    {
        _animator = _bossBase.GetComponent<Animator>();
        _animator.SetBool(ActionState.isMoving.ToString(), true);
        _skillManager = _bossBase.SkillManager;
        _timer = 0f;
        _maxChasingTime = 5f;
    }

    public void UpdateState()
    {
        var stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("GetHit")) return;

        float distanceToPlayer = _bossBase.CalculateDistanceToPlayer();
        Transform player = _bossBase.PlayerTrans;

        if (_bossBase.MetaStateMachine.CurrentPhaseState is NormalPhase)
        {
            _bossBase.HandleChasing();
            return;
        }

        _bossBase.HandleChasing();

        if (distanceToPlayer <= _bossBase.AttackRange &&
                _skillManager.IsSkillReady(BossSkillType.Melee))
        {
            if (_meleeEnterTime < 0f) _meleeEnterTime = Time.time; // bắt đầu đếm thời gian
            if (Time.time - _meleeEnterTime >= _requiredStayTime)
            {
                Cast(BossSkillType.Melee, ActionState.knifeSkill.ToString(), player);
                _meleeEnterTime = -1f;
            }
        }
        else _meleeEnterTime = -1f; // reset nếu player ra khỏi vùng

        if (distanceToPlayer > _bossBase.AttackRange && distanceToPlayer <= 10f &&
                _skillManager.IsSkillReady(BossSkillType.SpitFire))
        {
            if (_spitFireEnterTime < 0f) _spitFireEnterTime = Time.time;
            if (Time.time - _spitFireEnterTime >= _requiredStayTime)
            {
                Cast(BossSkillType.SpitFire, ActionState.spitFireSkill.ToString(), player);
                _spitFireEnterTime = -1f;
            }
        }
        else _spitFireEnterTime = -1f;

        if (distanceToPlayer > 10f && distanceToPlayer <= 12f &&
            _skillManager.IsSkillReady(BossSkillType.Magic))
        {
            Cast(BossSkillType.Magic, ActionState.magicSkill.ToString(), player);
        }

        if (distanceToPlayer > 12f)
        {
            _timer += Time.deltaTime;
            if (_timer >= _maxChasingTime &&
                _skillManager.IsSkillReady(BossSkillType.Jump))
            {
                Cast(BossSkillType.Jump, ActionState.jumpSkill.ToString(), player);
            }
        }
        else _timer = 0f;
    }

    private void Cast(BossSkillType type, string animKey, Transform target)
    {
        _animator.SetTrigger(ActionState.castSkill.ToString());
        _stateMachine.ChangeState(new CastSkillState(_bossBase, animKey, _stateMachine));
        _skillManager.TryCastSkill(type, _bossBase.transform, target);
    }

    public void ExitState()
    {
        _animator.SetBool(ActionState.isMoving.ToString(), false);
    }
}
