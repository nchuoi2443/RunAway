﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BossBase : MonoBehaviour
{
    public MetaStateMachine MetaStateMachine;
    public SkillManager SkillManager;
    public Transform PlayerTrans;
    public BossHealth BossHealth;

    public float MoveSpeed = 3f;
    public float Hp = 100;
    public float MeleeRange = 5f;
    public float SpitFireRange = 7f;
    public float MagicRange = 8f;
    public float OverRange = 10f;

    public event System.Action OnTakeDamage;

    private void Start()
    {
        PlayerTrans = FindObjectOfType<PlayerHealth>().transform;
    }

    public void BossUpdate()
    {
        FlipToFacingPlayer();
    }
    
    public void FlipToFacingPlayer()
    {
        if (CastSkillState.Instance != null) return; 

        Vector3 direction = PlayerTrans.position - transform.position;
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }


    public void HandleChasing()
    {
        transform.position = Vector3.MoveTowards(transform.position, PlayerTrans.position, Time.deltaTime * MoveSpeed);
    }

    public float CalculateDistanceToPlayer()
    {
        return Vector3.Distance(transform.position, PlayerTrans.position);
    }

    void HandleDead()
    {
        Debug.Log("Boss is dead");
    }

    public void TakeDamage(float dmg)
    {
        Hp -= dmg;

        OnTakeDamage?.Invoke();

        if (Hp <= 0)
        {
            if (MetaStateMachine.CurrentPhaseState is NormalPhase)
            {
                MetaStateMachine.ChangePhase(new RagePhase(this, MetaStateMachine));
            }
            else
            {
                HandleDead();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //gameObject.GetComponent<BossHealth>().TakeDamage(2);
    }
}
