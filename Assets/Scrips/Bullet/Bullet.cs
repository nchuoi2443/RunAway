using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] private float speed = 10f;

    [Range(1, 10)]
    [SerializeField] private float lifeTime = 3f;
    //[SerializeField] private Transform target;
    private Rigidbody2D rb;
    private Vector2 direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Tự động hủy đạn sau khi hết thời gian tồn tại
        Destroy(gameObject, lifeTime);

        // Tìm vị trí ban đầu của Player tại thời điểm đạn sinh ra
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            moveToDirPos(player.transform);
        }
    }

    public virtual void moveToDirPos(Transform dirPos)
    {
        // Tính toán hướng từ viên đạn đến Player
        Vector2 targetPosition = dirPos.position;

        // Tính toán hướng (normalized) từ viên đạn tới Player
        direction = (targetPosition - (Vector2)transform.position).normalized;

        // Xoay viên đạn để nó hướng về phía Player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void FixedUpdate()
    {
        // Di chuyển viên đạn theo hướng đã tính
        rb.velocity = direction * speed;
    }
}
