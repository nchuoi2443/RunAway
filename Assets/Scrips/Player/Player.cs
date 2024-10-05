using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPos;
    [Range(0.1f, 3f)]
    [SerializeField] private float fireRate = 1f;
    private float fireTimer;

    private Rigidbody2D rb;
    private float mx;
    private float my;

    private Vector2 mousePos;

    private Animator animator;
    private SpriteRenderer mySpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fireTimer = fireRate;
        animator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInput();
        
        playerFacingMouse(); 
        /*
        //get mouse position on the screen
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //calculate the angle btw gun of player and mouse pos
        float angle = Mathf.Atan2(mousePos.y - rb.position.y, mousePos.x - rb.position.x) * Mathf.Rad2Deg - 90f ;
        //rotate 
        transform.localRotation = Quaternion.Euler(0,0,angle);
        */
        if (Input.GetMouseButton(0) && fireTimer <= 0f)
        {
            Shoot();
            fireTimer = fireRate;
        } else
        {
            fireTimer -= Time.deltaTime;
        }
        //
    }

    private void playerInput()
    {
        //get value of the input 
        mx = Input.GetAxisRaw("Horizontal");
        my = Input.GetAxisRaw("Vertical");
        //set value for animator
        animator.SetFloat("moveX", mx);
        animator.SetFloat("moveY", my);
    }

    private void playerFacingMouse()
    {
        //get mouse position on the screen
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //flip player to the part have mouse
        if (mousePos.x < transform.position.x)
        {
            mySpriteRenderer.flipX = true;
        }
        else
        {
            mySpriteRenderer.flipX = false;
        }

    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, firingPos.position, firingPos.rotation);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(mx,my).normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            LevelManager.manager.GameOver();
            Destroy(collision.gameObject);
            //Destroy(gameObject);
        }
    }
}
