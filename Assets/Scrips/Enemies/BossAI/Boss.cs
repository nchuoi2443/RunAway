using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBase : MonoBehaviour
{
    public BossState CurrentState;
    public float Hp = 100;
    public SkillManager SkillManager;
    public Transform Player;
    public float AttackRange = 5f;

    private void Update()
    {
        switch (CurrentState)
        {
            case BossState.Idle:
                HandleIdle();
                break;
            case BossState.Chasing:
                HandleChasing();
                break;
            case BossState.Attacking:
                HandleAttacking();
                break;
            case BossState.UsingSkill:
                HandleUsingSkill();
                break;
            case BossState.Enraged:
                HandleEnraged();
                break;
            case BossState.Dead:
                HandleDead();
                break;
        }
    }

    void HandleIdle()
    {
        if (Vector3.Distance(transform.position, Player.position) < AttackRange)
        {
            CurrentState = BossState.Attacking;
        }
    }

    void HandleChasing()
    {
        transform.position = Vector3.MoveTowards(transform.position, Player.position, Time.deltaTime * 3);
        if (Vector3.Distance(transform.position, Player.position) < AttackRange)
        {
            CurrentState = BossState.Attacking;
        }
    }

    void HandleAttacking()
    {
        SkillManager.TryCastSkill(0); // Ví dụ luôn cast skill đầu tiên
        CurrentState = BossState.Idle;
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
        if (Hp <= 0 && CurrentState != BossState.Dead)
        {
            CurrentState = BossState.Dead;
        }
    }
}
