using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RangeEnemy : MonoBehaviour
{
    private GameObject bulletPrefab;
    [Range(5f, 10f)]
    [SerializeField] private float attackRange = 5f;
    [Range(0.5f, 3f)]
    [SerializeField] private float fireRate = 1.5f;
    private float fireRateTimer;
    [SerializeField] private Transform firePos;


}
