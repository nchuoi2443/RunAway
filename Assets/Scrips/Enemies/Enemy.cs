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

    private KnockBack knockBack;
    private Vector2 randomDirection;

    protected bool IsBocked = false;
    public KnockBack KnockBack { get { return knockBack; } set { knockBack = value; } }

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
        knockBack = GetComponent<KnockBack>();
    }

    public virtual void FixedUpdate()
    {
        
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
            /*LevelManager.manager.GameOver();
            //Destroy(collision.gameObject);
            Target = null;*/
        }
        else if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            if (TypeOfEnemy == "chasingEnemy") LevelManager.manager.updateScore(1);
            else if (TypeOfEnemy == "rangeEnemy") LevelManager.manager.updateScore(3);
            /*Destroy(collision.gameObject);
            Destroy(gameObject);*/
        }
        else if (collision.gameObject.CompareTag("Environment"))
        {
            Vector2 collisionNormal = collision.contacts[0].normal;  // Lấy vector pháp tuyến của vật va chạm
            SetRandomDirection(collisionNormal);
            IsBocked = true;
            StartCoroutine(MoveToRandomPos());
        }
    }

    private void SetRandomDirection(Vector2 collisionNormal)
    {
        // Tạo hai vector vuông góc (perpendicular) với vector pháp tuyến của vật va chạm
        Vector2 leftDirection = new Vector2(-collisionNormal.y, collisionNormal.x);   // Sang trái
        Vector2 rightDirection = new Vector2(collisionNormal.y, -collisionNormal.x);  // Sang phải

        // Random chọn hướng trái hoặc phải
        float randomValue = UnityEngine.Random.Range(0f, 1f);  // Trả về giá trị giữa 0 và 1
        Vector2 randomDirection = (randomValue < 0.5f) ? leftDirection : rightDirection;

        // Đặt vận tốc cho enemy di chuyển theo hướng đã chọn
        rb.velocity = randomDirection * speed;
    }

    public IEnumerator MoveToRandomPos()
    {
        yield return new WaitForSeconds(1f);
        IsBocked = false;
        Debug.Log("Move to randonpos" + IsBocked); 
    }
}
