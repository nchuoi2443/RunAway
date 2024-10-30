using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private float pickUpDistance = 5f;
    [SerializeField] private float accelartionRate = 1f;
    [SerializeField] private float moveSpeed = 7f;

    private Vector3 moveDir;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.zero;

    }

    private void Update()
    {
        Vector3 playerPos = PlayerController.Instance.transform.position;
        Vector3 dirToPlayer = playerPos - transform.position;
        if (dirToPlayer.magnitude <= pickUpDistance)
        {
            moveDir = dirToPlayer.normalized;
            moveSpeed += accelartionRate * Time.deltaTime;
        }
        else
        {
            moveDir = Vector3.zero;
            moveSpeed = 0;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDir * moveSpeed;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            DetectPickUpType();
            Destroy(gameObject);
        }

    }

    private void DetectPickUpType()
    {
        EconomyManager.Instance.UpdateCurrentCoin();
    }
}
