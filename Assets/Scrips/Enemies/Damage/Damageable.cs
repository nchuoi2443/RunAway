using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    [SerializeField] private float damageOnEnemy = 2;
    //private PlayerBaseStats playerStats;

    private void Start()
    {
        //playerStats = PlayerController.Instance.GetComponent<PlayerBaseStats>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        //enemyHealth?.TakeDamage(playerStats.CalculateDamage(damageOnEnemy));
        enemyHealth?.TakeDamage(damageOnEnemy);
    }
}

