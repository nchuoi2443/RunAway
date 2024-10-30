using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject pickUpPrefab;


    public void SpawnPickUp(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(pickUpPrefab, transform.position, Quaternion.identity);
        }
    }
}
