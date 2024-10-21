using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destrucable : MonoBehaviour
{
    [SerializeField] private GameObject destructVFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Damagable>())
        {
            Instantiate(destructVFX, transform.position, Quaternion.identity);  
            Destroy(gameObject);
        }
    }
}
