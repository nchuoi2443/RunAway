using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : IState
{
    private BossBase _bossBase;
    private Animator _animator;
    private float _timer;
    private float _maxChasingTime;

    private float _knifeCooldown = 5f;
    private float _jumpCooldown = 8f;
    private float _spitFireCooldown = 6f;
    private float _castMagicCooldown = 10f;

    private float _lastKnifeTime = -Mathf.Infinity;
    private float _lastJumpTime = -Mathf.Infinity;
    private float _lastSpitFireTime = -Mathf.Infinity;
    private float _lastCastMagicTime = -Mathf.Infinity;

    private StateMachine _stateMachine;

    public RunState(BossBase bossBase, StateMachine stateMachine)
    {
        this._bossBase = bossBase;
        _stateMachine = stateMachine;
    }

    public void EnterState()
    {
        _animator = _bossBase.GetComponent<Animator>();
        _animator.SetBool(ActionState.isMoving.ToString(), true);
        _timer = 0f;
        _maxChasingTime = 5f;
    }

    public void UpdateState()
    {
        float distanceToPlayer = _bossBase.CalculateDistanceToPlayer();

        if (_bossBase.MetaStateMachine.CurrentPhaseState is NormalPhase)
        {
            _bossBase.HandleChasing();
        }
        else // Rage phase
        {
            if (distanceToPlayer <= _bossBase.AttackRange && Time.time >= _lastKnifeTime + _knifeCooldown)
            {
                _animator.SetTrigger(ActionState.castSkill.ToString());
                _stateMachine.ChangeState(new CastSkillState(_bossBase, ActionState.knifeSkill.ToString(), _stateMachine));
                _lastKnifeTime = Time.time;
            }
            else if (distanceToPlayer > _bossBase.AttackRange && distanceToPlayer <= 10f && Time.time >= _lastSpitFireTime + _spitFireCooldown)
            {
                _animator.SetTrigger(ActionState.castSkill.ToString());
                _stateMachine.ChangeState(new CastSkillState(_bossBase, ActionState.spitFireSkill.ToString(), _stateMachine));
                _lastSpitFireTime = Time.time;
            }
            else if (distanceToPlayer > 10f && Time.time >= _lastCastMagicTime + _castMagicCooldown)
            {
                _animator.SetTrigger(ActionState.castSkill.ToString());
                _stateMachine.ChangeState(new CastSkillState(_bossBase, ActionState.magicSkill.ToString(), _stateMachine));
                _lastCastMagicTime = Time.time;
            }
            else
            {
                _timer += Time.deltaTime;
                if (_timer >= _maxChasingTime && Time.time >= _lastJumpTime + _jumpCooldown)
                {
                    _animator.SetTrigger(ActionState.castSkill.ToString());
                    _stateMachine.ChangeState(new CastSkillState(_bossBase, ActionState.jumpSkill.ToString(), _stateMachine));
                    _lastJumpTime = Time.time;
                }
            }
        }
    }

    public void ExitState()
    {
        
    }
}
