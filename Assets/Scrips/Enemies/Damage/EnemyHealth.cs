using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private GameObject deadVFXFrefab;
    private int currentHealth;
    private KnockBack knockBack;
    private float knockBackThrust = 15f;
    private GetHit getHit;

    private void Awake()
    {
        currentHealth = maxHealth;
        knockBack = GetComponent<KnockBack>();
        getHit = GetComponent<GetHit>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        knockBack.GetKnockBack(PlayerController.Instance.transform, knockBackThrust);
        StartCoroutine(getHit.GetHitEffect());
        if (currentHealth <= 0)
        {
            Instantiate(deadVFXFrefab, transform.position, quaternion.identity);
            //Destroy(deadVFX, 1f);
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
