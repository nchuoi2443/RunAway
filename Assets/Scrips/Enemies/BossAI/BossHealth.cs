using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : EnemyHealth
{
    private MetaStateMachine _metaStateMachine;
    private Animator _animator;
    private float _maxHealthRageState;

    public override void Awake()
    {
        base.Awake();
        _metaStateMachine = GetComponent<MetaStateMachine>();
        _animator = GetComponent<Animator>();
        isImmuneDamage = false;
        _maxHealthRageState = 40;
    }

    public override void HPRunOut()
    {
        if (_metaStateMachine.CurrentPhaseState is NormalPhase)
        {
            MaxHealth = _maxHealthRageState;
            CurrentHealth = _maxHealthRageState;
            StartCoroutine(ImmuneDamageWhenTransform("Tranformation"));
            _metaStateMachine.ChangePhase(new RagePhase(gameObject.GetComponent<BossBase>(), _metaStateMachine));
        }
        else
        {
            
            _animator.SetTrigger(ActionState.isDeath.ToString());
        }
    }

    public override void CalculateDamage(float damage)
    {
        float damageTaken = playerStats.CalculateDamage(damage);
        CurrentHealth -= damageTaken;
        CharacterEvents.characterTookDmg.Invoke(gameObject, damageTaken, playerStats.IsCrit);

        if (CastSkillState.Instance != null) return; //can't be interrupted when cast skill

        if (_metaStateMachine.CurrentPhaseState is NormalPhase)
        {
            gameObject.GetComponent<Animator>().SetTrigger(ActionState.getHit_Sub.ToString());
        }
        else
        {
            gameObject.GetComponent<Animator>().SetTrigger(ActionState.getHit_Base.ToString());
        }
    }

    private IEnumerator ImmuneDamageWhenTransform(string stateName)
    {
        _animator.SetTrigger(ActionState.doTransform.ToString());
        isImmuneDamage = true;

        while (!_animator.GetCurrentAnimatorStateInfo(0).IsName(stateName))
            yield return null;

        while (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            yield return null;

        isImmuneDamage = false;
    }
}
