using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : EnemyBase
{
    protected override void Start()
    {
        base.Start();
        typeEnemy = typeOfEnemy.FollowEnemy;
    }

    private void Update()
    {
        if (Target)
        {
            GetTarget();
        }
        if (Target == null)
        {
            Debug.Log("we got nothing");
        }
        else
        {
            //rotateTowardTarget();
        }
    }

    public void FixedUpdate()
    {
        if (IsBocked)
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
