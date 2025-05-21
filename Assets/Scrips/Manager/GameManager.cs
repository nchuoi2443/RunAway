using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private EnemyFactory enemyFactory;
    [SerializeField] private float timeBetweenSpawn = 5f;
    //public List<EnemyBase> enemies = new List<EnemyBase>();
    [SerializeField] private Transform _bossSpawnPos;

    [SerializeField] private List<Transform> spawnPositions = new List<Transform>();

    private int chasingEnemyNum;
    private int rangeEnemyNum;

    private List<EnemyBase> enemies = new List<EnemyBase>();

    public List<EnemyBase> Enemies { get { return enemies; } set { enemies = value; } }
    private void Start()
    {
        chasingEnemyNum = 3;
        rangeEnemyNum = 2;
        StartCoroutine(SpawnEnemies());
    }

    //random vị trí xuất hiện của kẻ địch
    public Transform GetRandomSpawnPosition()
    {
        return spawnPositions[Random.Range(0, spawnPositions.Count)];
    }

    public IEnumerator SpawnEnemies()
    {
        // Tạo 3 kẻ địch loại Chasing
        for (int i = 0; i < chasingEnemyNum; i++)
        {
            //enemies.Add(enemyFactory.CreateEnemy("chasingEnemy", GetRandomSpawnPosition().position).GetComponent<FollowEnemy>());
            SpawnEnemy(EnemyType.ChasingEnemy, GetRandomSpawnPosition().position);
            yield return new WaitForSeconds(timeBetweenSpawn);
        }

        // Tạo 2 kẻ địch loại Range
        for (int i = 0; i < rangeEnemyNum; i++)
        {
            //enemies.Add(enemyFactory.CreateEnemy("rangeEnemy", GetRandomSpawnPosition().position).GetComponent<ShootEnemy>());
            SpawnEnemy(EnemyType.RangeEnemy, GetRandomSpawnPosition().position);
            yield return new WaitForSeconds(timeBetweenSpawn);
        }
    }

    public void SpawnBoss()
    {
        enemyFactory.CreateEnemy(EnemyType.Boss, _bossSpawnPos.position);
    }

    public void RemoveEnemy(EnemyBase enemy)
    {
        enemies.Remove(enemy);
    }

    public void RemoveAllEnemies()
    {
        if (enemies.Count == 0) return;
        foreach (var enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
        enemies.Clear();
    }

    public void SpawnEnemy(EnemyType type, Vector3 spawnPosition)
    {
        enemies.Add(enemyFactory.CreateEnemy(type, spawnPosition).GetComponent<EnemyBase>());
    }
}
