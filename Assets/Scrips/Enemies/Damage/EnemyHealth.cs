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
    //tạo một biến để lưu loại enemy, dùng để xác định điểm khi enemy die, đồng thời tạo một biến khác để truy cập vào object của enemyheath, từ đó lấy được script baseEnemy và lấy được loại enemy từ baseEnemy
    // Variable to store the type of enemy
    private string typeOfEnemy;
    private EnemyBase baseEnemy;

    private void Awake()
    {
        currentHealth = maxHealth;
        knockBack = GetComponent<KnockBack>();
        getHit = GetComponent<GetHit>();
        baseEnemy = GetComponent<EnemyBase>();
        if (baseEnemy != null)
        {
            typeOfEnemy = baseEnemy.typeEnemy.ToString();
            Debug.Log(typeOfEnemy);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        knockBack.GetKnockBack(PlayerController.Instance.transform, knockBackThrust);
        StartCoroutine(getHit.GetHitEffect());
        if (currentHealth <= 0)
        {
            Instantiate(deadVFXFrefab, transform.position, quaternion.identity);

            if (typeOfEnemy == "chasingEnemy") LevelManager.Instance.updateScore(1);
            else if (typeOfEnemy == "rangeEnemy") LevelManager.Instance.updateScore(3);
            //Destroy(deadVFX, 1f);
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
