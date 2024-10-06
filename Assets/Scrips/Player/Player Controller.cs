using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{    
    [SerializeField] private float movementSpeed = 2f;  
    
    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;

    private Vector2 mousePos;
    private Animator animator;
    private SpriteRenderer mySpriteRenderer;

    private bool facingLeft;

    public bool FacingLeft { get { return facingLeft; } set { facingLeft = value; } }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
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
            FacingLeft = true;
        }
        else
        {
            mySpriteRenderer.flipX = false;
            FacingLeft = false;
        }

    }
}
