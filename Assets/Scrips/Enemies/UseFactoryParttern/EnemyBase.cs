using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public enum typeOfEnemy
{
    ChasingEnemy,
    FollowEnemy,
    ShootEnemy
}
public abstract class EnemyBase : MonoBehaviour, IEnemy
{
    public typeOfEnemy typeEnemy;
    [SerializeField] private Transform target;
    public Transform Target
    {
        get { return target; }
        set { target = value; }
    }
    [SerializeField] private float speed = 3f;

    private string typeOfEnemy;

    private bool stopMovingWhileAttacking;

    private KnockBack knockBack;
    private Vector2 randomDirection;

    protected bool IsBocked = false;
    public KnockBack KnockBack { get { return knockBack; } set { knockBack = value; } }
    private Rigidbody2D rb;
    public Rigidbody2D Rb
    {
        get { return rb; }
        set { rb = value; }
    }
    private Animator enemyAnimator;
    public Animator EnemyAnimator { get { return enemyAnimator; } set { enemyAnimator = value; } }

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        knockBack = GetComponent<KnockBack>();
    }

    public virtual void GetTarget()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    public void FacingPlayer()
    {
        //flip enemy to face player
        if (target.position.x < transform.position.x)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void MoveTowardPlayer()
    {
        float vectorX = transform.position.x - target.position.x;
        float vectorY = transform.position.y - target.position.y;

        rb.velocity = new Vector2(vectorX, vectorY).normalized * -speed;
    }

    public void SetRandomDirection(Vector2 collisionNormal)
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


    //use to change enemy direction if enemy hit object, after that, change direction to player
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Environment"))
        {
            Vector2 collisionNormal = collision.contacts[0].normal;  // Lấy vector pháp tuyến của vật va chạm
            SetRandomDirection(collisionNormal);
            IsBocked = true;
            StartCoroutine(MoveToRandomPos());
        }
    }

    public abstract void Shoot();

    public abstract void EnemyAttacking();
}
