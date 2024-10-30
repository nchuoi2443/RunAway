using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private EnemyFactory enemyFactory;
    private float timeBetweenSpawn = 5f;
    public List<EnemyBase> enemies = new List<EnemyBase>();

    [SerializeField] private List<Transform> spawnPositions = new List<Transform>();
    private void Start()
    {
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
        for (int i = 0; i < 3; i++)
        {
            //Vector3 spawnPosition = new Vector3(i * 2, 0, 0); // Tạo vị trí xuất hiện cho kẻ địch
            enemyFactory.CreateEnemy("chasingEnemy", GetRandomSpawnPosition().position);
            enemies.Add(enemyFactory.CreateEnemy("chasingEnemy", GetRandomSpawnPosition().position).GetComponent<EnemyBase>());
            yield return new WaitForSeconds(timeBetweenSpawn);
        }

        // Tạo 2 kẻ địch loại Range
        for (int i = 0; i < 2; i++)
        {
            //Vector3 spawnPosition = new Vector3(i * 2 + 5, 0, 0); // Vị trí khác cho kẻ địch, có thể nâng cấp lên, tạo một list chứa các vị trí khác nhau rồi random để tạo vị trí xuất hiện ngẫu nhiên
            enemyFactory.CreateEnemy("rangeEnemy", GetRandomSpawnPosition().position);
            enemies.Add(enemyFactory.CreateEnemy("rangeEnemy", GetRandomSpawnPosition().position).GetComponent<EnemyBase>()); // get component EnemyBase
            yield return new WaitForSeconds(timeBetweenSpawn);
        }
    }
}
