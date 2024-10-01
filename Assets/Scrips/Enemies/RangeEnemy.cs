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

    private void Awake()
    {
        fireRateTimer = fireRate;
        TypeOfEnemy = "rangeEnemy";
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
            rotateTowardTarget();
        }
    }

    //add enemy with max range attack and fire range have the same value, and will update after done that!
    private void FixedUpdate()
    {
        //if (Target == null) return;
        if (Vector2.Distance(Target.position, transform.position) > attackRange)
        {
            //move forward
            Rb.velocity = transform.up * Speed;
        } else
        {
            
            if (fireRateTimer <= 0)
            {
                Shoot();
                fireRateTimer = fireRate;
            } else
            {
                fireRateTimer -= Time.deltaTime;
            }
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePos.position, firePos.rotation);
    }
}
