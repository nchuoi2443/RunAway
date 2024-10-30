using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    void GetTarget();
    void FacingPlayer();
    void MoveTowardPlayer();
    void SetRandomDirection(Vector2 collisionNormal);
    IEnumerator MoveToRandomPos();
    void Shoot();
    void EnemyAttacking();
    void OnCollisionEnter2D(Collision2D collision);
}
