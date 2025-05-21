using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

public class ProjectTileSpawner : MonoBehaviour
{
    
    public float spreadAngle = 60f;
    public int bulletCount = 10;
    public float bulletSpeed = 5f;
    public float bulletLifetime = 5f;
    public float delayBetweenBullets = 0.1f;
    public float waveDuration = 5f;
    public Transform firePoint;

    private Transform target;
    private Coroutine currentWaveCoroutine;
    private bool sweepingRight = true;


    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnEnable()
    {
        ResetSpawnerState();
        currentWaveCoroutine = StartCoroutine(SpawnWaveCoroutine());
    }

    private void OnDisable()
    {
        if (currentWaveCoroutine != null)
        {
            StopCoroutine(currentWaveCoroutine);
            currentWaveCoroutine = null;
        }
    }

    private void ResetSpawnerState()
    {
        sweepingRight = true;
        // Nếu có thêm state nào khác (cooldown, phase, v.v.) thì reset ở đây
    }

    private void CheckTurnRight()
    {
        float distanceToTargetX = target.position.x - firePoint.position.x;
        if (distanceToTargetX < 0)
        {
            sweepingRight = false;
        }
        else if (distanceToTargetX > 0)
        {
            sweepingRight = true;
        }
    }

    private IEnumerator SpawnWaveCoroutine()
    {
        float elapsedTime = 0f;
        CheckTurnRight();
        while (elapsedTime < waveDuration)
        {
            float startAngle = -spreadAngle / 2f;
            float angleStep = spreadAngle / (bulletCount - 1);

            if (!sweepingRight)
            {
                startAngle = spreadAngle / 2f;
                angleStep = -angleStep;
            }

            for (int i = 0; i < bulletCount; i++)
            {
                float angle = startAngle + i * angleStep;
                Vector2 dir = Quaternion.Euler(0, 0, angle) * Vector2.right;

                float finalSpeed = sweepingRight ? bulletSpeed : -bulletSpeed;

                GameObject bullet = BulletPooling.Instance.GetBullet();
                bullet.transform.position = firePoint.position;
                bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
                bullet.SetActive(true);

                bullet.GetComponent<BossProjectTile>().Init(dir, finalSpeed, bulletLifetime);

                yield return new WaitForSeconds(delayBetweenBullets);
                elapsedTime += delayBetweenBullets;

                if (elapsedTime >= waveDuration)
                    yield break;
            }
        }
    }


    /*[Button ("Fire")]
    public void Fire()
    {
        StartWave(4);
    }*/
}
