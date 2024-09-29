using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnerTime = 5f;

    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private bool canSpawn = true;
    private void Start()
    {
        StartCoroutine(Spawner());
    }

 

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnerTime);

        while (canSpawn)
        {
            yield return wait;

            //spawn enemy
            int rand = Random.Range(0, enemyPrefabs.Length);
            GameObject enemyToSpawn = enemyPrefabs[rand];
            Instantiate(enemyToSpawn, transform.position, Quaternion.identity);

        }
    }
}
