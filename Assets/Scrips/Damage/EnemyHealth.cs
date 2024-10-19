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


    private void Awake()
    {
        currentHealth = maxHealth;
        knockBack = GetComponent<KnockBack>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        knockBack.GetKnockBack(PlayerController.Instance.transform, 15f);
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
