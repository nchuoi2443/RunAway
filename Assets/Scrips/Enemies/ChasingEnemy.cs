using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemy : Enemy
{
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
            rotateTowardTarget();
        }
    }

    private void FixedUpdate()
    {
        //move forward
        Rb.velocity = transform.up * Speed;
    }
}
