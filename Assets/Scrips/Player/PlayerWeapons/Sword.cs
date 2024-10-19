using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject slashPrefab;
    [SerializeField] private Transform slashPosSpawn;
    [SerializeField] private Transform weaponCollider;
    [SerializeField] private float swordAttackCD = 0.5f;

    private Animator animator;
    private PlayerControls playerControls;
    private PlayerController playerController;
    private ActiveWeapon activeWeapon;
    private bool attackButtonPressed, isAttacking = false;
    //private Collider2D swordCollider;


    private GameObject slashAnim;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponentInParent<PlayerController>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
        //swordCollider = GetComponentInParent<Collider2D>();
    }
    private void Start()
    {
        playerControls.Combat.Attack.started += _ => StartAttacking();
        playerControls.Combat.Attack.canceled += _ => StopAttacking();
    }

    private void Update()
    {
        followingOffSet();
        Attack();
    }

    private void StartAttacking()
    {
        attackButtonPressed = true;
    }

    private void StopAttacking()
    {
        attackButtonPressed = false;
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();
        }
        playerControls.Enable();
    }

   
    private void Attack()
    {
        if (attackButtonPressed && !isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("Slash");
            weaponCollider.gameObject.SetActive(true);

            slashAnim = Instantiate(slashPrefab, slashPosSpawn.position, Quaternion.identity);
            slashAnim.transform.parent = this.transform.parent;
            StartCoroutine(AttackCDCoroutine());
        }
    }

    private IEnumerator AttackCDCoroutine()
    {
        yield return new WaitForSeconds(swordAttackCD);
        isAttacking = false;
    }

    public void doneAttackAnim()
    {
        weaponCollider.gameObject.SetActive(false);
    }

    //flip anim swing up and swing down
    public void SwingUpFlipAnim()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (playerController.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnim()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (playerController.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void followingOffSet()
    {
        Vector3 mousePos = Input.mousePosition;
        //bug không lấy được instance của player controller, do scrip playerController nằm ở đối tượng parents 
        //nên getcombonent không khởi tạo được instance của playerController
        Vector3 playerScreenpoint = Camera.main.WorldToScreenPoint(playerController.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenpoint.x)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Killed enemy");
        }
    }
}
