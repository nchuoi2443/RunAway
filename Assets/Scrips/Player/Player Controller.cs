using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{    
    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private float DashCD = 1f;
    public static PlayerController Instance { get; private set; }

    private PlayerControls playerControls;
    private Vector2 movement;


    private Rigidbody2D rb;

    private Vector2 mousePos;
    private Animator animator;
    private SpriteRenderer mySpriteRenderer;
    private float startMoveSpeed;
    private bool facingLeft;
    private bool isDashing = false;
 
    public bool FacingLeft { get { return facingLeft; } }
    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        startMoveSpeed = movementSpeed;
    }

    private void Start()
    {
        playerControls.Combat.Dash.performed += _ => Dash();
    }

    //dash and cooldown of dash
    private void Dash()
    {
        if (!isDashing)
        {
            isDashing = true;
            movementSpeed *= dashSpeed;
            trailRenderer.emitting = true;
            StartCoroutine(DashRoutine());
        }
    }

    private IEnumerator DashRoutine()
    {
        float DashTime = 0.2f;
        yield return new WaitForSeconds(DashTime);
        movementSpeed = startMoveSpeed;
        trailRenderer.emitting = false;
        yield return new WaitForSeconds(DashCD);
        isDashing = false;
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();
        }
        playerControls.Enable();
    }

    private void Update()
    {
        PlayerInput();
        playerFacingMouse();
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.move.ReadValue<Vector2>();

        animator.SetFloat("moveX", movement.x);
        animator.SetFloat("moveY", movement.y);
    }

    private void Move()
    {
        rb.MovePosition(rb.position + movement * (movementSpeed * Time.deltaTime));
    }

    private void playerFacingMouse()
    {
        //get mouse position on the screen
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //flip player to the part have mouse
        if (mousePos.x < transform.position.x)
        {
            mySpriteRenderer.flipX = true;
            facingLeft = true;
        }
        else
        {
            mySpriteRenderer.flipX = false;
            facingLeft = false;
        }

    }
}
