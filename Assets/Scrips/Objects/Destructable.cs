using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destrucable : MonoBehaviour
{
    [SerializeField] private GameObject destructVFX;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Damagable>() || collision.gameObject.GetComponent<Bullet>())
        {
            Instantiate(destructVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
            rateGenerateCoin();
        }
    }

    public void rateGenerateCoin()
    {
        int rate = Random.Range(0, 100);
        if (rate < 70)
        {
            GetComponent<PickUpSpawner>().SpawnPickUp(2);
        }
    }
}
