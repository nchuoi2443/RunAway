using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemy : Enemy
{
    public override void Start()
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

    private void FixedUpdate()
    {
        //move forward
        moveTorwardPlayer();
    }
}
