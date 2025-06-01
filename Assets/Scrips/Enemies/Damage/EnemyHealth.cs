using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private GameObject deadVFXFrefab;

    public float CurrentHealth;
    public float MaxHealth = 100;
    private float knockBackThrust = 15f;
    private string typeOfEnemy;
    private KnockBack knockBack;
    private GetHit getHit;
    private EnemyBase baseEnemy;
    protected bool isImmuneDamage = false;
    protected PlayerBaseStats playerStats;

    public virtual void Awake()
    {
        CurrentHealth = MaxHealth;
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
        if (isImmuneDamage)
        {
            return;
        }

        CalculateDamage(damage);
        knockBack.GetKnockBack(PlayerController.Instance.transform, knockBackThrust);
        StartCoroutine(getHit.GetHitEffect());
        UpdateScore();
    }

    private void UpdateScore()
    {
        if (CurrentHealth <= 0)
        {
            if (typeOfEnemy == null)
            {
                HPRunOut();
                return;
            }
            
            Instantiate(deadVFXFrefab, transform.position, quaternion.identity);

            if (typeOfEnemy == "chasingEnemy")
            {
                LevelManager.Instance.updateScore(1);
            }
            else if (typeOfEnemy == "rangeEnemy")
            {
                LevelManager.Instance.updateScore(3);
            }

            HPRunOut();
        }
    }

    public virtual void HPRunOut()
    {
        GetComponent<PickUpSpawner>().SpawnPickUp(2);
        GameManager.Instance.RemoveEnemy(baseEnemy);
        Destroy(gameObject);
    }

    public virtual void CalculateDamage(float damage)
    {
        float damageTaken = playerStats.CalculateDamage(damage);
        CurrentHealth -= damageTaken;
        CharacterEvents.characterTookDmg.Invoke(gameObject, damageTaken, playerStats.IsCrit);
        gameObject.GetComponent<Animator>().SetTrigger(ActionState.getHit.ToString());
    }
}
