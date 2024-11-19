using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy : EnemyBase
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
        typeEnemy = typeOfEnemy.rangeEnemy;
        if (Target!)
        {
            GetTarget();
        }
    }

    private void Update()
    {

        FacingPlayer();
    }

    public void FixedUpdate()
    {
        if (IsBocked || PlayerHealth.Instance.isDead)
        {
            return;
        }
        if (!KnockBack.isKnockBack)
        {
            if (Vector2.Distance(Target.position, transform.position) > attackRange)
            {
                MoveTowardPlayer();

            }
            else
            {
                EnemyAttacking();
                //Debug.Log("hello");
            }
        }
    }

    public override void EnemyAttacking()
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

    public override void Shoot()
    {
        Instantiate(bulletPrefab, firePos.position, firePos.rotation);
    }
}
