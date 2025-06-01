using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : EnemyHealth
{
    private MetaStateMachine _metaStateMachine;
    public Animator Animator;
    private float _maxHealthRageState;
    public bool IsDead = false;

    public override void Awake()
    {
        base.Awake();
        _metaStateMachine = GetComponent<MetaStateMachine>();
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
            IsDead = true;
            Animator.SetTrigger(ActionState.isDeath.ToString());
            Debug.Log("BossHealth: SetTrigger isDeath called, IsDead=" + IsDead);
            FindObjectOfType<PickUpSpawner>().SpawnPickUp(5);
            EnemySpawner.Instance.IsEndBossWay();
            StartCoroutine(WaintToDestroyBoss());
            
        }
    }

    IEnumerator WaintToDestroyBoss()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
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
        Animator.SetTrigger(ActionState.doTransform.ToString());
        isImmuneDamage = true;

        while (!Animator.GetCurrentAnimatorStateInfo(0).IsName(stateName))
            yield return null;

        while (Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            yield return null;

        isImmuneDamage = false;
    }
}
