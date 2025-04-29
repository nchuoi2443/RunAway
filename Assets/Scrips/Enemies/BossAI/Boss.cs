using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBase : MonoBehaviour
{
    public Transform Player;
    public float AttackRange = 3f;
    public float CurrentHealth = 100f;

    public void MoveTo(Vector3 pos) => /* navmesh or move logic */ Debug.Log($"Moving to {pos}");
    public void PlayAnimation(string anim) => Debug.Log($"Playing animation: {anim}");
    public void Attack() => Debug.Log("Boss attacking!");
    public void PowerAttack() => Debug.Log("Boss RAGE attack!");
    public void CastUltimate() => Debug.Log("Boss casting ultimate!");

    public bool CanSeePlayer() => true; // Dummy
    public float DistanceToPlayer() => Vector3.Distance(transform.position, Player.position);
    public bool IsSkillDone() => true; // Dummy
}
