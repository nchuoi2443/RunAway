using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float rotateSpeed = 0.25f;

    private string typeOfEnemy;

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

    // Getter và Setter cho rotateSpeed
    public float RotateSpeed
    {
        get { return rotateSpeed; }
        set { rotateSpeed = value; }
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
    }


    public void getTarget()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            Target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    public void rotateTowardTarget()
    {
        Vector2 targetDirection = Target.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rotateSpeed);
        
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
