using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemy : Enemy
{
    protected override void Start()
    {
        base.Start();
        TypeOfEnemy = "chasingEnemy";
    }
    private void Update()
    {
        if (Target!)
        {
            getTarget();
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
    
    public override void FixedUpdate()
    {
        if (IsBocked)
        {
            return;
        }
        //move forward
        if (!KnockBack.isKnockBack)
        {
            moveTorwardPlayer();
        }
    }
}
