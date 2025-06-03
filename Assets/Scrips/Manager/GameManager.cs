using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private EnemyFactory enemyFactory;
    [SerializeField] private float _timeSpawnRange = 5f;
    [SerializeField] private float _timeSpawnChasingEnemy = 4f;
    //public List<EnemyBase> enemies = new List<EnemyBase>();
    [SerializeField] private Transform _bossSpawnPos;
    [SerializeField] private BossUIHandle _bossUIHanle;

    [SerializeField] private List<Transform> spawnPositions = new List<Transform>();
    public int ChasingEnemyNum;
    public int RangeEnemyNum;
    private Coroutine chasingCoroutine;
    private Coroutine rangeCoroutine;
    private List<EnemyBase> enemies = new List<EnemyBase>();

    private int _totalEnemiesInBattleField;
    public int MaxEnemiesInBattleField = 7;

    public List<EnemyBase> Enemies { get { return enemies; } set { enemies = value; } }
    private void Start()
    {
        StartSpawning();
    }

    public Transform GetRandomSpawnPosition()
    {
        return spawnPositions[Random.Range(0, spawnPositions.Count)];
    }

    public void StartSpawning()
    {
        if (chasingCoroutine == null)
            chasingCoroutine = StartCoroutine(SpawnChasingEnemies());
        if (rangeCoroutine == null)
            rangeCoroutine = StartCoroutine(SpawnRangeEnemies());
    }

    public void StopSpawning()
    {
        if (chasingCoroutine != null)
        {
            StopCoroutine(chasingCoroutine);
            chasingCoroutine = null;
        }
        if (rangeCoroutine != null)
        {
            StopCoroutine(rangeCoroutine);
            rangeCoroutine = null;
        }
    }

    private IEnumerator SpawnChasingEnemies()
    {
        while (_totalEnemiesInBattleField >= MaxEnemiesInBattleField)
        {
            yield return null;
        }

        for (int i = 0; i < ChasingEnemyNum; i++)
        {
            SpawnEnemy(EnemyType.ChasingEnemy, GetRandomSpawnPosition().position);
            _totalEnemiesInBattleField++;
            yield return new WaitForSeconds(_timeSpawnChasingEnemy);
        }
        chasingCoroutine = null;
    }

    private IEnumerator SpawnRangeEnemies()
    {
        while (_totalEnemiesInBattleField >= MaxEnemiesInBattleField)
        {
            yield return null;
        }

        for (int i = 0; i < RangeEnemyNum; i++)
        {
            SpawnEnemy(EnemyType.RangeEnemy, GetRandomSpawnPosition().position);
            _totalEnemiesInBattleField++;
            yield return new WaitForSeconds(_timeSpawnRange);
        }
        rangeCoroutine = null;
    }

    public void SpawnBoss()
    {
        enemyFactory.CreateEnemy(EnemyType.Boss, _bossSpawnPos.position);
        PlayerHealth.Instance.ResetHealth();
        _bossUIHanle.gameObject.SetActive(true);
    }

    public void RemoveEnemy(EnemyBase enemy)
    {
        enemies.Remove(enemy);
        _totalEnemiesInBattleField--;
    }

    public void RemoveAllEnemies()
    {
        _totalEnemiesInBattleField = 0;
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
