using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Singleton<PlayerHealth>
{
    public bool isDead { get; private set; }

    [SerializeField] private float maxHealth = 10;
    [SerializeField] private float knockBackThrust = 15f;
    [SerializeField] private float damageRecoveryTime = 1f;
    [SerializeField] private TextMeshProUGUI _currentHealTxt;

    private Slider healthBar;
    private float currentHealth;
    private KnockBack knockBack;
    private GetHit getHit;
    private bool canTakeDamage = true;

    const string DEATH_HASH = "Dead";

    // Replace the problematic property with a standard property implementation
    public float MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }
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
        StartCoroutine(RegenHealth());
    }

    private IEnumerator RegenHealth()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            HealthRecovery();
        }
    }

    private void HealthRecovery()
    {
        currentHealth = Mathf.Min(currentHealth + PlayerBaseStats.Instance.BaseSelfHealingRate, maxHealth);
        UpdateHealSlider();
    }

    public void TakeDamage(float damage)
    {
        if (!canTakeDamage) return;
        float finalDamage = PlayerBaseStats.Instance.CalculateDamagePlayerReceived(damage);
        currentHealth -= finalDamage;
        CharacterEvents.characterTookDmg.Invoke(gameObject, damage, false);
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

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        isDead = false;
        UpdateHealSlider();
    }

    public void UpdateHealSlider()
    {
        if(healthBar == null)
        {
            healthBar = GameObject.Find("HealthSlider").GetComponent<Slider>();
        }
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
        _currentHealTxt.text = currentHealth.ToString("F1") + "/" + maxHealth.ToString();
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
        
    }

    private void Die()
    {
        Destroy(gameObject);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        string bossTag = collision.gameObject.tag;
        if (enemy != null)
        {
            knockBack.GetKnockBack(collision.gameObject.transform, knockBackThrust);
            TakeDamage(1);
        }
        else if (bullet != null)
        {
            if (bullet) Destroy(bullet.gameObject);
            knockBack.GetKnockBack(collision.gameObject.transform, knockBackThrust);
            TakeDamage(bullet.BulletDamage);
        }
        else if (bossTag == "BossCollider")
        {
            BossBase bossBase = collision.gameObject.GetComponent<BossBase>();
            if (bossBase.MetaStateMachine.CurrentPhaseState is NormalPhase)
            {
                knockBack.GetKnockBack(bossBase.transform, knockBackThrust);
                TakeDamage(3);
            }
            else
            {
                knockBack.GetKnockBack(bossBase.transform, knockBackThrust);
                TakeDamage(5);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BossBase bossBase = FindObjectOfType<BossBase>();
        string bossTag = collision.gameObject.tag;
        if (bossTag == "BossCollider")
        {
            knockBack.GetKnockBack(bossBase.gameObject.transform, knockBackThrust);
            TakeDamage(1);
        }
    }
}
