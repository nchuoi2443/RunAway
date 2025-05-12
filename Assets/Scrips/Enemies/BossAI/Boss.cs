using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BossBase : MonoBehaviour
{
    public MetaStateMachine MetaStateMachine;
    public SkillManager SkillManager;
    public Transform Player;

    public float MoveSpeed = 3f;
    public float Hp = 100;
    public float AttackRange = 5f;
    public float ChasingRange = 10f;

    public event System.Action OnTakeDamage;

    public void BossUpdate()
    {
        
    }

    public bool HandleIdle()
    {
        if (Vector3.Distance(transform.position, Player.position) < ChasingRange)
        {
            return false;
        }
        return true;
    }

    public void HandleChasing()
    {
        transform.position = Vector3.MoveTowards(transform.position, Player.position, Time.deltaTime * MoveSpeed);
    }

    public float CalculateDistanceToPlayer()
    {
        return Vector3.Distance(transform.position, Player.position);
    }

    void HandleAttacking()
    {
        SkillManager.TryCastSkill(0); // Ví dụ luôn cast skill đầu tiên
       
    }

    void HandleUsingSkill()
    {
        // Có thể thêm logic riêng nếu boss chuyển sang mode xài skill đặc biệt
    }

    void HandleEnraged()
    {
        // Tăng tốc độ, cooldown giảm, v.v.
    }

    void HandleDead()
    {
        Debug.Log("Boss is dead");
    }

    public void TakeDamage(float dmg)
    {
        Hp -= dmg;

        OnTakeDamage?.Invoke();

        if (Hp <= 0)
        {
            if (MetaStateMachine.CurrentPhaseState is NormalPhase)
            {
                MetaStateMachine.ChangePhase(new RagePhase(this, MetaStateMachine));
            }
            else
            {
                HandleDead();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TakeDamage(10);
    }
}
