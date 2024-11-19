using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : EnemyBase
{
    protected override void Start()
    {
        base.Start();
        typeEnemy = typeOfEnemy.chasingEnemy;
    }

    private void Update()
    {
        if (Target)
        {
            GetTarget();
        }
    }

    public void FixedUpdate()
    {
        if (IsBocked || PlayerHealth.Instance.isDead)
        {
            return;
        }
        //move forward
        if (!KnockBack.isKnockBack)
        {
            MoveTowardPlayer();
        }
    }
    public override void EnemyAttacking()
    {
        
    }

    public override void Shoot()
    {
        
    }

    
}
