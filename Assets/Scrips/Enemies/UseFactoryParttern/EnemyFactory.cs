using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public GameObject chasingEnemyPrefab;
    public GameObject rangeEnemyPrefab;
    [SerializeField] private GameObject _bossPrefab;

    // Hàm để tạo ra đối tượng GameObject kẻ địch từ Prefab
    public GameObject CreateEnemy(EnemyType enemyType, Vector3 spawnPosition)
    {
        GameObject enemyInstance = null;

        switch (enemyType)
        {
            case EnemyType.ChasingEnemy:
                enemyInstance = Instantiate(chasingEnemyPrefab, spawnPosition, Quaternion.identity);
                break;

            case EnemyType.RangeEnemy:
                enemyInstance = Instantiate(rangeEnemyPrefab, spawnPosition, Quaternion.identity);
                break;
            case EnemyType.Boss:
                enemyInstance = Instantiate(_bossPrefab, spawnPosition, Quaternion.identity);
                break;
            default:
                Debug.LogError("Unknown enemy type!");
                break;
        }

        return enemyInstance;
    }
}

public enum EnemyType
{
    ChasingEnemy,
    RangeEnemy,
    Boss,
}