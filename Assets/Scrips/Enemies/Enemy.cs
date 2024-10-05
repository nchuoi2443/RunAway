using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 3f;

    private string typeOfEnemy;

    private Animator enemyAnimator;
    private bool stopMovingWhileAttacking;
    public Animator EnemyAnimator { get { return enemyAnimator; } set { enemyAnimator = value; } }
    public string TypeOfEnemy
    {
        get { return typeOfEnemy; }
        set { typeOfEnemy = value; }

    }
    // Getter và Setter cho target
    public Transform Target
    {
        get { return target; }
        set { target = value; }
    }

    // Getter và Setter cho speed
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }


    private Rigidbody2D rb;

    // Getter và Setter cho rb (Rigidbody2D)
    public Rigidbody2D Rb
    {
        get { return rb; }
        set { rb = value; }
    }
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
    }


    public void getTarget()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            Target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    public void facingPlayer()
    {
        //flip enemy to face player
        if (Target.position.x < transform.position.x)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }

    }

    public void moveTorwardPlayer()
    {
        float vectorX = transform.position.x - Target.position.x;
        float vectorY = transform.position.y - Target.position.y;

        rb.velocity = new Vector2(vectorX, vectorY).normalized * -speed;
    }

 
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            LevelManager.manager.GameOver();
            //Destroy(collision.gameObject);
            Target = null;
        } else if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            if (TypeOfEnemy == "chasingEnemy") LevelManager.manager.updateScore(1);
            else if (TypeOfEnemy == "rangeEnemy") LevelManager.manager.updateScore(3);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
