using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    [SerializeField] private int damageOnEnemy = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Trigger Collision");
        if (collision.gameObject.GetComponent<EnemyHealth>())
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(damageOnEnemy);
        }
    }
}

