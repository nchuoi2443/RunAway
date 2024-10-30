using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RangeEnemy : Enemy
{
    [SerializeField] private GameObject bulletPrefab;
    [Range(5f, 10f)]
    [SerializeField] private float attackRange = 5f;
    [Range(0.5f, 3f)]
    [SerializeField] private float fireRate = 1.5f;
    private float fireRateTimer;
    [SerializeField] private Transform firePos;
    private bool canAttack;

    protected override void Start()
    {
        base.Start();
        fireRateTimer = fireRate;
        TypeOfEnemy = "rangeEnemy";
        if (Target!)
        {
            getTarget();
        }
    }

    private void Update()
    {
        
        facingPlayer();
    }

    //add enemy with max range attack and fire range have the same value, and will update after done that!
    public override void FixedUpdate()
    {
        if (IsBocked)
        {
            return;
        }
        if (!KnockBack.isKnockBack)
        {
            if (Vector2.Distance(Target.position, transform.position) > attackRange)
            {
                moveTorwardPlayer();

            }
            else
            {
                enemyAttacking();
                //Debug.Log("hello");
            }
        }
    }

    public void enemyAttacking()
    {
        if (fireRateTimer <= 0)
        {
            EnemyAnimator.SetTrigger("onRanged");
            fireRateTimer = fireRate;
        }
        else
        {
            fireRateTimer -= Time.deltaTime;
        }

    }

    public void Shoot()
    {
        Instantiate(bulletPrefab, firePos.position, firePos.rotation);
    }
}
