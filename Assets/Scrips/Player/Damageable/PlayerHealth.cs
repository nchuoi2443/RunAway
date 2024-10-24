using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private float knockBackThrust = 15f;
    [SerializeField] private float damageRecoveryTime = 1f;

    private int currentHealth;
    private KnockBack knockBack;
    private GetHit getHit;
    private bool canTakeDamage = true;

    private void Awake()
    {
        currentHealth = maxHealth;
        knockBack = GetComponent<KnockBack>();
        getHit = GetComponent<GetHit>();
    }

    public void TakeDamage(int damage)
    {
        if (!canTakeDamage) return;
        currentHealth -= damage;
        
        StartCoroutine(getHit.GetHitEffect());
        if (currentHealth <= 0)
        {
            //Die();
        }
        else
        {
            StartCoroutine(RecoverFromDamage());
        }
    }

    private IEnumerator RecoverFromDamage()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemy)
        {
            knockBack.GetKnockBack(collision.gameObject.transform, knockBackThrust);
            TakeDamage(1);
        } 
            
    }
}
