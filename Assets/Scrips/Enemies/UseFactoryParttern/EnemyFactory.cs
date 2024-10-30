using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public GameObject chasingEnemyPrefab;
    public GameObject rangeEnemyPrefab;

    // Hàm để tạo ra đối tượng GameObject kẻ địch từ Prefab
    public GameObject CreateEnemy(string type, Vector3 spawnPosition)
    {
        GameObject enemyInstance = null;

        switch (type)
        {
            case "chasingEnemy":
                enemyInstance = Instantiate(chasingEnemyPrefab, spawnPosition, Quaternion.identity);
                break;

            case "rangeEnemy":
                enemyInstance = Instantiate(rangeEnemyPrefab, spawnPosition, Quaternion.identity);
                break;

            default:
                Debug.LogError("Unknown enemy type!");
                break;
        }

        return enemyInstance;
    }
}
