using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Singleton<PlayerHealth>
{
    public bool isDead { get; private set; }

    [SerializeField] private int maxHealth = 10;
    [SerializeField] private float knockBackThrust = 15f;
    [SerializeField] private float damageRecoveryTime = 1f;

    private Slider healthBar;
    private int currentHealth;
    private KnockBack knockBack;
    private GetHit getHit;
    private bool canTakeDamage = true;

    private const string TOWN_TEXT = "scene1";
    const string DEATH_HASH = "Dead";
    protected override void Awake()
    {
        currentHealth = maxHealth;
        knockBack = GetComponent<KnockBack>();
        getHit = GetComponent<GetHit>();
        isDead = false;
    }

    private void Start()
    {
        UpdateHealSlider();
    }

    public void TakeDamage(int damage)
    {
        if (!canTakeDamage) return;
        currentHealth -= damage;
        StartCoroutine(getHit.GetHitEffect());
        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            Destroy(ActiveWeapon.Instance.gameObject);
            currentHealth = 0;
            GetComponent<Animator>().SetTrigger(DEATH_HASH);
            Debug.Log("Player died");
            StartCoroutine(DelayedGameOver());

            //Die();

        }
        else
        {
            StartCoroutine(RecoverFromDamage());
            UpdateHealSlider();
        }
    }

    private void UpdateHealSlider()
    {
        if(healthBar == null)
        {
            healthBar = GameObject.Find("HealthSlider").GetComponent<Slider>();
        }
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }


    private IEnumerator RecoverFromDamage()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }

    private IEnumerator DelayedGameOver()
    {
        yield return new WaitForSeconds(2f); // Wait for 2 seconds
        LevelManager.Instance.GameOver();
        LevelManager.Instance.GameOver();
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        if (enemy || bullet)
        {
            if(bullet) Destroy(bullet.gameObject);
            knockBack.GetKnockBack(collision.gameObject.transform, knockBackThrust);
            TakeDamage(1);
        } 
            
    }
}
