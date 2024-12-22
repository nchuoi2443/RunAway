using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private GameObject deadVFXFrefab;

    private float currentHealth;
    private KnockBack knockBack;
    private float knockBackThrust = 15f;
    private GetHit getHit;
    //tạo một biến để lưu loại enemy, dùng để xác định điểm khi enemy die, đồng thời tạo một biến khác để truy cập vào object của enemyheath, từ đó lấy được script baseEnemy và lấy được loại enemy từ baseEnemy
    // Variable to store the type of enemy
    private string typeOfEnemy;
    private EnemyBase baseEnemy;

    private PlayerBaseStats playerStats;
    private void Awake()
    {
        currentHealth = maxHealth;
        knockBack = GetComponent<KnockBack>();
        getHit = GetComponent<GetHit>();
        baseEnemy = GetComponent<EnemyBase>();

        playerStats = GameObject.Find("Player").GetComponent<PlayerBaseStats>();

        if (baseEnemy != null)
        {
            typeOfEnemy = baseEnemy.typeEnemy.ToString();
        }
    }

    public void TakeDamage(float damage)
    {
        float damageTaken = playerStats.CalculateDamage(damage);
        currentHealth -= damageTaken;
        CharacterEvents.characterTookDmg.Invoke(gameObject, damageTaken, playerStats.IsCrit);

        knockBack.GetKnockBack(PlayerController.Instance.transform, knockBackThrust);
        StartCoroutine(getHit.GetHitEffect());
        UpdateScore();
    }

    private void UpdateScore()
    {
        if (currentHealth <= 0)
        {
            Instantiate(deadVFXFrefab, transform.position, quaternion.identity);

            if (typeOfEnemy == "chasingEnemy")
            {
                LevelManager.Instance.updateScore(1);
            }
            else if (typeOfEnemy == "rangeEnemy")
            {
                LevelManager.Instance.updateScore(3);
            }

            Die();
        }
    }

    private void Die()
    {
        GetComponent<PickUpSpawner>().SpawnPickUp(2);
        Destroy(gameObject);
        GameManager.Instance.RemoveEnemy(baseEnemy);
    }
}
