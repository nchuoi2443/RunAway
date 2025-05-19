using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectTile : MonoBehaviour
{
    private Vector2 moveDirection;
    private float speed;
    private float lifeTime;
    private float timer;
    Animator _animator;
    private bool isCollided = false;

    public void Init(Vector2 dir, float spd, float lifetime)
    {
        moveDirection = dir.normalized;
        speed = spd;
        lifeTime = lifetime;
        timer = 0f;
        _animator = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        speed = 0f;
    }

    void Update()
    {
        if (isCollided)
            return;
        transform.position += (Vector3)(moveDirection * speed * Time.deltaTime);

        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            isCollided = true;
            playerHealth.TakeDamage(1);
            _animator.SetTrigger("HitPlayer");
            StartCoroutine(HitPlayer());
        }
    }

    IEnumerator HitPlayer()
    {
        while (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < .75f){
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
